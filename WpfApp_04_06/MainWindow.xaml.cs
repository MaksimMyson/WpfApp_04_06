using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Timers;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer updateTimer;  // Явно вказуємо System.Timers.Timer
        public ObservableCollection<Process> Processes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Processes = new ObservableCollection<Process>();
            ProcessListView.ItemsSource = Processes;
            updateTimer = new System.Timers.Timer(1000);  // Явно вказуємо System.Timers.Timer
            updateTimer.Elapsed += UpdateProcesses;
            updateTimer.Start();
        }

        private void UpdateProcesses(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Processes.Clear();
                foreach (var process in Process.GetProcesses())
                {
                    Processes.Add(process);
                }
            });
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IntervalTextBox.Text, out int interval))
            {
                updateTimer.Interval = interval;
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}
