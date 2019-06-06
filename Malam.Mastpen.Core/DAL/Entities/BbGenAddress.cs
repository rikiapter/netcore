using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public int? EntityTypeId { get; set; }
        public int? EntityId { get; set; }
        public int? CityId { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string EntranceNo { get; set; }
        public string AptNo { get; set; }
        public int? Pob { get; set; }
        public int? ZipCode { get; set; }
        public string Comments { get; set; }
        public string CoorX { get; set; }
        public string CoorY { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public City City { get; set; }
        public EntityType Entity { get; set; }
        public EntityType EntityType { get; set; }
    }
}
