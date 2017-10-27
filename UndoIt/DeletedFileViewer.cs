using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KFS.FileSystems;
using KickassUndelete;
using System.Collections;
using System.IO;
using System.Threading;
using KFS.Disks;
using GuiComponents;
using System.Reflection;
using System.Globalization;
using System.Resources;

namespace RecoveryThisFile
{

    public partial class DeletedAll_File : UserControl
    {
       
        public static string ID { set { _filePath = value; } }

        private static bool _isChecked;
        public static bool IsChecked { set { _isChecked = value; } }

        private static bool _scanAll;
        public static bool scanAll
        {
            set { _scanAll = value; }
        }

        public int value
        {
            get { return progressBar.Value; }
        }

        private static string chance;
        private static string chance1;
        private static string chance2;
        private static string chance3;
        private static string chance4;
        private static string chance5;

        public List<ListViewItem> listItem { get { return _files; } }

        delegate void ConvertDocumentDelegate(string fileName);

        private string EMPTY_FILTER_TEXT = "Enter filter text here...";

        private Dictionary<FileRecoveryStatus, string> _recoveryDescriptions;
           /* =
                new Dictionary<FileRecoveryStatus, string>() {
                {FileRecoveryStatus.Unknown,Properties.Resources.strChance4},
                {FileRecoveryStatus.Resident,Properties.Resources.strChance5},
                {FileRecoveryStatus.Recoverable,Properties.Resources.strChance},
                {FileRecoveryStatus.MaybeOverwritten,Properties.Resources.strChance1},
                {FileRecoveryStatus.PartiallyOverwritten,Properties.Resources.strChance2},
                {FileRecoveryStatus.Overwritten,Properties.Resources.strChance3}};*/

        private Dictionary<FileRecoveryStatus, Color> _recoveryColors =
                new Dictionary<FileRecoveryStatus, Color>() {
                {FileRecoveryStatus.Unknown,Color.FromArgb(255,255,255)}, // Orange
                {FileRecoveryStatus.Resident,Color.FromArgb(255,255,255)}, // Green
                {FileRecoveryStatus.Recoverable,Color.FromArgb(255,255,255)}, // Green
                {FileRecoveryStatus.MaybeOverwritten,Color.FromArgb(255,255,255)}, // Orange
                {FileRecoveryStatus.PartiallyOverwritten,Color.FromArgb(255,222,168)}, // Orange
                {FileRecoveryStatus.Overwritten,Color.FromArgb(242,21,21)}}; // Orange

        //public HashSet<string> _systemFIle = new HashSet<string>() { ".DOC", ".DOCX", ".XLS", ".XLSX", ".PDF", ".PNG", ".JPG", ".TXT", ".PPT", ".PPTX", ".MP3", ".MP4" };

        private HashSet<string> _systemFileExtensions =
            new HashSet<string>() { ".DLL", ".TMP", ".CAB", ".LNK", ".LOG", ".EXE", ".XML", ".INI", ".PDB", ".RESW", ".CSPROJ",".APPXMANIFEST",".RTF" };

        private string _message;// = "<span align=\"center\">"+Properties.Resources.strMessage+"</span>";
        private string _message1;
        private string txtscan;
        private string finished;

        private Scanner _scanner;
        private FileSavingQueue _fileSavingQueue;
        private ProgressPopup _progressPopup;
        private string _mostRecentlySavedFile;
        private string _filter = "";
        private int _numCheckedItems = 0;
        private int _numSelectedItems = 0;
        private bool _matchUnknownFileTypes = false;
        public bool _scanning;
        private static string _filePath;

        public List<ListViewItem> _files = new List<ListViewItem>();
       
        private ListViewColumnSorter _lvwColumnSorter;

        private Dictionary<string, ExtensionInfo> _extensionMap;
        private ImageList _imageList;

        private string strButton;
        private string strButton1;

        private ResourceManager res_man;
        private CultureInfo cul;
        private Assembly a;

        public DeletedAll_File(Scanner scanner)
        {
            InitializeComponent();
            _lvwColumnSorter = new ListViewColumnSorter();
            fileView.ListViewItemSorter = _lvwColumnSorter;
            _extensionMap = new Dictionary<string, ExtensionInfo>();
            _imageList = new ImageList();
            fileView.SmallImageList = _imageList;

            _scanner = scanner;
            scanner.ProgressUpdated += new EventHandler(state_ProgressUpdated);
            scanner.ScanStarted += new EventHandler(state_ScanStarted);
            scanner.ScanFinished += new EventHandler(state_ScanFinished);

            _fileSavingQueue = new FileSavingQueue();
            _fileSavingQueue.Finished += FileSavingQueue_Finished;
            _progressPopup = new ProgressPopup(_fileSavingQueue);
            UpdateFilterTextBox();
            if (_isChecked == true)
            {
                progressBar.Hide();
            }
            else
            {
                progressBar.Show();
            }

            progressBar.BackColor = Color.White;
            ControlExtensions.DoubleBuffered(fileView, true);
            var culture = Properties.Settings.Default.language;
            switch (culture)
            {
                case "en-US":
                    switch_language("en-US", "RecoveryThisFile.Language.en");
                    break;
                case "es-US":
                    switch_language("es-US", "RecoveryThisFile.Language.sp");
                    break;
                case "fr-FR":
                    switch_language("fr-FR", "RecoveryThisFile.Language.fr");
                    break;
                case "de-DE":
                    switch_language("de-DE", "RecoveryThisFile.Language.ge");
                    break;
                case "ja-JP":
                    switch_language("ja-JP", "RecoveryThisFile.Language.ja");
                    break;
            }
            GC.Collect();
        }

        public DeletedAll_File()
        {
            InitializeComponent();
        }

        public void switch_language(string locale, string rsname)
        {
            this.cul = new CultureInfo(locale);
            this.a = Assembly.Load("RecoveryThisFile");
            this.res_man = new ResourceManager(rsname, a);

            this.strButton = res_man.GetString("strButton", cul);
            this.strButton1 = res_man.GetString("strButton1", cul);
            this.bRestoreFiles.Text = res_man.GetString("strButton2", cul);
          
            _message = "<span align=\"center\">" + res_man.GetString("strMessage", cul) + "</span>";
            _message1 = res_man.GetString("strMessage1", cul);
            chance1 = res_man.GetString("strChance1", cul);
            chance2 = res_man.GetString("strChance2", cul);
            chance3 = res_man.GetString("strChance3", cul);
            chance4 = res_man.GetString("strChance4", cul);
            chance5 = res_man.GetString("strChance5", cul);
            chance = res_man.GetString("strChance", cul);

            _recoveryDescriptions =
                new Dictionary<FileRecoveryStatus, string>() {
                {FileRecoveryStatus.Unknown,chance4},
                {FileRecoveryStatus.Resident,chance5},
                {FileRecoveryStatus.Recoverable,chance},
                {FileRecoveryStatus.MaybeOverwritten,chance1},
                {FileRecoveryStatus.PartiallyOverwritten,chance2},
                {FileRecoveryStatus.Overwritten,chance3}};

            txtscan = res_man.GetString("strScan", cul);
            finished = res_man.GetString("strFinished", cul);
            fileView.Columns[0].Text = res_man.GetString("strName", cul);
            fileView.Columns[1].Text = res_man.GetString("strType", cul);
            fileView.Columns[2].Text = res_man.GetString("strSize", cul);
            fileView.Columns[3].Text = res_man.GetString("strModified", cul);
            fileView.Columns[4].Text = res_man.GetString("strPath", cul);
            fileView.Columns[5].Text = res_man.GetString("strChances", cul);

            EMPTY_FILTER_TEXT = res_man.GetString("strMessage17", cul);
            this.tbFilter.Text = EMPTY_FILTER_TEXT;
        }

        #region Method
        private void UpdateFilterTextBox()
        {
            if (tbFilter.Text.Length == 0 || tbFilter.Text == EMPTY_FILTER_TEXT)
            {
                tbFilter.Text = EMPTY_FILTER_TEXT;
                tbFilter.ForeColor = Color.Gray;
                tbFilter.Font = new System.Drawing.Font(tbFilter.Font, FontStyle.Italic);
            }
            else
            {
                tbFilter.ForeColor = Color.Black;
                tbFilter.Font = new System.Drawing.Font(tbFilter.Font, FontStyle.Regular);
            }
        }

        public void state_ScanStarted(object sender, EventArgs ea)
        {
            if (_files.Count > 0) { _files.Clear(); }
            _scanning = true;
            try
            {
                this.Invoke(new Action(() =>
                {
                    SetScanButtonScanning();
                    UpdateTimer.Start();
                }));
            }
            catch (InvalidOperationException exc) { Console.WriteLine(exc); }
        }

        public void state_ScanFinished(object sender, EventArgs ea)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    foreach (ListViewItem item in _files)
                    {
                        item.SubItems[4].Text = ((INodeMetadata)item.Tag).GetFileSystemNode().Path;
                        item.SubItems[5].Text = _recoveryDescriptions[((INodeMetadata)item.Tag).ChanceOfRecovery];
                        item.BackColor = _recoveryColors[((INodeMetadata)item.Tag).ChanceOfRecovery];
                    }
                    
                    UpdateTimer.Stop();
                    UpdateTimer_Tick(null, null);
                    fileView.BeginUpdate();
                    fileView.Items.Clear();
                    fileView.Items.AddRange(_files.Where(FilterMatches).ToArray());
                    fileView.EndUpdate();
                
                }));
                _scanning = false;
                lvi = null;
                // _files = null;
                FrmShowAllcs.Process_Status = 1;

                if (FrmShowAllcs.Status)
                {
                    Thread.Sleep(1000);
                    progressBar.Hide();
                }
                else
                {
                    HideProgress();
                }
                GC.Collect();
            }
            catch (InvalidOperationException exc) { Console.WriteLine(exc); }
            SetScanButtonFinished();
        }

        public void Files_Clear() { _files.Clear(); }
        public void HideProgress()
        {
            Thread.Sleep(1000);
            progressBar.Hide();
        }

        void state_ProgressUpdated(object sender, EventArgs ea)
        {
            try
            {
                this.BeginInvoke(new Action(() =>
                {
                    SetProgress(_scanner.Progress);
                }));
            }
            catch (InvalidOperationException exc) { Console.WriteLine(exc); }
        }

        public void SetScanButtonScanning()
        {
            if (_isChecked == true)
            {
                FrmShowAllcs.Instance.IsSelected = true;
                progressBar.Hide();
                if (bRestoreFiles.Visible) { bRestoreFiles.Hide(); }
            }
            else
            {
                FrmShowAllcs.Instance.IsSelected = true;
                progressBar.Show();
                if (bRestoreFiles.Visible) { bRestoreFiles.Hide(); }
            }
        }

        public void SetScanButtonFinished()
        {
            tbFilter.Enabled = true;
            cbShowUnknownFiles.Enabled = true;

            FrmShowAllcs.Instance.IsSelected = false;
            if (_isChecked == false)
            {
                FrmShowAllcs.Instance.FileScanned = finished;
                FrmShowAllcs.Instance.btnScan.Text = txtscan;
                FrmShowAllcs.Instance.btnScan.Tag = "scan";
                Invoke(new Action(() => { bRestoreFiles.Show(); }));
            }
        }
       
        private List<ListViewItem> MakeListItems(IList<INodeMetadata> metadatas)
        {
            List<ListViewItem> items = new List<ListViewItem>(metadatas.Count);
            for (int i = 0; i < metadatas.Count; i++)
            {
                    ListViewItem item = MakeListItem(metadatas[i]);
                    items.Add(item);
            }
            return items;
        }

        ListViewItem lvi = null;
        private ListViewItem MakeListItem(INodeMetadata metadata)
        {
            IFileSystemNode node = metadata.GetFileSystemNode();
            string ext = "";
            try
            {
                ext = Path.GetExtension(metadata.Name);
            }
            catch (ArgumentException exc) { Console.WriteLine(exc); }
            if (!_extensionMap.ContainsKey(ext))
            {
                _extensionMap[ext] = new ExtensionInfo(ext);
            }
            ExtensionInfo extInfo = _extensionMap[ext];
            if (extInfo.Image != null && !extInfo.Image.Size.IsEmpty) // icon file tren listview
            {
                if (!_imageList.Images.ContainsKey(ext))
                {
                    _imageList.Images.Add(ext, extInfo.Image);
                }
            }
         
            lvi = new ListViewItem(new string[] {
                metadata.Name,
                extInfo.SystemName,
                KFS.DataStream.Util.FileSizeToHumanReadableString(node.Size),
                metadata.LastModified.ToString(System.Globalization.CultureInfo.CurrentCulture),
                                node.Path,
                _recoveryDescriptions[metadata.ChanceOfRecovery]
            });
            //_ts = node.Path;
            // Thread t = new Thread(() => { GC.Collect(); });
            lvi.BackColor = _recoveryColors[metadata.ChanceOfRecovery];
            lvi.ImageKey = ext;
            lvi.Tag = metadata;

            return lvi;
        }

        private void SetProgress(double progress)
        {
            progressBar.Value = (int)(progress * progressBar.Maximum);
            FrmShowAllcs.Instance.ProgressValue = progressBar.Value;
        }

        private void FilterBy(string filter, bool showUnknownFileTypes, string filePath)
        {
            string upperFilter = filter.ToUpperInvariant();
            string upperPather1 = filePath.ToUpperInvariant();

            if (_filter != upperFilter
                    || showUnknownFileTypes != _matchUnknownFileTypes || _filePath != upperPather1)
            {
                // Check whether the new filter is more restrictive than the old filter.
                // If so, only iterate over the displayed list items and remove the ones that don't match.

                if (upperFilter.StartsWith(_filter) && (showUnknownFileTypes == _matchUnknownFileTypes || !showUnknownFileTypes)
                    && upperPather1.StartsWith(_filePath))
                {
                    _filter = upperFilter;
                    _filePath = upperPather1;
                    _matchUnknownFileTypes = showUnknownFileTypes;

                    fileView.BeginUpdate();
                    // THis is premature optimization
                    for (int i = 0; i < fileView.Items.Count; i++)
                    {
                        if (!FilterMatches(fileView.Items[i]))
                        {
                            fileView.Items.RemoveAt(i);
                            i--;
                        }
                    }
                    fileView.EndUpdate();
                    GC.Collect();
                }
                else
                {
                    _filter = upperFilter;
                    _filePath = upperPather1;
                    _matchUnknownFileTypes = showUnknownFileTypes;

                    fileView.BeginUpdate();
                    fileView.Items.Clear();
                    fileView.Items.AddRange(_files.Where(FilterMatches).ToArray());
                    fileView.EndUpdate();
                    GC.Collect();
                }
            }
        }

        private bool FilterMatches(ListViewItem item)
        {
            if (_filePath != "")
            {
                return (item.SubItems[0].Text.ToUpperInvariant().Contains(_filter))
                        && (_matchUnknownFileTypes
                                || !IsSystemOrUnknownFile(item)) && item.SubItems[4].Text.ToUpperInvariant().Contains(_filePath);
            }
            else
            {
                return (item.SubItems[0].Text.ToUpperInvariant().Contains(_filter))
                        && (_matchUnknownFileTypes
                                || !IsSystemOrUnknownFile(item));
            }
        }

        private bool IsSystemOrUnknownFile(ListViewItem item)
        {
            try
            {
                string ext = Path.GetExtension(item.SubItems[0].Text);
                return !_extensionMap.ContainsKey(ext)
                        || _extensionMap[ext].UnrecognisedExtension
                || _systemFileExtensions.Contains(ext.ToUpper());
            }
            catch (ArgumentException)
            {
                return true;
            }
        }

        
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            IList<INodeMetadata> deletedFiles = _scanner.GetDeletedFiles();
            int fileCount = deletedFiles.Count;
            if (fileCount > _files.Count)
            {
                var items = MakeListItems(deletedFiles.GetRange(_files.Count, fileCount - _files.Count));
                _files.AddRange(items.Where(FilterMatches).ToArray());
                fileView.BeginUpdate();
                fileView.Items.AddRange(items.Where(FilterMatches).ToArray());
                fileView.EndUpdate();
                try
                {
                    fileView.Items[fileView.Items.Count - 1].EnsureVisible();
                }
                catch { }
            }
            deletedFiles = null;  
        }

        private void fileView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                bool check = RangeTree.Database.CheckData();
                if (fileView.SelectedItems.Count == 1)
                {
                    INodeMetadata metadata = fileView.SelectedItems[0].Tag as INodeMetadata;
                    if (metadata != null)
                    {
                        ContextMenu menu = new ContextMenu();
                        MenuItem recoverFile = new MenuItem(strButton, new EventHandler(delegate (object o, EventArgs ea)
                        {
                                                
                            if (check)
                            {
                                if (_progressPopup.Visible)
                                {
                                    FrmShowAllcs.Instance.MessageBox(""+Properties.Resources.strMessage1);
                                }
                                else
                                {
                                    PromptUserToSaveFile(metadata, fileView.SelectedItems[0]);
                                }
                            }
                            else
                            {
                                Register popup = new Register();
                                popup.StartPosition = FormStartPosition.CenterParent;
                                popup.ShowDialog(this);
                                //popup.BringToFront();
                                popup.Dispose();
                            }
                        }));
                        recoverFile.Enabled = !_scanning;
                        menu.MenuItems.Add(recoverFile);
                        menu.Show(fileView, e.Location);
                    }
                }
                else if (fileView.SelectedItems.Count > 1)
                {
                    // We need slightly different behaviour to save multiple files.
                    ContextMenu menu = new ContextMenu();
                    MenuItem recoverFiles = new MenuItem(strButton1, new EventHandler(delegate (object o, EventArgs ea)
                    {
                        if (check)
                        {
                            if (_progressPopup.Visible)
                            {
                                FrmShowAllcs.Instance.MessageBox("" + Properties.Resources.strMessage1);
                            }
                            {
                                PromptUserToSaveFiles(fileView.SelectedItems);
                            }
                        }
                        else
                        {
                            Register popup = new Register();
                            popup.StartPosition = FormStartPosition.CenterParent;
                            popup.ShowDialog(this);
                            //popup.BringToFront();
                            popup.Dispose();
                        }
                    }));
                    recoverFiles.Enabled = !_scanning;
                    menu.MenuItems.Add(recoverFiles);
                    menu.Show(fileView, e.Location);
                }
            }
        }

        private void PromptUserToSaveFile(INodeMetadata metadata, ListViewItem item)
        {
            if (metadata != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%SystemDrive");
                saveFileDialog.FileName = metadata.Name;
                saveFileDialog.Filter = "Any Files|*.*";
                saveFileDialog.Title = "Select a Location";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Check that the drive isn't the same as the drive being copied from.
                    using(Dialog dialog = new Dialog(_message))
                    {
                        if(saveFileDialog.FileName[0] == _scanner.DiskName[0])
                        {
                            dialog.StartPosition = FormStartPosition.CenterParent;
                            DialogResult result = dialog.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                IFileSystemNode node = metadata.GetFileSystemNode();
                                SaveSingleFile(node, saveFileDialog.FileName);
                                fileView.Items.Remove(item);
                            }
                            dialog.Dispose();
                        }
                        else
                        {
                            IFileSystemNode node = metadata.GetFileSystemNode();
                            SaveSingleFile(node, saveFileDialog.FileName);
                            fileView.Items.Remove(item);
                        }
                    }
                }
            }
        }

        private void SaveSingleFile(IFileSystemNode node, string filePath)
        {
            _mostRecentlySavedFile = filePath;
            if (!_progressPopup.Visible)
            {
                _progressPopup.Show(this);
            }
            _fileSavingQueue.Push(filePath, node);
        }

        private void PromptUserToSaveFiles(IEnumerable items)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                // Check that the drive isn't the same as the drive being copied from.
                using(Dialog dialog = new Dialog(_message))
                {
                    if (folderDialog.SelectedPath[0] == _scanner.DiskName[0])
                    {
                        dialog.StartPosition = FormStartPosition.CenterParent;
                        DialogResult result = dialog.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            List<IFileSystemNode> nodes = new List<IFileSystemNode>();
                            foreach (ListViewItem item in items)
                            {
                                INodeMetadata metadata = item.Tag as INodeMetadata;
                                if (metadata != null)
                                {
                                    nodes.Add(metadata.GetFileSystemNode());
                                    fileView.BeginUpdate();
                                    fileView.Items.Remove(item);
                                    fileView.EndUpdate();
                                }
                            }
                            SaveMultipleFiles(nodes, folderDialog.SelectedPath);
                        }
                        dialog.Dispose();
                    }
                    else
                    {
                        List<IFileSystemNode> nodes = new List<IFileSystemNode>();
                        foreach (ListViewItem item in items)
                        {
                            INodeMetadata metadata = item.Tag as INodeMetadata;
                            if (metadata != null)
                            {
                                nodes.Add(metadata.GetFileSystemNode());
                                fileView.BeginUpdate();
                                fileView.Items.Remove(item);
                                fileView.EndUpdate();
                            }
                        }
                        SaveMultipleFiles(nodes, folderDialog.SelectedPath);
                    }    
                }
            }
        }

        private void SaveMultipleFiles(IEnumerable<IFileSystemNode> nodes, string folderPath)
        {
            foreach (IFileSystemNode node in nodes)
            {
                string file = PathUtils.MakeFileNameValid(node.Name);
                string fileName = Path.Combine(folderPath, file);
                if (System.IO.File.Exists(fileName))
                {
                    int copyNum = 1;
                    string newFileName;
                    do
                    {
                        newFileName = Path.Combine(Path.GetDirectoryName(fileName),
                            string.Format("{0} ({1}){2}", Path.GetFileNameWithoutExtension(fileName),
                            copyNum, Path.GetExtension(fileName)));
                        copyNum++;
                    } while (System.IO.File.Exists(newFileName));
                    fileName = newFileName;
                }
                SaveSingleFile(node, fileName);
            }
        }

        private void FileSavingQueue_Finished()
        {
            if (!string.IsNullOrEmpty(_mostRecentlySavedFile))
            {
                System.Diagnostics.Process.Start("explorer.exe", "/select, \"" + _mostRecentlySavedFile + '"');
            }
        }

        // sắp xếp record trong list
        private void fileView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // nếu click vào cột đang được sắp xếp
            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // thay đổi cách sắp xếp giá trị trong cột
                if (_lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    _lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // đặt giá trị mặc định cho cột là sắp xếp theo kiểu tăng dẫn
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = SortOrder.Ascending;
            }
            // thực hiện sắp xếp
            this.fileView.Sort();
        }

        private void tbFilter_Enter(object sender, EventArgs e)
        {
            if (tbFilter.Text.Length == 0 || tbFilter.Text == EMPTY_FILTER_TEXT)
            {
                tbFilter.Text = "";
                tbFilter.ForeColor = Color.Black;
                tbFilter.Font = new System.Drawing.Font(tbFilter.Font, FontStyle.Regular);
            }
        }

        private void tbFilter_Leave(object sender, EventArgs e)
        {
            UpdateFilterTextBox();
        }

        public void Filter2()
        {
            if (_filePath == "")
                FilterBy("", cbShowUnknownFiles.Checked, "");
            else
                FilterBy("", cbShowUnknownFiles.Checked, _filePath);
        }

        private void Filter()
        {
            if (tbFilter.Text.Length > 0 && tbFilter.Text != EMPTY_FILTER_TEXT)
            {
                if (_filePath == "")
                    FilterBy(tbFilter.Text, cbShowUnknownFiles.Checked, "");
                else
                    FilterBy(tbFilter.Text, cbShowUnknownFiles.Checked, _filePath);
            }
            else
            {
                if (_filePath == "")
                    FilterBy("", cbShowUnknownFiles.Checked, "");
                else
                    FilterBy("", cbShowUnknownFiles.Checked, _filePath);
            }
        }

      
        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbFilter.Text.Length == 0 || tbFilter.Text == EMPTY_FILTER_TEXT)
                {
                    if (_filePath == "")
                        FilterBy("", cbShowUnknownFiles.Checked, "");
                    else
                        FilterBy("", cbShowUnknownFiles.Checked, _filePath);
                }
            }
            catch { }
        }

        private void cbShowUnknownFiles_CheckedChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void bRestoreFiles_Click(object sender, EventArgs e)
        {
            bool check = RangeTree.Database.CheckData();
            if (check)
            {
                if (_progressPopup.Visible)
                {
                    FrmShowAllcs.Instance.MessageBox(_message1);
                }
                else
                {
                    if (_numCheckedItems == 1)
                    {
                        PromptUserToSaveFile(fileView.CheckedItems[0].Tag as INodeMetadata, fileView.CheckedItems[0]);
                    }
                    else if (_numCheckedItems > 1)
                    {
                        PromptUserToSaveFiles(fileView.CheckedItems);
                    }
                    else if (_numSelectedItems == 1)
                    {
                        PromptUserToSaveFile(fileView.SelectedItems[0].Tag as INodeMetadata, fileView.SelectedItems[0]);
                    }
                    else if (_numSelectedItems > 1)
                    {
                        PromptUserToSaveFiles(fileView.SelectedItems);
                    }
                }
            }
            else
            {
                Register popup = new Register();
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.ShowDialog(this);
                //popup.BringToFront();
                popup.Dispose();
            }
        }

        private void UpdateRestoreButton()
        {
            if (!_scanning)
            {
                bRestoreFiles.Enabled = _numCheckedItems > 0 || _numSelectedItems > 0;
            }
        }

        private void fileView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_scanning) { e.NewValue = CheckState.Unchecked; }
            else
            {
                // Update the number of checked items
                _numCheckedItems += e.NewValue == CheckState.Checked ? 1 : -1;
                UpdateRestoreButton();
            }
        }

        private void fileView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (_scanning) { e.Item.Selected = false; }
            else
            {
                _numSelectedItems += e.IsSelected ? 1 : -1;
                UpdateRestoreButton();
            }
        }

        private void DeletedFileViewer_Leave(object sender, EventArgs e)
        {
            if(_files.Count > 0) { _files.Clear(); }
            _scanAll = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void tbFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Filter();
            }
        }

        #endregion
    }
}
