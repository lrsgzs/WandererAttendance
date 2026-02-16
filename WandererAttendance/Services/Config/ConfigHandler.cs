using System.ComponentModel;
using Microsoft.Extensions.Logging;
using WandererAttendance.Abstraction;
using WandererAttendance.Models;

namespace WandererAttendance.Services.Config;

public class ConfigHandler
{
    public ConfigModel Data { get; private set; }
    
    private ILogger<ConfigHandler> Logger { get; }
    private ConfigServiceBase ConfigService { get; }
    
    public ConfigHandler(ILogger<ConfigHandler> logger, ConfigServiceBase configService)
    {
        Logger = logger;
        ConfigService = configService;
        
        Data = new ConfigModel();
        Data.PropertyChanged += OnPropertyChanged;
        InitializeConfig();
    }

    /// <summary>
    /// 初始化配置文件，检查路径是否存在并加载或创建配置文件。
    /// </summary>
    public void InitializeConfig()
    {
        Logger.LogInformation("加载配置文件...");
        
        Data.PropertyChanged -= OnPropertyChanged;
        Data = ConfigService.LoadConfig();
        Data.PropertyChanged += OnPropertyChanged;
    }
    
    /// <summary>
    /// 当数据属性更改时触发，调用 Save 方法保存配置。
    /// </summary>
    /// <param name="sender">触发事件的对象</param>
    /// <param name="e">属性更改事件参数</param>
    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Save();
    }

    /// <summary>
    /// 保存数据到配置文件，记录日志并在发生异常时进行处理。
    /// </summary>
    public void Save()
    {
        Logger.LogInformation("保存配置文件...");
        ConfigService.SaveConfig(Data);
    }
}