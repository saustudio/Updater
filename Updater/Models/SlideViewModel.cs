using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Updater.UtillsClasses;

namespace Updater.Models
{
    public class SlideViewModel : ViewModelBase
    {
        public BitmapImage Image { get; set; }

        public string Title { get; set; }

        public string News { get; set; }
    }
}
