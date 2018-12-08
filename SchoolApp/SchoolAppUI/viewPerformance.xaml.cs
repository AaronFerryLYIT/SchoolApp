using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolAppUI
{
    /// <summary>
    /// Interaction logic for viewPerformance.xaml
    /// </summary>
    public partial class viewPerformance : Page
    {
        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/2f2e4d43-ad0a-402b-b638-799b64762190/wpf-charting-getting-started-how-to-use-the-datavisualization-chart-component-in-wpf-using-mvvm?forum=MSWinWebChart
        //https://www.c-sharpcorner.com/UploadFile/mahesh/charting-in-wpf/
        //https://www.youtube.com/watch?v=WMAmQ8VMAis
        public viewPerformance()
        {
            InitializeComponent();
            //loadChart();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void loadChart()
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //not working null pointer exception
            ((ColumnSeries)statsChart.Series[0]).ItemsSource =
                new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Uncle Dave", 25),
                    new KeyValuePair<string, int>("Ross Tweddell", 21)
                };
        }
    }
}
