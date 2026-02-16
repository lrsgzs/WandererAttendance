using Microsoft.Extensions.Logging;
using WandererAttendance.Abstraction;
using WandererAttendance.Models;

namespace WandererAttendance.Services.Config;

public class ProfileConfigHandler(ILogger<ProfileConfigHandler> logger, ConfigServiceBase configService)
    : ConfigHandlerBase<ProfileConfigModel>(logger, configService, () => new ProfileConfigModel(ProfileName))
{
    public static string ProfileName = "EMPTY";
}