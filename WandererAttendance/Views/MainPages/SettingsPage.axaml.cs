using Avalonia.Controls;
using WandererAttendance.Abstraction;
using WandererAttendance.Attributes;
using WandererAttendance.Services.Config;

namespace WandererAttendance.Views.MainPages;

[MainPageInfo("设置", "settings", "\uEF27")]
public partial class SettingsPage : UserControl
{
    public MainConfigHandler MainConfigHandler { get; } = IAppHost.GetService<MainConfigHandler>();
    
    public SettingsPage()
    {
        DataContext = this;
        InitializeComponent();
    }
}