using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BikeSharingAPI.Helpers
{
    public static class HelperResponse
    {
        public static IActionResult GenerateResponse<T>(
            HttpStatusCode statusCode,
            T data
        )
        {
            if (data != null)
            {
                return new ContentResult
                {
                    StatusCode = 200,
                    Content = data.ToString()
                };
            }

            return new NoContentResult();
        } 
    }
}