using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Updater.Enums;
using Updater.UtillsClasses;

namespace Updater.Models
{
    public class NewsItemViewModel : ViewModelBase
    {
        public NewsTypes Type { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }
    }
}
