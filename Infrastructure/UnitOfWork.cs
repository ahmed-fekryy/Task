using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;

        public IStudentRepository Students { get; private set; }
        public ISubjectRepository Subjects { get; private set; }
        public IStudentSubjectRepository StudentSubjects { get; private set; }
        public UnitOfWork(AppDbContext context, IStudentRepository students, ISubjectRepository subjects, IStudentSubjectRepository studentSubjects)
        {
            _context = context;
            Students = students;
            Subjects = subjects;
            StudentSubjects = studentSubjects;
        }




        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); 
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
