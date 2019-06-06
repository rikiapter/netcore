
namespace Malam.Mastpen.Core.BL.Responses
{ 
    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }
}
