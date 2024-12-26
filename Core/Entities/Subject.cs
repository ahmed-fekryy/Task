using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Subject
    {
        #region Subject Properties (Basic Data Fields)
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        #endregion

        #region Navigation Properties
        // Navigation property to link Subject to StudentSubject relationships
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
        #endregion
    }
}
