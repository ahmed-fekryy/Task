using Application.Service.Interface;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public  class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subject), "Subject cannot be null");

            // Check if the subject name is provided
            if (string.IsNullOrEmpty(subject.SubjectName))
                throw new ArgumentException("Subject name is required");

            await _unitOfWork.Subjects.AddAsync(subject);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteSubjectAsync(int subjectId)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(subjectId);
            if (subject == null)
                throw new KeyNotFoundException("Subject not found");

            await _unitOfWork.Subjects.DeleteAsync(subjectId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _unitOfWork.Subjects.GetAllAsync();
        }

        public async Task<Subject> GetSubjectByIdAsync(int subjectId)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(subjectId);
            if (subject == null)
                throw new KeyNotFoundException("Subject not found");

            return subject;
        }

        public async Task<Subject> GetSubjectByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Subject name cannot be null or empty");

            var subject = await _unitOfWork.Subjects.GetByNameAsync(name);
            if (subject == null)
                throw new KeyNotFoundException($"No subject found with the name {name}");

            return subject;
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subject), "Subject cannot be null.");

            await _unitOfWork.Subjects.UpdateAsync(subject);
            await _unitOfWork.SaveAsync();
        }
    }
}
