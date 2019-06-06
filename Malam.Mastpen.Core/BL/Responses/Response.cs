namespace Malam.Mastpen.Core.BL.Responses
{
    public class Response : IResponse
    {
        public string Message { get; set; }

        public bool DIdError { get; set; }

        public string ErrorMessage { get; set; }
    }
}