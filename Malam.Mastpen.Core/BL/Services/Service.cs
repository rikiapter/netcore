using Microsoft.Extensions.Logging;
using Malam.Mastpen.Core.BL.Contracts;
using Malam.Mastpen.Core.DAL;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL.Entities;
using System;
using Malam.Mastpen.Core.BL.Requests;
using static Malam.Mastpen.API.Commom.Infrastructure.GeneralConsts;
using Malam.Mastpen.HR.Core.BL.Requests;
using System.Linq;

namespace Malam.Mastpen.Core.BL.Services
{
    public abstract class Service : IService
    {
        protected bool Disposed;
        protected IUserInfo UserInfo;
        protected readonly BlobStorageService blobStorageService;
        public Service(IUserInfo userInfo, MastpenBitachonDbContext dbContext, BlobStorageService blobStorageService)
        {
            UserInfo = userInfo;
            DbContext = dbContext;
            this.blobStorageService = blobStorageService;
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


        public async Task<SingleResponse<PhoneMail>> GetPhoneMailAsync(int EntityTypeId, int EntityId)
        {
            var response = new SingleResponse<PhoneMail>();
            // Get the Employee by Id
            response.Model = await DbContext.GetPhoneMailAsync(new PhoneMail { EntityTypeId = EntityTypeId, EntityId = EntityId });

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

        public async Task<SingleResponse<PhoneMail>> UpdatePhoneMailAsync(PhoneMail phoneMail, Type type)
        {
            var response = new SingleResponse<PhoneMail>();


            var EntityTypeId = DbContext.GetEntityTypeIdByEntityTypeName(type).Result;

            phoneMail.EntityTypeId = EntityTypeId;


           var entity= GetPhoneMailAsync((int)phoneMail.EntityTypeId, (int)phoneMail.EntityId);
            if (entity.Result.Model != null)
            {
                entity.Result.Model.Email = phoneMail.Email;
                entity.Result.Model.PhoneNumber = phoneMail.PhoneNumber;
                entity.Result.Model.PhoneTypeId = phoneMail.PhoneTypeId;

                DbContext.Update(entity.Result.Model, UserInfo);

                await DbContext.SaveChangesAsync();
            }
            response.SetMessageSucssesPost(nameof(CreatePhoneMailAsync), phoneMail.EntityId ?? 0);

            response.Model = phoneMail;

            return response;
        }

        public async Task<SingleResponse<Docs>> GetDocsAsync(int EntityTypeId, int EntityId,int documentType)
        {
            var response = new SingleResponse<Docs>();
            // Get the Employee by Id
             var res= await  DbContext.GetDocsAsync(new Docs { EntityTypeId = EntityTypeId, EntityId = EntityId ,DocumentTypeId=documentType});
            response.Model =  res;
            response.SetMessageGetById(nameof(GetAddressAsync), EntityTypeId);
            return response;
        }
        public async Task<SingleResponse<Docs>> CreateDocsAsync(Docs docs, Type type, string fileName, FileRequest file, int documentType)
        {
            var query =  DbContext.GetDocsAsync(new Docs { EntityTypeId = (int)docs.EntityTypeId, EntityId = (int)docs.EntityId, DocumentTypeId = documentType });

            var docExist = await query;
            docs.DocumentTypeId = documentType;
            if (docExist!= null)
            {
                //למחוק את הכתובת הקודמת מהבלוב
                //  blobStorageService.DeleteBlobData(docExist.Result.Model.DocumentPath);
                docs.DocumentPath = blobStorageService.UploadFileToBlob(fileName, file);

                return await UpdateDocsAsync(docs, type);

            }
            else if (file != null)
            {
                if (!string.IsNullOrEmpty(file.ContentType) || !string.IsNullOrEmpty(file.ContentType))
                {
                    var response = new SingleResponse<Docs>();

                    docs.DocumentPath = blobStorageService.UploadFileToBlob(fileName, file);

                    var EntityTypeId = DbContext.GetEntityTypeIdByEntityTypeName(type).Result;

                    docs.EntityTypeId = EntityTypeId;

                    DbContext.Add(docs, UserInfo);

                    await DbContext.SaveChangesAsync();

                    response.SetMessageSucssesPost(nameof(CreateDocsAsync), docs.EntityId ?? 0);

                    response.Model = docs;

                    return response;
                }
                else return new SingleResponse<Docs>();
            }
            else return new SingleResponse<Docs>();
         
          
        }
        public async Task<SingleResponse<Docs>> UpdateDocsAsync(Docs docs, Type type)
        {
            var response = new SingleResponse<Docs>();

            var EntityTypeId = DbContext.GetEntityTypeIdByEntityTypeName(type).Result;

            docs.EntityTypeId = EntityTypeId;


            var entity = GetDocsAsync((int)docs.EntityTypeId, (int)docs.EntityId,(int)docs.DocumentTypeId);
            if (entity.Result.Model != null)
            {
                entity.Result.Model.DocumentPath = docs.DocumentPath;
                entity.Result.Model.IsDocumentSigned =docs.IsDocumentSigned;
                entity.Result.Model.LanguageId = docs.LanguageId;

                DbContext.Update(entity.Result.Model, UserInfo);

                await DbContext.SaveChangesAsync();
            }


            response.SetMessageSucssesPost(nameof(UpdateDocsAsync), docs.EntityId ?? 0);

            response.Model = docs;

            return response;
        }


        public async Task<SingleResponse<EmployeeProffesionType>> CreateEmployeeProffesionTypeAsync(EmployeeProffesionType proffesionType, Type type)
        {
            var response = new SingleResponse<EmployeeProffesionType>();

            DbContext.Add(proffesionType, UserInfo);

            await DbContext.SaveChangesAsync();

            response.SetMessageSucssesPost(nameof(CreateEmployeeProffesionTypeAsync), proffesionType.EmployeeAuthorizationId );

            response.Model = proffesionType;

            return response;
        }

        public async Task<SingleResponse<EmployeeProffesionType>> UpdateProffesionTypeAsync(EmployeeProffesionType proffesionType, Type type)
        {
            var response = new SingleResponse<EmployeeProffesionType>();


            var entity = DbContext.EmployeeProffesionType.FirstOrDefault(item => item.EmployeeId == proffesionType.EmployeeId && item.ProffesionTypeId == proffesionType.ProffesionTypeId);
            if (entity != null)
            {
                entity.ProffesionTypeId = proffesionType.ProffesionTypeId;

                DbContext.Update(entity, UserInfo);

                await DbContext.SaveChangesAsync();
            }
            response.SetMessageSucssesPost(nameof(CreatePhoneMailAsync), proffesionType.EmployeeAuthorizationId);

            response.Model = proffesionType;

            return response;
        }
    }
}
