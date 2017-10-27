using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KFS.Disks;
using KFS.FileSystems.NTFS;
using System.Threading;
using System.Management;
using System.IO;
using KickassUndelete;
using KFS.FileSystems;
using System.Collections;
using RecoveryThisFile.RangeTree;
using GuiComponents;
using System.Diagnostics;
using System.Reflection;
using System.Globalization;
using System.Resources;

namespace RecoveryThisFile
{
    public partial class FrmShowAllcs : UserControl
    {
        private static FrmShowAllcs _instance;
        public static FrmShowAllcs Instance
        {
            get
            { if (_instance == null)
                    _instance = new FrmShowAllcs();
                return _instance;
            }
        }
        public bool IsSelected
        {
            set { _isSelected = value; }
        }
                             
        public string FileScanned
        {
            set { lbfile.Text = value; }
        }
      

        public bool scanAll
        {
            get { return true; }
        }

        private static bool _status = true;
        public static bool Status
        {
            get { return _status; }
        }

        private static int process_status;
        public static int Process_Status
        {
            get { return process_status; }
            set { process_status = value; }
        }

        private const string EMPTY_FILTER_TEXT = "Enter filter text here...";

        private static int _progressvalue;
        public int ProgressValue
        {
            set { _progressvalue = value; }
        }
        
        public int IV { set { iv = value; } }
        public int JV { set { jv = value; } }

        public List<string> listPath { get; set; }

        public bool drive { get; set; }

        IFileSystem _fileSystem;
        public Dictionary<IFileSystem, Scanner> _scanners = new Dictionary<IFileSystem, Scanner>();
        public Dictionary<IFileSystem, DeletedAll_File> _deletedViewers = new Dictionary<IFileSystem, DeletedAll_File>();

        public List<IFileSystemStore> FileSystemStore = new List<IFileSystemStore>();

        private static bool _isSelected;
        private static AutoResetEvent event_1 = new AutoResetEvent(false);

        private ImageList _imageList;
        private ListViewColumnSorter _lvwColumnSorter;
        private ProgressPopup _progressPopup;
        private FileSavingQueue _fileSavingQueue;
        private string _mostRecentlySavedFile;
        private string _message;// = "<span align=\"center\">"+Properties.Resources.strMessage+"</span>";
        private string _message1;
        private int _numCheckedItems = 0;
        private int _numSelectedItems = 0;
        private string _filter = "";
        IEnumerable<ListViewItem> distinctItem;
        int iv = 0;
        int jv = 0;

        int sum = 0;

        private ResourceManager res_man;
        private CultureInfo cul;
        private Assembly a;

        private string scan;
        private string stop;
        private string finished;
        private string warning;
        private string stopping;

        public FrmShowAllcs()
        {
            InitializeComponent();
            _imageList = new ImageList();
            fileView.SmallImageList = _imageList;

            ControlExtensions.DoubleBuffered(fileView, true);

            _lvwColumnSorter = new ListViewColumnSorter();
            fileView.ListViewItemSorter = _lvwColumnSorter;

            _fileSavingQueue = new FileSavingQueue();
            _fileSavingQueue.Finished += FileSavingQueue_Finished;
            _progressPopup = new ProgressPopup(_fileSavingQueue);
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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        public void switch_language(string locale, string rsname)
        {
            this.cul = new CultureInfo(locale);
            this.a = Assembly.Load("RecoveryThisFile");
            this.res_man = new ResourceManager(rsname, a);
            this.btnSearch.Text = res_man.GetString("strSearch", cul);
            this.btnScan.Text = res_man.GetString("strStop", cul);
            this.scan = res_man.GetString("strScan",cul);
            this.stop = res_man.GetString("strStop", cul);
            this.stopping = res_man.GetString("strStopping");
            this.finished = res_man.GetString("strFinished");
            this.warning = res_man.GetString("strWarning");
            this.bRestoreFiles.Text = res_man.GetString("strButton2", cul);
            _message = "<span align=\"center\">" + res_man.GetString("strMessage", cul) + "</span>";
            _message1 = res_man.GetString("strMessage1", cul);
        }

        #region disk tree
        public void SetFileSystem(IFileSystemStore logicalDisk)
        {
            if (logicalDisk.FS != null)
            {
                GC.Collect();
                if (!_scanners.ContainsKey(logicalDisk.FS))
                {
                    _scanners[logicalDisk.FS] = new Scanner(logicalDisk.ToString(), logicalDisk.FS);
                    _deletedViewers[logicalDisk.FS] = new DeletedAll_File(_scanners[logicalDisk.FS]);
                    AddDeletedFileViewer(_deletedViewers[logicalDisk.FS]);
                }
                if (_fileSystem != null && _scanners.ContainsKey(_fileSystem))
                {
                    _deletedViewers[_fileSystem].Hide();
                }
                _fileSystem = logicalDisk.FS;
                _deletedViewers[logicalDisk.FS].Show();
            }
        }

        private void AddDeletedFileViewer(DeletedAll_File viewer)
        {
            int MARGIN = 1;
            panel2.Controls.Add(viewer);
            viewer.Top = viewer.Left = MARGIN;
            viewer.Width = panel2.Width - MARGIN * 2;
            viewer.Height = panel2.Height - MARGIN * 2;
            viewer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }
        
        #endregion    
       
        ToolTip tt = new ToolTip();

        private void button1_Click(object sender, EventArgs e)
        {
            if ((string)btnScan.Tag == "stop")
            {
                this.btnScan.Text = stopping;
                if (drive == false)
                {
                    timer2.Stop();
                    progressBar1.Hide();
                    progressBar1.Value = 0;
                    _progressvalue = 0;
                }

                foreach (var item in FileSystemStore)
                {
                    try
                    {
                        _scanners[item.FS].StopScan();
                        _deletedViewers[item.FS].state_ScanFinished(sender, e);
                        _deletedViewers[item.FS].Filter2();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        btnScan.Tag = "scan";
                        btnScan.Text = scan;
                        lbfile.Text = finished;
                    }
                }
                btnScan.Tag = "scan";
                btnScan.Text = scan;
                lbfile.Text = finished;
                this.bRestoreFiles.Enabled = true;
                this.bRestoreFiles.Show();
            }
            else
            {
                if (drive == false)
                {
                    DeletedAll_File.IsChecked = true;
                    progressBar1.Show();
                    progressBar1.Value = 0;
                    process_status = 0;
                    iv = 0;
                    jv = 0;
                    _progressvalue = 0;
                    timer2.Start();
                }
                else
                {
                    DeletedAll_File.IsChecked = false;
                    foreach (var item in FileSystemStore)
                    {
                        try
                        {
                            _scanners[item.FS].StartScan();
                           // _deletedViewers[item.FS].state_ScanFinished(sender, e);
                        }
                        catch
                        {
                            //System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                    }
                }
                btnScan.Tag = "stop";
                btnScan.Text = stop;
                this.bRestoreFiles.Hide();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (process_status <= 1 && iv < FileSystemStore.Count)
            {
                lock (this)
                {
                    if (process_status != 1)
                    {
                        if (jv == 0)
                        {
                            _status = false;
                            jv++;
                            if (FileSystemStore.ElementAt(iv) != null)
                            {
                                //diskTree.SelectedNode = diskTree.Nodes[iv];
                                  Scanner.NodePath = listPath.ElementAt(iv);
                                  SetFileSystem(FileSystemStore.ElementAt(iv));
                             //   System.Windows.Forms.MessageBox.Show(listPath.ElementAt(0).ToString() + " " + FileSystemStore.ElementAt(iv).ToString());
                                _scanners[FileSystemStore.ElementAt(iv).FS].StartScan();
                            }
                            else
                            {
                                process_status = 1;
                            }
                        }
                    }

                    if (process_status == 1)
                    {
                        process_status = 0;
                        jv = 0;
                        try
                        {
                            distinctItem = (from ListViewItem item in _deletedViewers[FileSystemStore.ElementAt(iv).FS].listItem select (ListViewItem)item.Clone()).Distinct();
                            fileView.Items.AddRange(distinctItem.ToArray());
                        }
                        catch (Exception ex) { Debug.WriteLine(ex.Message); }

                        sum = _deletedViewers[this.FileSystemStore.ElementAt(iv).FS].value + sum;//
                        iv++;
                    }
                }

                if ((_progressvalue + sum) <= progressBar1.Maximum)
                {
                    progressBar1.Value = _progressvalue + sum;
                }
            }
            if (iv >= FileSystemStore.Count)
            {
                timer2.Stop();
                progressBar1.Value = 0;
                sum = 0;
                _progressvalue = 0;
                progressBar1.Hide();
                btnScan.Text = scan;
                btnScan.Tag = "scan";
                lbfile.Text = finished;
                this.bRestoreFiles.Enabled = true;
                this.bRestoreFiles.Show();
            }
        }

        public void StopScanningWhenFormLeave(object sender, EventArgs e)
        {
            if (drive == false)
            {
                DeletedAll_File.IsChecked = false;
                timer2.Stop();
                progressBar1.Hide();
                progressBar1.Value = 0;

                foreach (var item in FileSystemStore)
                {
                    try
                    {
                        _scanners[item.FS].StopScan();
                        _deletedViewers[item.FS].state_ScanFinished(sender, e);
                        GC.Collect();
                    }
                    catch { btnScan.Text = scan; btnScan.Text = "scan"; lbfile.Text = finished; }
                }
            }
            else
            {
                _scanners[FileSystemStore.ElementAt(0).FS].StopScan();
                _deletedViewers[FileSystemStore.ElementAt(0).FS].state_ScanFinished(sender, e);
            }
        }

        public void MessageBox(string message)
        {
            using (Dialog dialog = new Dialog(message, false, true, false, warning))
            {
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.ShowDialog();
                dialog.Dispose();
            }
        }

        private void fileView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
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

        private void FileSavingQueue_Finished()
        {
            if (!string.IsNullOrEmpty(_mostRecentlySavedFile))
            {
                System.Diagnostics.Process.Start("explorer.exe", "/select, \"" + _mostRecentlySavedFile + '"');
            }
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
                        MenuItem recoverFile = new MenuItem(Properties.Resources.strButton, new EventHandler(delegate (object o, EventArgs ea)
                        {

                            if (check)
                            {
                                if (_progressPopup.Visible)
                                {
                                    FrmShowAllcs.Instance.MessageBox("" + Properties.Resources.strMessage1);
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
                    //    recoverFile.Enabled = !_scanning;
                        menu.MenuItems.Add(recoverFile);
                        menu.Show(fileView, e.Location);
                    }
                }
                else if (fileView.SelectedItems.Count > 1)
                {
                    // We need slightly different behaviour to save multiple files.
                    ContextMenu menu = new ContextMenu();
                    MenuItem recoverFiles = new MenuItem(Properties.Resources.strButton1, new EventHandler(delegate (object o, EventArgs ea)
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
                 //   recoverFiles.Enabled = !_scanning;
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
                        IFileSystemNode node = metadata.GetFileSystemNode();
                        SaveSingleFile(node, saveFileDialog.FileName);
                        fileView.Items.Remove(item);
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

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {


        }

        private void PromptUserToSaveFiles(IEnumerable items)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                // Check that the drive isn't the same as the drive being copied from.

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

        private bool FilterMatches(ListViewItem item)
        {
            return (item.SubItems[0].Text.ToUpperInvariant().Contains(_filter));
        }

        private void FilterBy(string filter)
        {
            string upperFilter = filter.ToUpperInvariant();
            // string upperPather1 = filePath.ToUpperInvariant();

            if (_filter != upperFilter)
            {
                // Check whether the new filter is more restrictive than the old filter.
                // If so, only iterate over the displayed list items and remove the ones that don't match.
                if (upperFilter.StartsWith(_filter))
                {
                    _filter = upperFilter;
                    //   _filePath = upperPather1;

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
                    //   _filePath = upperPather1;

                    fileView.BeginUpdate();
                    fileView.Items.Clear();
                    fileView.Items.AddRange(distinctItem.Where(FilterMatches).ToArray());
                    fileView.EndUpdate();
                    GC.Collect();
                }
            }
        }

        private void Filter()
        {
            if (tbFilter.Text.Length > 0 && tbFilter.Text != EMPTY_FILTER_TEXT)
            {
                    FilterBy(tbFilter.Text);
            }
        }

        private void tbFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Filter();
            }
        }

        private void tbFilter_Leave(object sender, EventArgs e)
        {
            UpdateFilterTextBox();
        }

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

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text.Length == 0 || tbFilter.Text == EMPTY_FILTER_TEXT)
            {
                FilterBy("");
            }
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
            if ((string)btnScan.Tag!="stop")
            {
                bRestoreFiles.Enabled = _numCheckedItems > 0 || _numSelectedItems > 0;
            }
        }

        private void fileView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if ((string)btnScan.Tag == "stop") { e.NewValue = CheckState.Unchecked; }
            else
            {
                _numCheckedItems += e.NewValue == CheckState.Checked ? 1 : -1;
                UpdateRestoreButton();
            }
        }
        private void fileView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if ((string)btnScan.Tag == "stop") { e.Item.Selected = false; }
            else
            {
                _numSelectedItems += e.IsSelected ? 1 : -1;
                UpdateRestoreButton();
            }
        }

    }

}
