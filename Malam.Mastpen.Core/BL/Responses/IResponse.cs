namespace Malam.Mastpen.Core.BL.Responses
{
    public interface IResponse
    {
        string Message { get; set; }

        bool DIdError { get; set; }

        string ErrorMessage { get; set; }
    }
}
