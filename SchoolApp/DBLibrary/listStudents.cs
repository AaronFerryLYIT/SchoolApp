using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    using System;
    using System.Collections.Generic;

    public partial class listStudents
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        List<int> listOfClassIds;
        public listStudents()
        {
            this.Classes = new HashSet<Class>();
            this.Results = new HashSet<Result>();
            this.Users = new HashSet<User>();
            listOfClassIds = new List<int>();
        }

        public int User_id { get; set; }
        public string Student_name { get; set; }
        public string Address { get; set; }
        public DateTime Dob { get; set; }
        public int Mark { get; set; }
        //to hold the class id
        public List<int> Class_id {
            get
            {
                return listOfClassIds;
            }
            //set; 
        }

        //public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class> Classes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
