using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace Sopfim.CustomControls
{
    /// <summary>
    /// Interaction logic for DatePickerAmpm.xaml
    /// </summary>
    public partial class DatePickerAmpm : UserControl
    {
        public DatePickerAmpm()
        {
            InitializeComponent();
            var desc =  DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, _spinText.GetType());
            desc.AddValueChanged(_spinText, TextBlockChanged);
        }

        public static readonly DependencyProperty SelectedDateTimeProperty = DependencyProperty.Register("SelectedDateTime", typeof(DateTime?),
            typeof(DatePickerAmpm), new FrameworkPropertyMetadata(null));

        // .NET Property wrapper
        public DateTime? SelectedDateTime
        {
            get
            {
                return (DateTime?)GetValue(SelectedDateTimeProperty);
            }
            set
            {
                SetValue(SelectedDateTimeProperty, value);
            }
        }

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool),
            typeof(DatePickerAmpm), new FrameworkPropertyMetadata(false));

        // .NET Property wrapper
        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }


        private void TextBlockChanged(object sender, EventArgs eventArgs)
        {
            int hour = (_spinText.Text) == "AM" ? 5 : 17;
            if (SelectedDateTime.HasValue)
            {
                SelectedDateTime = new DateTime(SelectedDateTime.Value.Year,
                    SelectedDateTime.Value.Month, SelectedDateTime.Value.Day, hour, 0,0);
            }
        }

        private void _spinner_Spin(object sender, SpinEventArgs e)
        {
            var spinner = (ButtonSpinner)sender;
            var textBlock = (TextBlock)spinner.Content;
            textBlock.Text = (textBlock.Text == "AM") ? "PM" : "AM";
        }

        private void _datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_datePicker.SelectedDate.HasValue)
            {
                var newDate = _datePicker.SelectedDate.Value;
                if (SelectedDateTime.HasValue)
                    SelectedDateTime = new DateTime(newDate.Year, newDate.Month, newDate.Day,
                                                    SelectedDateTime.Value.Hour, SelectedDateTime.Value.Minute,
                                                    SelectedDateTime.Value.Second);
                else
                {
                    SelectedDateTime = new DateTime(newDate.Year, newDate.Month, newDate.Day,5, 0,0);
                }
            }
            else
            {
                SelectedDateTime = null;
            }
                
        }

    }
}
