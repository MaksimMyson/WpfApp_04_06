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
        private Process _process;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LaunchProcess_Click(object sender, RoutedEventArgs e)
        {
            string processPath = ProcessPathTextBox.Text;
            if (!string.IsNullOrEmpty(processPath))
            {
                try
                {
                    _process = new Process();
                    _process.StartInfo.FileName = processPath;
                    _process.EnableRaisingEvents = true;

                    _process.Start();

                    StatusTextBlock.Text = "Process is running...";
                    WaitButton.IsEnabled = true;
                    KillButton.IsEnabled = true;
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

        private async void WaitForExit_Click(object sender, RoutedEventArgs e)
        {
            if (_process != null && !_process.HasExited)
            {
                StatusTextBlock.Text = "Waiting for process to exit...";

                // Asynchronously wait for the process to exit
                await Task.Run(() => _process.WaitForExit());

                // After the process exits, get the exit code
                int exitCode = _process.ExitCode;
                StatusTextBlock.Text = $"Process exited with code: {exitCode}";

                WaitButton.IsEnabled = false;
                KillButton.IsEnabled = false;
            }
        }

        private void KillProcess_Click(object sender, RoutedEventArgs e)
        {
            if (_process != null && !_process.HasExited)
            {
                try
                {
                    _process.Kill();
                    StatusTextBlock.Text = "Process was killed.";
                    WaitButton.IsEnabled = false;
                    KillButton.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    StatusTextBlock.Text = $"Error killing process: {ex.Message}";
                }
            }
        }
    }
}