using CommunityToolkit.Mvvm.ComponentModel;

namespace WandererAttendance.Models;

public partial class ConfigModel : ObservableRecipient
{
    [ObservableProperty] private string _test = "hello";
}