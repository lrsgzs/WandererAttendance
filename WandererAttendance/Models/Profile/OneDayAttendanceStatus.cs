using System;
using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.ComponentModels;

namespace WandererAttendance.Models.Profile;

public partial class OneDayAttendanceStatus : ObservableRecipient
{
    [ObservableProperty] private ObservableDictionary<Guid, AttendanceStatus> _persons = [];
}