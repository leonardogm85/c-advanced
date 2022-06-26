using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace C.Wpf06
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isCompleted = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            TextDirectory.Focus();
        }

        private async void ButtonCount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isCompleted = false;

                TextDirectory.IsEnabled = false;
                ButtonCount.IsEnabled = false;

                TextProgress.Clear();

                Task progressTask = Task.Factory.StartNew(ShowCountProgress);

                Result result = await Task.Factory.StartNew(CountNumberOfFilesAndDirectories);

                ShowNumberOfFilesAndDirectories(result);
            }
            catch (ArgumentException exception)
            {
                isCompleted = true;

                TextProgress.Clear();

                MessageBox.Show(this, exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                TextDirectory.IsEnabled = true;
                ButtonCount.IsEnabled = true;
            }
        }

        private void ShowCountProgress()
        {
            while (!isCompleted)
            {
                Dispatcher.Invoke(() =>
                {
                    TextProgress.AppendText($"Counting number of files and directories {Environment.NewLine}");
                    TextProgress.ScrollToEnd();
                });

                Thread.Sleep(1000);
            }
        }

        private Result CountNumberOfFilesAndDirectories()
        {
            string directoryName = Dispatcher.Invoke(() => TextDirectory.Text);

            if (string.IsNullOrWhiteSpace(directoryName))
            {
                throw new ArgumentException("A valid directory must be provided.");
            }

            Counter counter = new Counter();

            return counter.CountNumberOfFilesAndDirectories(directoryName);
        }

        private void ShowNumberOfFilesAndDirectories(Result result)
        {
            isCompleted = true;

            Dispatcher.Invoke(() =>
            {
                TextProgress.AppendText($"Number of directories: {result.NumberOfDirectories} {Environment.NewLine}");
                TextProgress.AppendText($"Number of files: {result.NumberOfFiles} {Environment.NewLine}");
                TextProgress.ScrollToEnd();
            });
        }
    }

    internal class Counter
    {
        public Result CountNumberOfFilesAndDirectories(string directoryName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);

            if (!directoryInfo.Exists)
            {
                throw new ArgumentException($"The {directoryName} directory does not exist.");
            }

            Result result = new Result();

            IncrementNumberOfFilesAndDirectories(result, directoryInfo);

            return result;
        }

        private void IncrementNumberOfFilesAndDirectories(Result result, DirectoryInfo directory)
        {
            try
            {
                result.IncrementNumberOfFiles(directory.GetFiles().Length);

                foreach (DirectoryInfo subdirectory in directory.GetDirectories())
                {
                    result.IncrementNumberOfDirectories(1);

                    IncrementNumberOfFilesAndDirectories(result, subdirectory);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }
    }

    internal class Result
    {
        public int NumberOfDirectories { get; private set; }
        public int NumberOfFiles { get; private set; }

        public void IncrementNumberOfDirectories(int number)
        {
            NumberOfDirectories += number;
        }

        public void IncrementNumberOfFiles(int number)
        {
            NumberOfFiles += number;
        }
    }
}
