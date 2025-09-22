using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Updater.UtillsClasses;

namespace Updater.Models
{
    public class ShopViewModel : ViewModelBase
    {
        private ObservableCollection<ShopItemViewModel> _shopItems;

        public ObservableCollection<ShopItemViewModel> ShopItems
        {
            get { return _shopItems; }
            set
            {
                _shopItems = value;
                OnPropertyChanged(nameof(ShopItems));
            }
        }

        public void Initialize(List<ShopItemViewModel> shopItems)
        {
            ShopItems = new ObservableCollection<ShopItemViewModel>(shopItems);
        }
    }
}
