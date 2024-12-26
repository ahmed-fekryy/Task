using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStudentSubjectRepository
    {
        Task AddAsync(StudentSubject studentSubject);
        Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(int studentId);
        Task DeleteAsync(int studentId, int subjectId);
        Task<StudentSubject> GetByIdsAsync(int studentId, int subjectId);
    }
}
