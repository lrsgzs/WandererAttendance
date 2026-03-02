using System;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.Abstraction;
using WandererAttendance.ComponentModels;
using WandererAttendance.Models.Profile;

namespace WandererAttendance.Models;

public partial class ProfileConfigModel : ConfigBase
{
    [JsonIgnore]
    public override string ConfigFilePath => Utils.GetFilePath("Profiles", $"{Profile.Name}.json");
    
    [ObservableProperty] private Profile.Profile _profile;
    [ObservableProperty] private ObservableDictionary<DateOnly, OneDayAttendanceStatus> _statuses = [];

    public ProfileConfigModel()
    {
        Profile = new Profile.Profile();
    }
    
    public ProfileConfigModel(string name)
    {
        Profile = new Profile.Profile { Name = name };
    }
}