using System;
using Microsoft.Extensions.Logging;

namespace Malam.Mastpen.Core.BL.Responses
{
    public static class ResponseExtensions
    {
        //public static voId SetError(this IResponse response,string actionName, Exception ex)
        //{
        //    // todo: Save error in log file

        //    response.DIdError = true;

        //    if (ex is MastpenException cast)
        //    {
        //        logger?.LogError("There was an error on '{0}': {1}", actionName, ex);

        //        response.ErrorMessage = ex.Message;
        //    }
        //    else
        //    {
        //        logger?.LogCritical("There was a critical error on '{0}': {1}", actionName, ex);

        //        response.ErrorMessage = "There was an internal error, please contact to technical support.";
        //    }


        //}

        public static void SetMessagePages(this IResponse response,string actionName,  int pageNumber,double PageCount,int ItemsCount)
        {
            // todo: Save error in log file

            response.DIdError = false;

            response.Message = string.Format("Page {0} of {1}, Total of products: {2}.", pageNumber, PageCount, ItemsCount);

        }


        public static void SetMessageGetById(this IResponse response, string actionName, int Id)
        {
            // todo: Save error in log file

            response.DIdError = false;

            response.Message = string.Format("Find Result for {0} = {1} ", actionName, Id);

        }

        public static void SetMessageSucssesPut(this IResponse response, string actionName, int Id)
        {
            // todo: Save error in log file

            response.DIdError = false;

            response.Message = string.Format("Sucsses Update {0} = {1} ", actionName, Id);

        }

        public static void SetMessageSucssesPost(this IResponse response, string actionName, int Id)
        {
            // todo: Save error in log file

            response.DIdError = false;

            response.Message = string.Format("Sucsses Create {0} = {1} ", actionName, Id);

        }

        public static void SetMessageSucssesDelete(this IResponse response, string actionName, int Id)
        {
            // todo: Save error in log file

            response.DIdError = false;

            response.Message = string.Format("Sucsses Delete {0} = {1} ", actionName, Id);

        }

    }
}
