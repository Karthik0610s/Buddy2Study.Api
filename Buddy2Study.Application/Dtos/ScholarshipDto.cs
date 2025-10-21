using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Dtos
{
    public class ScholarshipDto
    {
        public int Id { get; set; }
        public string ScholarshipCode { get; set; }
        public string ScholarshipName { get; set; }
        public string ScholarshipType { get; set; }
        public string Description { get; set; }
        public string EligibilityCriteria { get; set; }
        public string ApplicableCourses { get; set; }
        public string ApplicableDepartments { get; set; }
        public decimal? MinPercentageOrCGPA { get; set; }
        public decimal? MaxFamilyIncome { get; set; }
        public decimal ScholarshipAmount { get; set; }
        public bool IsRenewable { get; set; }
        public string? RenewalCriteria { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SponsorId { get; set; }
        public string Status { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ScholarshipLimit { get; set; }
    }
}


