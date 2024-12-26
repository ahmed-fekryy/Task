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
    public class SubjectRepository : ISubjectRepository
    {

        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Subject entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Subject entity cannot be null.");

            // Validate if required fields are present
            if (string.IsNullOrEmpty(entity.SubjectName))
                throw new ArgumentException("Subject name is required.");

            await _context.Subjects.AddAsync(entity);
 
        }


        public async Task DeleteAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                throw new KeyNotFoundException("Subject not found.");

            _context.Subjects.Remove(subject);

        }


        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }


        public async Task<Subject> GetByIdAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                throw new KeyNotFoundException("Subject not found.");

            return subject;
        }


        public async Task<Subject> GetByNameAsync(string name)
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Subject name cannot be null or empty.", nameof(name));

                var subject = await _context.Subjects
                    .FirstOrDefaultAsync(s => s.SubjectName == name);

                if (subject == null)
                    throw new KeyNotFoundException($"No subject found with the name {name}.");

                return subject;
            }


        public async Task UpdateAsync(Subject entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Subject entity cannot be null.");

            var existingSubject = await _context.Subjects.FindAsync(entity.Id);
            if (existingSubject == null)
                throw new KeyNotFoundException("Subject not found.");

            
            if (_context.Entry(existingSubject).State == EntityState.Detached)
            {
                _context.Subjects.Attach(existingSubject);
            }

           
            _context.Entry(existingSubject).CurrentValues.SetValues(entity);

            
            _context.Entry(existingSubject).State = EntityState.Modified;
        }
    }
}
