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
        //needed to check for class id
        int teacher_id = 1;
        public AddResult(int teacher_id)
        {
            InitializeComponent();
            this.teacher_id = teacher_id;
        }

        private void createResult(int mark, string comment, string name)
        {
            //have to set a value, this value is headmaster
            int user_id = 1;
            int class_id = 1;
            //search for the user_id of the name chosen 
            foreach (var user in db.Users)
            {
                if (name == user.name)
                {
                    user_id = user.user_id;
                }
            }
            //search for the class id
            foreach (var c in db.Classes)
            {
                if(teacher_id == c.user_id)
                {
                    class_id = c.class_id;
                }
            }
            Result newMark = new Result();
            newMark.user_id = user_id;
            newMark.class_id = class_id;
            newMark.result_mark = mark;
            newMark.result_date = DateTime.Today;
            newMark.comment = comment;
            saveResult(newMark);
        }

        private void saveResult(Result result)
        {
            db.Entry(result).State = System.Data.Entity.EntityState.Added;
            int saved = db.SaveChanges();
            if (saved == 0)
            {
                MessageBox.Show("Result not added");
            }
        }
        private void AddResultbtn_Click(object sender, RoutedEventArgs e)
        {
            int convertMark = 0;
            bool createEntry = false;
            //to make sure that the value entered for a mark is an int
            foreach (char c in mark.Text)
            {
                //if value is not an int, then set text and break out of loop
                if(c >= '0' && c <= '9')
                {
                    createEntry = true;
                }
                else
                {
                    errorLbl.Content = "The result has to be a integer value!";
                    createEntry = false;
                    break;
                }
            }
            //if value is an int then parse then create entry
            if(createEntry == true)
            {
                //try to convert a string to an int and have to catch the exception
                try
                {
                    convertMark = Int32.Parse(mark.Text);
                }
                catch(Exception message)
                {
                    message.ToString();
                    throw;
                }
                //if converted make sure mark is between 100 and 0
                if(convertMark <= 100 && convertMark >= 0)
                {
                    createResult(convertMark, comment.Text, studentCombo.Text);
                }
                else
                {
                    errorLbl.Content = "Value is too high or too low!";
                }
                
            }

            //empty the boxes to show name added
            mark.Text = String.Empty;
            comment.Text = String.Empty;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            students.Clear();
            //populate the combobox of students
            studentCombo.ItemsSource = students;
            foreach (var student in db.Users.Where(s=>s.user_role == "student"))
            {
                students.Add(student);
            }

            studentCombo.Items.Refresh();
        }
    }
}
