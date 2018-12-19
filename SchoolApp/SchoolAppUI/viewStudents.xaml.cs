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
    /// Interaction logic for viewStudents.xaml
    /// </summary>
    public partial class viewStudents : Page
    {
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");
        //this is the new class instance to hold and display information of the student
        List<listStudents> students = new List<listStudents>();
        int teacherID = 0;
        //List<User> students = new List<User>();
        public viewStudents(int teacherID)
        {
            InitializeComponent();
            this.teacherID = teacherID;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //set source of the items as users in list
            lstViewStuds.ItemsSource = students;
            /*foreach (var item in db.Users)
            {
                var student = from u in db.Users
                              from r in db.Results
                              //from c in db.Classes
                              where u.user_id == r.user_id
                              select new listStudents()
                              {
                                  User_id = u.user_id,
                                  Student_name = u.name,
                                  Address = u.address,
                                  Dob = u.dob,
                                  Mark = r.result_mark
                              };
                students.Add(student);
            }*/

            //Couldn't get inner join in linq working so using this method to populate the list
            foreach (var stud in db.Users.Where(s => s.user_role == "student"))
            {
                //if users result class id equals the class id of the teacher(need teachers id to find the right class first)
                //var test = from r in db.Results from c in db.Classes where r.class_id == c.class_id;
                //if()
                //create a new instance of a student to add to list
                listStudents newStudent = new listStudents
                {
                    User_id = stud.user_id,
                    Student_name = stud.username,
                    Address = stud.address,
                    Dob = stud.dob
                };
                //this foreach gets the mark from the result table
                foreach (var result in db.Results.Where(r => r.user_id == newStudent.User_id))
                {
                    newStudent.Mark = result.result_mark;
                    //suppose to add class id to a list in the instance
                    //newStudent.Class_id.Add(result.class_id);
                }
                students.Add(newStudent);
                //only show student that is in a teachers class
                /*int teacherClass = 0;
                foreach (var tClass in db.Classes.Where(c => c.user_id == teacherID))
                {
                    teacherClass = tClass.class_id;
                }*/
                //this was a loop that would only add the student if they had a result given by the teacher
                /*for(int i =0; i < newStudent.Class_id.Count; i++)
                {
                    if (newStudent.Class_id[i] == teacherClass)
                    {
                        
                    }
                }*/
            }
            
        }

        private void LstViewStuds_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ListViewItem item = (ListViewItem)sender;
            /*if (e.Source.Equals(lstViewStuds.SelectedItem))
            {
                MessageBox.Show("Its working");
            }*/
            /*if(lstViewStuds.SelectedItems.Count > 0)
            {
                MessageBox.Show("Its working"+lstViewStuds.SelectedItems[0].ToString());
            }*/
            var item = ((FrameworkElement)e.OriginalSource).DataContext as User;
            if (item != null)
            {
                MessageBox.Show("Item's Double Click handled!");
            }

        }
    }
}
