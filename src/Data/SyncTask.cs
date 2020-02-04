using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyLib.File;

namespace MySynchFiles.Data {
    class SyncTask {
        public delegate void SychComplete();
        private SychComplete _callback = null;

        /// <summary>
        /// start 
        /// </summary>
        /// <param name="model"></param>
        public void Start(SychComplete callback, SyncFileDataModel model) {
            var models = new ObservableCollection<SyncFileDataModel>();
            models.Add(model);
            this.Start(callback, models);
        }

        /// <summary>
        /// start 
        /// </summary>
        /// <param name="model"></param>
        public void Start(SychComplete callback, ObservableCollection<SyncFileDataModel> models) {
            this._callback = callback;
            Task.Run(() => {
                foreach(var model in models) {
                    this.Sync(model);
                }
                //Application.Current.Dispatcher.Invoke(() => {
                //    model.DisplayName = file.NameWithoutExtension;
                //    model.LocalFile = file.FilePath;
                //    if (ShowEditSynchFile(model)) {
                //        this._appData.SyncFiles.Add(model);
                //        this._appData.Save();
                //    }
                //});
                this._callback.Invoke();
            });
        }

        /// <summary>
        ///  sync file
        /// </summary>
        /// <param name="model"></param>
        private void Sync(SyncFileDataModel model) {
            model.UpdateDateTime = "";
            model.CheckDateTime = DateTime.Now.ToString("HH:mm:ss");
            var src = new FileOperator(model.LocalFile);
            var dest = new FileOperator(model.ServerFile);
            if ((null == src || !src.Exists()) && (null == dest || !dest.Exists())) {
                model.UpdateDateTime = "x";
                return;
            }

            if (null == src || !src.Exists()) {
                dest.Copy(src);
                model.UpdateDateTime = dest.LastWriteTimeString + "↓";
                return;
            }
            if (null == dest || !dest.Exists()) {
                src.Copy(dest);
                model.UpdateDateTime = src.LastWriteTimeString + "↑";
                return;
            }

            switch (src.LastWriteTimeString.CompareTo(dest.LastWriteTimeString)) {
                case -1:
                    dest.Copy(src);
                    model.UpdateDateTime = dest.LastWriteTimeString + "↓";
                    break;
                case 0:
                    // NOP
                    break;
                case 1:
                    src.Copy(dest);
                    model.UpdateDateTime = src.LastWriteTimeString + "↑";
                    break;
            }

        }
    }
}
