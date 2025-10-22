using System;

namespace Buddy2Study.Application.Dtos
{
    public class ScholarshipDto
    {
        public int Id { get; set; }
        public string ScholarshipCode { get; set; }            // Required
        public string ScholarshipName { get; set; }            // Required
        public string? ScholarshipType { get; set; }           // Nullable
        public string? Description { get; set; }               // Nullable
        public string? EligibilityCriteria { get; set; }      // Nullable
        public string? ApplicableCourses { get; set; }        // Nullable
        public string? ApplicableDepartments { get; set; }    // Nullable
        public decimal? MinPercentageOrCGPA { get; set; }     // Nullable
        public decimal? MaxFamilyIncome { get; set; }         // Nullable
        public decimal? ScholarshipAmount { get; set; }       // Nullable
        public bool IsRenewable { get; set; }                 // Not nullable
        public string? RenewalCriteria { get; set; }          // Nullable
        public DateTime? StartDate { get; set; }              // Nullable
        public DateTime? EndDate { get; set; }                // Nullable
        public int SponsorId { get; set; }                    // Required
        public string Status { get; set; }                    // Required
        public string? FileName { get; set; }                 // Nullable
        public string? FilePath { get; set; }                 // Nullable
        public string? CreatedBy { get; set; }                // Nullable now
        public DateTime? CreatedDate { get; set; }            // Nullable
        public string? ModifiedBy { get; set; }               // Nullable
        public DateTime? ModifiedDate { get; set; }           // Nullable
        public int? ScholarshipLimit { get; set; }            // Nullable
    }
}
