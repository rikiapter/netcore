
using Malam.Mastpen.Core.BL.Contracts;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.HR.Core.BL.Requests;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IPagedResponse<Organization>> GetOrganizationsAsync(int pageSize = 10, int pageNumber = 1, int? OrganizationId = null, string OrganizationName = null)
        {

            var response = new PagedResponse<Organization>();

            // Get the "proposed" query from repository
            var query = DbContext.GetOrganization(OrganizationId, OrganizationName);// אם רוצים לפי סינונים מסוימים אז יש להשתמש בפונקציה

            // Set paging values
            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.ItemsCount = await query.CountAsync();

            // Get the specific page from database
            // response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

            response.SetMessagePages(nameof(GetOrganizationsAsync), pageNumber, response.PageCount, response.ItemsCount);

            response.Model = await query
            .Paging(pageSize, pageNumber)
            .ToListAsync();

            // throw new NotImplementedException();
            return response;
        }


        public async Task<SingleResponse<OrganizationResponse>> GetOrganizationAsync(int Id)
        {
            var response = new SingleResponse<OrganizationResponse>();

            var query = DbContext.GetOrganizationsAsync(new Organization { OrganizationId = Id });

            response.Model = await query.FirstOrDefaultAsync();

            response.SetMessageGetById(nameof(GetOrganizationAsync), Id);
            return response;
        }

        public async Task<SingleResponse<Organization>> GetOrganizationByOrganizationNameAsync(int Id)
        {
            var response = new SingleResponse<Organization>();
            // Get the Organization by Id
            response.Model = await DbContext.GetOrganizationeByIdAsync(new Organization { OrganizationId = Id });

            return response;
        }

        // PUT
        public async Task<Response> UpdateOrganizationAsync(Organization Organization)
        {
            var response = new Response();

            // Update entity in repository
            DbContext.Update(Organization, UserInfo);

            response.Message = string.Format("Sucsses Put for Site Organization = {0} ", Organization.OrganizationId);
            // Save entity in database
            await DbContext.SaveChangesAsync();

            return response;
        }

        // DELETE
        public async Task<Response> DeleteOrganizationAsync(int Id)
        {

            var response = new Response();

            // Get Organization by Id
            var entity = await DbContext.GetOrganizationeByIdAsync(new Organization { OrganizationId = Id });

            // Remove entity from repository
            DbContext.Remove(entity);

            response.Message = string.Format("Sucsses Delete Site Organization = {0} ", Id);

            // Delete entity in database
            await DbContext.SaveChangesAsync();

            return response;
        }
    }
}