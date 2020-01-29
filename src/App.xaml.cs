using MySynchFiles.Util;
using System;
using System.Threading;
using System.Windows;

namespace MySynchFiles {
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application {
        #region Declaration
        private Mutex _mutex = new Mutex(false, "MySynchFiles.Mutex");
        private SynchMain _mainWindow = null;
        #endregion

        #region Event
        private void Application_Startup(object sender, StartupEventArgs e) {
            if (!this._mutex.WaitOne(0, false)) {
                IntPtr hMWnd = NativeMethods.FindWindow(null, AppCommon.GetAppName());
                if (hMWnd != null && NativeMethods.IsWindow(hMWnd)) {
                    var hCWnd = NativeMethods.GetLastActivePopup(hMWnd);
                    if (hCWnd != null && NativeMethods.IsWindow(hCWnd) && NativeMethods.IsWindowVisible(hCWnd)) {
                        NativeMethods.ShowWindow(hCWnd, (int)NativeMethods.SW.SHOWNORMAL);
                        NativeMethods.SetForegroundWindow(hCWnd);
                    }
                }

                this._mutex.Close();
                this._mutex = null;
                Shutdown();
            } else {
                this._mainWindow = new SynchMain();
                this._mainWindow.Show();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e) {
            if (this._mutex != null) {
                this._mutex.ReleaseMutex();
                this._mutex.Close();
                this._mutex = null;
            }
        }
        #endregion
    }
}
