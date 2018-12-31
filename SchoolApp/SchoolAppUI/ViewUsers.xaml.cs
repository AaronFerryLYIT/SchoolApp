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
    /// Interaction logic for ViewUsers.xaml
    /// </summary>
    public partial class ViewUsers : Page
    {
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");
        List<User> users = new List<User>();
        User selectedUser;
        bool ifSearch = false;
        string searchString;

        public ViewUsers(bool search, string searchString)
        {
            InitializeComponent();
            ifSearch = search;
            this.searchString = searchString;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            resetList();
            if(users.Count == 0)
            {
                MessageBox.Show("No users were found.");
            }
        }

        private void resetList()
        {
            //set source of the items as users in list
            lstViewUsers.ItemsSource = users;
            users.Clear();
            if (!ifSearch)
            {
                foreach (var user in db.Users)
                {
                    users.Add(user);
                }
            }
            else if (ifSearch)
            {
                foreach (var user in db.Users.Where(s => s.name.Contains(searchString)))
                {
                    users.Add(user);
                }
            }
            lstViewUsers.Items.Refresh();
        }

        private void LstViewUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstViewUsers.SelectedIndex >= 0)
            {
                //set the local user variable to the selected item in the list view
                selectedUser = users.ElementAt(lstViewUsers.SelectedIndex);
                if (selectedUser.user_id > 0 && selectedUser.user_role == "student")
                {
                    menuHeaderDelete.IsEnabled = true;
                    menuHeaderEdit.IsEnabled = true;
                }//dont allow user to delete a teacher or headmaster
                else if(selectedUser.user_id > 0 && (selectedUser.user_role == "teacher" || selectedUser.user_role == "headmaster"))
                {
                    menuHeaderEdit.IsEnabled = true;
                }
            }            
        }

        private void MenuHeaderDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
            if (MessageBoxResult.Yes == result)
            {
                deleteUsersResults();
                db.Users.RemoveRange(db.Users.Where(del => del.user_id == selectedUser.user_id));
                db.SaveChanges();
                resetList();
            }
            
        }

        private void deleteUsersResults()
        {
            if(selectedUser.user_role == "student")
            {
                //go through each result and delete the result connected with the student
                foreach (var result in db.Results.Where(del => del.user_id == selectedUser.user_id))
                {
                    db.Results.RemoveRange(db.Results.Where(del => del.user_id == selectedUser.user_id));
                }
            }
        }

        //displays pop up screen
        private void MenuHeaderEdit_Click(object sender, RoutedEventArgs e)
        {
            editUserPopUp popup = new editUserPopUp(selectedUser);
            popup.ShowDialog();
            popup.Close();
            //resets list after pop up is closed
            resetList();
        }
    }
}
