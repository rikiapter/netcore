

using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL.Entities;
using System.Threading.Tasks;

namespace Malam.Mastpen.Core.BL.Contracts
{
    public interface IOrganizationService : IService
    {
        Task<SingleResponse<Organization>> GetOrganizationIdAsync(int Id);
        Task<SingleResponse<Organization>> CreateOrganizationAsync(Organization organization);
    }
}
