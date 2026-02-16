using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.Abstraction;

namespace WandererAttendance.Models;

public partial class ProfileConfigModel : ConfigBase
{
    public override string ConfigFilePath => Utils.GetFilePath("Profiles", $"{Profile.Name}.json");
    [ObservableProperty] private Profile.Profile _profile;

    public ProfileConfigModel()
    {
        Profile = new Profile.Profile();
    }
    
    public ProfileConfigModel(string name)
    {
        Profile = new Profile.Profile { Name = name };
    }
}