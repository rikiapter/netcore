
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Contracts;
using Malam.Mastpen.Core.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.BL.Requests;



namespace Malam.Mastpen.Core.BL.Services
{
    public class SiteService : Service, ISiteService
    {
        public SiteService(IUserInfo userInfo, MastpenBitachonDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<SingleResponse<SiteResponse>> GetSiteAsync(int Id)
        {
            var response = new SingleResponse<SiteResponse>();
            // Get the site by Id
            response.Model = await DbContext.GetSitesAsync(new Sites { SiteId = Id });

            response.SetMessageGetById(nameof(GetSiteAsync), Id);
            return response;
        }


        public async Task<SingleResponse<SiteEmployee>> GetSitesByEmployeeIdAsync(int Id)
        {
            var response = new SingleResponse<SiteEmployee>();
            // Get list by Employee by Id
            response.Model = await DbContext.GetSitesByEmployeeIdAsync(new SiteEmployee { EmployeeId = Id });

            response.SetMessageGetById(nameof(GetSitesByEmployeeIdAsync), Id);
            return response;
        }




    }


}
