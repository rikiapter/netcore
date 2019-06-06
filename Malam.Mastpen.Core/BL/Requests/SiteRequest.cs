

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.Core.DAL;

namespace Malam.Mastpen.Core.BL.Requests
{
#pragma warning disable CS1591



    public class SiteRequest: Sites
    {
 
    }

    public class SiteResponse : Sites
    {
        public Address Addresses { get; set; }
    }






    public static class ExtensionsSite
    {
        public static SiteResponse ToEntity(this Sites request,Address address)
        => new SiteResponse
        {
            SiteId = request.SiteId,
            SiteName=request.SiteName,
            SiteActivityStartDate=request.SiteActivityStartDate,
            SiteTypeId=request.SiteTypeId,
            SiteStatusId=request.SiteStatusId,
            OrganizationId=request.OrganizationId,
            Comment =request.Comment,
            Organization=request.Organization,
            SiteStatus=request.SiteStatus,
            SiteType=request.SiteType,
            EquipmenAtSite=request.EquipmenAtSite,
            SiteRole=request.SiteRole,
            Addresses=address
        };


    }



#pragma warning restore CS1591
}
