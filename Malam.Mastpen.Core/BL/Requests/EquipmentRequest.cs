using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.Core.DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Malam.Mastpen.Core.BL.Requests
{

    public class EquipmentAtSiteRequest
    {
        public int EquipmentAtSiteId { get; set; }

        public int? EquipmentId { get; set; }

        public int? SiteId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Comment { get; set; }

        public int? userInsert { get; set; }

        public DateTime? dateInsert { get; set; }

        public int? UserUpdate { get; set; }

        public DateTime? dateUpdate { get; set; }

        public bool? state { get; set; }

    }

    public class EquipmentRequest
    {
        public int EquipmentId { get; set; }

        public int? EquipmentTypeId { get; set; }

        public string ManufactureName { get; set; }

        public int? ManufactureSerialNumber { get; set; }

        public string Model { get; set; }

        public int? OrganizationId { get; set; }

        public int? EquipmentStatusTypeId { get; set; }

        public string Comment { get; set; }

        public int? userInsert { get; set; }

        public DateTime? dateInsert { get; set; }

        public int? UserUpdate { get; set; }

        public DateTime? dateUpdate { get; set; }

        public bool? state { get; set; }

    }
    public static class ExtensionsEquipment
    {
        public static EquipmenAtSite ToEntity(this EquipmentAtSiteRequest request, MastpenBitachonDbContext dbContext)
      => new EquipmenAtSite
      {
          EquipmentId = request.EquipmentId
      };

        public static Equipment ToEntity(this EquipmentRequest request, MastpenBitachonDbContext dbContext)
      => new Equipment
      {
          EquipmentId = request.EquipmentId
      };
    }
}