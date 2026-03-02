using System;
using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.Services.Config;

namespace WandererAttendance.ViewModels.MainPages;

public partial class HomePageViewModel : ObservableRecipient
{
    public MainConfigHandler MainConfigHandler { get; }
    public DateOnly TodayDate { get; } = DateOnly.FromDateTime(DateTime.Now);

    public HomePageViewModel(MainConfigHandler mainConfigHandler)
    {
        MainConfigHandler = mainConfigHandler;
    }
}