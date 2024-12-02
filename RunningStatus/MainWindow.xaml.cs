using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RunningStatus
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializePlaceholders();
        }

        private void InitializePlaceholders()
        {
            // Initialize placeholders for ComboBoxes
            AttractieComboBox.Text = "Selecteer attractie";
            AttractieComboBox.Foreground = Brushes.Gray;
            AttractieComboBox.GotFocus += RemovePlaceholder;
            AttractieComboBox.LostFocus += AddPlaceholder;

            StatusComboBox.Text = "Selecteer status";
            StatusComboBox.Foreground = Brushes.Gray;
            StatusComboBox.GotFocus += RemovePlaceholder;
            StatusComboBox.LostFocus += AddPlaceholder;

            // Initialize placeholder for TextBox
            OpmerkingenBox.Text = "Voer opmerkingen in...";
            OpmerkingenBox.Foreground = Brushes.Gray;
            OpmerkingenBox.GotFocus += RemovePlaceholder;
            OpmerkingenBox.LostFocus += AddPlaceholder;
        }

        private void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.Text == "Selecteer attractie" || comboBox.Text == "Selecteer status")
                {
                    comboBox.Text = "";
                    comboBox.Foreground = Brushes.Black;
                }
            }
            else if (sender is TextBox textBox)
            {
                if (textBox.Text == "Voer opmerkingen in...")
                {
                    textBox.Text = "";
                    textBox.Foreground = Brushes.Black;
                }
            }
        }

        private void AddPlaceholder(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (string.IsNullOrWhiteSpace(comboBox.Text))
                {
                    comboBox.Text = comboBox.Name == "AttractieComboBox" ? "Selecteer attractie" : "Selecteer status";
                    comboBox.Foreground = Brushes.Gray;
                }
            }
            else if (sender is TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "Voer opmerkingen in...";
                    textBox.Foreground = Brushes.Gray;
                }
            }
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement the logic for the Post button click event
            string attractie = AttractieComboBox.Text;
            string status = StatusComboBox.Text;
            string opmerkingen = OpmerkingenBox.Text;

            if (attractie == "Selecteer attractie" || status == "Selecteer status" || opmerkingen == "Voer opmerkingen in...")
            {
                MessageBox.Show("Vul alle velden in voordat u post.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Logic to handle the post action
                MessageBox.Show($"Attractie: {attractie}\nStatus: {status}\nOpmerkingen: {opmerkingen}", "Gegevens gepost", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
