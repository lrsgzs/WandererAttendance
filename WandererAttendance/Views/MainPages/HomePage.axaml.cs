using Avalonia.Controls;
using WandererAttendance.Abstraction;
using WandererAttendance.Attributes;
using WandererAttendance.Services.Config;
using WandererAttendance.ViewModels.MainPages;

namespace WandererAttendance.Views.MainPages;

[MainPageInfo("主页", "home", "\uE994")]
public partial class HomePage : UserControl
{
    public MainConfigHandler MainConfigHandler { get; } = IAppHost.GetService<MainConfigHandler>();
    public HomePageViewModel ViewModel { get; } = IAppHost.GetService<HomePageViewModel>();
    
    public HomePage()
    {
        DataContext = this;
        InitializeComponent();
    }
}