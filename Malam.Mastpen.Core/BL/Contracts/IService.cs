using System;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.Core.DAL.Entities;

namespace Malam.Mastpen.Core.BL.Contracts
{
    public interface IService : IDisposable
    {
        MastpenBitachonDbContext DbContext { get; }

        Task<SingleResponse<Address>> GetAddressAsync(int EntityTypeId, int EntityId);
        Task<SingleResponse<PhoneMail>> CreatePhoneMailAsync(PhoneMail phoneMail, Type type);
    }
}
