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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        User curUser = new User();
        ///user is passed in from the login screen
        public Dashboard(User newUser)
        {
            InitializeComponent();
            ///initialise the newuser to the class's curuser
            curUser = newUser;
        }

        private void viewStud_Click(object sender, RoutedEventArgs e)
        {
            viewStudents viewStudents = new viewStudents();
            frameMain.Navigate(viewStudents);
        }

        private void viewStats_Click(object sender, RoutedEventArgs e)
        {
            viewPerformance viewStats = new viewPerformance();
            frameMain.Navigate(viewStats);
        }

        private void addResult_Click(object sender, RoutedEventArgs e)
        {
            AddResult add = new AddResult();
            frameMain.Navigate(add);
        }

        private void viewResult_Click(object sender, RoutedEventArgs e)
        {
            ViewResult viewResult = new ViewResult();
            frameMain.Navigate(viewResult);
        }

        private void viewUsers_Click(object sender, RoutedEventArgs e)
        {
            ViewUsers allUsers = new ViewUsers();
            frameMain.Navigate(allUsers);
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            frameMain.Navigate(addUser);
        }
        ///this is executed after window has been loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ///this will set the label at top of screen to user's name
            userName.Content = curUser.name;
            ///check user role and change visibility settings depending on user privilege
            if (curUser.user_role == "headmaster")
            {
                viewStud.Visibility = Visibility.Collapsed;
                viewUsers.Visibility = Visibility.Visible;
                viewResult.Visibility = Visibility.Collapsed;
                addResult.Visibility = Visibility.Collapsed;
                viewStats.Visibility = Visibility.Collapsed;
                addUser.Visibility = Visibility.Visible;
                txtBoxSearch.Visibility = Visibility.Visible;
                search.Visibility = Visibility.Visible;
            }
            else if (curUser.user_role == "teacher")
            {
                viewStud.Visibility = Visibility.Visible;
                viewUsers.Visibility = Visibility.Collapsed;
                viewResult.Visibility = Visibility.Collapsed;
                addResult.Visibility = Visibility.Visible;
                viewStats.Visibility = Visibility.Visible;
                addUser.Visibility = Visibility.Collapsed;
                txtBoxSearch.Visibility = Visibility.Visible;
                search.Visibility = Visibility.Visible;
            }
            else if (curUser.user_role == "student")
            {
                viewStud.Visibility = Visibility.Collapsed;
                viewUsers.Visibility = Visibility.Collapsed;
                viewResult.Visibility = Visibility.Visible;
                addResult.Visibility = Visibility.Collapsed;
                viewStats.Visibility = Visibility.Visible;
                addUser.Visibility = Visibility.Collapsed;
                txtBoxSearch.Visibility = Visibility.Collapsed;
                search.Visibility = Visibility.Collapsed;
            }
        }
    }
}
