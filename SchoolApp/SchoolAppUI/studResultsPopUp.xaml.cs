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
using System.Windows.Shapes;

namespace SchoolAppUI
{
    /// <summary>
    /// Interaction logic for studResultsPopUp.xaml
    /// </summary>
    public partial class studResultsPopUp : Window
    {
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");
        List<Result> listResults = new List<Result>();
        Result studResult;
        public studResultsPopUp(Result student)
        {
            InitializeComponent();
            this.studResult = student;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            resetList();
        }

        private void resetList()
        {
            //set source of the items as users in list
            lstViewStudRslts.ItemsSource = listResults;
            listResults.Clear();
            //find the results connect to the current clicked user
            foreach (var studResult in db.Results.Where(r => r.user_id == studResult.user_id))
            {
                listResults.Add(studResult);
            }
            lstViewStudRslts.Items.Refresh();
        }
        //will asked if result wants to be deleted and will remove result from system
        private void DeleteResults_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this result?", "Delete Result", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (MessageBoxResult.Yes == result)
            {
                db.Results.RemoveRange(db.Results.Where(del => del.result_id == studResult.result_id));
                db.SaveChanges();
                resetList();               
            }
        }

        private void LstViewStudRslts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //has to be >= or first name wont register click
            if (lstViewStudRslts.SelectedIndex >= 0)
            {
                //set the local user variable to the selected item in the list view
                studResult = listResults.ElementAt(lstViewStudRslts.SelectedIndex);
                if (studResult.user_id >= 0)
                {
                    //will allow user to click on delete result
                    deleteResults.IsEnabled = true;
                }
            }
        }
    }
}
