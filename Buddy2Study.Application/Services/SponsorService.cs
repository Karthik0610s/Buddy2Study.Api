using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Interfaces;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;

namespace Buddy2Study.Application.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _SponsorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SponsorService"/> class.
        /// </summary>
        /// <param name="SponsorRepository">The repository for accessing SponsorDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public SponsorService(ISponsorRepository SponsorRepository, IMapper mapper)
        {
            _SponsorRepository = SponsorRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<SponsorDto>> GetSponsorsDetails(int? id)
        {
            var Sponsors = await _SponsorRepository.GetSponsorsDetails(id);

            var SponsorDetails = _mapper.Map<IEnumerable<SponsorDto>>(Sponsors);
            return SponsorDetails;
        }
        /// <inheritdoc/>
        public async Task<SponsorDto> InsertSponsorDetails(SponsorDto SponsorDto)
        {

            var Sponsor = _mapper.Map<Sponsors>(SponsorDto);
            var enPassword = BCrypt.Net.BCrypt.HashPassword(Sponsor.PasswordHash);
            Sponsor.PasswordHash = enPassword;
            var insertedData = await _SponsorRepository.InsertSponsorDetails(Sponsor);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Sponsor insertion failed.");
            }
            return _mapper.Map<SponsorDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateSponsorDetails(SponsorDto SponsorDto)
        {
            var Sponsor = _mapper.Map<Sponsors>(SponsorDto);
            await _SponsorRepository.UpdateSponsorDetails(Sponsor);
        }
        /// <inheritdoc/>
       


    }

}