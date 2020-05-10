
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Contracts;
using Malam.Mastpen.Core.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.BL.Requests;
using Malam.Mastpen.Core.DAL;
using IdentityModel.Client;
using System.Net.Http;

namespace Malam.Mastpen.Core.BL.Services
{
    public class ErrorService : Service
    {

        public ErrorService(IUserInfo userInfo, MastpenBitachonDbContext dbContext, BlobStorageService blobStorageService)
            : base(userInfo, dbContext, blobStorageService)
        {

        }
        public async Task<SingleResponse<BbError>> CreateErrorAsync(BbError error)
        {
            var response = new SingleResponse<BbError>();
            // Get the Employee by Id
            var res = DbContext.Add(error);
            await DbContext.SaveChangesAsync();
            response.Model = error;
            return response;
        }

    }
}