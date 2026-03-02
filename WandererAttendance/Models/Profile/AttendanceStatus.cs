using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WandererAttendance.Models.Profile;

public partial class AttendanceStatus : ObservableRecipient
{
    [ObservableProperty] private ObservableCollection<Guid> _statuses = [];
}