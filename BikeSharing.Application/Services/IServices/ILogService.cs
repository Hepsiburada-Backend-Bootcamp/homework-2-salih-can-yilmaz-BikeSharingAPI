
using BikeSharing.Domain.Enums;

namespace BikeSharing.Application.Services.IServices
{
    public interface ILogService
    {
         bool Log(string message, EnumLogLevel logLevel = EnumLogLevel.INFORMATION);
    }
}