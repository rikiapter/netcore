
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
using System.Collections.Generic;
using Malam.Mastpen.Core.DAL.Dbo;

namespace Malam.Mastpen.Core.BL.Services
{
    public class AlertService : Service
    {

        public AlertService(IUserInfo userInfo, MastpenBitachonDbContext dbContext, BlobStorageService blobStorageService)
            : base(userInfo, dbContext, blobStorageService)
        {

        }
        public async Task<ListResponse<AlertsResponse>> GetAlertAsync(int siteId, int ModuleID)
        {


            var response = new ListResponse<AlertsResponse>();
            var list = new List<AlertsResponse>();
            var listAlert =  DbContext.GetAlertsAsync( siteId, ModuleID);

            //יצירת רשימה חדשה הכוללת בתוכה גם את המזהה והשם של נושא הערה
            foreach (var alert in listAlert)
            {
                if (alert != null)
                {
                    var codeentity = DbContext.GetEntityTable(alert.EntityType.EntityTypeName, alert.EntityId ?? 0);
                    list.Add(alert.ToEntity(codeentity == null ? new ParameterCodeEntity() : codeentity.FirstOrDefault()));
                }
            }


                response.Model = list;

            response.SetMessageGetById(nameof(GetAlertAsync), siteId);
            return response;
        }

        public async Task<SingleResponse<Alerts>> GetAlertAsync(Alerts alert)
        {


            var response = new SingleResponse<Alerts>();



            response.Model = DbContext.GetAlertsAsync(alert).FirstOrDefault() ;

         
            return response;
        }
        public async Task<SingleResponse<Alerts>> CreateAlertAsync(Alerts Alert)
        {
            var response = new SingleResponse<Alerts>();
            // Get the Employee by Id
            var res = DbContext.Add(Alert);
            await DbContext.SaveChangesAsync();
            response.Model = Alert;
            response.SetMessageGetById(nameof(CreateAlertAsync), Alert.SiteId??0);
            return response;
        }


        // PUT
        public async Task<ResponseBasic> UpdateAlertAsync(Alerts Alert)
        {
            var response = new ResponseBasic();


            var entity = await DbContext.Alerts.FirstOrDefaultAsync(item => item.AlertId == Alert.AlertId);
            if (entity != null)
            {
                entity.Comment = Alert.Comment;
                entity.AlertStatusId = Alert.AlertStatusId;
       

                DbContext.Update(entity, UserInfo);

                await DbContext.SaveChangesAsync();
            }

            response.Message = string.Format("Sucsses Put for  Alert = {0} ", Alert.AlertId);

            return response;
        }


    }
}
