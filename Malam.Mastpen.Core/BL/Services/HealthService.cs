﻿
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

namespace Malam.Mastpen.Core.BL.Services
{
    public class HealthService : Service
    {

        public HealthService(IUserInfo userInfo, MastpenBitachonDbContext dbContext, BlobStorageService blobStorageService)
            : base(userInfo, dbContext, blobStorageService)
        {

        }


        public async Task<IPagedResponse<EmployeeResponse>> GetEmployeesAsync(int pageSize = 10, int pageNumber = 1, int? EmployeeId = null, string EmployeeName = null, string IdentityNumber = null, int? OrganizationId = null, int? PassportCountryId = null, int? ProffesionType = null, int? SiteId = null, int? EmployeeIsNotInSiteId = null, bool isEmployeeEntry = false, bool sortByAuthtorization = false,
            bool sortByTraining = false,
            bool sortByWorkPermit = false,
            bool sortHealthDeclaration=false)//, int? SiteId = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var response = new PagedResponse<EmployeeResponse>();

            // Get the "proposed" query from repository
            var query = DbContext.GetEmployee(EmployeeId, EmployeeName, IdentityNumber, OrganizationId, PassportCountryId, ProffesionType, SiteId, EmployeeIsNotInSiteId, isEmployeeEntry, sortByAuthtorization, sortByTraining, sortByWorkPermit);// אם רוצים לפי סינונים מסוימים אז יש להשתמש בפונקציה

            // Set paging values

            // response.ItemsCount = await query.CountAsync();

            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.ItemsCount = await query.CountAsync();


            response.Model = await query
            .Paging(pageSize, pageNumber)
            .ToListAsync();

            
            //Distinct
            response.Model = response.Model.GroupBy(s => s.EmployeeId)
                                                 .Select(grp => grp.FirstOrDefault())
                                                 .ToList();

            //סינון לפי אב
            response.Model = response.Model.OrderBy(x => x.FirstName);

            //סינון לפי הדרכות וכו
            response.Model = sortByAuthtorization ? response.Model.OrderBy(x => x.EmployeeAuthtorization.regular) : response.Model;
            response.Model = sortByWorkPermit ? response.Model.OrderBy(x => x.EmployeeWorkPermit.regular) : response.Model;
            response.Model = sortByTraining ? response.Model.OrderBy(x => x.EmployeeTraining.regular) : response.Model;

            //סינון לפי הצהרת בריאות
            response.Model = sortHealthDeclaration  ? response.Model.OrderBy(x => x.isHealthDeclaration) : response.Model;

            //מי נמצא כרגע באתר
            response.Model = isEmployeeEntry && SiteId.HasValue ? response.Model.OrderByDescending(x => x.isEmployeeEntry) : response.Model;

            //מי משויך לאתר
            response.Model = SiteId.HasValue ? response.Model.Where(x => x.SiteId == SiteId) : response.Model;

            
            var rr = response.Model;
            //מי לא משויך לאתר
            if (EmployeeIsNotInSiteId.HasValue)
            {
                rr = response.Model.Where(x => x.SiteId == EmployeeIsNotInSiteId);

                foreach (var model in rr)
                    response.Model = response.Model.Where(x => x.EmployeeId != model.EmployeeId);
                //             response.Model = EmployeeIsNotInSiteId.HasValue ? response.Model.Where(x => x.SiteId != EmployeeIsNotInSiteId) : response.Model;
            }


            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.ItemsCount = response.Model.Count();
            response.SetMessagePages(nameof(GetEmployeesAsync), pageNumber, response.PageCount, response.ItemsCount);
            // throw new NotImplementedException();
            return response;
        }


        public async Task<IPagedResponse<EmployeeDeclarationResponse>> GetEmployeeHealteDeclarationAsync(int pageSize = 10, int pageNumber = 1, int? OrganizationId = null,  int? SiteId = null,
    bool isHealthDeclaration = false)//, int? SiteId = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var response = new PagedResponse<EmployeeDeclarationResponse>();

            // Get the "proposed" query from repository
            var query = DbContext.GetEmployeeHealteDeclaration(OrganizationId, SiteId, isHealthDeclaration);// אם רוצים לפי סינונים מסוימים אז יש להשתמש בפונקציה

            // Set paging values

            // response.ItemsCount = await query.CountAsync();

            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.ItemsCount = await query.CountAsync();


            response.Model = await query
            //.Paging(pageSize, pageNumber)
            .ToListAsync();


            //Distinct
            response.Model = response.Model.GroupBy(s => s.EmployeeId)
                                                 .Select(grp => grp.FirstOrDefault())
                                                 .ToList();

            //סינון לפי אב
            response.Model = response.Model.OrderBy(x => x.FirstName);

            //מי משויך לאתר
            response.Model = SiteId.HasValue ? response.Model.Where(x => x.SiteId == SiteId) : response.Model;


            var rr = response.Model;
           


            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.ItemsCount = response.Model.Count();
            response.SetMessagePages(nameof(GetEmployeesAsync), pageNumber, response.PageCount, response.ItemsCount);
            // throw new NotImplementedException();
            return response;
        }
        public async Task<ISingleResponse<EmployeeGuid>> GetEmployeeByPhone(string Phone = null)//, int? SiteId = null)//, int? SiteId = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var response = new SingleResponse<EmployeeGuid>();

            // Get the "proposed" query from repository
            var query = DbContext.GetEmployeesByPhoneAsync(Phone);//,  SiteId);// אם רוצים לפי סינונים מסוימים אז יש להשתמש בפונקציה



            response.Model = await query.FirstOrDefaultAsync();
            if(response.Model==null)
                response.Model= new EmployeeGuid();

            return response;
        }
        public async Task<ISingleResponse<List<EmployeeGuid>>> GetEmployeesByOrganizationAsync(int? OrganizationId = null)//, int? SiteId = null)//, int? SiteId = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var response = new SingleResponse<List<EmployeeGuid>>();

            // Get the "proposed" query from repository
            var query = DbContext.GetEmployeesByOrganizationAsync(OrganizationId);//,  SiteId);// אם רוצים לפי סינונים מסוימים אז יש להשתמש בפונקציה



            response.Model = await query.ToListAsync();


            return response;
        }
        public async Task<ISingleResponse<MainScreenHealthResponse>> GetMainScreenHealthAsync(int siteId)
        {

            var response = new SingleResponse<MainScreenHealthResponse>();
            // Get list by Employee by Id
            //var query = DbContext.GetNumberEmployeesOnSiteAsync(siteId);
            DateTime date = DateTime.Now.Date;//today

            response.Model = new MainScreenHealthResponse();

            var ListEmployees= await DbContext.GetNumberEmployeesAsync(siteId).ToListAsync();
            response.Model.NumberEmployees = ListEmployees.Count();
            response.Model.NumberEmployeesOnSite = await DbContext.GetNumberEmployeesOnSiteAsync(siteId, date).GroupBy(x => x.EmployeeId).CountAsync();
            response.Model.EmployeesWithHotBody = await DbContext.GetAlertsAsync(siteId, 2, 6).CountAsync();
      
            response.Model.NumberVisitors = await DbContext.GetHealthDeclarationAsync(siteId).CountAsync();
         

           //עובדים באתר
            var ListEmployeesOnSite = await DbContext.GetNumberEmployeesOnSiteAsync(siteId, date).ToListAsync();
           
            //עובדים שנתנו הצהרת בריאות
            var ListHealthDeclarationAsync = await DbContext.GetHealthDeclarationAsync(siteId,2).ToListAsync();

            //עובדים נמצאים באתר עם הצהרת בריאות
            var resultWithHealthDeclarationOnSite = from e in ListEmployeesOnSite
                         join h in ListHealthDeclarationAsync 
                         on e.EmployeeId equals h.EntityId 
                         select new { h.EntityId };

            response.Model.WithoutHealthDeclarationOnSite = ListEmployeesOnSite.Count() - resultWithHealthDeclarationOnSite.Count();


            //עובדים  עם הצהרת בריאות
            var resultWithHealthDeclaration = from e in ListEmployees
                         join h in ListHealthDeclarationAsync
                         on e.EmployeeId equals h.EntityId
                         select new { h.EntityId };


            //עובדים ללא הצהרת בריאות
            response.Model.WithoutHealthDeclaration = response.Model.NumberEmployees - resultWithHealthDeclaration.Count();


            //if (response.Model.NumberEmployeesOnSite != 0)
            //    response.Model.PresentEmployees = response.Model.NumberEmployees / response.Model.NumberEmployeesOnSite * 100;

            if (ListEmployees.Count() != 0)
                response.Model.PresentEmployees = resultWithHealthDeclaration.Count() / ListEmployees.Count() * 100;

            response.SetMessageGetById(nameof(MainScreenHealthResponse), siteId);
            return response;
        }

        
        // POST
        public async Task<SingleResponse<EmployeeTemperature>> CreateEmployeeTemperatureAsync(EmployeeTemperature temperature)
        {
            var response = new SingleResponse<EmployeeTemperature>();

            // Add entity to repository
            DbContext.Add(temperature, UserInfo);
            // Save entity in database
            await DbContext.SaveChangesAsync();

            response.Model = temperature;

            return response;
        }
        // POST
        public async Task<SingleResponse<HealthDeclaration>> CreateHealthDeclarationAsync(HealthDeclaration healthDeclaration)
        {
            var response = new SingleResponse<HealthDeclaration>();

            DbContext.Add(healthDeclaration, UserInfo);

            await DbContext.SaveChangesAsync();

            response.Model = healthDeclaration;

            return response;
        }
        // get
        public async Task<SingleResponse<HealthDeclaration>> GetHealthDeclarationAsync(HealthDeclaration healthDeclaration)
        {
            var response = new SingleResponse<HealthDeclaration>();
            response.Model = await DbContext.GetHealthDeclarationAsync(healthDeclaration).FirstOrDefaultAsync();
            return response;
        }

        // get
        public async Task<ListResponse<EmployeeResponse>> GetVisitHealthDeclarationAsync(int site)
        {
            var response = new ListResponse<EmployeeResponse>();
            response.Model = await DbContext.GetVisitHealthDeclarationAsync(new HealthDeclaration { SiteId = site }).ToListAsync();
            return response;
        }
    }
}