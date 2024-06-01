using BusinessLogic;
using DataAccess;
using System.Collections.ObjectModel;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _threadManager.Stop();
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
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

        private void GeneratedDataListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}