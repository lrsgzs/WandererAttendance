using System.Text.Json;
using WandererAttendance.Models;

namespace WandererAttendance.Abstraction;

public abstract class ConfigServiceBase
{
    public readonly string ConfigFileName = "config.json";
    public readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public abstract ConfigModel LoadConfig();
    public abstract void SaveConfig(ConfigModel config);
    
    protected string GetConfigFilePath()
    {
        return Utils.GetFilePath("Config", ConfigFileName);
    }
}