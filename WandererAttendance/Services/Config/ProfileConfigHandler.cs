using Microsoft.Extensions.Logging;
using WandererAttendance.Abstraction;
using WandererAttendance.Models;

namespace WandererAttendance.Services.Config;

public class ProfileConfigHandler(ILogger<ProfileConfigHandler> logger, ConfigServiceBase configService)
    : ConfigHandlerBase<ProfileConfigModel>(logger, configService, () =>
    {
        var model = new ProfileConfigModel(ProfileService.ProfileName);
        foreach (var kvp in GlobalConstants.DefaultStatuses)
        {
            model.Profile.Statuses.Add(kvp.Key, kvp.Value);
        }

        return model;
    });
