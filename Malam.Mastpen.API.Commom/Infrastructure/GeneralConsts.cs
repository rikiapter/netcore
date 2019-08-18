
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam.Mastpen.API.Commom.Infrastructure
{
    public static class GeneralConsts
    {
        public const string LOG_INVOKE = "'{0}' has been invoked";
        public const string LOG_SUCCESS = "The {0} have been retrieved successfully.";
        public const string HEADERS_DOESNT_EXIST = "Authorizaion header doesn't exist";
        public static class StoredProcedure
        {
            public const string SP_DIS_GET_DISABLED = "sp_DIS_GetDisabled";
            public const string SP_NECIM_SEARCH_GET = "spNechimSearchGet";
        }

        public  enum DocumentType
        {
            CopyofID=1,
            CopyPassport,
            CopyWorkPermit,
            Training,
            Authtorization,
            Signature,
            FaceImage,
            Note
        }
        public enum EntityTypeEnum
        {
            Sites=1,
            Employee,
            EmplyeePicture,
            EmployeeAuthtorization,
            EmployeeTraining,
            EmployeeWorkPermit,
            Note
        }

        public enum OrganizationTypeEnum
        {
            MainOrganization = 1,
            SecondaryOrganization

        }
    }

}
