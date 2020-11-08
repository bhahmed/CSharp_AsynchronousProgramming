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
            resultsWindow.Text = string.Empty;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var results = DemoMethods.RunDownloadSync();
            ReportWebsiteInfo(results);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: {elapsedMs}";
        }

        private async void ExecuteAsync_OnClick(object sender, RoutedEventArgs e)
        {
            

            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var results = await DemoMethods.RunDownloadAsync(progress);
            ReportWebsiteInfo(results);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: {elapsedMs}";
        }

        private void ReportProgress(object sender, ProgressReportModel e)
        {
            progressBar.Value = e.PercentageComplete;
            ReportWebsiteInfo(e.SitesDownloaded);
        }

        private async void ExecuteParallelAsync_OnClick(object sender, RoutedEventArgs e)
        {
            resultsWindow.Text = string.Empty;

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
            resultsWindow.Text = string.Empty;
            foreach (WebsiteDataModel websiteDataModel in data)
            {
                resultsWindow.Text +=
                    $"{websiteDataModel.WebsieUrl} downloaded: {websiteDataModel.WebsiteData.Length} characters long.{Environment.NewLine}";
            }
        }
    }
}
