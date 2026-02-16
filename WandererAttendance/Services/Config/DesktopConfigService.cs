using System.IO;
using System.Text.Json;
using WandererAttendance.Abstraction;
using WandererAttendance.Models;

namespace WandererAttendance.Services.Config;

public class DesktopConfigService : ConfigServiceBase
{
    public override ConfigModel LoadConfig()
    {
        var filePath = GetConfigFilePath();

        if (!File.Exists(filePath)) return new ConfigModel();
        
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<ConfigModel>(json, JsonOptions) ?? new ConfigModel();
    }
    
    public override void SaveConfig(ConfigModel config)
    {
        var filePath = GetConfigFilePath();
        var json = JsonSerializer.Serialize(config, JsonOptions);
        File.WriteAllText(filePath, json);
    }
}