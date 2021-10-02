using AutoMapper;
using BikeSharing.Application.DTOs.Sessions;
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
using BikeSharingAPI.Helpers;
using System.Net;
using BikeSharingAPI.Models;
using BikeSharingAPI.Enums;

namespace BikeSharingAPI.Controllers
{
    [Route("api/v1/Sessions")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _SessionService;

        public SessionController(ISessionService sessionService)
        {
            this._SessionService = sessionService;
        }

        /// <summary>
        /// Tum sessionlarin bir listesini doner.
        /// </summary>
        /// <returns>Json formatinda kullanici listesi.</returns>
        [HttpGet]
        public IActionResult GetSessionList(
            string filter = "",
            string orderByParams = "",
            string fields = ""
            )
        {
            Log.Information(SharedData.LogMessageRequestReceived);

            try
            {
                List<SessionReadDTO> sessionModel = _SessionService.GetSessions(filter, orderByParams, fields);
                if (sessionModel != null && sessionModel.Count > 0)
                {
                    return Ok(sessionModel);
                }
                else
                {
                    return NotFound(SharedData.MessageSessionsNotFound);
                }
            }
            catch (Exception ex)
            {
                return HelperResponse.GenerateResponse(
                    EnumResponseFormat.JSON,
                    HttpStatusCode.InternalServerError,
                    new ErrorModel { ErrorMessage = ex.Message}
                    );
            }
        }

        /// <summary>
        /// Id'si verilen sessioni doner.
        /// </summary>
        /// <param name="id">Kullanici id'si</param>
        /// <returns>Eger bulursa, session bilgileri json formatinda.</returns>
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetSession([FromRoute] Guid id)
        {
            Log.Information(SharedData.LogMessageRequestReceived);

            try
            {
                SessionReadDTO sessionModel = _SessionService.GetSession(id);

                if (sessionModel != null)
                {
                    return Ok(sessionModel);
                }
                else
                {
                    return NotFound(SharedData.MessageSessionNotFound);
                }
            }
            catch (Exception ex)
            {
                return HelperResponse.GenerateResponse(
                    EnumResponseFormat.JSON,
                    HttpStatusCode.InternalServerError,
                    new ErrorModel { ErrorMessage = ex.Message }
                    );
            }
        }

        /// <summary>
        /// Body'den okudugu model ile yeni bir session yaratir.
        /// </summary>
        /// <param name="sessionCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateSession(
            [FromBody] SessionCreateDTO sessionCreateDTO
            )
        {
            Log.Information(SharedData.LogMessageRequestReceived);

            if (_SessionService.CreateSession(sessionCreateDTO))
            {
                return NoContent();
            }
            else
            {
                Log.Error(SharedData.MessageCantCreateSession);

                return HelperResponse.GenerateResponse(
                    EnumResponseFormat.JSON,
                    HttpStatusCode.InternalServerError,
                    new ErrorModel { ErrorMessage = SharedData.MessageCantCreateSession }
                    );
            }
        }

        /// <summary>
        /// Id ile eşleştirdiği kaydi verilen degerler ile veritabanında gunceller. Degeri belirtilmeyen
        /// alanlara o alanlarin default degeri atanir.
        /// </summary>
        /// <param name="sessionUpdateDTO"></param>
        /// <returns>if succeeds 204; if fails 400 or 500</returns>
        [HttpPut]
        public IActionResult PutSession([FromBody]SessionUpdateDTO sessionUpdateDTO)
        {
            Log.Information(SharedData.LogMessageRequestReceived);

            if (_SessionService.UpdateSession(sessionUpdateDTO, true))
            {
                return NoContent();
            }
            else
            {
                Log.Error(SharedData.MessageCantUpdateSession);

                return HelperResponse.GenerateResponse(
                    EnumResponseFormat.JSON,
                    HttpStatusCode.InternalServerError,
                    new ErrorModel { ErrorMessage = SharedData.MessageCantUpdateSession }
                    );
            }
        }

        /// <summary>
        /// Id ile eşleştirdiği kaydi verilen degerler ile veritabanında gunceller.
        /// </summary>
        /// <param name="sessionUpdateDTO"></param>
        /// <returns>if succeeds 204; if fails 400 or 500</returns>
        [HttpPatch]
        public IActionResult PatchSession([FromBody] SessionUpdateDTO sessionUpdateDTO)
        {
            Log.Information(SharedData.LogMessageRequestReceived);

            if (_SessionService.UpdateSession(sessionUpdateDTO))
            {
                return NoContent();
            }
            else
            {
                Log.Error(SharedData.MessageCantUpdateSession);

                return HelperResponse.GenerateResponse(
                    EnumResponseFormat.JSON,
                    HttpStatusCode.InternalServerError,
                    new ErrorModel { ErrorMessage = SharedData.MessageCantUpdateSession }
                    );
            }
        }

        /// <summary>
        /// Id'si verilen Session icin veritabanindan silme islemi yapar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteSession([FromRoute] Guid id)
        {
            Log.Information(SharedData.LogMessageRequestReceived);

            _SessionService.DeleteSession(id);

            return Ok();
        }
    }
}
