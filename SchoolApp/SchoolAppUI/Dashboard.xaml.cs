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
        //the user is passed in from the login screen
        public Dashboard(User newUser)
        {
            InitializeComponent();
            //initialise the newuser to the class's curuser
            curUser = newUser;
        }

        private void viewStud_Click(object sender, RoutedEventArgs e)
        {
            //pass in the teacher's id
            //false means it isn't a search click and null means no text was entered
            viewStudents viewStudents = new viewStudents(curUser.user_id, false, null);
            frameMain.Navigate(viewStudents);
        }

        private void viewStats_Click(object sender, RoutedEventArgs e)
        {
            viewPerformance viewStats = new viewPerformance(curUser);
            frameMain.Navigate(viewStats);
        }

        private void addResult_Click(object sender, RoutedEventArgs e)
        {
            //pass user id for the purpose of checking their class id
            AddResult add = new AddResult(curUser.user_id);
            frameMain.Navigate(add);
        }

        private void viewResult_Click(object sender, RoutedEventArgs e)
        {
            ViewResult viewResult = new ViewResult(curUser.user_id);
            frameMain.Navigate(viewResult);
        }

        private void viewUsers_Click(object sender, RoutedEventArgs e)
        {
            //false means it isn't a search click and null means no text was entered
            ViewUsers allUsers = new ViewUsers(false, null);
            frameMain.Navigate(allUsers);
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            frameMain.Navigate(addUser);
        }

        //this is used to determine if the search if from a teacher or headmaster
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //this checks to see who is the user so it will go to the right frame
            //true is passed to let frame know this is a search click and the search term is passed as well
            if (txtBoxSearch.Text == "")
            {
                txtBoxSearch.Text = "Field was left empty!";
            }
            else
            {
                if (curUser.user_role == "headmaster")
                {
                    ViewUsers userSearch = new ViewUsers(true, txtBoxSearch.Text);
                    frameMain.Navigate(userSearch);
                }
                else if (curUser.user_role == "teacher")
                {
                    viewStudents studentSearch = new viewStudents(curUser.user_id, true, txtBoxSearch.Text);
                    frameMain.Navigate(studentSearch);
                }
            }
        }

        //this is executed after window has been loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this will set the label at top of screen to user's name
            userName.Content = curUser.name;
            //check user role and change visibility settings depending on user privilege
            if (curUser.user_role == "headmaster")
            {
                viewUsers.Visibility = Visibility.Visible;
                addUser.Visibility = Visibility.Visible;
                txtBoxSearch.Visibility = Visibility.Visible;
                search.Visibility = Visibility.Visible;
            }
            else if (curUser.user_role == "teacher")
            {
                viewStud.Visibility = Visibility.Visible;
                addResult.Visibility = Visibility.Visible;
                viewStats.Visibility = Visibility.Visible;
                txtBoxSearch.Visibility = Visibility.Visible;
                search.Visibility = Visibility.Visible;
            }
            else if (curUser.user_role == "student")
            {
                viewResult.Visibility = Visibility.Visible;
                viewStats.Visibility = Visibility.Visible;
            }
        }
    }
}
