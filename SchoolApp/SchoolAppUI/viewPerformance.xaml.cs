using DBLibrary;
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
    public partial class viewPerformance : Page
    {
        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/2f2e4d43-ad0a-402b-b638-799b64762190/wpf-charting-getting-started-how-to-use-the-datavisualization-chart-component-in-wpf-using-mvvm?forum=MSWinWebChart
        //https://www.c-sharpcorner.com/UploadFile/mahesh/charting-in-wpf/
        //https://www.youtube.com/watch?v=WMAmQ8VMAis
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");
        List<User> students = new List<User>();
        public viewPerformance()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //populate the combobox of students
            studentPerCombo.ItemsSource = students;
            foreach (var student in db.Users.Where(s => s.user_role == "student"))
            {
                students.Add(student);
            }
            //not working null pointer exception
            /*((ColumnSeries)statsChart.Series[0]).ItemsSource =
                new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Uncle Dave", 25),
                    new KeyValuePair<string, int>("Ross Tweddell", 21)
                };*/
        }
    }
}
