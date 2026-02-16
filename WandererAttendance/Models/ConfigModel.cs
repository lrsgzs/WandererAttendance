using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.Abstraction;

namespace WandererAttendance.Models;

public partial class ConfigModel : BaseConfig
{
    public override string ConfigFilePath => Utils.GetFilePath("Config.json");
    
    [ObservableProperty] private string _test = "hello";
}