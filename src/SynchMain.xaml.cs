using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySynchFiles.Component;
using System.Collections.ObjectModel;
using MySynchFiles.Data;
using MyLib.File;
using System.Windows.Threading;

namespace MySynchFiles {


    /// <summary>
    /// Constructor
    /// </summary>
    public partial class SynchMain : Window {

        #region Declaration
        private DispatcherTimer _timer = new DispatcherTimer();
        private bool _isSynch = false;
        private readonly System.Windows.Forms.NotifyIcon _notifyIcon = new System.Windows.Forms.NotifyIcon();
        private CustomContextMenu _synchFilesMenu = new CustomContextMenu();
        private AppRepository _appData = null;
        private enum ListMenuItemId : int {
            Add,
            Edit,
            Delete
        }
        #endregion

        #region Constructor
        public SynchMain() {
            InitializeComponent();
            this.Initialize();
        }
        #endregion

        #region Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (this.WindowState == WindowState.Normal) {
                this._appData.Pos.X = this.Left;
                this._appData.Pos.Y = this.Top;
                this._appData.Save();
            }
        }


        /// <summary>
        /// list context menu add click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItemMenuAddClick(object sender, EventArgs e) {
            var model = new SyncFileDataModel();
            if (this.ShowEditSynchFile(model)) {
                this._appData.SyncFiles.Add(model);
                this._appData.Save();
            }
        }

        /// <summary>
        /// list context menu edit click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItemMenuEditClick(object sender, EventArgs e) {
            var model = this.cSyncFiles.SelectedItem as SyncFileDataModel;
            if (this.ShowEditSynchFile(model)) {
                this._appData.Save();
            }
        }

        /// <summary>
        /// list context menu delete click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItemMenuDeleteClick(object sender, EventArgs e) {
            var item = this.cSyncFiles.SelectedItem as SyncFileDataModel;
            this._appData.SyncFiles.Remove(item);
            this._appData.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cSyncFiles_ContextMenuOpening(object sender, ContextMenuEventArgs e) {
            var model = this.cSyncFiles.GetItemAt(Mouse.GetPosition(this.cSyncFiles));
            var isItemSelected = (null != model);
            this._synchFilesMenu.SetMenuItemEnabled((int)ListMenuItemId.Edit, isItemSelected);
            this._synchFilesMenu.SetMenuItemEnabled((int)ListMenuItemId.Delete, isItemSelected);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cSyncFiles_Drop(object sender, DragEventArgs e) {
            if (!(e.Data.GetData(DataFormats.FileDrop) is string[] files)) {
                return;
            }
            var model = new SyncFileDataModel();
            var file = new FileOperator(files[0]);
            Task.Run(() => {
                Application.Current.Dispatcher.Invoke(() => {
                    model.DisplayName = file.NameWithoutExtension;
                    model.LocalFile = file.FilePath;
                    if (ShowEditSynchFile(model)) {
                        this._appData.SyncFiles.Add(model);
                        this._appData.Save();
                    }
                });
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            switch(e.Key) {
                case Key.Escape:
                    e.Handled = true;
                    this.SetWindowsState(true);
                    break;
            }
        }

        /// <summary>
        /// timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e) {
            this._timer.Stop();
            this._isSynch = true;
            new SyncTask().Start(this.SyncComplete, this._appData.SyncFiles);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyMenuSynchNow_Click(object sender, EventArgs e) {
            if (this._isSynch) {
                return;
            }
            this._isSynch = true;
            this._timer.Stop();
            new SyncTask().Start(this.SyncComplete, this._appData.SyncFiles);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyMenuShow_Click(object sender, EventArgs e) {
            this.SetWindowsState(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyMenuExit_Click(object sender, EventArgs e) {
            this.Close();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// initialize
        /// </summary>
        private void Initialize() {
            this._appData = AppRepository.GetInstance();

            // restore window pos
            var x = MyLib.Util.Common.GetWindowPosition(this._appData.Pos.X, this.Width, SystemParameters.VirtualScreenWidth);
            var y = MyLib.Util.Common.GetWindowPosition(this._appData.Pos.X, this.Height, SystemParameters.VirtualScreenHeight);

            if (0 <= x) {
                this.Left = x;
            }
            if (0 <= y) {
                this.Top = y;
            }

            // restore data
            this.cSyncFiles.DataContext = this._appData.SyncFiles;

            // create context menu
            this.CreateContextMenu();
            this.cSyncFiles.ContextMenu = this._synchFilesMenu;

            // prepare timer
            this._timer.Interval = new TimeSpan(0, 5, 0);
            this._timer.Tick += Timer_Tick;

            // start initial sync
            this._isSynch = true;
            new SyncTask().Start(this.SyncComplete, this._appData.SyncFiles);

            // Prepare Nofity
            this.SetUpNotifyIcon();
        }


        /// <summary>
        /// Create context menu for CategoryList
        /// </summary>
        private void CreateContextMenu() {
            var item = new CustomContextMenu.MenuItemData();
            item.Click += ListItemMenuAddClick;
            item.Id = (int)ListMenuItemId.Add;
            item.Text = ListMenuItemId.Add.ToString();
            this._synchFilesMenu.AddItem(item);

            item = new CustomContextMenu.MenuItemData();
            item.Click += ListItemMenuEditClick;
            item.Id = (int)ListMenuItemId.Edit;
            item.Text = ListMenuItemId.Edit.ToString();
            this._synchFilesMenu.AddItem(item);

            this._synchFilesMenu.AddSeparator();

            item = new CustomContextMenu.MenuItemData();
            item.Click += ListItemMenuDeleteClick;
            item.Id = (int)ListMenuItemId.Delete;
            item.Text = ListMenuItemId.Delete.ToString();
            item.ForeGround = "#FF0000";
            this._synchFilesMenu.AddItem(item);
        }

        /// <summary>
        /// set up notify icon
        /// </summary>
        private void SetUpNotifyIcon() {
            this._notifyIcon.Text = "My Synch Files";
            this._notifyIcon.Icon = new System.Drawing.Icon("app.ico");
            var menu = new System.Windows.Forms.ContextMenuStrip();

            var menuItemSyncNow = new System.Windows.Forms.ToolStripMenuItem {
                Text = "synch",
            };
            menuItemSyncNow.Click += this.NotifyMenuSynchNow_Click;
            menu.Items.Add(menuItemSyncNow);


            var menuItemShow = new System.Windows.Forms.ToolStripMenuItem {
                Text = "show",
            };
            menuItemShow.Click += this.NotifyMenuShow_Click;
            menu.Items.Add(menuItemShow);

            var menuItemExit = new System.Windows.Forms.ToolStripMenuItem {
                Text = "exit"
            };
            menuItemExit.Click += this.NotifyMenuExit_Click;
            menu.Items.Add(menuItemExit);

            this._notifyIcon.ContextMenuStrip = menu;
            this._notifyIcon.Visible = false;
        }

        /// <summary>
        /// set window state
        /// </summary>
        /// <param name="minimize">true:minize, false:normalize</param>
        private void SetWindowsState(bool minimize) {
            this.WindowState = minimize ? WindowState.Minimized : WindowState.Normal;
            this.ShowInTaskbar = !minimize;
            this._notifyIcon.Visible = minimize;
            if (minimize) {
                this._appData.Pos.X = this.Left;
                this._appData.Pos.Y = this.Top;
                this._appData.Save();
            } else {
                this.Activate();
            }
        }

        /// <summary>
        /// show dialog
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool ShowEditSynchFile(SyncFileDataModel model) {
            var result =  new EditSyncFile(this, model).ShowDialog() ?? false;
            if (result & !this._isSynch) {
                this._timer.Stop();
                this._isSynch = true;
                new SyncTask().Start(this.SyncComplete,  model);
            }
            return result;
        }

        private void SyncComplete() {
            this._isSynch = false;
            this._timer.Start();
        }
        #endregion
    }

}
