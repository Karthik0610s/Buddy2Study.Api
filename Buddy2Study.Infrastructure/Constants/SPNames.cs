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
        public static string SP_DELETEUSER = "";
        public static string SP_UPDATESTUDENT = "sp_UpdateStudent";
        public static string SP_UPDATEUSER = "";
        public const string SP_UPDATEFILE = "sp_UpdateFile";

        public static string SP_GETSPONSORSALL = "sp_GetAllSponsors";
        public static string SP_INSERTSPONSOR = "sp_InsertStudent";
        public static string SP_UPDATESPONSOR = "sp_UpdateStudent";

        public static string SP_GETINSTITUTIONSALL = "[sp_GetInstitutionsWithUser]";
        public static string SP_INSERTINSTITUTION = "sp_InsertInstitution";
        public static string SP_UPDATEINSTITUTION = "sp_UpdateInstitution";


    }
}
