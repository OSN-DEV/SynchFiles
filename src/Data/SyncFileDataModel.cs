using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySynchFiles.Data {
    public class SyncFileDataModel : BindableBase {

        #region Public Property
        /// <summary>
        /// 表示名称
        /// </summary>
        public string DisplayName {
            get => _displayName;
            set => SetProperty(ref _displayName, value, () => OnPropertyChanged(nameof(DisplayName)));
        }
        private string _displayName = "";
            
        /// <summary>
        /// ローカルファイル
        /// </summary>
        public string LocalFile {
            get => _localFile;
            set => SetProperty(ref _localFile, value, () => OnPropertyChanged(nameof(LocalFile)));
        }
        private string  _localFile = "";

        /// <summary>
        /// サーバーファイル
        /// </summary>
        public string ServerFile {
            get => _serverFile;
            set => SetProperty(ref _serverFile, value, () => OnPropertyChanged(nameof(ServerFile)));
        }
        private string _serverFile = "";

        /// <summary>
        /// 同期実行日時
        /// </summary>
        public string CheckDateTime {
            get => _checkDateTime;
            set => SetProperty(ref _checkDateTime, value, () => OnPropertyChanged(nameof(ServerFile)));
        }
        private string _checkDateTime = "";


        /// <summary>
        /// ファイル更新日時
        /// </summary>
        public string UpdateDateTime {
            get => _updateDateTime;
            set => SetProperty(ref _updateDateTime, value, () => OnPropertyChanged(nameof(UpdateDateTime)));
        }
        private string _updateDateTime = "";
        #endregion

    }
}
