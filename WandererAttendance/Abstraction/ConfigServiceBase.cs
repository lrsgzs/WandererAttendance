using System.Text.Json;

namespace WandererAttendance.Abstraction;

public abstract class ConfigServiceBase
{
    protected readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public abstract T LoadConfig<T>(T fallback) where T : BaseConfig;
    public abstract void SaveConfig<T>(T config) where T : BaseConfig;
}