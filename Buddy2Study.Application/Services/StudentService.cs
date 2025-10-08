using AutoMapper;
using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Interfaces;

namespace Buddy2Study.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsDetails(int? id)
        {
            var students = await _studentRepository.GetStudentsDetails(id);
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> InsertStudentDetails(StudentDto studentDto)
        {
            var student = _mapper.Map<Students>(studentDto);

            // ✅ Encrypt password before saving
            if (!string.IsNullOrEmpty(student.PasswordHash))
            {
                student.PasswordHash = BCrypt.Net.BCrypt.HashPassword(student.PasswordHash);
            }

            var insertedData = await _studentRepository.InsertStudentDetails(student);
            if (insertedData == null)
                throw new Exception("Student insertion failed.");

            return _mapper.Map<StudentDto>(insertedData);
        }

        public async Task UpdateStudentDetails(StudentDto studentDto)
        {
            var student = _mapper.Map<Students>(studentDto);
            await _studentRepository.UpdateStudentDetails(student);
        }
    }
}