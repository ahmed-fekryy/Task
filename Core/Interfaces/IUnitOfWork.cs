using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IStudentRepository Students { get; }
        public ISubjectRepository Subjects { get; }
        public IStudentSubjectRepository StudentSubjects { get; }
        Task SaveAsync();  
    }
}
