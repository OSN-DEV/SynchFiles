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
using MySynchFiles.Data;
namespace MySynchFiles {
    /// <summary>
    /// Edit Sync FIle
    /// </summary>
    public partial class EditSyncFile : Window {
        #region Constructor
        public EditSyncFile() {
            InitializeComponent();
        }

        public EditSyncFile(Window owner, SyncFileDataModel data) {
            InitializeComponent();
            this.Owner = owner;
            this.DataContext = data;
        }
        #endregion

        #region Event
        private void CustomTextBox_PreviewDragOver(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effects = DragDropEffects.Copy;
                e.Handled = true;
            }
        }
        private void CustomTextBox_Drop(object sender, DragEventArgs e) {
            if (!(e.Data.GetData(DataFormats.FileDrop) is string[] files)) {
                return;
            }
            var txt = sender as TextBox;
            var model = (SyncFileDataModel)this.DataContext;

            txt.Text = files[0];
            if (txt.Tag.ToString() == "Local") {
                model.LocalFile = files[0];
            } else {
                model.ServerFile = files[0];
            }

            ((TextBox)sender).Text = files[0];
        }
        public void OK_Click(object sender, EventArgs e) {
            this.DialogResult = true;
        }
        #endregion

    }
}
