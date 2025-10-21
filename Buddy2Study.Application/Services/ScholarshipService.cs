using AutoMapper;
using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Interfaces;

namespace Buddy2Study.Application.Services
{
    public class ScholarshipService : IScholarshipService
    {
        private readonly IScholarshipRepository _scholarshipRepository;
        private readonly IMapper _mapper;

        public ScholarshipService(IScholarshipRepository scholarshipRepository, IMapper mapper)
        {
            _scholarshipRepository = scholarshipRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get scholarship details based on Id and role (student/sponsor)
        /// </summary>
        public async Task<IEnumerable<ScholarshipDto>> GetScholarshipsDetails(int id, string role)
        {
            var scholarships = await _scholarshipRepository.GetScholarshipsDetails(id, role);
            return _mapper.Map<IEnumerable<ScholarshipDto>>(scholarships);
        }

        /// <summary>
        /// Insert a new scholarship record
        /// </summary>
        public async Task<ScholarshipDto> InsertScholarship(ScholarshipDto scholarshipDto)
        {
            var scholarship = _mapper.Map<Scholarships>(scholarshipDto);
            var insertedData = await _scholarshipRepository.InsertScholarship(scholarship);

            if (insertedData == null)
                throw new Exception("Scholarship insertion failed.");

            return _mapper.Map<ScholarshipDto>(insertedData);
        }

        /// <summary>
        /// Update an existing scholarship record
        /// </summary>
        public async Task UpdateScholarship(ScholarshipDto scholarshipDto)
        {
            var scholarship = _mapper.Map<Scholarships>(scholarshipDto);
            await _scholarshipRepository.UpdateScholarship(scholarship);
        }

        /// <summary>
        /// Delete a scholarship record by Id
        /// </summary>
        public async Task DeleteScholarship(int id, string modifiedBy)
        {
            await _scholarshipRepository.DeleteScholarship(id, modifiedBy);
        }
    }
}
