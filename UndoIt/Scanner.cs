// Copyright (C) 2013  Joey Scarr, Lukas Korsika
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using RecoveryThisFile;
using KFS.FileSystems;
using KFS.FileSystems.NTFS;
using MB.Algodat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace KickassUndelete {
	/// <summary>
	/// Encapsulates the state of a scan for deleted files.
	/// </summary>
	public class Scanner {

        private List<INodeMetadata> _deletedFiles;// = new List<INodeMetadata>();
		private double _progress;
        private double _total;
        private DateTime _startTime;
		private Thread _thread;
		public bool _scanCancelled;
		private IFileSystem _fileSystem;
		private string _diskName;

        private static string _fileType;
        public static string FileType
        {
            set { _fileType = value; }
        }

        private static string _fileName;
        public static string FileName
        {
            set { _fileName = value; }
        }

        private static string _year;
        public static string year
        {
            set { _year = value; }
        }

        private static string _nodepath;
        public static string NodePath
        {
            set { _nodepath = value; }
        }

       
        /// <summary>
        /// Constructs a Scanner on the specified filesystem.
        /// </summary>
        /// <param name="fileSystem">The filesystem to scan.</param>
        public Scanner(string diskName, object fileSystem) {
			_fileSystem = (IFileSystem) fileSystem;
			_diskName = diskName;
		}

		public IList<INodeMetadata> GetDeletedFiles() {
			lock (_deletedFiles) {
				return new List<INodeMetadata>(_deletedFiles);
			}
		}

		public string DiskName {
			get { return _diskName; }
		}

		/// <summary>
		/// The Device ID, e.g. "C:".
		/// </summary>
		public string DeviceID {
			get { return _fileSystem.Store.DeviceID; }
		}

		/// <summary>
		/// Lấy tiến độ hiện tại của quá trình quét
		/// </summary>
		public double Progress {
			get { return _progress; }
		}

        public double Total
        {
            get { return _total; }
        }
		/// <summary>
		/// Bắt đầu quét trên file hệ thống
		/// </summary>
		public void StartScan() {
			_scanCancelled = false;
            _deletedFiles = new List<INodeMetadata>();
            _thread = new Thread(Run);
            _thread.Start();
            _thread.IsBackground = false;
        }

        public void StartScanType()
        {
            _scanCancelled = false;
            _deletedFiles = new List<INodeMetadata>();
            _thread = new Thread(RunType);
            _thread.Start();
            _thread.IsBackground = false;
        }

        public void StartScanName()
        {
            _scanCancelled = false;
            _deletedFiles = new List<INodeMetadata>();
            _thread = new Thread(RunName);
            _thread.Start();
            _thread.IsBackground = false;
        }

        /// <summary>
        /// Cancels the currently running scan.
        /// </summary>
        public void CancelScan() {
			_scanCancelled = true;
		}

        public void StopScan()
        {
            _scanCancelled = true;
            _thread.Abort();
        }
        #region Run a Scan: All, Name , Type

        private HashSet<string> _systemFIle = new HashSet<string>() { "DOC","DOTX","DOTM","DOCB","DOCX","XLS","XLSX","TXT","PPT","PPTX", "POT", "PPS", "PPTM",
                                                                      "POTX","PPAM","PPSX","PPSM","SLDX","SLDM","XLL","XLA","XLSB","XLSM","XLTX","XLTM"};
        private void Run() {
            GC.Collect();
			// Dictionary storing a tree that allows us to rebuild deleted file paths.
			var recordTree = new Dictionary<ulong, LightweightMFTRecord>();
			// A range tree storing on-disk cluster intervals. Allows us to tell whether files are overwritten.
			var runIndex = new RangeTree<ulong, RangeItem>(new RangeItemComparer());

			ulong numFiles;

			OnScanStarted();
			_progress = 0;
			OnProgressUpdated();

			// TODO: Replace me with a search strategy selected from a text box!
			ISearchStrategy strat = _fileSystem.GetDefaultSearchStrategy();

			if (_fileSystem is FileSystemNTFS) {
				var ntfsFS = _fileSystem as FileSystemNTFS;          
				numFiles = ntfsFS.MFT.StreamLength / (ulong)(ntfsFS.SectorsPerMFTRecord * ntfsFS.BytesPerSector);
			}

			Console.WriteLine("Beginning scan...");
			_startTime = DateTime.Now;

            try
            {
                strat.Search(new FileSystem.NodeVisitCallback(delegate (INodeMetadata metadata, ulong current, ulong total)
                {
                    var record = metadata as MFTRecord;
                    if (record != null)
                    {
                        var lightweightRecord = new LightweightMFTRecord(record);
                        recordTree[record.RecordNum] = lightweightRecord;

                        foreach (IRun run in record.Runs)
                        {
                            runIndex.Add(new RangeItem(run, lightweightRecord));
                        }
                    }

                    // điều kiện để thêm file vào danh sách tìm được
                    if (metadata != null && metadata.Deleted && metadata.Name != null
                            && !metadata.Name.EndsWith(".manifest", StringComparison.OrdinalIgnoreCase)
                            && !metadata.Name.EndsWith(".cat", StringComparison.OrdinalIgnoreCase)
                            && !metadata.Name.EndsWith(".mum", StringComparison.OrdinalIgnoreCase))
                    {
                        IFileSystemNode node = metadata.GetFileSystemNode();
                       
                        // lấy đường dẫn tạp tin của file
                        node.Path = PathUtils.Combine(GetPathForRecord(recordTree, record.ParentDirectory), node.Name);
                        
                        if (node.Size > 0 && node.Path.Contains(_nodepath))
                        {
                            if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "week")
                            {
                                if ((node.LastModified >= DateTime.Now.AddDays(-7) && node.LastModified <= DateTime.Now))
                                {
                                    lock (_deletedFiles)
                                    {
                                        _deletedFiles.Add(metadata);
                                    }

                                    FrmShowAllcs.Instance.FileScanned = node.Path;
                                }
                            }
                            else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "month")
                            {
                                if ((node.LastModified >= DateTime.Now.AddMonths(-3) && node.LastModified <= DateTime.Now))
                                {
                                    lock (_deletedFiles)
                                    {
                                        _deletedFiles.Add(metadata);
                                    }
                                    FrmShowAllcs.Instance.FileScanned = node.Path;
                                }
                            }
                            else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "alltime")
                            {
                                lock (_deletedFiles)
                                {
                                    _deletedFiles.Add(metadata);
                                }
                                FrmShowAllcs.Instance.FileScanned = node.Path;
                            }
                            else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "year")
                            {
                                string _nam = RecoveryThisFile.Properties.Settings.Default.lastYear;
                                if ((node.LastModified >= DateTime.Now.AddYears(-(int.Parse(_nam))) && node.LastModified <= DateTime.Now))
                                {
                                    lock (_deletedFiles)
                                    {
                                        _deletedFiles.Add(metadata);
                                    }
                                    FrmShowAllcs.Instance.FileScanned = node.Path;
                                }
                            }
                        }
                    }

                    _total = total;
                    if (current % 100 == 0)
                    {
                        _progress = (double)current / (double)total;
                        OnProgressUpdated();
                    }
                    return !_scanCancelled;
                }));
            }
            catch { }

			if (_fileSystem is FileSystemNTFS) {
				List<INodeMetadata> fileList;
				lock (_deletedFiles) {
					fileList = _deletedFiles;
				}
				//foreach (var file in fileList)
                for(int i =0; i< fileList.Count;i++){
					var record = fileList[i] as MFTRecord;
					var node = fileList[i].GetFileSystemNode();
                   
                   
					//node.Path = PathUtils.Combine(GetPathForRecord(recordTree, record.ParentDirectory), node.Name);

                    // nếu file có thể gi đè được thì chuyển sang trạng thái Recoverable.
                    if (record.ChanceOfRecovery == FileRecoveryStatus.MaybeOverwritten)
                    {
                        record.ChanceOfRecovery = FileRecoveryStatus.Recoverable;
                        // Query all the runs for this node.
                        foreach (IRun run in record.Runs) {
							List<RangeItem> overlapping = runIndex.Query(new Range<ulong>(run.LCN, run.LCN + run.LengthInClusters - 1));

							if (overlapping.Count(x => x.Record.RecordNumber != record.RecordNum) > 0) {
								record.ChanceOfRecovery = FileRecoveryStatus.PartiallyOverwritten;
								break;
							}
						}
					}
				}
			}

			runIndex.Clear();
			recordTree.Clear();
            
			GC.Collect();

			TimeSpan timeTaken = DateTime.Now - _startTime;
			if (!_scanCancelled) {
				Console.WriteLine("Scan complete! Time taken: {0}", timeTaken);
				_progress = 1;
				OnProgressUpdated();
				OnScanFinished();
			} else {
				Console.WriteLine("Scan cancelled! Time taken: {0}", timeTaken);
			}
		}

        #region Run
        private void RunType()
        {
            GC.Collect();
            // Dictionary storing a tree that allows us to rebuild deleted file paths.
            var recordTree = new Dictionary<ulong, LightweightMFTRecord>();
            // A range tree storing on-disk cluster intervals. Allows us to tell whether files are overwritten.
            var runIndex = new RangeTree<ulong, RangeItem>(new RangeItemComparer());

            ulong numFiles;

            OnScanStarted();
            _progress = 0;
            OnProgressUpdated();

            // TODO: Replace me with a search strategy selected from a text box!
            ISearchStrategy strat = _fileSystem.GetDefaultSearchStrategy();

            if (_fileSystem is FileSystemNTFS)
            {
                var ntfsFS = _fileSystem as FileSystemNTFS;
                numFiles = ntfsFS.MFT.StreamLength / (ulong)(ntfsFS.SectorsPerMFTRecord * ntfsFS.BytesPerSector);
            }

            Console.WriteLine("Beginning scan...");
            _startTime = DateTime.Now;

            try
            {
                strat.Search(new FileSystem.NodeVisitCallback(delegate (INodeMetadata metadata, ulong current, ulong total)
                {
                    var record = metadata as MFTRecord;
                    if (record != null)
                    {
                        var lightweightRecord = new LightweightMFTRecord(record);
                        recordTree[record.RecordNum] = lightweightRecord;

                        foreach (IRun run in record.Runs)
                        {
                            runIndex.Add(new RangeItem(run, lightweightRecord));
                        }
                    }

                    // điều kiện để thêm file vào danh sách tìm được
                    if (metadata != null && metadata.Deleted && metadata.Name != null
                            && !metadata.Name.EndsWith(".manifest", StringComparison.OrdinalIgnoreCase)
                            && !metadata.Name.EndsWith(".cat", StringComparison.OrdinalIgnoreCase)
                            && !metadata.Name.EndsWith(".mum", StringComparison.OrdinalIgnoreCase))
                    {
                        IFileSystemNode node = metadata.GetFileSystemNode();
                        int index = node.Name.LastIndexOf(".");
                        string nodeType = node.Name.Substring(index + 1);
                        string _nam = RecoveryThisFile.Properties.Settings.Default.lastYear;

                        // lấy đường dẫn tạp tin của file
                        node.Path = PathUtils.Combine(GetPathForRecord(recordTree, record.ParentDirectory), node.Name);

                        if (node.Size > 0 && node.Path.Contains(_nodepath))
                        {
                            //--- none
                            if (RecoveryThisFile.Properties.Settings.Default.radio == "none")
                            {
                                if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "week")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddDays(-7) && node.LastModified <= DateTime.Now) && (nodeType.Contains("doc") || nodeType.Contains("DOC")))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "month")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddMonths(-3) && node.LastModified <= DateTime.Now) && (nodeType.Contains(_fileType) || nodeType.Contains(_fileType.ToUpper())))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "alltime")
                                {
                                    if (nodeType.Contains(_fileType) || nodeType.Contains(_fileType.ToUpper()))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "year")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddYears(-(int.Parse(_nam))) && node.LastModified <= DateTime.Now)
                                                       && (nodeType.Contains(_fileType) || nodeType.Contains(_fileType.ToUpper())))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                            }

                            //--- document
                            if (RecoveryThisFile.Properties.Settings.Default.radio == "document")
                            {
                                if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "week")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddDays(-7) && node.LastModified <= DateTime.Now) && (_systemFIle.Contains(nodeType) || _systemFIle.Contains(nodeType.ToUpper())))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "month")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddMonths(-3) && node.LastModified <= DateTime.Now) && (_systemFIle.Contains(nodeType) || _systemFIle.Contains(nodeType.ToUpper())))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "alltime")
                                {
                                    if (_systemFIle.Contains(nodeType) || _systemFIle.Contains(nodeType.ToUpper()))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "year")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddYears(-(int.Parse(_nam))) && node.LastModified <= DateTime.Now)
                                                       && (_systemFIle.Contains(nodeType) || _systemFIle.Contains(nodeType.ToUpper())))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                            }

                            //--- find all video file
                            if (RecoveryThisFile.Properties.Settings.Default.radio == "video")
                            {
                                if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "week")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddDays(-7) && node.LastModified <= DateTime.Now) && (nodeType.Contains("mp4") || nodeType.Contains("MP4")
                                                                                                                                || nodeType.Contains("avi") || nodeType.Contains("AVI")
                                                                                                                                || nodeType.Contains("flv") || nodeType.Contains("FLV")
                                                                                                                                || nodeType.Contains("wmv") || nodeType.Contains("WMV")
                                                                                                                                || nodeType.Contains("asf") || nodeType.Contains("ASF")
                                                                                                                                || nodeType.Contains("mpeg") || nodeType.Contains("MPEG")))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "month")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddMonths(-3) && node.LastModified <= DateTime.Now) && (nodeType.Contains("mp4") || nodeType.Contains("MP4")
                                                                                                                                   || nodeType.Contains("avi") || nodeType.Contains("AVI")
                                                                                                                                   || nodeType.Contains("flv") || nodeType.Contains("FLV")
                                                                                                                                   || nodeType.Contains("wmv") || nodeType.Contains("WMV")
                                                                                                                                   || nodeType.Contains("asf") || nodeType.Contains("ASF")
                                                                                                                                   || nodeType.Contains("mpeg") || nodeType.Contains("MPEG")))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "alltime")
                                {
                                    if (nodeType.Contains("mp4") || nodeType.Contains("MP4")
                                      || nodeType.Contains("avi") || nodeType.Contains("AVI")
                                      || nodeType.Contains("flv") || nodeType.Contains("FLV")
                                      || nodeType.Contains("wmv") || nodeType.Contains("WMV")
                                      || nodeType.Contains("asf") || nodeType.Contains("ASF")
                                      || nodeType.Contains("mpeg") || nodeType.Contains("MPEG"))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "year")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddYears(-(int.Parse(_nam))) && node.LastModified <= DateTime.Now)
                                                       && (nodeType.Contains("MP4") || nodeType.Contains("MP4")
                                                         || nodeType.Contains("avi") || nodeType.Contains("AVI")
                                                         || nodeType.Contains("flv") || nodeType.Contains("FLV")
                                                         || nodeType.Contains("wmv") || nodeType.Contains("WMV")
                                                         || nodeType.Contains("asf") || nodeType.Contains("ASF")
                                                         || nodeType.Contains("mpeg") || nodeType.Contains("MPEG")))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                            }

                            // find all photo file
                            if (RecoveryThisFile.Properties.Settings.Default.radio == "photo")
                            {
                                if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "week")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddDays(-7) && node.LastModified <= DateTime.Now) && (nodeType.Contains("jpg") || nodeType.Contains("JPG")
                                                                                                                                || nodeType.Contains("psd") || nodeType.Contains("PSD")
                                                                                                                                || nodeType.Contains("png") || nodeType.Contains("PNG")
                                                                                                                                || nodeType.Contains("tiff") || nodeType.Contains("TIFF")
                                                                                                                                || nodeType.Contains("gif") || nodeType.Contains("GIF")
                                                                                                                                || nodeType.Contains("bmp") || nodeType.Contains("BMP")))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "month")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddMonths(-3) && node.LastModified <= DateTime.Now) && (nodeType.Contains("jpg") || nodeType.Contains("JPG")
                                                                                                                     || nodeType.Contains("psd") || nodeType.Contains("PSD")
                                                                                                                                || nodeType.Contains("png") || nodeType.Contains("PNG")
                                                                                                                                || nodeType.Contains("tiff") || nodeType.Contains("TIFF")
                                                                                                                                || nodeType.Contains("gif") || nodeType.Contains("GIF")
                                                                                                                                || nodeType.Contains("bmp") || nodeType.Contains("BMP")))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "alltime")
                                {
                                    if (nodeType.Contains("jpg") || nodeType.Contains("JPG")
                                        || nodeType.Contains("psd") || nodeType.Contains("PSD")
                                        || nodeType.Contains("png") || nodeType.Contains("PNG")
                                        || nodeType.Contains("tiff") || nodeType.Contains("TIFF")
                                        || nodeType.Contains("gif") || nodeType.Contains("GIF")
                                        || nodeType.Contains("bmp") || nodeType.Contains("BMP"))
                                   {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                                else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "year")
                                {
                                    if ((node.LastModified >= DateTime.Now.AddYears(-(int.Parse(_nam))) && node.LastModified <= DateTime.Now)
                                                       && (nodeType.Contains("jpg") || nodeType.Contains("JPG")
                                                        || nodeType.Contains("psd") || nodeType.Contains("PSD")
                                                        || nodeType.Contains("png") || nodeType.Contains("PNG")
                                                        || nodeType.Contains("tiff") || nodeType.Contains("TIFF")
                                                        || nodeType.Contains("gif") || nodeType.Contains("GIF")
                                                        || nodeType.Contains("bmp") || nodeType.Contains("BMP")))
                                    {
                                        lock (_deletedFiles)
                                        {
                                            _deletedFiles.Add(metadata);
                                        }
                                        FrmFileType.Instance.FileTypeScanned = node.Path;
                                    }
                                }
                            }

                        }
                    }
                    if (current % 100 == 0)
                    {
                        _total = total;
                        _progress = (double)current / (double)total;
                        OnProgressUpdated();
                    }
                    return !_scanCancelled;
                }));
            }
            catch { }

            if (_fileSystem is FileSystemNTFS)
            {
                List<INodeMetadata> fileList;
                lock (_deletedFiles)
                {
                    fileList = _deletedFiles;
                }
                //foreach (var file in fileList)
                for (int i = 0; i < fileList.Count; i++)
                {
                    var record = fileList[i] as MFTRecord;
                    var node = fileList[i].GetFileSystemNode();

                  //  node.Path = PathUtils.Combine(GetPathForRecord(recordTree, record.ParentDirectory), node.Name);

                    // nếu file có thể gi đè được thì chuyển sang trạng thái Recoverable.
                    if (record.ChanceOfRecovery == FileRecoveryStatus.MaybeOverwritten)
                    {
                        record.ChanceOfRecovery = FileRecoveryStatus.Recoverable;
                        // Query all the runs for this node.
                        foreach (IRun run in record.Runs)
                        {
                            List<RangeItem> overlapping = runIndex.Query(new Range<ulong>(run.LCN, run.LCN + run.LengthInClusters - 1));

                            if (overlapping.Count(x => x.Record.RecordNumber != record.RecordNum) > 0)
                            {
                                record.ChanceOfRecovery = FileRecoveryStatus.PartiallyOverwritten;
                                break;
                            }
                        }
                    }
                }
            }

            runIndex.Clear();
            recordTree.Clear();
            GC.Collect();

            TimeSpan timeTaken = DateTime.Now - _startTime;
            if (!_scanCancelled)
            {
                Console.WriteLine("Scan complete! Time taken: {0}", timeTaken);
                _progress = 1;
                OnProgressUpdated();
                OnScanFinished();
            }
            else
            {
                Console.WriteLine("Scan cancelled! Time taken: {0}", timeTaken);
            }
        }

        private void RunName()
        {
            GC.Collect();
            // Dictionary storing a tree that allows us to rebuild deleted file paths.
            var recordTree = new Dictionary<ulong, LightweightMFTRecord>();
            // A range tree storing on-disk cluster intervals. Allows us to tell whether files are overwritten.
            var runIndex = new RangeTree<ulong, RangeItem>(new RangeItemComparer());

            ulong numFiles;

            OnScanStarted();
            _progress = 0;
            OnProgressUpdated();

            // TODO: Replace me with a search strategy selected from a text box!
            ISearchStrategy strat = _fileSystem.GetDefaultSearchStrategy();

            if (_fileSystem is FileSystemNTFS)
            {
                var ntfsFS = _fileSystem as FileSystemNTFS;
                numFiles = ntfsFS.MFT.StreamLength / (ulong)(ntfsFS.SectorsPerMFTRecord * ntfsFS.BytesPerSector);
            }

            Console.WriteLine("Beginning scan...");
            _startTime = DateTime.Now;

            try
            {
                strat.Search(new FileSystem.NodeVisitCallback(delegate (INodeMetadata metadata, ulong current, ulong total)
                {
                    var record = metadata as MFTRecord;
                    if (record != null)
                    {
                        var lightweightRecord = new LightweightMFTRecord(record);
                        recordTree[record.RecordNum] = lightweightRecord;

                        foreach (IRun run in record.Runs)
                        {
                            runIndex.Add(new RangeItem(run, lightweightRecord));
                        }
                    }

                    // điều kiện để thêm file vào danh sách tìm được
                    if (metadata != null && metadata.Deleted && metadata.Name != null
                            && !metadata.Name.EndsWith(".manifest", StringComparison.OrdinalIgnoreCase)
                            && !metadata.Name.EndsWith(".cat", StringComparison.OrdinalIgnoreCase)
                            && !metadata.Name.EndsWith(".mum", StringComparison.OrdinalIgnoreCase))
                    {
                        IFileSystemNode node = metadata.GetFileSystemNode();
                        string nodeName = node.Name;
                        string _nam = RecoveryThisFile.Properties.Settings.Default.lastYear;
                      
                        // lấy đường dẫn tạp tin của file
                        node.Path = PathUtils.Combine(GetPathForRecord(recordTree, record.ParentDirectory), node.Name);

                        if (node.Size > 0 && node.Path.Contains(_nodepath))
                        {
                            if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "week")
                            {
                                if ((node.LastModified >= DateTime.Now.AddDays(-7) && node.LastModified <= DateTime.Now)
                                         && node.Name.ToLower().Contains(_fileName.ToLower()))
                                {
                                    lock (_deletedFiles)
                                    {
                                        _deletedFiles.Add(metadata);
                                    }
                                    FrmFileName.Instance.FileNameScanned = node.Path;
                                }
                            }
                            else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "month")
                            {
                                if ((node.LastModified >= DateTime.Now.AddMonths(-3) && node.LastModified <= DateTime.Now)
                                         && node.Name.ToLower().Contains(_fileName.ToLower()))
                                {
                                    lock (_deletedFiles)
                                    {
                                        _deletedFiles.Add(metadata);
                                    }
                                    FrmFileName.Instance.FileNameScanned = node.Path;
                                }

                            }
                            else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "alltime")
                            {
                                if (node.Name.ToLower().Contains(_fileName.ToLower()))
                                {
                                    lock (_deletedFiles)
                                    {
                                        _deletedFiles.Add(metadata);
                                    }
                                    FrmFileName.Instance.FileNameScanned = node.Path;
                                }
                            }
                            else if (RecoveryThisFile.Properties.Settings.Default.TimeSetting == "year")
                            {
                                if ((node.LastModified >= DateTime.Now.AddYears(-(int.Parse(_nam))) && node.LastModified <= DateTime.Now)
                                                  && node.Name.ToLower().Contains(_fileName.ToLower()))
                                {
                                    lock (_deletedFiles)
                                    {
                                        _deletedFiles.Add(metadata);
                                    }
                                    FrmFileName.Instance.FileNameScanned = node.Path;
                                }
                            }
                        }
                    }

                    if (current % 100 == 0)
                    {
                        _total = total;
                        _progress = (double)current / (double)total;
                        OnProgressUpdated();
                    }
                    return !_scanCancelled;
                }));
            }
            catch { }

            if (_fileSystem is FileSystemNTFS)
            {
                List<INodeMetadata> fileList;
                lock (_deletedFiles)
                {
                    fileList = _deletedFiles;
                }
                //foreach (var file in fileList)
                for (int i = 0; i < fileList.Count; i++)
                {
                    var record = fileList[i] as MFTRecord;
                    var node = fileList[i].GetFileSystemNode();

                    // nếu file có thể gi đè được thì chuyển sang trạng thái Recoverable.
                    if (record.ChanceOfRecovery == FileRecoveryStatus.MaybeOverwritten)
                    {
                        record.ChanceOfRecovery = FileRecoveryStatus.Recoverable;
                        // Query all the runs for this node.
                        foreach (IRun run in record.Runs)
                        {
                            List<RangeItem> overlapping = runIndex.Query(new Range<ulong>(run.LCN, run.LCN + run.LengthInClusters - 1));

                            if (overlapping.Count(x => x.Record.RecordNumber != record.RecordNum) > 0)
                            {
                                record.ChanceOfRecovery = FileRecoveryStatus.PartiallyOverwritten;
                                break;
                            }
                        }
                    }
                }
            }

            runIndex.Clear();
            recordTree.Clear();
            GC.Collect();

            TimeSpan timeTaken = DateTime.Now - _startTime;
            if (!_scanCancelled)
            {
                Console.WriteLine("Scan complete! Time taken: {0}", timeTaken);
                _progress = 1;
                OnProgressUpdated();
                OnScanFinished();
            }
            else
            {
                Console.WriteLine("Scan cancelled! Time taken: {0}", timeTaken);
            }
        }
        #endregion


        #endregion

        private string GetPathForRecord(Dictionary<ulong, LightweightMFTRecord> recordTree, ulong recordNum) {
			if (recordNum == 0 || !recordTree.ContainsKey(recordNum)
					|| recordTree[recordNum].ParentRecord == recordNum) {
				// This is the root record
				return DeviceID;
			} else {
				var record = recordTree[recordNum];
				if (record.Path == null) {
					if (!record.IsDirectory) {
						// This isn't a directory, so the path must have been broken.
						return PathUtils.Combine(DeviceID, "?");
					} else {
						record.Path = PathUtils.Combine(GetPathForRecord(recordTree, record.ParentRecord), record.FileName);
					}
				}
				return record.Path;
			}
		}

		/// <summary>
		/// This event fires repeatedly as the scan progresses.
		/// </summary>
		public event EventHandler ProgressUpdated;
		private void OnProgressUpdated() {
			if (ProgressUpdated != null) {
				ProgressUpdated(this, null);
			}
		}

		/// <summary>
		/// This event fires when the scan is started.
		/// </summary>
		public event EventHandler ScanStarted;
		private void OnScanStarted() {
			if (ScanStarted != null) {
				ScanStarted(this, null);
			}
		}

		/// <summary>
		/// This event fires when the scan finishes.
		/// </summary>
		public event EventHandler ScanFinished;
		private void OnScanFinished() {
			if (ScanFinished != null) {
				ScanFinished(this, null);
                _scanCancelled = true;
                _deletedFiles = null;
                _thread.Abort();
                GC.Collect();
            }
		}
    }
}
