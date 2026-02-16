using System.Collections.ObjectModel;
using WandererAttendance.Attributes;

namespace WandererAttendance.Services;

public static class MainPagesRegistryService
{
    public static ObservableCollection<MainPageInfo> Items { get; } = [];
    public static ObservableCollection<MainPageInfo> FooterItems { get; } = [];
}