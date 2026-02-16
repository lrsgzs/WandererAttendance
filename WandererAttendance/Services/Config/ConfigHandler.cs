using Microsoft.Extensions.Logging;
using WandererAttendance.Abstraction;
using WandererAttendance.Models;

namespace WandererAttendance.Services.Config;

public class ConfigHandler(ILogger<ConfigHandler> logger, ConfigServiceBase configService)
    : ConfigHandlerBase<ConfigModel>(logger, configService, () => new ConfigModel());