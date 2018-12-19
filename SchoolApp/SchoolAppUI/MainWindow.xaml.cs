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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ///setup connection to the db with string
        ///"metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;MultipleActiveResultSets=True;App=EntityFramework'"
        
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string curUser = txtUsername.Text;
            string curPassword = txtPassword.Password;
            //.where uses a lambda expression, like an if statement
            //.Where(t => t.username == curUser && t.password == curPassword)
            foreach (var user in db.Users)
            {
                if(user.username == curUser && user.password == curPassword)
                {
                    Dashboard dashboard = new Dashboard(user);
                    this.Hide();
                    dashboard.ShowDialog();
                    //try to create a loop where once dashboard is closed go back to login screen
                    isClosed();
                    /*if (dashboard.Closing)
                    {
                        this.Close();
                    }*/
                }
                else
                {
                    //if user not found display error message and clear boxes
                    errorMsgLogin.Visibility = Visibility.Visible;
                    txtUsername.Text = String.Empty;
                    txtPassword.Password = String.Empty;
                }
                
            }
        }

        private void isClosed()
        {
            this.Close();
            //throw new NotImplementedException();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }
    }
}
