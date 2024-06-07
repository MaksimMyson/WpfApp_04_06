using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp_04_06
{

    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private int _interval = 5000; // Default update interval is 5 seconds

        public MainWindow()
        {
            InitializeComponent();
            UpdateProcessList();
            SetupTimer();
        }

        private void SetupTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(_interval);
            _timer.Tick += (s, e) => UpdateProcessList();
            _timer.Start();
        }

        private void UpdateProcessList()
        {
            var processes = Process.GetProcesses()
                                   .OrderBy(p => p.ProcessName)
                                   .Select(p => new
                                   {
                                       p.ProcessName,
                                       p.Id
                                   }).ToList();

            ProcessListView.ItemsSource = processes;
        }

        private void ProcessListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProcessListView.SelectedItem is not null)
            {
                var selectedProcess = ProcessListView.SelectedItem as dynamic;
                DisplayProcessDetails(selectedProcess.Id);
            }
        }

        private void DisplayProcessDetails(int processId)
        {
            try
            {
                var process = Process.GetProcessById(processId);
                var details = $"Process Name: {process.ProcessName}\n" +
                              $"PID: {process.Id}\n" +
                              $"Start Time: {process.StartTime}\n" +
                              $"Total Processor Time: {process.TotalProcessorTime}\n" +
                              $"Threads: {process.Threads.Count}\n" +
                              $"Instances: {Process.GetProcessesByName(process.ProcessName).Length}";

                ProcessDetailsTextBlock.Text = details;
            }
            catch (Exception ex)
            {
                ProcessDetailsTextBlock.Text = $"Error retrieving process details: {ex.Message}";
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateProcessList();
        }

        private void SetIntervalButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IntervalTextBox.Text, out int interval))
            {
                _interval = interval;
                _timer.Interval = TimeSpan.FromMilliseconds(_interval);
            }
            else
            {
                MessageBox.Show("Please enter a valid interval in milliseconds.");
            }
        }

        private void EndProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessListView.SelectedItem is not null)
            {
                var selectedProcess = ProcessListView.SelectedItem as dynamic;
                var processId = selectedProcess.Id;
                try
                {
                    var process = Process.GetProcessById(processId);
                    process.Kill();
                    UpdateProcessList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error ending process: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a process to end.");
            }
        }
    }
}