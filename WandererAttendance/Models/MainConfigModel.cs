using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.Abstraction;

namespace WandererAttendance.Models;

public partial class MainConfigModel : ConfigBase
{
    public override string ConfigFilePath => Utils.GetFilePath("Config.json");
    
    [ObservableProperty] private string _test = "hello";
    [ObservableProperty] private string _profileName = "test";
}