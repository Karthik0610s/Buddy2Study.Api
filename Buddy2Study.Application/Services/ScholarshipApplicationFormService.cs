using AutoMapper;
using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Interfaces;
using Buddy2Study.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Services
{
    public class ScholarshipApplicationFormService : IScholarshipApplicationFormService
    {
        private readonly IScholarshipApplicationFormRepository _repository;
        private readonly IMapper _mapper;
        public ScholarshipApplicationFormService(IScholarshipApplicationFormRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScholarshipApplicationFormDto>> GetScholarshipsApplicationForm(int? id)
        {
            //var students = await _studentRepository.GetStudentDetails(id);

            
            var scholarship =await _repository.GetScholarshipsApplicationForm(id);
            var scholarshipDetails = _mapper.Map<IEnumerable<ScholarshipApplicationFormDto>>(scholarship);
            return scholarshipDetails;
        }

        public async Task<ScholarshipApplicationFormDto> InsertScholarshipApplicationForm(ScholarshipApplicationFormDto dto)
        {
            //return await _repository.InsertScholarshipApplicationForm(dto);
            var scholarship = _mapper.Map<ScholarshipApplicationForm>(dto);
            var insertedData = await _repository.InsertScholarshipApplicationForm(scholarship);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Student insertion failed.");
            }
            return _mapper.Map<ScholarshipApplicationFormDto>(insertedData);
        }

        public async Task UpdateScholarshipApplicationForm(ScholarshipApplicationFormDto dto)
        {
            var scholarship = _mapper.Map<ScholarshipApplicationForm>(dto);
            await _repository.UpdateScholarshipApplicationForm(scholarship);
        }

        public async Task <bool> DeleteScholarshipApplicationForm(int id)
        {
            return await _repository.DeleteScholarshipApplicationForm(id);
        }

        public virtual async Task<string> UpdateFilepathdata(string target, int id, string files, string TypeofUser)
        {
            try
            {
                return await _repository.UpdateFilepathdata(target, id, files, TypeofUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
