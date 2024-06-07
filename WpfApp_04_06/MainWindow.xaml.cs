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

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            string num1 = Number1TextBox.Text;
            string num2 = Number2TextBox.Text;
            string operation = OperationTextBox.Text;

            // Створюємо аргументи для передачі дочірньому процесу
            string arguments = $"{num1} {num2} {operation}";

            // Шлях до вашого дочірнього процесу
            string processPath = "ChildProcess.exe";

            // Запускаємо дочірній процес
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = processPath;
            startInfo.Arguments = arguments;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            using (Process process = Process.Start(startInfo))
            {
                // Отримуємо результати з дочірнього процесу
                string result = process.StandardOutput.ReadToEnd();

                // Відображаємо результат у текстовому полі
                ResultTextBox.Text = result;
            }
        }
    }
}