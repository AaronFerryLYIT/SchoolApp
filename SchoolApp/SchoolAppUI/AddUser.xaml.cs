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
        public AddUser()
        {
            InitializeComponent();
        }

        private void createUser(string username, string password, string user_role, string name, string address, DateTime date)
        {
            User newUser = new User();
            newUser.username = username;
            newUser.password = password;
            newUser.user_role = user_role;
            newUser.name = name;
            newUser.address = address;
            newUser.dob = date;
            saveUser(newUser);
        }

        private void saveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }

        private void AddResultbtn_Click(object sender, RoutedEventArgs e)
        {
            //converts date picker choice to a datetime object
            DateTime covertedDate = DateTime.ParseExact(date.Text, "d", null);
            createUser(username.Text, password.Text, "student", name.Text, address.Text, covertedDate);
            //empties the boxes of text
            username.Text = String.Empty;
            password.Text = String.Empty;
            name.Text = String.Empty;
            address.Text = String.Empty;
            date.Text = String.Empty;
        }
    }
}
