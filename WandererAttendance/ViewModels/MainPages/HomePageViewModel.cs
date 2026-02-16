using CommunityToolkit.Mvvm.ComponentModel;
using WandererAttendance.Services.Config;

namespace WandererAttendance.ViewModels.MainPages;

public partial class HomePageViewModel : ObservableRecipient
{
    public ConfigHandler ConfigHandler { get; }

    public HomePageViewModel(ConfigHandler configHandler)
    {
        ConfigHandler = configHandler;
    }
}