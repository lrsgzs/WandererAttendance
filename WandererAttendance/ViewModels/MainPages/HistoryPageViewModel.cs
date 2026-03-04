using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WandererAttendance.ViewModels.MainPages;

public partial class HistoryPageViewModel : ObservableRecipient
{
    [ObservableProperty] private int _selectedPage = 0;
    
    [ObservableProperty] private DateTime _selectedDate = DateTime.Today;
}