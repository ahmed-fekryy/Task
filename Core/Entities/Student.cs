using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public  class Student
    {
        #region Student Properties (Basic Data Fields)
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        #endregion

        #region Navigation Properties
        // Navigation property to link Student to StudentSubject relationship
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
        #endregion
    }
}
