using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.BL.Responses
{
    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }
}
