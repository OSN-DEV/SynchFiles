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
namespace MySynchFiles {


    /// <summary>
    /// Constructor
    /// </summary>
    public partial class SynchMain : Window {

        #region Declaration
        private readonly System.Windows.Forms.NotifyIcon _notifyIcon = new System.Windows.Forms.NotifyIcon();
        private ObservableCollection<SyncFileDataModel> _model;
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
            this._model = new ObservableCollection<SyncFileDataModel>();
            foreach(var model in this._appData.SyncFiles) {
                this._model.Add(new SyncFileDataModel() {
                    DisplayName = model.DisplayName,
                    LocalFile = model.LocalFile,
                    ServerFile = model.ServerFile
                });
            }

            this._model.Add(new SyncFileDataModel() {
                DisplayName = "xxx",
                UpdateDateTime= "2019/01/11 12:12 ↑",
                 CheckDateTime ="2019/01/11 12:13"
            });
            this.cSyncFiles.DataContext = this._model;

            // create context menu
            this.CreateContextMenu();
            this.cSyncFiles.ContextMenu = this._synchFilesMenu;
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
            //this._notifyIcon.Text = "My Synch FIles";
            //this._notifyIcon.Icon = new System.Drawing.Icon("app.ico");
            //var menu = new System.Windows.Forms.ContextMenuStrip();
            //var menuItemShow = new System.Windows.Forms.ToolStripMenuItem {
            //    Text = "show",
            //};
            //menuItemShow.Click += this.NotifyMenuShow_Click;
            //menu.Items.Add(menuItemShow);

            //var menuItemExit = new System.Windows.Forms.ToolStripMenuItem {
            //    Text = "exit"
            //};
            //menuItemExit.Click += this.NotifyMenuExit_Click;
            //menu.Items.Add(menuItemExit);

            //this._notifyIcon.ContextMenuStrip = menu;
            //this._notifyIcon.Visible = false;
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
            return new EditSyncFile(this, model).ShowDialog() ?? false;
        }
        #endregion

    }

}
