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

namespace MySynchFiles {


    /// <summary>
    /// Constructor
    /// </summary>
    public partial class SynchMain : Window {

        #region Declaration
        private readonly System.Windows.Forms.NotifyIcon _notifyIcon = new System.Windows.Forms.NotifyIcon();
        #endregion

        #region Constructor
        public SynchMain() {
            InitializeComponent();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// initialize
        /// </summary>
        private void Initialize() {

        }

        /// <summary>
        /// Create context menu for CategoryList
        /// </summary>
        private void CreateContextMenu() {
            //CustomContextMenu.MenuItemData CreateItem(int id, string text, RoutedEventHandler handler, bool isDelete = false) {
            //    var item = new CustomContextMenu.MenuItemData {
            //        Id = id,
            //        Text = text
            //    };
            //    item.Click += handler;
            //    if (isDelete) {
            //        item.ForeGround = "#FF0000";
            //    }
            //    return item;
            //}

            //this._categoryMenu.AddItem(CreateItem((int)ContextMenuId.Add, "Add", CategoryContextMenuAdd_Click));
            //this._categoryMenu.AddItem(CreateItem((int)ContextMenuId.Edit, "Edit", CategoryContextMenuEdit_Click));
            //this._categoryMenu.AddSeparator();
            //this._categoryMenu.AddItem(CreateItem((int)ContextMenuId.Delete, "Delete", CategoryContextMenuDelete_Click, true));

            //this._itemMenu.AddItem(CreateItem((int)ContextMenuId.Add, "Add", ItemContextMenuAdd_Click));
            //this._itemMenu.AddItem(CreateItem((int)ContextMenuId.Edit, "Edit", ItemContextMenuEdit_Click));
            //this._itemMenu.AddItem(CreateItem((int)ContextMenuId.Detail, "Detail", ItemContextMenuDetail_Click));
            //this._itemMenu.AddSeparator();
            //this._itemMenu.AddItem(CreateItem((int)ContextMenuId.Delete, "Delete", ItemContextMenuDelete_Click, true));
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
        #endregion
    }

}
