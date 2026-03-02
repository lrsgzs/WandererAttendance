using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.Abstraction;

namespace WandererAttendance.Models;

public partial class MainConfigModel : ConfigBase
{
    [JsonIgnore]
    public override string ConfigFilePath => Utils.GetFilePath("Config.json");
    
    [ObservableProperty] private string _profileName = "Default";
}