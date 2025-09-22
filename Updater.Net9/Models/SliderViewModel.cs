using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Timers;
using Updater.UtillsClasses;

namespace Updater.Models
{
    public class SliderViewModel : ViewModelBase
    {
        private System.Timers.Timer _slideTimer;

        private ObservableCollection<SlideViewModel> _slides;

        public ObservableCollection<SlideViewModel> Slides
        {
            get { return _slides; }
            set
            {
                _slides = value;
                OnPropertyChanged(nameof(Slides));
            }
        }

        private SlideViewModel _selectedSlide;

        public SlideViewModel SelectedSlide
        {
            get { return _selectedSlide; }
            set
            {
                _selectedSlide = value;
                OnPropertyChanged(nameof(SelectedSlide));
            }
        }

        public void Initialize(List<SlideViewModel> slides)
        {
            Slides = new ObservableCollection<SlideViewModel>(slides);
            SelectedSlide = Slides.FirstOrDefault();
            _slideTimer = new System.Timers.Timer(5000);
            _slideTimer.Elapsed += OnTimedEvent;
            _slideTimer.AutoReset = true;
            _slideTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                var currentItem = Slides.IndexOf(SelectedSlide);
                var max = Slides.Count;
                if (currentItem + 1 > max)
                    SelectedSlide = Slides[0];
                else
                {
                    SelectedSlide = Slides[currentItem + 1];
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
