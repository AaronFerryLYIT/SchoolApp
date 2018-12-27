using DBLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for ViewResult.xaml
    /// </summary>
    public partial class ViewResult : Page
    {
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");
        List<Result> results = new List<Result>();
        int curUser = 0;
        public ViewResult(int user)
        {
            InitializeComponent();
            this.curUser = user;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //set source of the items as users in list
            lstViewResults.ItemsSource = results;
            foreach (var stud in db.Results.Where(id => id.user_id == curUser))
            {
                results.Add(stud);
            }
            if (results.Count == 0)
            {
                noResultsLbl.Visibility = Visibility.Visible;
                noResultsLbl.Content = "There are no results to show!";
            }
            else
            {
                lstViewResults.Visibility = Visibility.Visible;
            }
        }
    }
}
