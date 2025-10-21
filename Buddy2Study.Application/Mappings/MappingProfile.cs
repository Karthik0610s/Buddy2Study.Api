using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using Buddy2Study.Application.Dtos;
using Buddy2Study.Domain.Entities;
namespace Buddy2Study.Application.Mappings
{
    /// <summary>
    /// Represents a AutoMapper mapping profile for mapping between entities and DTOs.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
    {
            // Define mappings between Brand and BrandDto

            CreateMap<ScholarshipApplicationForm, ScholarshipApplicationFormDto>().ReverseMap();
            CreateMap<EducationDto, EducationDetails>().ReverseMap();
            CreateMap<UserDto, Users>().ReverseMap();
            // CreateMap<UserDto, Users>().ReverseMap();
            CreateMap<Students, StudentDto>().ReverseMap();
            CreateMap<LoginUserDto, Users>().ReverseMap();
            CreateMap<SponsorDto, Sponsors>().ReverseMap();
            CreateMap<Scholarships, ScholarshipDto>().ReverseMap();



            CreateMap<InstitutionDto, Institution>().ReverseMap();


            CreateMap<FileUpload, FileUploadDto>().ReverseMap();
            


        }
    }
}
