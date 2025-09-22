using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Updater.Controls
{
    public class ButtonImage : ButtonBase
    {
        public string Stretch
        {
            get => (string)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(
            nameof(Stretch), typeof(string), typeof(ButtonImage), new PropertyMetadata(""));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(ButtonImage), new PropertyMetadata(""));

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            nameof(Description), typeof(string), typeof(ButtonImage), new PropertyMetadata(""));

        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            nameof(Source), typeof(ImageSource), typeof(ButtonImage), new PropertyMetadata(default(ImageSource)));
        
        public ICommand MyCommand
        {
            get => (ICommand)GetValue(MyCommandProperty);
            set => SetValue(MyCommandProperty, value);
        }

        public static readonly DependencyProperty MyCommandProperty = DependencyProperty.Register(
            nameof(MyCommand), typeof(ICommand), typeof(ButtonImage), new PropertyMetadata(default(ICommand)));

        public ButtonImage()
        {
            DefaultStyleKey = typeof(ButtonImage);
        }
    }
}