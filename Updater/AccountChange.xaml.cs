using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Updater.Models;
using Updater.UtillsClasses;
using Updater.Utils;

namespace Updater
{
    public partial class AccountChange : Window
    {
        public ObservableCollection<AccountBase> SavedAccounts { get; set; }
        public AccountBase SelectedAccount { get; set; } = null;

        public AccountChange(List<AccountBase> savedAccounts)
        {
            SavedAccounts = new ObservableCollection<AccountBase>(savedAccounts);
            DataContext = this;
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            SelectedAccount = null;
            Close();
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public ICommand Delete => new RelayCommand(x =>
        {
            var deleteResult = AccountHandler.DeleteAccount((AccountBase) x);
            if (deleteResult)
            {
                SavedAccounts.Remove((AccountBase) x);
            }
        });

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}