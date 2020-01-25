using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySynchFiles.Data {
    public class SyncFileDataModel {

        #region Public Property
        /// <summary>
        /// 表示名称
        /// </summary>
        public string DisplayName { set; get; } = "";

        /// <summary>
        /// ローカルファイル
        /// </summary>
        public string LocalFile { set; get; } = "";

        /// <summary>
        /// サーバーファイル
        /// </summary>
        public string ServerFile { set; get; } = "";

        /// <summary>
        /// 同期実行日時
        /// </summary>
        public string CheckDateTime { set; get; } = "";

        /// <summary>
        /// ファイル更新日時
        /// </summary>
        public string UpdateDateTime { set; get; } = "";
        #endregion

    }
}
