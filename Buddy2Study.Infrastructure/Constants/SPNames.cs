using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Infrastructure.Constants
{
    public  class SPNames
    {
        public const string SP_GETALLSCHOLARSHIPAPPLICATIONFORM = "[sp_GetScholarshipApplications]";
        public const string SP_INSERTSCHOLARSHIPAPPLICATIONFORM = "[sp_InsertScholarshipApplication]";
        public const string SP_UPDATESCHOLARSHIPAPPLICATIONFORM = "sp_UpdateScholarshipApplication";
        public const string SP_DELETESCHOLARSHIPAPPLICATIONFORM = "[sp_DeleteScholarshipApplicationSimple]";

        public static string SP_GETUSERSALL = "sp_GetUsersAll";

        public static string SP_INSERTSTUDENT = "sp_InsertStudent";
        public static string SP_UPDATESTUDENT = "sp_UpdateStudent";
        public static string SP_GETALLSTUDENT = "sp_GetAllStudents";

        public const string SP_UPDATEFILE = "sp_UpdateFile";

        public static string SP_GETSPONSORSALL = "sp_GetAllSponsors";
        public static string SP_INSERTSPONSOR = "sp_InsertSponsor";
        public static string SP_UPDATESPONSOR = "sp_UpdateSponsor";

        public static string SP_GETINSTITUTIONSALL = "[sp_GetInstitutionsWithUser]";
        public static string SP_INSERTINSTITUTION = "sp_InsertInstitution";
        public static string SP_UPDATEINSTITUTION = "sp_UpdateInstitution";


        public const string SP_GETSCHOLARSHIPBYSTUDENT = "sp_GetScholarshipByStudent";
        public const string SP_GETSCHOLARSHIPBYSPONSOR = "sp_GetScholarshipBySponsor";
        public const string SP_INSERTSCHOLARSHIP = "sp_InsertScholarship";
        public const string SP_UPDATESCHOLARSHIP = "sp_UpdateScholarship";



    }
}
