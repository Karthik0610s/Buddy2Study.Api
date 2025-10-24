using System;

namespace Buddy2Study.Domain.Entities
{
    public class Scholarships
    {
        public int Id { get; set; }
        public string ScholarshipCode { get; set; }            // Not nullable
        public string ScholarshipName { get; set; }            // Not nullable
        public string? ScholarshipType { get; set; }           // Nullable
        public string? Description { get; set; }               // Nullable
        public string? EligibilityCriteria { get; set; }      // Nullable
        public string? ApplicableCourses { get; set; }        // Nullable
        public string? ApplicableDepartments { get; set; }    // Nullable
        public decimal? MinPercentageOrCGPA { get; set; }     // Nullable
        public decimal? MaxFamilyIncome { get; set; }         // Nullable
        public decimal? ScholarshipAmount { get; set; }       // Nullable
        public bool IsRenewable { get; set; }                 // Not nullable, default 0/false
        public string? RenewalCriteria { get; set; }          // Nullable
        public DateTime? StartDate { get; set; }              // Nullable
        public DateTime? EndDate { get; set; }                // Nullable
        public int SponsorId { get; set; }                    // Not nullable
        public string Status { get; set; }                    // Not nullable, default "Active"
        public string? FileName { get; set; }                 // Nullable
        public string? FilePath { get; set; }                 // Nullable
        public string CreatedBy { get; set; }                 // Not nullable
        public DateTime CreatedDate { get; set; }             // Not nullable
        public string? ModifiedBy { get; set; }               // Nullable
        public DateTime? ModifiedDate { get; set; }           // Nullable
        public int? ScholarshipLimit { get; set; }
        public List<string> Files { get; set; } = new();// Nullable
    }
    public class ScholarshipStatus
    {
        public int Id { get; set; }
        public string ScholarshipCode { get; set; }
        public string Name { get; set; }
        public string Eligibility { get; set; }
        public decimal Amount { get; set; }
        public string LogoName { get; set; }
        public string LogoPath { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DaysToGo { get; set; }
    }

    public class FeaturedScholarship
    {
        public string ScholarshipCode { get; set; }
        public string ScholarshipName { get; set; }
        public string LogoName { get; set; }
        public string LogoPath { get; set; }
        public DateTime Deadline { get; set; }
        public string FeaturedType { get; set; }
    }
}
