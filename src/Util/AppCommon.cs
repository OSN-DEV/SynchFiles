using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySynchFiles.Util {
    class AppCommon {
        /// <summary>
        /// get app name
        /// </summary>
        /// <returns></returns>
        public static string GetAppName() {
            var fullname = typeof(App).Assembly.Location;
            var info = System.Diagnostics.FileVersionInfo.GetVersionInfo(fullname);
            var ver = info.FileVersion;
            return "MySynchFiles(" + ver + ")";
        }
    }
}
