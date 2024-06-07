using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_04_06
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LaunchNotepad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("notepad.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error launching Notepad: {ex.Message}");
            }
        }

        private void LaunchCalculator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("calc.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error launching Calculator: {ex.Message}");
            }
        }

        private void LaunchPaint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("mspaint.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error launching Paint: {ex.Message}");
            }
        }

        private void LaunchCustomApp_Click(object sender, RoutedEventArgs e)
        {
            string appPath = CustomAppTextBox.Text;
            if (!string.IsNullOrEmpty(appPath))
            {
                try
                {
                    Process.Start(appPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error launching custom app: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter the path to the custom app.");
            }
        }
    }
}