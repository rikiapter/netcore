
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

    public class GeneralService : Service, IGeneralService
    {
        public GeneralService(IUserInfo userInfo, MastpenBitachonDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        //public async Task<SingleResponse<Address>> GetAddressAsync(int EntityTypeId,int EntityId)
        //{
        //    var response = new SingleResponse<Address>();
        //    // Get the Employee by Id
        //    response.Model = await DbContext.GetAddressAsync(new Address { EntityTypeId = EntityTypeId,EntityId=EntityId });

        //    response.SetMessageGetById(nameof(GetAddressAsync), EntityTypeId);
        //    return response;
        //}
    }
}