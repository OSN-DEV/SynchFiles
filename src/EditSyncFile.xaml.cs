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
        public void OK_Click(object sender, EventArgs e) {
            this.DialogResult = true;
        }

        #endregion
    }
}
