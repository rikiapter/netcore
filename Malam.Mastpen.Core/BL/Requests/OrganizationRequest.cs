using Malam.Mastpen.Core.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Malam.Mastpen.HR.Core.BL.Requests
{
   public class OrganizationRequest : Organization
    {
        public FileRequest fileRequest { get; set; }
        public string uri { get; set; }

        public PhoneMail phonMail { get; set; }
    }




    public static class ExtensionsEmployee
    {


        //public static OrganizationRequest ToEntity(this Organization request)
        //    => new OrganizationRequest
        //    {

        //        OrganizationId = request.OrganizationId,
        //        //UserInsert = request.UserInsert,
        //        //DateInsert = request.DateInsert,
        //        //UserUpdate = request.UserUpdate,
        //        //DateUpdate = request.DateUpdate,
        //        //State = request.State,



        //    };

        public static OrganizationRequest ToEntity(this Organization request, PhoneMail phone, Docs docs)
            => new OrganizationRequest
            {
        
                OrganizationId = request.OrganizationId,
                OrganizationName=request.OrganizationName,
                OrganizationExpertiseType=request.OrganizationExpertiseType,
                OrganizationExpertiseTypeId=request.OrganizationExpertiseTypeId,
                OrganizationParentId=request.OrganizationParentId,
                OrganizationNumber=request.OrganizationNumber,
                OrganizationType=request.OrganizationType,
                OrganizationTypeId=request.OrganizationTypeId,
                Comment=request.Comment,

                uri = docs == null ? null : docs.DocumentPath,

                State = request.State,
                phonMail = phone,
      

            };

    }

}
