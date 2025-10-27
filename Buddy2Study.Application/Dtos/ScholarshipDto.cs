using System;
using System.Collections.Generic;

namespace Buddy2Study.Application.Dtos
{
    public class ScholarshipDto
    {
        public int Id { get; set; }
        public string ScholarshipCode { get; set; }            // Required
        public string ScholarshipName { get; set; }            // Required
        public string? ScholarshipType { get; set; }           // Nullable
        public string? Description { get; set; }               // Nullable
        public string? EligibilityCriteria { get; set; }       // Nullable
        public string? ApplicableCourses { get; set; }         // Nullable
        public string? ApplicableDepartments { get; set; }     // Nullable
        public decimal? MinPercentageOrCGPA { get; set; }      // Nullable
        public decimal? MaxFamilyIncome { get; set; }          // Nullable

        
        public string? ScholarshipAmount { get; set; }         // Nullable

        public bool IsRenewable { get; set; }                  // Not nullable
        public string? RenewalCriteria { get; set; }           // Nullable
        public DateTime? StartDate { get; set; }               // Nullable
        public DateTime? EndDate { get; set; }                 // Nullable
        public int? SponsorId { get; set; }                    // Nullable (in DB)
        public string? Status { get; set; }                    // Nullable
        public string? FileName { get; set; }                  // Nullable
        public string? FilePath { get; set; }                  // Nullable
        public string? CreatedBy { get; set; }                 // Nullable
        public DateTime? CreatedDate { get; set; }             // Nullable
        public string? ModifiedBy { get; set; }                // Nullable
        public DateTime? ModifiedDate { get; set; }            // Nullable
        public int? ScholarshipLimit { get; set; }             // Nullable

        // Newly added fields from DB
        public string? Documents { get; set; }                 // Nullable
        public string? Eligibility { get; set; }               // Nullable
        public string? WebportaltoApply { get; set; }          // Nullable
        public string? CanApply { get; set; }                  // Nullable
        public string? ContactDetails { get; set; }            // Nullable
        public List<string> Files { get; set; } = new();       // Optional
    }

    public class ScholarshipStatusDto
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

    public class FeaturedScholarshipDto
    {
        public string ScholarshipCode { get; set; }
        public string ScholarshipName { get; set; }
        public string LogoName { get; set; }
        public string LogoPath { get; set; }
        public DateTime Deadline { get; set; }
        public string FeaturedType { get; set; }
    }

}
