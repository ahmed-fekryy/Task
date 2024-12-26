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
    public class StudentRepository : IStudentRepository
    {

        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Student entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
                throw new ArgumentException("Student name is required.");

            // Check if student with the same name already exists
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.Name == entity.Name);
            if (existingStudent != null)
                throw new InvalidOperationException("A student with this name already exists.");

            // Add the student to the DbSet
            await _context.Students.AddAsync(entity);
          
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                throw new KeyNotFoundException("Student not found.");

            _context.Students.Remove(student); 

        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync(); 
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                throw new KeyNotFoundException("Student not found.");

            return student;
        }

        public async Task<Student> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Name == name); 

            if (student == null)
                throw new KeyNotFoundException($"No student found with the name {name}.");

            return student;
        }

        public async Task UpdateAsync(Student entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Student entity cannot be null");

            var existingStudent = await _context.Students.FindAsync(entity.Id);
            if (existingStudent == null)
                throw new KeyNotFoundException("Student not found.");

            // Update the existing student
            _context.Entry(existingStudent).CurrentValues.SetValues(entity);

            // Optionally validate other properties if necessary
            if (string.IsNullOrEmpty(entity.Name))
                throw new ArgumentException("Student name is required");

            // No need to explicitly call Update here since SetValues will handle the tracking.
        }
    }

}


