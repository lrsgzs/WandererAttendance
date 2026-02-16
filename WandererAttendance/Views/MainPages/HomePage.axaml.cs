using Avalonia.Controls;
using WandererAttendance.Abstraction;
using WandererAttendance.Attributes;
using WandererAttendance.Services.Config;
using WandererAttendance.ViewModels.MainPages;

namespace WandererAttendance.Views.MainPages;

[MainPageInfo("主页", "home", "\uE994")]
public partial class HomePage : UserControl
{
    public ConfigHandler ConfigHandler { get; } = IAppHost.GetService<ConfigHandler>();
    public HomePageViewModel ViewModel { get; } = IAppHost.GetService<HomePageViewModel>();
    
    public HomePage()
    {
        DataContext = this;
        InitializeComponent();
    }
}