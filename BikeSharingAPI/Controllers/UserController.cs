using AutoMapper;
using BikeSharing.Application.DTOs.Users;
using BikeSharing.Application.Services.IServices;
using BikeSharing.Application.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeSharing.Domain.Enums;
using BikeSharing.Application;
using Serilog;
using BikeSharingAPI.Enums;
using BikeSharingAPI.Helpers;
using BikeSharingAPI.Models;
using System.Net;

namespace BikeSharingAPI.Controllers
{
    [Route("api/v1/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            this._UserService = userService;
            
        }

        /// <summary>
        /// Tum kullanicilarin bir listesini doner.
        /// </summary>
        /// <returns>Json formatinda kullanici listesi.</returns>
        [HttpGet]
        public IActionResult GetUserList(
            string filter = "",
            string orderByParams = "",
            string fields = ""
            )
        {
            Log.Information(SharedData.LogMessageRequestReceived);
            try
            {
                List<UserReadDTO> userReadDTOs = _UserService.GetUsers(filter, orderByParams, fields);

                if(userReadDTOs != null && userReadDTOs.Count > 0)
                {
                    return Ok(userReadDTOs);
                }
                else
                {
                    return NotFound(SharedData.MessageUsersNotFound);
                }
            }

            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return HelperResponse.GenerateResponse(
                    EnumResponseFormat.JSON,
                    HttpStatusCode.InternalServerError,
                    new ErrorModel { ErrorMessage = ex.Message }
                    );
            }
        }

        /// <summary>
        /// Id'si verilen kullaniciyi doner.
        /// </summary>
        /// <param name="id">Kullanici id'si</param>
        /// <returns>Eger bulursa, kullanici bilgileri json formatinda.</returns>
        [Route("{id}")]
        [Route("{id}/Sessions")]
        [HttpGet]
        public IActionResult GetUser([FromRoute]int id)
        {
            Log.Information(SharedData.LogMessageRequestReceived);
            try
            {
                if (HttpContext.Request.Path.Value.Contains("/Sessions"))
                {
                    return RedirectToAction("GetSessionList", "Session", new { filter = $"UserId = {id}" });                    
                }

                UserReadDTO userReadDTO = _UserService.GetUser(id);

                if(userReadDTO != null)
                {
                    return Ok(userReadDTO);
                }
                else
                {
                    return NotFound(SharedData.MessageUserNotFound);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return HelperResponse.GenerateResponse(
                    EnumResponseFormat.JSON,
                    HttpStatusCode.InternalServerError,
                    new ErrorModel { ErrorMessage = ex.Message }
                    );
            }
        }

        /// <summary>
        /// Body'den okudugu model ile yeni bir user yaratir.
        /// </summary>
        /// <param name="userCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateUser(
            [FromBody] UserCreateDTO userCreateDTO
            )
        {
            Log.Information(SharedData.LogMessageRequestReceived);

            if (_UserService.CreateUser(userCreateDTO))
            {
                return NoContent();
            }
            else
            {
                Log.Error(SharedData.MessageCantCreateUser);

                return HelperResponse.GenerateResponse(
                    EnumResponseFormat.JSON,
                    HttpStatusCode.InternalServerError,
                    new ErrorModel { ErrorMessage = SharedData.MessageCantCreateUser }
                    );
            }            
        }

        /// <summary>
        /// Id ile eşleştirdiği kaydi verilen degerler ile veritabanında gunceller. Degeri belirtilmeyen
        /// alanlara o alanlarin default degeri atanir.
        /// </summary>
        /// <param name="userUpdateDTO"></param>
        /// <returns>if succeeds 204; if fails 400 or 500</returns>
        [HttpPut]
        public IActionResult PutUser([FromBody] UserUpdateDTO userUpdateDTO)
        {
            Log.Information(SharedData.LogMessageRequestReceived);

            if (_UserService.UpdateUser(userUpdateDTO, true))
            {
                return NoContent();
            }
            else
            {
                Log.Error(SharedData.MessageCantUpdateUser);

                return HelperResponse.GenerateResponse(
                    EnumResponseFormat.JSON,
                    HttpStatusCode.InternalServerError,
                    new ErrorModel { ErrorMessage = SharedData.MessageCantUpdateUser }
                    );
            }
        }

        /// <summary>
        /// Id ile eşleştirdiği kaydi verilen degerler ile veritabanında gunceller.
        /// </summary>
        /// <param name="userUpdateDTO"></param>
        /// <returns>if succeeds 204; if fails 400 or 500</returns>
        [HttpPatch]
        public IActionResult PatchUser([FromBody] UserUpdateDTO userUpdateDTO)
        {
            Log.Information(SharedData.LogMessageRequestReceived);

            if (_UserService.UpdateUser(userUpdateDTO))
            {
                return NoContent();
            }
            else
            {
                Log.Error(SharedData.MessageCantUpdateUser);

                return HelperResponse.GenerateResponse(
                    EnumResponseFormat.JSON,
                    HttpStatusCode.InternalServerError,
                    new ErrorModel { ErrorMessage = SharedData.MessageCantUpdateUser }
                    );
            }
        }

        /// <summary>
        /// Id'si verilen User icin veritabanindan silme islemi yapar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            Log.Information(SharedData.LogMessageRequestReceived);

            _UserService.DeleteUser(id);

            return Ok();
        }
    }
}
