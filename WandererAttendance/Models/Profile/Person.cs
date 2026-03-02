using System;
using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.Enums;

namespace WandererAttendance.Models.Profile;

public partial class Person : ObservableRecipient
{
    [ObservableProperty] private Guid _guid = Guid.NewGuid();
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _id = string.Empty;
    [ObservableProperty] private HumanSex _sex = HumanSex.Unknown;
}