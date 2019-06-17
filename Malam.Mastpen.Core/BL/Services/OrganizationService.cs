
using Malam.Mastpen.Core.BL.Contracts;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.Core.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Malam.Mastpen.Core.BL.Services
{

    public class OrganizationService : Service, IOrganizationService
    {
        public OrganizationService(IUserInfo userInfo, MastpenBitachonDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }


        public async Task<SingleResponse<Organization>> GetOrganizationIdAsync(int Id)
        {
            var response = new SingleResponse<Organization>();

            // Get query
            var query = DbContext.GetOrganizationeByIdAsync(new Organization { OrganizationId = Id });

            // Retrieve items, set model for response
            response.Model =  query.Result;

            response.SetMessageGetById(nameof(GetOrganizationIdAsync), Id);
            return response;
        }

        // POST
        public async Task<SingleResponse<Organization>> CreateOrganizationAsync(Organization organization)
        {
            var response = new SingleResponse<Organization>();

            // Add entity to repository
            DbContext.Add(organization, UserInfo);
            // Save entity in database
            await DbContext.SaveChangesAsync();

            response.SetMessageSucssesPost(nameof(CreateOrganizationAsync), organization.OrganizationId);
            // Set the entity to response model
            response.Model = organization;

            return response;
        }
    }
}