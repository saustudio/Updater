using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Updater.Enums;
using Updater.UtillsClasses;

namespace Updater.Models
{
    public class NewsViewModel : ViewModelBase
    {
        #region Props

        private bool _allChecked = true;

        public bool AllChecked
        {
            get { return _allChecked; }
            set
            {
                _allChecked = value;
                OnPropertyChanged(nameof(AllChecked));
                OnPropertyChanged(nameof(ShowingNews));
            }
        }

        private bool _newsChecked;

        public bool NewsChecked
        {
            get { return _newsChecked; }
            set
            {
                _newsChecked = value;
                OnPropertyChanged(nameof(NewsChecked));
                OnPropertyChanged(nameof(ShowingNews));
            }
        }

        private bool _notifyChecked;

        public bool NotifyChecked
        {
            get { return _notifyChecked; }
            set
            {
                _notifyChecked = value;
                OnPropertyChanged(nameof(NotifyChecked));
                OnPropertyChanged(nameof(ShowingNews));
            }
        }

        private bool _eventsChecked;

        public bool EventsChecked
        {
            get { return _eventsChecked; }
            set
            {
                _eventsChecked = value;
                OnPropertyChanged(nameof(EventsChecked));
                OnPropertyChanged(nameof(ShowingNews));
            }
        }

        #endregion

        public IEnumerable<NewsItemViewModel> ShowingNews => NewsItems.Where(Predicate).Take(5);

        private bool Predicate(NewsItemViewModel item)
        {
            if (AllChecked ||
                NewsChecked && item.Type == NewsTypes.News ||
                NotifyChecked && item.Type == NewsTypes.Notifications ||
                EventsChecked && item.Type == NewsTypes.Events)
            {
                return true;
            }

            return false;
        }

        private ObservableCollection<NewsItemViewModel> _newsItems;

        public ObservableCollection<NewsItemViewModel> NewsItems
        {
            get { return _newsItems; }
            set
            {
                _newsItems = value;
                OnPropertyChanged(nameof(NewsItems));
            }
        }

        public void Initialize(List<NewsItemViewModel> newsItems)
        {
            NewsItems = new ObservableCollection<NewsItemViewModel>(newsItems);
        }
    }
}
