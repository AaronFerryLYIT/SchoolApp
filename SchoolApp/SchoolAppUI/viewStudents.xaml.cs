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
        listStudents selectedUser;
        int teacherID = 0;
        //holds value to see if the click was a search click and the name entered
        bool ifSearch = false;
        string searchString;

        //List<User> students = new List<User>();
        public viewStudents(int teacherID, bool ifSearch, string searchString)
        {
            InitializeComponent();
            this.teacherID = teacherID;
            this.ifSearch = ifSearch;
            this.searchString = searchString;
        }

        private void resetList()
        {
            //set source of the items as users in list
            lstViewStuds.ItemsSource = students;
            students.Clear();
            if (!ifSearch)
            {
                //Couldn't get inner join in linq working so using this method to populate the list
                foreach (var stud in db.Users.Where(s => s.user_role == "student"))
                {
                    //if users result class id equals the class id of the teacher(need teachers id to find the right class first)
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
                }
            }
            else if (ifSearch)
            {
                //Couldn't get inner join in linq working so using this method to populate the list
                foreach (var stud in db.Users.Where(s => s.user_role == "student" && s.name.Contains(searchString)))
                {
                    //if users result class id equals the class id of the teacher(need teachers id to find the right class first)
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
                }
            }
            lstViewStuds.Items.Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            resetList();
            if (students.Count == 0)
            {
                MessageBox.Show("No students were found.");
            }
        }

        private void ViewStudentsResults_Click(object sender, RoutedEventArgs e)
        {
            Result studentResult = new Result();
            foreach (var result in db.Results.Where(r => r.user_id == selectedUser.User_id))
            {
                studentResult = result;
            }
            //MessageBox.Show("Its working now create popup window");
            studResultsPopUp popup = new studResultsPopUp(studentResult);
            popup.ShowDialog();
            popup.Close();
        }

        private void LstViewStuds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstViewStuds.SelectedIndex >= 0)
            {
                //set the local user variable to the selected item in the list view
                selectedUser = students.ElementAt(lstViewStuds.SelectedIndex);
                if (selectedUser.User_id >= 0)
                {
                    viewStudentsResults.IsEnabled = true;
                }
            }
        }
    }
}
