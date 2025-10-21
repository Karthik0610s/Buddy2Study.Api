using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<ScholarshipDto>> GetScholarshipsDetails(int id, string role)
        {
            var scholarships = await _scholarshipRepository.GetScholarshipsDetails(id, role);
            return _mapper.Map<IEnumerable<ScholarshipDto>>(scholarships);
        }

        public async Task<ScholarshipDto> InsertScholarship(ScholarshipDto scholarshipDto)
        {
            var scholarship = _mapper.Map<Scholarships>(scholarshipDto);
            var inserted = await _scholarshipRepository.InsertScholarship(scholarship);

            if (inserted == null)
                throw new Exception("Insert failed");

            return _mapper.Map<ScholarshipDto>(inserted);
        }

        public async Task UpdateScholarship(ScholarshipDto scholarshipDto)
        {
            var scholarship = _mapper.Map<Scholarships>(scholarshipDto);
            await _scholarshipRepository.UpdateScholarship(scholarship);
        }

        public async Task<bool> DeleteScholarship(int id, string modifiedBy)
        {
            await _scholarshipRepository.DeleteScholarship(id, modifiedBy);
            return true;
        }
    }
}
