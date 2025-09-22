using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using JSONProject;

using MessageEx;

using Updater.Annotations;
using Updater.Enums;
using Updater.Localization;
using Updater.Properties;
using Updater.UtillsClasses;

using Languages = Updater.Localization.Languages;

namespace Updater
{
    public partial class MainWindow
    {

        public class FileDownloadInfo
        {
            public string path;
            public int size;

            public FileDownloadInfo(string _p, int _s)
            {
                this.path = _p;
                this.size = _s;
            }
        }


        private Stopwatch stopwatch = new Stopwatch();
        private WebClient webclient = new WebClient();
        private bool dwload = false;
        private long totalSize = 0L;
        private double downloadSize = 0.0;
        private double lastDownload = 0.0;
        private double downloadSpeed = 0.0;
        private int precentProgress = 0;
        private List<FileDownloadInfo> loadFiles = new List<FileDownloadInfo>();
        private JSONObject hashes = new JSONObject("{}", false);
        private int indexLoad = 0;

        private const int VERSION = 1;

        public string SavePath { get; set; } = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Config.newpath[Config.type];

        private CancellationTokenSource _cts = new CancellationTokenSource();

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
                OnPropertyChanged(nameof(FullProc));
            }
        }

        #region Commands

        public ICommand ExitCommand { get; set; }

        public ICommand TrayCommand { get; set; }

        public ICommand OpenFromTrayCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ICommand QuickCommand { get; set; }

        public ICommand FullCommand { get; set; }

        public ICommand StartCommand { get; set; }

        public ICommand SettingsCommand { get; set; }

        #endregion

        //Библиотека для иконки в тулбаре, чтобы был один ехе я запаковал библиотеку в сборку, и при старте ее достаю
   

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateCommands()
        {
            ExitCommand = new RelayCommand(o =>
            {
                Settings.Default.Lang = (int)LangInfo.Lang;
                Settings.Default.Save();
                Application.Current.Shutdown();
            });

            TrayCommand = new RelayCommand(o =>
            {
                ShowStandardBalloon((ProgressFull * 100) / MaxProgressFull);
                WindowState = WindowState.Minimized;
            });
            OpenFromTrayCommand = new RelayCommand(o => { WindowState = WindowState.Normal; });
            CancelCommand = new RelayCommand(o => _cts?.Cancel(), can => IsBusy);
            QuickCommand = new RelayCommand(o => StartUpdateTask(UpdateTypes.Quick), can => !IsBusy);
            FullCommand = new RelayCommand(o => StartUpdateTask(UpdateTypes.Full), can => !IsBusy);
            StartCommand = new RelayCommand(o => StartUpdateTask(UpdateTypes.Quick)/*StartGame()*/, can => !IsBusy);
            SettingsCommand = new RelayCommand(o => OpenSettings());
        }

        private void OpenSettings()
        {
            /*var settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();*/

            throw new NotImplementedException();
            //try
            //{
            //    ProcessStartInfo _processStartInfo = new ProcessStartInfo();
            //    _processStartInfo.WorkingDirectory = SavePath;
            //    _processStartInfo.FileName = SavePath + "\\R2Option.exe";
            //    Process myProcess = Process.Start(_processStartInfo);
            //}
            //catch (Exception ex) { }
        }


        private int StartGame()
        {
            throw new NotImplementedException();

            //if (File.Exists(SavePath + "\\R2Client.exe"))
            //{
            //    this.WindowState = WindowState.Minimized;

            //    string prm = this.Parametr;

            //    try
            //    {
            //        if (File.Exists(SavePath + "\\R2.cfg"))
            //        {
            //            bool _del = true;
            //            string[] l = File.ReadAllLines(SavePath + "\\R2.cfg");
            //            foreach (var i in l)
            //            {
            //                if (i.IndexOf("channelserverip = 94.130.219.211") != -1 || i.IndexOf("channelserverip= 94.130.219.211") != -1)
            //                {
            //                    _del = false;
            //                    break;
            //                }
            //            }

            //            if (_del)
            //            {
            //                File.Delete(SavePath + "\\R2.cfg");
            //            }
            //        }
            //    }
            //    catch { }

            //    try
            //    {
            //        if (File.Exists(SavePath + "\\R2.cfg"))
            //        {
            //            string[] l = File.ReadAllLines(SavePath + "\\R2.cfg");
            //            for (int i = 0; i < l.Length; i++)
            //            {
            //                if (l[i].IndexOf("channelserverport") != -1)
            //                {
            //                    if (Config.serverType == 0)
            //                    {
            //                        l[i] = "channelserverport = 11025";
            //                    }
            //                    else if (Config.serverType == 1)
            //                    {
            //                        l[i] = "channelserverport = 9000";
            //                    }
            //                }
            //            }
            //            File.WriteAllLines(SavePath + "\\R2.cfg", l);
            //        }

            //        if (File.Exists(SavePath + "\\R2SafeMode.cfg"))
            //        {
            //            string[] l = File.ReadAllLines(SavePath + "\\R2SafeMode.cfg");
            //            for (int i = 0; i < l.Length; i++)
            //            {
            //                if (l[i].IndexOf("channelserverport") != -1)
            //                {
            //                    if (Config.serverType == 0)
            //                    {
            //                        l[i] = "channelserverport = 11025";
            //                    }
            //                    else if (Config.serverType == 1)
            //                    {
            //                        l[i] = "channelserverport = 9000";
            //                    }
            //                }
            //            }
            //            File.WriteAllLines(SavePath + "\\R2SafeMode.cfg", l);
            //        }
            //    }
            //    catch { }

            //    try
            //    {
            //        if (File.Exists(SavePath + "\\R2.ver"))
            //        {
            //            using (FileStream myStream = File.OpenRead(SavePath + "\\R2.ver"))
            //            {
            //                using (BinaryReader read = new BinaryReader(myStream))
            //                {
            //                    int v1 = read.ReadInt32();
            //                    int v2 = read.ReadInt32();
            //                    int v3 = read.ReadInt32();
            //                    if (v1 == 1 && v2 == 399 && v3 == 38)
            //                    {
            //                        prm = this.Parametr2;

            //                        try
            //                        {
            //                            if (File.Exists(SavePath + "\\R2.cfg"))
            //                            {
            //                                string[] l = File.ReadAllLines(SavePath + "\\R2.cfg");
            //                                foreach (var i in l)
            //                                {
            //                                    if (i.IndexOf("nation = 1") != -1 || i.IndexOf("nation=1") != -1 ||
            //                                        i.IndexOf("nation = 2") != -1 || i.IndexOf("nation=2") != -1 ||
            //                                        i.IndexOf("nation = 3") != -1 || i.IndexOf("nation=3") != -1 ||
            //                                        i.IndexOf("nation = 4") != -1 || i.IndexOf("nation=4") != -1 ||
            //                                         i.IndexOf("nation = 5") != -1 || i.IndexOf("nation=5") != -1)
            //                                    {
            //                                        File.Delete(SavePath + "\\R2.cfg");
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                        catch { }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch { }


            //    if (Config.type > 0)
            //    {
            //        try
            //        {

            //            INI.INIParser ini = new INI.INIParser(SavePath + "\\setting.cfg");

            //            if (Updater.Localization.Languages.Rus == LangInfo.Lang || Updater.Localization.Languages.Eng == LangInfo.Lang)
            //            {
            //                ini.Write("setting", "lng", "0");
            //            }

            //            if (Updater.Localization.Languages.Chi == LangInfo.Lang)
            //            {
            //                ini.Write("setting", "lng", "1");
            //            }

            //            if (Updater.Localization.Languages.Kor == LangInfo.Lang)
            //            {
            //                ini.Write("setting", "lng", "2");
            //            }
    
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "Launcher", MessageBoxButton.OK, MessageBoxImage.Error);
            //        }
            //    }

            //    try
            //    {
            //        throw new NotImplementedException();

            //        //ProcessStartInfo _processStartInfo = new ProcessStartInfo();
            //        //_processStartInfo.WorkingDirectory = SavePath;
            //        //_processStartInfo.FileName = SavePath + "\\R2Client.exe";
            //        //_processStartInfo.Arguments = prm;
            //        //// _processStartInfo.CreateNoWindow = true;
            //        //Process myProcess = Process.Start(_processStartInfo);

            //        //if (myProcess != null)
            //        //{
            //        //    //process.WaitForExit();
            //        //}
            //        //else
            //        //{
            //        //    MessageBox.Show("Error open R2Client.exe", "Launcher", MessageBoxButton.OK, MessageBoxImage.Error);
            //        //}

            //        ///*Process process = System.Diagnostics.Process.Start(SavePath + "\\R2Client.exe", prm);

            //        //if (process != null)
            //        //{
            //        //    //process.WaitForExit();
            //        //}
            //        //*/
            //        //Process.GetCurrentProcess().Kill();

            //        return 0;
            //    }
            //    catch(Exception ex) {
            //        MessageBox.Show(ex.Message, "Launcher", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }


             
            //}
            //else
            //{
            //    MessageBoxEx.ShowError("Missing file R2Client.exe\n" + SavePath + "\\R2Client.exe", "Error", 10000);
              
            //}


            //Application.Current.Shutdown();

            return 1;
        }

        private string GetCurrentGamePath()
        {
            switch (LangInfo.Lang)
            {
                case Languages.Rus:
                    return Path.Combine(SavePath, "Ru");
                case Languages.Eng:
                    return Path.Combine(SavePath, "Eng");
                default:
                    return Path.Combine(SavePath, "default");
            }   
        }

        private void getFileClient()
        {
            try
            {
                string js = webclient.DownloadString(Config.weburl[Config.type] + "/client.json" + Config._urlNocacheUrl());
                if (js.Length > 0 && js[0] == '{') { this.hashes = new JSONObject(js, false); }

            }
            catch (Exception ex)
            {
                MessageBoxEx.ShowError(ex.Message, "Error", 10000);
            }
        }

        private string getStringDownloadSize(double size)
        {
            string result = "Byte";
            if (size > 1024.0)
            {
                size /= 1024.0;
                result = "KB";
            }
            if (size > 1024.0)
            {
                size /= 1024.0;
                result = "MB";
            }
            /*if (size > 1024.0)
            {
                size /= 1024.0;
                result = "GB";
            }*/
            return result;
        }

        private double getDoubleDownloadSize(double size)
        {
            double num = size;
            if (num > 1024.0)
            {
                num /= 1024.0;
            }
            if (num > 1024.0)
            {
                num /= 1024.0;
            }
            /*if (num > 1024.0)
            {
                num /= 1024.0;
            }*/
            return Convert.ToDouble(num.ToString("0.00"));
        }


        private void UpdateDownloadSize()
        {
            this.DownloadBytes = string.Format("{0} {1} / {2} {3}", getDoubleDownloadSize(this.downloadSize), getStringDownloadSize(this.downloadSize), getDoubleDownloadSize(this.totalSize), getStringDownloadSize(this.totalSize));
        }

        private void UpdateDownloadSpeed()
        {
            //this.DownloadSpeed = string.Format("{0} /s",getDoubleDownloadSize(this.downloadSpeed));   
        }

        public void DownloadFile(string urlAddress, string location)
        {
            Uri address = urlAddress.StartsWith(Config.weburl[Config.type], StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri(Config.weburl[Config.type] + urlAddress);
            this.stopwatch.Start();
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(location)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(location));
                }
                this.lastDownload = 0.0;
                this.webclient.DownloadFileAsync(address, location);
            }
            catch (Exception ex)
            {
                MessageBoxEx.ShowError(ex.Message, "Error", 10000);
            }
        }


        private void ProgressDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.stopwatch.Reset();
            if (e.Cancelled)
            {
                MessageBoxEx.ShowError("Error Download", "Error", 10000);
            }
            else
            {
                try
                {
                    Directory.SetLastWriteTimeUtc(
                                         Directory.GetCurrentDirectory() + Config.newpath[Config.type] + this.loadFiles[this.indexLoad].path,
                                         DateTime.ParseExact(this.hashes[this.loadFiles[this.indexLoad].path.Replace(Directory.GetCurrentDirectory() + Config.newpath[Config.type], "")]["time"].str, "dd.MM.yyyy H:mm:ss",
                                         CultureInfo.InvariantCulture));
                }
                catch { }



                //this.ProgressFull++;
                this.indexLoad++;
                if (this.indexLoad < this.loadFiles.Count)
                {
                    this.DownloadFile(this.loadFiles[this.indexLoad].path + Config._urlNocacheUrl(), Directory.GetCurrentDirectory() + Config.newpath[Config.type] + this.loadFiles[this.indexLoad].path);
                }
                else
                {
                    dwload = false;
                }

            }
        }

        private void ProgressDownloadFileChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            

            this.downloadSize += (double)e.BytesReceived - this.lastDownload;
            this.lastDownload = (double)e.BytesReceived;
            this.downloadSpeed = (double)e.BytesReceived / this.stopwatch.Elapsed.TotalSeconds;
            this.precentProgress = e.ProgressPercentage;

            this.ProgressFull = this.downloadSize;

            UpdateDownloadSize();
            UpdateDownloadSpeed();
        }

        //private void UpdateChekMD5(string file, int start, int end)
        //{
        //    string key = string.Format("{0} {1}/{2}", file, start, end);
        //    this.Info = new LocString(key, key, key, key);
        //}

        private void ThreadPlay()
        {
            if (!Config.dev)
            {
                this.loadFiles.Clear();
                this.indexLoad = 0;
                this.downloadSize = 0.0;
                this.getFileClient();

                this.ProgressFull = 0;
                this.MaxProgressFull = (double)this.hashes.keys.Count;


                this.Info = _checkFiles;

                foreach (string key in this.hashes.keys)
                {
#warning MD5 rmove
                    throw new NotImplementedException();
                    //if (File.Exists(Directory.GetCurrentDirectory() + Config.newpath[Config.type] + key))
                    //{
                    //    FileInfo fileInfo = new FileInfo(Directory.GetCurrentDirectory() + Config.newpath[Config.type] + key);
                    //    if (this.hashes[key].HasField("important"))
                    //    {
                    //        if (HelperFile.FileMD5(Directory.GetCurrentDirectory() + Config.newpath[Config.type] + key) != this.hashes[key]["md5"].str)
                    //        {
                    //            this.loadFiles.Add(new FileDownloadInfo(key, Convert.ToInt32(this.hashes[key]["size"].str)));
                    //            this.totalSize += Convert.ToInt32(this.hashes[key]["size"].str);
                    //        }
                    //    }
                    //    else if (fileInfo.LastWriteTimeUtc.ToString("dd.MM.yyyy H:mm:ss") != this.hashes[key]["time"].str || fileInfo.Length.ToString() != this.hashes[key]["size"].str)
                    //    {
                    //        if (HelperFile.FileMD5(Directory.GetCurrentDirectory() + Config.newpath[Config.type] + key) != this.hashes[key]["md5"].str)
                    //        {
                    //            this.loadFiles.Add(new FileDownloadInfo(key, Convert.ToInt32(this.hashes[key]["size"].str)));
                    //            this.totalSize += Convert.ToInt32(this.hashes[key]["size"].str);
                    //        }
                    //        else
                    //        {
                    //            Directory.SetLastWriteTimeUtc(
                    //                 Directory.GetCurrentDirectory() + Config.newpath[Config.type] + key,
                    //                 DateTime.ParseExact(this.hashes[key]["time"].str, "dd.MM.yyyy H:mm:ss",
                    //                 CultureInfo.InvariantCulture));
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    this.loadFiles.Add(new FileDownloadInfo(key, Convert.ToInt32(this.hashes[key]["size"].str)));
                    //    this.totalSize += Convert.ToInt32(this.hashes[key]["size"].str);
                    //}
                    this.ProgressFull++;
                }



                if (this.loadFiles.Count > 0)
                {
                    this.dwload = true;
                    this.Info = _initText;
                    this.ProgressFull = 0;
                    this.MaxProgressFull = /*(double)this.loadFiles.Count;*/this.totalSize;
                    this.DownloadFile(this.loadFiles[this.indexLoad].path + Config._urlNocacheUrl(), Directory.GetCurrentDirectory() + Config.newpath[Config.type] + this.loadFiles[this.indexLoad].path);

                    while (dwload)
                    {
                        TaskUtills.Delay(1000).Wait();
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        private void ThreadUpdater()
        {

            if (!Config.dev)
            {
                this.loadFiles.Clear();
                this.indexLoad = 0;
                this.downloadSize = 0.0;
                this.getFileClient();

                this.ProgressFull = 0;
                this.MaxProgressFull = (double)this.hashes.keys.Count;

                this.Info = _checkFiles;

                foreach (string key in this.hashes.keys)
                {
#warning MD5 rmove
                    throw new NotImplementedException();

                    //if (File.Exists(Directory.GetCurrentDirectory() + Config.newpath[Config.type] + key))
                    //{
                    //    if (HelperFile.FileMD5(Directory.GetCurrentDirectory() + Config.newpath[Config.type] + key) != this.hashes[key]["md5"].str)
                    //    {
                    //        this.loadFiles.Add(new FileDownloadInfo(key, Convert.ToInt32(this.hashes[key]["size"].str)));
                    //        this.totalSize += Convert.ToInt32(this.hashes[key]["size"].str);
                    //    }
                    //}
                    //else
                    //{
                    //    this.loadFiles.Add(new FileDownloadInfo(key, Convert.ToInt32(this.hashes[key]["size"].str)));
                    //    this.totalSize += Convert.ToInt32(this.hashes[key]["size"].str);
                    //}
                    this.ProgressFull++;
                }


                if (this.loadFiles.Count > 0)
                {
                    this.dwload = true;
                    this.Info = _initText;
                    this.ProgressFull = 0;
                    this.MaxProgressFull = /*(double)this.loadFiles.Count;*/this.totalSize;
                    this.DownloadFile(this.loadFiles[this.indexLoad].path + Config._urlNocacheUrl(), Directory.GetCurrentDirectory() + Config.newpath[Config.type] + this.loadFiles[this.indexLoad].path);

                    while(dwload)
                    {
                        TaskUtills.Delay(1000).Wait();
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

        }

        private void StartUpdateTask(UpdateTypes type)
        {
            IsBusy = true;

            //Токен для отмены
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            //MessageBox.Show($"Starting update {type}");

            /*if(type == UpdateTypes.Full)
            {
                ThreadUpdater();
            }

            if (type == UpdateTypes.Quick)
            {
                ThreadPlay();
            }*/


            Task.Factory.StartNew(() =>
            {
                if (type == UpdateTypes.Full)
                {
                    ThreadUpdater();
                }

                if (type == UpdateTypes.Quick)
                {
                    ThreadPlay();
                }
            }, token).ContinueWith(UpdateEnd, TaskScheduler.FromCurrentSynchronizationContext());

            /*Task.Factory.StartNew(() =>
            {
                ThreadPlay();
                ProgressFull = 0;

           
                MaxProgressFull = 3;
                TaskUtills.Delay(1000).Wait();

                ProgressFull = 1;
                TaskUtills.Delay(1000).Wait();

           

                ProgressFull = 2;
                TaskUtills.Delay(1000).Wait();

               

                ProgressFull = 3;

            }, token).ContinueWith(UpdateEnd, TaskScheduler.FromCurrentSynchronizationContext()); ;

        */
        }

        private void UpdateEnd(Task obj)
        {
            while(StartGame() == 1);
            IsBusy = false;
        }
    }
}
