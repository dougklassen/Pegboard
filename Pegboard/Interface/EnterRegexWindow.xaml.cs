using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace DougKlassen.Revit.Perfect.Interface
{
    /// <summary>
    /// Interaction logic for EnterRegexWindow.xaml
    /// </summary>
    public partial class EnterRegexWindow : Window
    {
        public Regex enteredRegex
        {
            get;
            set;
        }

        public String RegexValue
        {
            get
            {
                return (String)GetValue(RegexValueProperty);
            }
            set
            {
                String oldValue = RegexValue;
                SetValue(RegexValueProperty, value);
                OnPropertyChanged(
                    new DependencyPropertyChangedEventArgs(RegexValueProperty, oldValue, value));
            }
        }
        public static readonly DependencyProperty RegexValueProperty = DependencyProperty.Register(
            "RegexValue",
            typeof(String),
            typeof(Window),
            new UIPropertyMetadata(null));

        public EnterRegexWindow()
        {
            InitializeComponent();
        }

        private void regexTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Check if the value is a valid regular expression
            try
            {
                Regex.Match(String.Empty, regexTextBox.Text);
                okButton.IsEnabled = true;
                statusLabel.Content = "Valid regular expression";
            }
            catch (ArgumentException)
            {
                okButton.IsEnabled = false;
                statusLabel.Content = "Invalid regular expression";
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
