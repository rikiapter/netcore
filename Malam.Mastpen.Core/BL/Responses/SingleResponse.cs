
namespace Malam.Mastpen.Core.BL.Responses
{
    public class SingleResponse<TModel> : ISingleResponse<TModel> where TModel : new()
    {
        public SingleResponse()
        {
            Model = new TModel();
        }

        public string Message { get; set; }

        public bool DIdError { get; set; }

        public string ErrorMessage { get; set; }

        public TModel Model { get; set; }
    }
}
