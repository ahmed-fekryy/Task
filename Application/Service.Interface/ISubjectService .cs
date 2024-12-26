using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Interface
{
    
        public interface ISubjectService
        {
            Task AddSubjectAsync(Subject subject);
            Task DeleteSubjectAsync(int subjectId);
            Task<IEnumerable<Subject>> GetAllSubjectsAsync();
            Task<Subject> GetSubjectByIdAsync(int subjectId);
            Task<Subject> GetSubjectByNameAsync(string name);
            Task UpdateSubjectAsync(Subject subject);
        }
    
}
