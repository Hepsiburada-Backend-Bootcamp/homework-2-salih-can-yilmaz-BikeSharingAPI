using System;
using System.Net;
using BikeSharingAPI.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BikeSharingAPI.Helpers
{
    public static class HelperResponse
    {
        public static IActionResult GenerateResponse<T>(
            EnumResponseFormat format,
            HttpStatusCode statusCode,
            T data
        )
        {
            if (data != null)
            {
                return new ContentResult
                {
                    StatusCode = Convert.ToInt32(statusCode),
                    ContentType = "application/json",
                    Content = data.ToString()
                };
            }

            return new NoContentResult();
        } 
    }
}