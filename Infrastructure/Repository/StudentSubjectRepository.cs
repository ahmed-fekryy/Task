using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class StudentSubjectRepository : IStudentSubjectRepository
    {
        private readonly AppDbContext _context;

        public StudentSubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StudentSubject studentSubject)
        {
            await _context.StudentSubjects.AddAsync(studentSubject);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(int studentId)
        {
            return await _context.StudentSubjects
                .Where(ss => ss.StudentId == studentId)
                .Select(ss => ss.Subject)
                .ToListAsync();
        }

        public async Task DeleteAsync(int studentId, int subjectId)
        {
            var studentSubject = await _context.StudentSubjects
                .FirstOrDefaultAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId);

            if (studentSubject != null)
            {
                _context.StudentSubjects.Remove(studentSubject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<StudentSubject> GetByIdsAsync(int studentId, int subjectId)
        {
            return await _context.StudentSubjects
                .FirstOrDefaultAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId);
        }
    }
}
