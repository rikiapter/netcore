using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.Core.BL.Requests;
using Malam.Mastpen.API.Responses;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.API.Infrastructure;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.API.Clients.Contracts;
using Malam.Mastpen.Core.BL.Contracts;

namespace Malam.Mastpen.API.Controllers
{

#pragma warning disable CS1591
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EquipmentController : MastpenController
    {
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly IEquipmentService EquipmentService;
        public EquipmentController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 IEquipmentService equipmentService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            EquipmentService = equipmentService;
        }
    }

}