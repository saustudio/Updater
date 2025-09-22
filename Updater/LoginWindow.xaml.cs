using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

using Logging;

using MessageEx;

using RR_DesignUI;

using Updater.Annotations;
using Updater.Controls;
using Updater.Localization;
using Updater.Models;
using Updater.Properties;
using Updater.UtillsClasses;
using Updater.Utils;

namespace Updater
{
    // Token: 0x0200002A RID: 42
    public partial class LoginWindow : Window, INotifyPropertyChanged
    {
        // Token: 0x17000027 RID: 39
        // (get) Token: 0x06000128 RID: 296 RVA: 0x00002C6F File Offset: 0x00000E6F
        // (set) Token: 0x06000129 RID: 297 RVA: 0x00002C77 File Offset: 0x00000E77
        public ICommand ExitCommand { get; set; }

        // Token: 0x17000028 RID: 40
        // (get) Token: 0x0600012A RID: 298 RVA: 0x00002C80 File Offset: 0x00000E80
        // (set) Token: 0x0600012B RID: 299 RVA: 0x00002C88 File Offset: 0x00000E88
        public ICommand TrayCommand { get; set; }

        // Token: 0x17000029 RID: 41
        // (get) Token: 0x0600012C RID: 300 RVA: 0x00002C91 File Offset: 0x00000E91
        // (set) Token: 0x0600012D RID: 301 RVA: 0x00002C99 File Offset: 0x00000E99
        public string Login
        {
            get
            {
                return this._login;
            }
            set
            {
                this._login = value;
                this.OnPropertyChanged("Login");
                this.CheckLogPass();
            }
        }

        // Token: 0x1700002A RID: 42
        // (get) Token: 0x0600012E RID: 302 RVA: 0x00002CB3 File Offset: 0x00000EB3
        // (set) Token: 0x0600012F RID: 303 RVA: 0x00002CBB File Offset: 0x00000EBB
        public string SignError
        {
            get
            {
                return this._signError;
            }
            set
            {
                this._signError = value;
                this.OnPropertyChanged("SignError");
            }
        }

        // Token: 0x1700002B RID: 43
        // (get) Token: 0x06000130 RID: 304 RVA: 0x00002CCF File Offset: 0x00000ECF
        // (set) Token: 0x06000131 RID: 305 RVA: 0x00002CD7 File Offset: 0x00000ED7
        public string Pass
        {
            get
            {
                return this._pass;
            }
            set
            {
                this._pass = value;
                this.OnPropertyChanged("Pass");
                this.CheckLogPass();
            }
        }

        // Token: 0x1700002C RID: 44
        // (get) Token: 0x06000132 RID: 306 RVA: 0x00002CF1 File Offset: 0x00000EF1
        public LocalizationsViewModel Localizations { get; } = new LocalizationsViewModel();

        // Token: 0x06000133 RID: 307 RVA: 0x0000798C File Offset: 0x00005B8C
        public void ThreadHwd()
        {
            throw new NotImplementedException();
            //if (this.device == null)
            //{
            //    DevicesHelper devicesHelper = new DevicesHelper();
            //    this.device = devicesHelper;
            //}
        }

        // Token: 0x06000134 RID: 308 RVA: 0x000079B0 File Offset: 0x00005BB0
        public LoginWindow()
        {
            base.Dispatcher.UnhandledException += delegate (object sender, DispatcherUnhandledExceptionEventArgs args)
            {
                MessageBox.Show(string.Concat(new string[]
                {
                    args.Exception.Message,
                    "\n",
                    args.Exception.Source,
                    "\n",
                    args.Exception.StackTrace
                }), "Updater Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            };
            if (this.InitRun())
            {
                new Thread(new ThreadStart(this.ThreadHwd)).Start();
                Animator.Start();
                base.DataContext = this;
                this.InitializeComponent();
                this.ExitCommand = new RelayCommand(delegate (object o)
                {
                    Settings.Default.Lang = (int)LangInfo.Lang;
                    Settings.Default.Save();
                    Application.Current.Shutdown();
                }, null);
                this.TrayCommand = new RelayCommand(delegate (object o)
                {
                    base.WindowState = WindowState.Minimized;
                }, null);
                string text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bin.dat";
                if (File.Exists(text))
                {
                    this.CheckedLogPass.IsChecked = new bool?(true);
                    this.Login = File.ReadAllText(text);
                    return;
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        // Token: 0x06000135 RID: 309 RVA: 0x00007B14 File Offset: 0x00005D14
        public bool IsProcessID(int ProcessId)
        {
            try
            {
                if (Process.GetProcessById(ProcessId) != null)
                {
                    return true;
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
            return false;
        }

        // Token: 0x06000136 RID: 310 RVA: 0x00007B48 File Offset: 0x00005D48
        private bool InitRun()
        {
            return true;
        }

        // Token: 0x06000137 RID: 311 RVA: 0x00002900 File Offset: 0x00000B00
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                base.DragMove();
            }
        }

        // Token: 0x06000138 RID: 312 RVA: 0x00002CF9 File Offset: 0x00000EF9
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            this.login.Focus();
            this.pass.Focus();
            this.SignIn.Focus();
            this.CheckLogPass();
        }

        // Token: 0x14000002 RID: 2
        // (add) Token: 0x06000139 RID: 313 RVA: 0x00007B58 File Offset: 0x00005D58
        // (remove) Token: 0x0600013A RID: 314 RVA: 0x00007B90 File Offset: 0x00005D90
        public event PropertyChangedEventHandler PropertyChanged;

        // Token: 0x0600013B RID: 315 RVA: 0x00002D25 File Offset: 0x00000F25
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged == null)
            {
                return;
            }
            propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // Token: 0x0600013C RID: 316 RVA: 0x00002D3E File Offset: 0x00000F3E
        private void CheckLogPass()
        {
            if (!string.IsNullOrWhiteSpace(this.Login) && !string.IsNullOrWhiteSpace(this.Pass))
            {
                this.SignIn.IsEnabled = true;
                return;
            }
            this.SignIn.IsEnabled = false;
        }

        // Token: 0x0600013D RID: 317 RVA: 0x00002D73 File Offset: 0x00000F73
        private void Pass_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            this.Pass = this.pass.Password;
        }

        // Token: 0x0600013E RID: 318 RVA: 0x00007BC8 File Offset: 0x00005DC8
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private bool ChAt(ref string _out)
        {
            bool flag = false;
            if (File.Exists(this._env))
            {
                flag = true;
                try
                {
                    byte[] array = File.ReadAllBytes(this._env);
                    if (array.Length != 0)
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            array[i] ^= 78;
                        }
                        _out = Encoding.UTF8.GetString(array);
                    }
                }
                catch
                {
                }
            }
            return flag;
        }

        // Token: 0x0600013F RID: 319 RVA: 0x00007C34 File Offset: 0x00005E34
        private void FileDeleteEx(string str)
        {
            if (File.Exists(str))
            {
                try
                {
                    File.Delete(str);
                }
                catch
                {
                }
            }
        }

        // Token: 0x06000140 RID: 320 RVA: 0x00007C64 File Offset: 0x00005E64
        private bool SessionResponse()
        {
            LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] CHECK RECEIVE SERVER", Array.Empty<object>());
            string text = Config.connect.ReceiveResponse();
            if (text.Length > 0)
            {
                throw new NotImplementedException();

                //    string[] array = text.Split(new char[] { '&' });
                //    LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] CHECK RECEIVE LENGTH SERVER", Array.Empty<object>());
                //    if (array.Length >= 3)
                //    {
                //        LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] CHECK RECEIVE DATA SERVER", Array.Empty<object>());
                //        if (array[0].IndexOf(Config.Base64DecodeEx("TVxWWElCWVxJXEJJ", 29)) != -1)
                //        {
                //            LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] CHECK RECEIVE MESSAGE SERVER", Array.Empty<object>());
                //            if (array[1] == Config.Base64DecodeEx("h4aGhw==", 182))
                //            {
                //                LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] CHECK MESSAGE[1] SERVER", Array.Empty<object>());
                //                Thread.Sleep(2300);
                //                string text2 = "";
                //                string text3 = "";
                //                if (this.ChAt(ref text2))
                //                {
                //                    text3 = "&" + Config.Base64DecodeEx("ho+NhYuc", 206) + "&" + text2;
                //                }
                //                string text4 = this.Login;
                //                if (Config.connect.SendString(string.Concat(new string[]
                //                {
                //                    Config.Base64DecodeEx("loeNg5KZgoeSh5mS4Pf29vTg", 198),
                //                    array[2],
                //                    "&",
                //                    text4,
                //                    "&",
                //                    ClientData.Sha1Hash(this.Pass),
                //                    "&",
                //                    this.device.HardwareId,
                //                    text3
                //                })))
                //                {
                //                    return this.SessionResponse();
                //                }
                //            }
                //            if (array[1] == Config.Base64DecodeEx("4OHh4w==", 209))
                //            {
                //                LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] CHECK MESSAGE[2] SERVER", Array.Empty<object>());
                //                this._balance = array[4];
                //                this._parametr = string.Format("\"" + Config.Base64DecodeEx("RCkyXCUpMlwmKVlAUWNZUEUpMkQkKW8kaTJEJSlFJS1HWXMpKTJEJilaUE1sWXMpKTJEJykyRCApbyVpMkQhKTJEVyUpQHMpKTJEVyYpQHMpKQ==", 20) + "\"", Config.Base64Encode(array[3]), Config.Base64Encode(array[2].Replace("|", "_")));
                //                this._parametr2 = string.Format(Config.Base64DecodeEx("5771uLn19fT39vzz9PL0ub70uOfl9PXz9vLz9vD09OX09fP28vD88fH05fT18/by8/by9vPl9PXz9vLz9vD09OX09fP28vP28v335fT18/by8P3z9vI=", 197), array[3], array[2].Replace("|", "_"));
                //                this.FileDeleteEx(this._env);
                //                return true;
                //            }
                //            if (array[1] == Config.Base64DecodeEx("MTExMQ==", 1))
                //            {
                //                MessageBoxEx.ShowWarning(array[2], Config.Base64DecodeEx("zvn55Pk=", 139), 10000U);
                //            }
                //        }
                //    }
            }
            LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] CHECK FAIL RECEIVE SERVER", Array.Empty<object>());
            return false;
        }

        // Token: 0x06000141 RID: 321 RVA: 0x00007F04 File Offset: 0x00006104
        private bool Authorization()
        {
            LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] START", Array.Empty<object>());
            this.SignIn.IsEnabled = false;

            throw new NotImplementedException();

            //if (this.device == null)
            //{
            //    //this.device = new DevicesHelper();
            //    LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] LOAD DEVICE", Array.Empty<object>());
            //}

            if (this.Login.Length >= 5 && this.Login.Length <= 80 && this.Pass.Length > 0 && this.Pass.Length <= 100)
            {
                if (this.Login.IndexOf("@") != -1 && this.Login.IndexOf(".") != -1)
                {
                    LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] CHECK LOGIN AND PASSWORD", Array.Empty<object>());
                    try
                    {
                        LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] CONNECT SERVER", Array.Empty<object>());
                        if (Config.connect.ConnectToServer())
                        {
                            LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] SEND DATA SERVER", Array.Empty<object>());
                            throw new NotImplementedException();
                            //if (Config.connect.SendString(Config.Base64DecodeEx("W0pATl9UT0pfSlRfLTo7OzotZX5nZw==", 11)))
                            //{
                            //    LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] START DATA SESSION SERVER", Array.Empty<object>());
                            //    if (this.SessionResponse())
                            //    {
                            //        LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] SUSSCED AUTH", Array.Empty<object>());
                            //        string text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + Config.Base64DecodeEx("v7Sz87m8qQ==", 221);
                            //        if (this.CheckedLogPass.IsChecked.GetValueOrDefault())
                            //        {
                            //            File.WriteAllText(text, this.Login);
                            //        }
                            //        else
                            //        {
                            //            this.FileDeleteEx(text);
                            //        }
                            //        LoginWindow._logInInfo = new LoginInfo
                            //        {
                            //            Login = this.Login,
                            //            Parametr = this._parametr,
                            //            Parametr2 = this._parametr2,
                            //            Balance = this._balance
                            //        };
                            //        Settings.Default.Lang = (int)LangInfo.Lang;
                            //        Settings.Default.Save();
                            //    }
                            //}
                        }
                        goto IL_252;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.ShowError(ex.Message, "Error", 10000U);
                        goto IL_252;
                    }
                }

                throw new NotImplementedException();
                //MessageBoxEx.ShowError(Config.Base64DecodeEx("JwAYDwIHCk4DDwcC", 110), "Error", 10000U);
            }
            else
            {
                throw new NotImplementedException();
                //MessageBoxEx.ShowError(Config.Base64DecodeEx("NxwVBhUXABEGVDgRGhMAHFQxBgYbBg==", 116), "Error", 10000U);
            }
IL_252:
            this.SignIn.IsEnabled = true;
            return true;
        }

        // Token: 0x06000142 RID: 322 RVA: 0x0000818C File Offset: 0x0000638C
        private void SignIn_OnClick(object sender, RoutedEventArgs e)
        {
            bool? isChecked = this.CheckedLogPass.IsChecked;
            if (isChecked != null && isChecked.GetValueOrDefault() && AccountHandler.GetAllAccounts().All((AccountBase x) => x.Login != this.Login))
            {
                AccountHandler.SaveAccount(new AccountBase
                {
                    Login = this.Login,
                    Pass = this.Pass
                }, true);
            }
            try
            {
                this.Authorization();
                if (LoginWindow._logInInfo != null)
                {
                    LoggUpdater.log.LogWrite(LoggUpdater.LogLevel.INFO_LOG, "[AUTHORIZATION] START FORM MAIN", Array.Empty<object>());
                    base.Dispatcher.BeginInvoke(new Action(delegate
                    {
                        MainWindow mainWindow = new MainWindow(LoginWindow._logInInfo);
                        Application.Current.MainWindow = mainWindow;
                        mainWindow.Show();
                        base.Close();
                    }), Array.Empty<object>());
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.ShowError(string.Concat(new string[] { ex.Message, "\n", ex.Source, "\n", ex.StackTrace }), "Error", 60000U);
            }
        }

        // Token: 0x06000143 RID: 323 RVA: 0x00002D86 File Offset: 0x00000F86
        private void ShowPass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ShowPassText.Visibility = Visibility.Visible;
            this.pass.Visibility = Visibility.Hidden;
        }

        // Token: 0x06000144 RID: 324 RVA: 0x00002DA0 File Offset: 0x00000FA0
        private void ShowPass_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ShowPassText.Visibility = Visibility.Hidden;
            this.pass.Visibility = Visibility.Visible;
        }

        // Token: 0x06000145 RID: 325 RVA: 0x00002DBA File Offset: 0x00000FBA
        private void Site_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("https://r2dispel.ru/");
        }

        // Token: 0x06000146 RID: 326 RVA: 0x00002DC7 File Offset: 0x00000FC7
        private void Forum_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("https://dispel-forum.ru/");
        }

        // Token: 0x06000147 RID: 327 RVA: 0x00002DD4 File Offset: 0x00000FD4
        private void Support_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("https://vk.com/im?sel=-168610367");
        }

        // Token: 0x06000148 RID: 328 RVA: 0x00002DBA File Offset: 0x00000FBA
        private void forgot_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("https://r2dispel.ru/");
        }

        // Token: 0x06000149 RID: 329 RVA: 0x00002DE1 File Offset: 0x00000FE1
        private void Discord_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("https://discord.gg/1");
        }

        // Token: 0x0600014A RID: 330 RVA: 0x00002DE1 File Offset: 0x00000FE1
        private void Instagram_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("https://discord.gg/1");
        }

        // Token: 0x0600014B RID: 331 RVA: 0x00002DE1 File Offset: 0x00000FE1
        private void Telegram_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("https://discord.gg/1");
        }

        // Token: 0x0600014C RID: 332 RVA: 0x00002DE1 File Offset: 0x00000FE1
        private void VK_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("https://discord.gg/1");
        }

        // Token: 0x0600014D RID: 333 RVA: 0x00008290 File Offset: 0x00006490
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            AccountChange accountChange = new AccountChange(AccountHandler.GetAllAccounts());
            accountChange.ShowDialog();
            if (accountChange.SelectedAccount == null)
            {
                return;
            }
            this.pass.Password = accountChange.SelectedAccount.Pass;
            this.Login = accountChange.SelectedAccount.Login;
        }

        // Token: 0x0400009A RID: 154
        //public DevicesHelper device;

        // Token: 0x0400009B RID: 155
        public static MutexHelper _mutexLauncher = new MutexHelper("z123F17gb1265");

        // Token: 0x0400009E RID: 158
        private string _parametr;

        // Token: 0x0400009F RID: 159
        private string _parametr2;

        // Token: 0x040000A0 RID: 160
        private string _login;

        // Token: 0x040000A1 RID: 161
        private string _balance;

        // Token: 0x040000A2 RID: 162
        private string _env = ""; 
        #warning not implemented
            //string.Concat(new string[]
        //{
        //    //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
        //    //"\\",
        //    //Config.Base64DecodeEx("+fLq", 156),
        //    //"\\",
        //    //Config.Base64DecodeEx("IRYmGUJDSjsZEkJdBhoS", 115)
        //});

        // Token: 0x040000A3 RID: 163
        public static LoginInfo _logInInfo = null;

        // Token: 0x040000A4 RID: 164
        private string _signError;

        // Token: 0x040000A5 RID: 165
        private string _pass = string.Empty;
    }
}
