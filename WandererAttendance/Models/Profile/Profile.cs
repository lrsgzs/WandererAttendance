using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.ComponentModels;

namespace WandererAttendance.Models.Profile;

public partial class Profile : ObservableRecipient
{
    public string Name { get; set; } = "EMPTY";
    [ObservableProperty] private ObservableCollection<Person> _persons = [];
    [ObservableProperty] private ObservableDictionary<Guid, Status> _statuses = [];
}