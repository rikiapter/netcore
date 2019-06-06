
using Malam.Mastpen.Core.BL.Requests;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL.Entities;
using System.Threading.Tasks;

namespace Malam.Mastpen.Core.BL.Contracts
{
    public interface ISiteService : IService
    {
        Task<SingleResponse<SiteResponse>> GetSiteAsync(int Id);
        Task<SingleResponse<SiteEmployee>> GetSitesByEmployeeIdAsync(int Id);


    }
}
