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
    public partial class viewPerformance : Page
    {
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");
        List<User> students = new List<User>();
        
        User currentUser;

        public viewPerformance(User curUser)
        {
            InitializeComponent();
            currentUser = curUser;
            studentPerCombo.SelectionChanged += new SelectionChangedEventHandler(comboBoxSelectionChanged);
        }

        private void comboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this is the make shift bar chart
            User selectedUser = students[studentPerCombo.SelectedIndex];
            createBarChart(selectedUser);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //will change the display depending on the user credentials
            if(currentUser.user_role == "teacher")
            {
                studentPerCombo.Visibility = Visibility.Visible;
                refreshList();
            }
            else if(currentUser.user_role == "student")
            {
                studentPerCombo.Visibility = Visibility.Collapsed;
                createBarChart(currentUser);
            }
        }

        private void createBarChart(User curUser)
        {
            double average = 0;
            int highest = 0;
            int lowest = 0;
            int count = 0;
            //create a temp list every time combo box value changes to hold current user's results
            //add result to list to find highest and lowest values
            //also calculate the average for each user
            List<Result> results = results = new List<Result>();
            foreach (var studentResult in db.Results.Where(r => r.user_id == curUser.user_id))
            {
                average = average + (double)studentResult.result_mark;
                count++;
                results.Add(studentResult);
            }

            highest = results.Max(h => h.result_mark);
            lowest = results.Min(l => l.result_mark);
            average = (average / count);
            average = Math.Round(average, 2);

            //set bar lengths to represent values
            avrMark.Width = average + 1;
            hghMark.Width = highest + 1;
            lowMark.Width = lowest + 1;

            //display value beside bars
            averageMark.Content = average + "/100";
            highestMark.Content = highest + "/100";
            lowestMark.Content = lowest + "/100";
        }

        private void refreshList()
        {
            //populate the combobox of students
            studentPerCombo.ItemsSource = students;
            students.Clear();
            foreach (var student in db.Users.Where(s => s.user_role == "student"))
            {
                students.Add(student);
            }
            studentPerCombo.Items.Refresh();
        }
    }
}
