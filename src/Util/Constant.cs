﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib.Util;


namespace MySynchFiles.Util {
    class Constant {
        /// <summary>
        /// アプリの設定関連情報
        /// </summary>
        public static readonly string SettingFile = Common.GetAppPath() + @"app.data";
    }
}
