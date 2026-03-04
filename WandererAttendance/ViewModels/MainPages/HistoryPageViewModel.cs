using System;
using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.Services.Config;

namespace WandererAttendance.ViewModels.MainPages;

public partial class HistoryPageViewModel : ObservableRecipient
{
    public ProfileConfigHandler ProfileConfigHandler { get; }
    
    [ObservableProperty] private int _selectedPage = 0;
    [ObservableProperty] private int _selectedSubPage = 0;
    
    [ObservableProperty] private DateTime _selectedDate = DateTime.Today;

    public HistoryPageViewModel(ProfileConfigHandler profileConfigHandler)
    {
        ProfileConfigHandler = profileConfigHandler;
    }
}