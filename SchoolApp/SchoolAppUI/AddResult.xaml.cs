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
    /// Interaction logic for AddResult.xaml
    /// </summary>
    public partial class AddResult : Page
    {
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");
        List<User> students = new List<User>();
        public AddResult()
        {
            InitializeComponent();
        }

        private void createResult(int mark, string comment, string name)
        {
            //have to set a value, this value is headmaster
            int user_id = 1;
            //search for the user_id of the name chosen 
            foreach(var user in db.Users)
            {
                if(name == user.name)
                {
                    user_id = user.user_id;
                }
            }
            Result newMark = new Result();
            newMark.user_id = user_id;
            newMark.class_id = 1;
            newMark.result_mark = mark;
            newMark.result_date = DateTime.Today;
            newMark.comment = comment;
            saveResult(newMark);
        }

        private void saveResult(Result result)
        {
            db.Entry(result).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }
        private void AddResultbtn_Click(object sender, RoutedEventArgs e)
        {
            int convertMark = 0;
            //try to convert a string to an int
            try
            {
                convertMark = Int32.Parse(mark.Text);
            }
            catch
            {
                //try to cause an error
            }
            createResult(convertMark, comment.Text, studentCombo.Text);
            //empty the boxes to show name added
            mark.Text = String.Empty;
            comment.Text = String.Empty;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //populate the combobox of students
            studentCombo.ItemsSource = students;
            foreach (var student in db.Users.Where(s=>s.user_role == "student"))
            {
                students.Add(student);
            }
        }
    }
}
