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

        #endregion
    }

}
