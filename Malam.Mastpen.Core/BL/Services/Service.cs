using Microsoft.Extensions.Logging;
using Malam.Mastpen.Core.BL.Contracts;
using Malam.Mastpen.Core.DAL;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL.Entities;
using System;

namespace Malam.Mastpen.Core.BL.Services
{
    public abstract class Service : IService
    {
        protected bool Disposed;
        protected IUserInfo UserInfo;

        public Service(IUserInfo userInfo, MastpenBitachonDbContext dbContext)
        {
            UserInfo = userInfo;
            DbContext = dbContext;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                DbContext?.Dispose();
                Disposed = true;
            }
        }

        public MastpenBitachonDbContext DbContext { get; }


        public async Task<SingleResponse<Address>> GetAddressAsync(int EntityTypeId, int EntityId)
        {
            var response = new SingleResponse<Address>();
            // Get the Employee by Id
            response.Model = await DbContext.GetAddressAsync(new Address { EntityTypeId = EntityTypeId, EntityId = EntityId });

            response.SetMessageGetById(nameof(GetAddressAsync), EntityTypeId);
            return response;
        }



        public async Task<SingleResponse<PhoneMail>> CreatePhoneMailAsync(PhoneMail phoneMail, Type type)
        {
            var response = new SingleResponse<PhoneMail>();

            var EntityTypeId =  DbContext.GetEntityTypeIdByEntityTypeName( type).Result;

            phoneMail.EntityTypeId = EntityTypeId;

            DbContext.Add(phoneMail, UserInfo);

            await DbContext.SaveChangesAsync();

            response.SetMessageSucssesPost(nameof(CreatePhoneMailAsync), phoneMail.EntityId??0);

            response.Model = phoneMail;

            return response;
        }
    }
}
