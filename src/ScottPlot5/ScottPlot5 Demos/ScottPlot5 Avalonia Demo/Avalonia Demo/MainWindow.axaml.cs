using Avalonia.Controls;

namespace Avalonia_Demo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Subtitle.Content = $"ScottPlot.Avalonia Version {ScottPlot.Version.VersionString}";

            // testing settings
            var plot = AvaPlot.Plot;
            plot.Axes.Bottom.Label.FontName = "Consolas";
            plot.Axes.Left.Label.FontName = "Consolas";
        }
    }
}
