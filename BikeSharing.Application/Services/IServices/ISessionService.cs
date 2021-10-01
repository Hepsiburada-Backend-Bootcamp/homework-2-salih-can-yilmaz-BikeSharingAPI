using BikeSharing.Application.DTOs.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeSharing.Application.Services.IServices
{
    public interface ISessionService
    {
        public SessionReadDTO GetSession(Guid Id);
        public List<SessionReadDTO> GetSessions(string filter, string orderByParams, string fields);
        public bool CreateSession(SessionCreateDTO sessionCreateDTO);
        bool UpdateSession(SessionUpdateDTO sessionUpdateDTO, bool setNullsToDefaults = false);
        public bool DeleteSession(Guid Id);

    }
}
