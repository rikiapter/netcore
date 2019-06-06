using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.Core.DAL;

namespace Malam.Mastpen.Core.BL.Requests
{
#pragma warning disable CS1591
    public class AddressRequest
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        public int? EntityTypeId { get; set; }
        [Required]
        public int? EntityId { get; set; }
        [Required]
        public int? CityId { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public int? HouseNumber { get; set; }
        [Required]
        public string EntranceNo { get; set; }
        [Required]
        public string AptNo { get; set; }
        [Required]
        public int? POB { get; set; }
        [Required]
        public int? ZipCode { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public string CoorX { get; set; }
        [Required]
        public string CoorY { get; set; }
    }

    public class DocsRequest
    {
        [Key]
        public int DocumentId { get; set; }
        [Required]
        public int? DocumentTypeId { get; set; }

        public int? EntityTypeId { get; set; }

        public int? EntityId { get; set; }

        public string DocumentPath { get; set; }

        public DateTime? DocumentDate { get; set; }

        public bool? IsDocumentSigned { get; set; }

        public DateTime? ValIdDate { get; set; }

        public int? LanguageId { get; set; }

    }
    public class DocTypeEntityRequest
    {
        public int DocumentEntityId { get; set; }

        public int? DocumentTypeId { get; set; }

        public int? EntityTypeId { get; set; }

    }

    public class NotesRequest
    {
        public int NoteId { get; set; }

        public int? EntityTypeId { get; set; }

        public int? EntityId { get; set; }

        public int? NoteTypeId { get; set; }

        public string NoteContent { get; set; }
    }

    public class OrganizationRequest
    {
        public int OrganizationId { get; set; }

        public int? OrganizationTypeId { get; set; }

        public string OrganizationName { get; set; }

        public int? OrganizationNumber { get; set; }

        public int? OrganizationParentId { get; set; }

    }

    public class PhoneMailRequest
    {
        public int ContactId { get; set; }

        public int? EntityTypeId { get; set; }

        public int? EntityId { get; set; }

        public int? PhoneTypeId { get; set; }

        public string PhoneNumber { get; set; }

        public string EMail { get; set; }
    }

    public class SiteRoleRequest
    {
        public int SiteRoleId { get; set; }

        public int? SiteId { get; set; }

        public int? EmployeeId { get; set; }

        public int? SiteRoleTypeId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Comment { get; set; }
    }

    public class SitesRequest
    {
        public int SiteId { get; set; }

        public string SiteName { get; set; }

        public DateTime? SiteActivityStartDate { get; set; }

        public int? SiteTypeId { get; set; }

        public int? SiteStatusId { get; set; }

        public int? OrganizationId { get; set; }
        public string Comment { get; set; }
    }
    public static class ExtensionsAddress
    {
        public static Address ToEntity(this AddressRequest request, MastpenBitachonDbContext dbContext)
            => new Address
            {
                AddressId = request.AddressId,
                EntityTypeId = request.EntityTypeId,
                EntityId = request.EntityId,
                CityId = request.CityId,
                StreetName = request.StreetName,
                HouseNumber = request.HouseNumber,
                EntranceNo = request.EntranceNo,
                AptNo = request.AptNo,
                Pob = request.POB,
                ZipCode = request.ZipCode,
                Comments = request.Comments,
                CoorX = request.CoorX,
                CoorY = request.CoorY

            };
    }

    public static class ExtensionsDocs
    {
        public static Docs ToEntity(this DocsRequest request, MastpenBitachonDbContext dbContext)
            => new Docs
            {
                DocumentId=request.DocumentId,
                EntityTypeId = request.EntityTypeId,
                EntityId = request.EntityId,

            };
    }

    public static class ExtensionsDocTypeEntity
    {
        public static DocTypeEntity ToEntity(this DocTypeEntityRequest request, MastpenBitachonDbContext dbContext)
            => new DocTypeEntity
            {
                EntityTypeId = request.EntityTypeId,

            };
    }

    public static class ExtensionsNotes
    {
        public static Notes ToEntity(this NotesRequest request, MastpenBitachonDbContext dbContext)
            => new Notes
            {
                EntityTypeId = request.EntityTypeId,

            };
    }

    public static class ExtensionsOrganization
    {
        public static Organization ToEntity(this OrganizationRequest request, MastpenBitachonDbContext dbContext)
            => new Organization
            {
                OrganizationId = request.OrganizationId,

            };
    }
    public static class ExtensionsPhoneMail
    {
        public static PhoneMail ToEntity(this PhoneMailRequest request, MastpenBitachonDbContext dbContext)
            => new PhoneMail
            {
                EntityId = request.EntityId,

            };
    }

    public static class ExtensionsSiteRole
    {
        public static SiteRole ToEntity(this SiteRoleRequest request, MastpenBitachonDbContext dbContext)
            => new SiteRole
            {
                SiteRoleId = request.SiteRoleId,

            };
    }
    public static class ExtensionsSites
    {
        public static Sites ToEntity(this SitesRequest request, MastpenBitachonDbContext dbContext)
            => new Sites
            {
                SiteId = request.SiteId,

            };
    }
#pragma warning restore CS1591
}
