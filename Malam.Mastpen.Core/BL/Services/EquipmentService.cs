using Malam.Mastpen.Core.BL.Contracts;
using Malam.Mastpen.Core.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Malam.Mastpen.Core.BL.Services
{

    public class EquipmentService : Service, IEquipmentService
    {
        public EquipmentService(IUserInfo userInfo, MastpenBitachonDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }
    }
}