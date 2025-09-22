using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Updater.Enums;
using Updater.Localization;
using Updater.Models;
using Updater.UtillsClasses;

namespace Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string Parametr = "";
        private string Parametr2 = "";
        public LoginInfo LoginInfo { get; }
        public MainWindow(LoginInfo loginInfo)
        {
            LoginInfo = loginInfo;
            InitializeComponent();
            CreateCommands();
            DataContext = this;
            Info = _startText;
            LoginLabel.Text = loginInfo.Login;
            //RecommendedNick.Text = loginInfo.Login;
            CoinLabel.Text = loginInfo.Balance + " Coin";
            Parametr = loginInfo.Parametr;
            Parametr2 = loginInfo.Parametr2;
            Localizations.LanguageChanged += LocalizationsOnLanguageChanged;
            SetLocalization();

            this.webclient.DownloadFileCompleted += this.ProgressDownloadFileCompleted;
            this.webclient.DownloadProgressChanged += this.ProgressDownloadFileChanged;
        }

        public MainWindow()
        {
            InitializeComponent();
            CreateCommands();
            DataContext = this;
            Info = _startText;
            Localizations.LanguageChanged += LocalizationsOnLanguageChanged;
            SetLocalization();
        }

        public ICommand Event => new RelayCommand(obj =>
            Process.Start(
                "https://dispel-forum.ru/threads/event-%D0%A1%D1%83%D0%BD%D0%B4%D1%83%D0%BA%D0%B8-%D0%B2%D0%B5%D1%82%D0%B5%D1%80%D0%B0%D0%BD%D0%B0-dispel.41/"));

        public ICommand Rating => new RelayCommand(obj => Process.Start("https://r2dispel.ru/stats"));
        public ICommand BaseKnowlage => new RelayCommand(obj => Process.Start("https://r2dispel.ru/database/"));
        public ICommand NodesDeveloper => new RelayCommand(obj => Process.Start("https://discord.gg/DphBrJSu6w"));
        public ICommand GoldCase => new RelayCommand(obj => Process.Start("https://discord.gg/DphBrJSu6w"));
        public ICommand Command1 => new RelayCommand(obj => Process.Start("https://r2dispel.ru"));
        public ICommand Command2 => new RelayCommand(obj => Process.Start("https://r2dispel.ru"));
        public ICommand Command3 => new RelayCommand(obj => Process.Start("https://r2dispel.ru"));
        public ICommand Command4 => new RelayCommand(obj => Process.Start("https://r2dispel.ru"));
        public ICommand Command5 => new RelayCommand(obj => Process.Start("https://r2dispel.ru"));
        public ButtonsModel GoldCaseButton => new ButtonsModel
        {
            ButtonText = "Подробнее",
            Description = "Перейдите на сайт для подробностей",
            Title = "Золотой сундук"
        };
        public ButtonsModel RatingButton => new ButtonsModel
        {
            ButtonText = "Подробнее",
            Description = "Перейдите на сайт для подробностей",
            Title = "Рейтинг"
        };
        public ButtonsModel BaseKnowlageButton => new ButtonsModel
        {
            ButtonText = "Подробнее",
            Description = "Перейдите на сайт для подробностей",
            Title = "База знаний"
        };
        public ButtonsModel DeveloperNodesButton => new ButtonsModel
        {
            ButtonText = "Подробнее",
            Description = "Перейдите на сайт для подробностей",
            Title = "Дневник разработчика"
        };
        public ButtonsModel EventButton => new ButtonsModel
        {
            ButtonText = "Подробнее",
            Description = "Перейдите на сайт для подробностей",
            Title = "Events"
        };


        #region Props

        private LocString _info;
        private LocString _downloadInfo;
        private double _progressFull = 100;
        private double _progressFile = 100;
        private double _maxProgressFile = 100;
        private double _maxProgressFull = 100;
        private string _downloadSpeed;
        private string _downloadBytes;

        public LocString Info
        {
            get { return _info; }
            set
            {
                _info = value;
                Debug.WriteLine(_info?.GetLocStr);
                OnPropertyChanged(nameof(Info));
            }
        }

        public string DownloadSpeed
        {
            get { return _downloadSpeed; }
            set
            {
                _downloadSpeed = value;
                OnPropertyChanged(nameof(DownloadSpeed));
            }
        }


        public string DownloadBytes
        {
            get { return _downloadBytes; }
            set
            {
                _downloadBytes = value;
                OnPropertyChanged(nameof(DownloadBytes));
            }
        }

        public LocString DownloadInfo
        {
            get { return IsBusy ? _downloadInfo : null; }
            set
            {
                _downloadInfo = value;
                Debug.WriteLine(_downloadInfo?.GetLocStr);
                OnPropertyChanged(nameof(DownloadInfo));
            }
        }


        public double ProgressFull
        {
            get { return _progressFull; }
            set
            {
                _progressFull = value;
                OnPropertyChanged(nameof(ProgressFull));
                OnPropertyChanged(nameof(FullProc));
            }
        }

        public double ProgressFile
        {
            get { return _progressFile; }
            set
            {
                _progressFile = value;
                OnPropertyChanged(nameof(ProgressFile));
            }
        }

        public string FullProc => !IsBusy ? string.Empty : $"{(int)((ProgressFull / MaxProgressFull) * 100)}%";

        public double MaxProgressFile
        {
            get { return _maxProgressFile; }
            set
            {
                _maxProgressFile = value;
                OnPropertyChanged(nameof(MaxProgressFile));
            }
        }

        public double MaxProgressFull
        {
            get { return _maxProgressFull; }
            set
            {
                _maxProgressFull = value;
                OnPropertyChanged(nameof(MaxProgressFull));
            }
        }

        #endregion

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //StartUpdateTask(UpdateTypes.Quick);
        }

        //Link
        private void DonateButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2dispel.ru/");
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Site_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2dispel.ru/");
        }

        private void Forum_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2Dispel.ru/");
        }

        private void Support_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2Dispel.ru/");
        }

        private void RecommendedButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2dispel.ru/");
        }

        private void Banner1_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2dispel.ru/");
        }

        private void Discord_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2dispel.ru/");
        }

        private void VK_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2dispel.ru/");
        }

        private void Telegram_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2dispel.ru/");
        }

        private void Insta_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2dispel.ru/");
        }
    }
}