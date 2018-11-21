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
        public Dashboard()
        {
            InitializeComponent();
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
    }
}
