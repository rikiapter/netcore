

using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.HR.Core.BL.Requests;
using System.Threading.Tasks;

namespace Malam.Mastpen.Core.BL.Contracts
{
    public interface IOrganizationService : IService
    {
        Task<SingleResponse<Organization>> GetOrganizationIdAsync(int Id);
        Task<SingleResponse<Organization>> CreateOrganizationAsync(Organization organization);
        Task<IPagedResponse<Organization>> GetOrganizationsAsync(int pageSize = 10, int pageNumber = 1, int? OrganizationId = null, string OrganizationName = null, int? IdentityNumber = null);

        Task<SingleResponse<OrganizationResponse>> GetOrganizationAsync(int Id);
        Task<Response> UpdateOrganizationAsync(Organization Organization);
        Task<SingleResponse<Organization>> GetOrganizationByOrganizationNameAsync(int Id);
        Task<Response> DeleteOrganizationAsync(int Id);
    }
}
