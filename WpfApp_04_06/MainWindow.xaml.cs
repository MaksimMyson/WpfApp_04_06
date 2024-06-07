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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            string wordToSearch = WordTextBox.Text;

            // Перевірка чи введені дані коректні
            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(wordToSearch))
            {
                MessageBox.Show("Будь ласка, введіть шлях до файлу та слово для пошуку.", "Помилка");
                return;
            }

            // Запуск дочірнього процесу і передача аргументів
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "ChildProcess.exe"; // Переконайтеся, що цей файл знаходиться в одній папці з виконуваним файлом цієї програми
            startInfo.Arguments = $"\"{filePath}\" \"{wordToSearch}\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            using (Process process = Process.Start(startInfo))
            {
                // Отримання результату з дочірнього процесу
                string result = process.StandardOutput.ReadToEnd();

                // Виведення результату
                ResultTextBox.Text = result;
            }
        }
    }
}