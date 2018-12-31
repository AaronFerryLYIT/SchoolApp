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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");
        TestMethods methods = new TestMethods();
        public AddUser()
        {
            InitializeComponent();
        }

        private void createUser(string username, string password, string user_role, string name, string address, DateTime date)
        {
            User newUser = new User();
            //if any field excedes the limit then this won't execute
            if(methods.dontExcedeCharLimit(username, 20) && methods.dontExcedeCharLimit(password, 15) && methods.dontExcedeCharLimit(name, 30) && methods.dontExcedeCharLimit(address, 50))
            {
                newUser.username = username;
                newUser.password = password;
                newUser.user_role = user_role;
                newUser.name = name;
                newUser.address = address;
                newUser.dob = date;
                saveUser(newUser);
            }
            else if(!methods.dontExcedeCharLimit(username, 20))
            {
                errorUserLbl.Content = "Username is too long!";
            }
            else if(!methods.dontExcedeCharLimit(password, 15))
            {
                errorUserLbl.Content = "Password is too long!";
            }
            else if(!methods.dontExcedeCharLimit(name, 30))
            {
                errorUserLbl.Content = "Name is too long!";
            }
            else if(!methods.dontExcedeCharLimit(address, 50))
            {
                errorUserLbl.Content = "Address is too long!";
            }          
        }

        private void saveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            //if save changes returns 0 that means it hasn't saved 1 does
            int saved = db.SaveChanges();
            if(saved == 0)
            {
                MessageBox.Show("User not added");
            }
        }

        private void AddUserbtn_Click(object sender, RoutedEventArgs e)
        {
            //this is to stop crash if a user doesn't enter a date value
            if (date.Text == "" || username.Text == "" || password.Text == "" || name.Text == "" || address.Text == "")
            {
                errorUserLbl.Content = "A field was left empty";
            }
            else
            {
                //get the string from a combo box item
                string role = ((ComboBoxItem)userRole.SelectedItem).Content.ToString();
                //converts date picker choice to a datetime object
                DateTime covertedDate = DateTime.ParseExact(date.Text, "d", null);
                createUser(username.Text, password.Text, role, name.Text, address.Text, covertedDate);
            } 
            //empties the boxes of text
            username.Text = String.Empty;
            password.Text = String.Empty;
            name.Text = String.Empty;
            address.Text = String.Empty;
            date.Text = String.Empty;
        }
    }
}
