using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Interface
{
    public interface  IStudentService
    {
        Task AddStudentAsync(Student student);
        Task DeleteStudentAsync(int studentId);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int studentId);
        Task UpdateStudentAsync(Student student);
    }
}
