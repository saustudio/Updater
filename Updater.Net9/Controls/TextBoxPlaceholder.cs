using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Updater.Controls
{
    public class TextBoxPlaceholder : TextBox
    {
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            nameof(Placeholder), typeof(string), typeof(TextBoxPlaceholder), new PropertyMetadata(""));

        public TextBoxPlaceholder()
        {
            DefaultStyleKey = typeof(TextBoxPlaceholder);
        }
    }
}
