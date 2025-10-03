using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Domain.Entities
{
    public  class ScholarshipApplicationForm
    {
        public int Id { get; set; }


        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;


        public string StudyLevel { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
        public string CourseOrMajor { get; set; } = string.Empty;
        public int? YearOfStudy { get; set; }
        public string GPAOrMarks { get; set; } = string.Empty;


        public int ScholarshipId { get; set; } = 0;
        public string Category { get; set; } = string.Empty;
        public DateTime? ApplicationDate { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;


        public string ExtraCurricularActivities { get; set; } = string.Empty;
        public string AwardsAchievements { get; set; } = string.Empty;
        public string NotesComments { get; set; } = string.Empty;


        public string  Status { get; set; } = string.Empty;


        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;


        public List<string> Files { get; set; } = new();
    }
    
}
