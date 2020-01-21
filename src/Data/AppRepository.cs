using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib.Data;
using MySynchFiles.Util;

namespace MySynchFiles.Data {
    class AppRepository : AppDataBase<AppRepository> {

        #region Declaration   
        public class Location {
            public double X { set; get; } = -1;
            public double Y { set; get; } = -1;
        }
        public Location Pos { set; get; } = new Location();
        public int Page { set; get; } = 0;
        #endregion

        #region Public Method
        /// <summary>
        /// get instantce
        /// </summary>
        /// <returns></returns>
        public static AppRepository GetInstance() {
            GetInstanceBase(Constant.SettingFile);
            if (!System.IO.File.Exists(Constant.SettingFile)) {
                _instance.Save();
            }
            return _instance;
        }

        /// <summary>
        /// save settings
        /// </summary>
        public void Save() {
            GetInstanceBase().SaveToXml(Constant.SettingFile);
        }
        #endregion

    }
}
