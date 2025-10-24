using System;

namespace Buddy2Study.Domain.Entities
{
    public class Scholarships
    {
        public int Id { get; set; }
        public string ScholarshipCode { get; set; }             // varchar(50) NOT NULL
        public string ScholarshipName { get; set; }             // varchar(150) NOT NULL
        public string? ScholarshipType { get; set; }            // varchar(50) NULL
        public string? Description { get; set; }                // varchar(1500) NULL
        public string? EligibilityCriteria { get; set; }        // varchar(1500) NULL
        public string? ApplicableCourses { get; set; }          // varchar(250) NULL
        public string? ApplicableDepartments { get; set; }      // varchar(250) NULL
        public decimal? MinPercentageOrCGPA { get; set; }       // decimal(4,2) NULL
        public decimal? MaxFamilyIncome { get; set; }           // decimal(12,2) NULL
        public string? ScholarshipAmount { get; set; }          // nvarchar(300) NULL
        public bool? IsRenewable { get; set; }                  // bit NULL (default 0)
        public string? RenewalCriteria { get; set; }            // varchar(300) NULL
        public DateTime? StartDate { get; set; }                // date NULL
        public DateTime? EndDate { get; set; }                  // date NULL
        public int? SponsorId { get; set; }                     // int NULL
        public string? Status { get; set; }                     // varchar(20) NULL (default 'Active')
        public string? FileName { get; set; }                   // varchar(255) NULL
        public string? FilePath { get; set; }                   // varchar(500) NULL
        public string? CreatedBy { get; set; }                  // varchar(150) NULL
        public DateTime? CreatedDate { get; set; }              // datetime NULL (default getdate())
        public string? ModifiedBy { get; set; }                 // varchar(150) NULL
        public DateTime? ModifiedDate { get; set; }             // datetime NULL
        public int? ScholarshipLimit { get; set; }              // int NULL
        public string? Documents { get; set; }                  // nvarchar(2000) NULL
        public string? Eligibility { get; set; }                // varchar(200) NULL
        public string? WebportaltoApply { get; set; }           // nvarchar(1500) NULL
        public string? CanApply { get; set; }                   // nvarchar(800) NULL
        public string? ContactDetails { get; set; }             // nvarchar(800) NULL
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
