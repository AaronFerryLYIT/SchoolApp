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
    /// Interaction logic for editUserPopUp.xaml
    /// </summary>
    public partial class editUserPopUp : Window
    {
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");
        User selectedUser;
        TestMethods methods = new TestMethods();
        public editUserPopUp(User selectedUser)
        {
            InitializeComponent();
            this.selectedUser = selectedUser;
        }

        private void updateUser(string username, string password, string user_role, string name, string address, DateTime date)
        {
            if (methods.dontExcedeCharLimit(username, 20) && methods.dontExcedeCharLimit(password, 15) && methods.dontExcedeCharLimit(name, 30) && methods.dontExcedeCharLimit(address, 50))
            {
                //find the user in the system and change their details
                foreach (var user in db.Users.Where(u => u.user_id == selectedUser.user_id))
                {
                    user.username = username;
                    user.password = password;
                    user.user_role = user_role;
                    user.name = name;
                    user.address = address;
                    user.dob = date;
                }
            }
            else if (!methods.dontExcedeCharLimit(username, 20))
            {
                errorLbl.Content = "Username is too long!";
            }
            else if (!methods.dontExcedeCharLimit(password, 15))
            {
                errorLbl.Content = "Password is too long!";
            }
            else if (!methods.dontExcedeCharLimit(name, 30))
            {
                errorLbl.Content = "Name is too long!";
            }
            else if (!methods.dontExcedeCharLimit(address, 50))
            {
                errorLbl.Content = "Address is too long!";
            }
            //if save changes returns 0 that means it hasn't saved 1 does
            db.SaveChanges();
        }
        
        private void UpdateUserbtn_Click(object sender, RoutedEventArgs e)
        {
            //this is to stop crash if a user doesn't enter a date value
            if (dob.Text == "" || username.Text == "" || password.Text == "" || name.Text == "" || address.Text == "")
            {
                errorLbl.Content = "A field was left empty";
            }
            else
            {
                //get the string from a combo box item
                string role = ((ComboBoxItem)userRoleCombo.SelectedItem).Content.ToString();
                //converts date picker choice to a datetime object
                DateTime covertedDate = DateTime.ParseExact(dob.Text, "d", null);
                updateUser(username.Text, password.Text, role, name.Text, address.Text, covertedDate);
                //closes the pop up window
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            username.Text = selectedUser.username;
            password.Text = selectedUser.password;
            name.Text = selectedUser.name;
            address.Text = selectedUser.address;
            //dob.DisplayDate = selectedUser.dob;
            //sets the combo box to the right user role of selected user
            //userRoleCombo.SelectedItem 
        }
    }
}
