using AutoMapper;
using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Services
{
    public class InstitutionService:IInstitutionService
    {
        private readonly IInstitutionRepository _InstitutionRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionService"/> class.
        /// </summary>
        /// <param name="InstitutionRepository">The repository for accessing InstitutionDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public InstitutionService(IInstitutionRepository InstitutionRepository, IMapper mapper)
        {
            _InstitutionRepository = InstitutionRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<InstitutionDto>> GetInstitutionsDetails(int? id)
        {
            var Institutions = await _InstitutionRepository.GetInstitutionsDetails(id);

            var InstitutionDetails = _mapper.Map<IEnumerable<InstitutionDto>>(Institutions);
            return InstitutionDetails;
        }
        /// <inheritdoc/>
        public async Task<InstitutionDto> InsertInstitutionDetails(InstitutionDto InstitutionDto)
        {
            var Institution = _mapper.Map<Institution>(InstitutionDto);
            Institution.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Institution.PasswordHash);

            try
            {
                var insertedData = await _InstitutionRepository.InsertInstitutionDetails(Institution);

                if (insertedData == null)
                    throw new Exception("Institution insertion failed.");

                return _mapper.Map<InstitutionDto>(insertedData);
            }
            catch (Exception ex)
            {
                // Re-throw to controller so it can send error to frontend
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task UpdateInstitutionDetails(InstitutionDto InstitutionDto)
        {
            var Institution = _mapper.Map<Institution>(InstitutionDto);
            await _InstitutionRepository.UpdateInstitutionDetails(Institution);
        }
        /// <inheritdoc/>

    }
}
