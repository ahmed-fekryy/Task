using Application.Service.Interface;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddStudentAsync(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null");

       
            if (string.IsNullOrEmpty(student.Name))
                throw new ArgumentException("Student name is required");

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null)
                throw new KeyNotFoundException("Student not found.");

            await _unitOfWork.Students.DeleteAsync(studentId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null)
                throw new KeyNotFoundException("Student not found.");

            return student;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null");

            var existingStudent = await _unitOfWork.Students.GetByIdAsync(student.Id);
            if (existingStudent == null)
                throw new KeyNotFoundException("Student not found.");

            if (string.IsNullOrEmpty(student.Name))
                throw new ArgumentException("Student name is required");
   
            await _unitOfWork.Students.UpdateAsync(student);
           
            await _unitOfWork.SaveAsync();
        }


    }
}


