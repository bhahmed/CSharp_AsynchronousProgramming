using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteSync_OnClick(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var results = DemoMethods.RunDownloadSync();
            ReportWebsiteInfo(results);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: {elapsedMs}";
        }

        private async void ExecuteAsync_OnClick(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var results = await DemoMethods.RunDownloadAsync();
            ReportWebsiteInfo(results);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: {elapsedMs}";
        }

        private async void ExecuteParallelAsync_OnClick(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var results = await DemoMethods.RunDownloadParallelAsync();
            ReportWebsiteInfo(results);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: {elapsedMs}";
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void ReportWebsiteInfo(List<WebsiteDataModel> data)
        {
            foreach (WebsiteDataModel websiteDataModel in data)
            {
                resultsWindow.Text +=
                    $"{websiteDataModel.WebsieUrl} downloaded: {websiteDataModel.WebsiteData.Length} characters long.{Environment.NewLine}";
            }
        }
    }
}
