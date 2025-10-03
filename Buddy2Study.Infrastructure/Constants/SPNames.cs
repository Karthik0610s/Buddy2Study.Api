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

        public static string SP_GETUSERSALL = "";
        public static string SP_INSERTUSER = "";
        public static string SP_DELETEUSER = "";
        public static string SP_UPDATEUSER = "";
        public const string SP_UPDATEFILE = "sp_UpdateFile";
    }
}
