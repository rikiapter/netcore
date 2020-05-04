

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.Core.DAL.Dbo;

namespace Malam.Mastpen.Core.BL.Requests
{
#pragma warning disable CS1591



    public class AlertsResponse
    {
        public int AlertId { get; set; }
        public int? AlertTypeId { get; set; }
        public int? SiteId { get; set; }
        public int? EntityTypeId { get; set; }
        public int? EntityId { get; set; }
        public DateTime? Date { get; set; }
        public int? AlertStatus { get; set; }
        public DateTime? AlertValidDate { get; set; }
        public string Comment { get; set; }


        public ParameterCodeEntity CodeEntity { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }


        public AlertType AlertType { get; set; }
        public EntityType EntityType { get; set; }
        public Sites Site { get; set; }
    }

    public static class ExtensionsAlerts
    {

        public static AlertsResponse ToEntity(this Alerts request, ParameterCodeEntity CodeEntity)
        => new AlertsResponse
        {
            AlertId = request.AlertId,
            AlertTypeId = request.AlertTypeId,
            SiteId = request.SiteId,
            EntityTypeId = request.EntityTypeId,
            EntityId = request.EntityId,
            Date = request.Date,
            AlertStatus = request.AlertStatusId,
            AlertValidDate = request.AlertValidDate,
            Comment = request.Comment,


            CodeEntity = CodeEntity,
            UserInsert = request.UserInsert,
            DateInsert = request.DateInsert,
            UserUpdate = request.UserUpdate,
            DateUpdate = request.DateUpdate,
            State = request.State,


            AlertType = request.AlertType,
            EntityType = request.EntityType,
            Site = request.Site
        };


    }

}