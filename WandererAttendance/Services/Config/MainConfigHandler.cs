using Microsoft.Extensions.Logging;
using WandererAttendance.Abstraction;
using WandererAttendance.Models;

namespace WandererAttendance.Services.Config;

public class MainConfigHandler(ILogger<MainConfigHandler> logger, ConfigServiceBase configService)
    : ConfigHandlerBase<MainConfigModel>(logger, configService, () => new MainConfigModel());