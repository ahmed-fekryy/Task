using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public  class StudentSubject
    {
        #region Properties (Primary Key & Additional Data)
        // Composite primary key for the many-to-many relationship
        public int StudentId { get; set; } // Foreign Key for Student
        public int SubjectId { get; set; } // Foreign Key for Subject
        #endregion

        #region Navigation Properties (Links to Other Entities)
        // Navigation property for the Student entity
        public virtual Student Student { get; set; } // Navigation property for Student
        public virtual Subject Subject { get; set; } // Navigation property for Subject
        #endregion
    }
}

