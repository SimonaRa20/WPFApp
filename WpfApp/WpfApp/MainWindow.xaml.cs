using BusinessLogic;
using DataAccess;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private ThreadManager _threadManager;
        private ObservableCollection<GeneratedData> _generatedDataList = new ObservableCollection<GeneratedData>();

        public MainWindow()
        {
            InitializeComponent();
            GeneratedDataListView.ItemsSource = _generatedDataList;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(ThreadCountTextBox.Text, out int threadCount) && threadCount >= 2 && threadCount <= 15)
                {
                    ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
                    _threadManager = new ThreadManager(threadCount, UpdateUI);
                    _threadManager.Start();
                    StartButton.IsEnabled = false;
                    StopButton.IsEnabled = true;
                }
                else
                {
                    ErrorMessageTextBlock.Visibility = Visibility.Visible;
                    ErrorMessageTextBlock.Text = "Please enter a valid number of threads (2-15).";
                }
            }
            catch (Exception ex)
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = $"Error starting threads: {ex.Message}";
            }
        }

        private async void StopButton_Click(object sender, RoutedEventArgs e) // Changed return type to async void
        {
            try
            {
                await _threadManager.Stop();
                StartButton.IsEnabled = true;
                StopButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = $"Error stopping threads: {ex.Message}";
            }
        }

        private void UpdateUI(GeneratedData data)
        {
            Dispatcher.Invoke(() =>
            {
                if (_generatedDataList.Count >= 20)
                {
                    _generatedDataList.RemoveAt(0);
                }
                _generatedDataList.Add(data);
            });
        }
    }
}
