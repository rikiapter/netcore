namespace Malam.Mastpen.Core.BL.Responses
{
    public class ResponseBasic : IResponse
    {
        public string Message { get; set; }

        public bool DIdError { get; set; }

        public string ErrorMessage { get; set; }
    }
}