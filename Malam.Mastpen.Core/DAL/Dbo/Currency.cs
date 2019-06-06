﻿using System;

namespace Malam.Mastpen.Core.DAL.Dbo
{
    public class Currency : IAuditableEntity
    {
        public Currency()
        {
        }

        public string CurrencyID { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencySymbol { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }
        public int? UserInsert { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? DateInsert { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? UserUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? DateUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool? State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
