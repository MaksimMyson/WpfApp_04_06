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

        private async void LaunchProcess_Click(object sender, RoutedEventArgs e)
        {
            string processPath = ProcessPathTextBox.Text;
            if (!string.IsNullOrEmpty(processPath))
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = processPath;
                    process.EnableRaisingEvents = true;

                    process.Start();

                    StatusTextBlock.Text = "Process is running...";

                    // Asynchronously wait for the process to exit
                    await Task.Run(() => process.WaitForExit());

                    // After the process exits, get the exit code
                    int exitCode = process.ExitCode;
                    StatusTextBlock.Text = $"Process exited with code: {exitCode}";
                }
                catch (Exception ex)
                {
                    StatusTextBlock.Text = $"Error launching process: {ex.Message}";
                }
            }
            else
            {
                MessageBox.Show("Please enter the path to the application.");
            }
        }
    }
}