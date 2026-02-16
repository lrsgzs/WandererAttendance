using System.IO;
using System.Text.Json;
using WandererAttendance.Abstraction;

namespace WandererAttendance.Services.Config;

public class DesktopConfigService : ConfigServiceBase
{
    public override T LoadConfig<T>(T fallback)
    {
        var filePath = fallback.ConfigFilePath;
        if (!File.Exists(filePath)) return fallback;
        
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json, JsonOptions) ?? fallback;
    }

    public override void SaveConfig<T>(T config)
    {
        var filePath = config.ConfigFilePath;
        var json = JsonSerializer.Serialize(config, JsonOptions);
        File.WriteAllText(filePath, json);
    }
}