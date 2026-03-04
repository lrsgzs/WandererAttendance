using Avalonia.Controls;
using Avalonia.Interactivity;
using WandererAttendance.Abstraction;
using WandererAttendance.Attributes;
using WandererAttendance.ViewModels.MainPages;

namespace WandererAttendance.Views.MainPages;

[MainPageInfo("历史记录", "history", "\uE990", true, true)]
public partial class HistoryPage : UserControl
{
    public HistoryPageViewModel ViewModel { get; } = IAppHost.GetService<HistoryPageViewModel>();
    
    public HistoryPage()
    {
        DataContext = this;
        InitializeComponent();
    }

    private void ButtonRefresh_OnClick(object? sender, RoutedEventArgs e)
    {
        var view = IAppHost.GetService<MainView>();
        view.SelectNavigationItemById("history");
    }
}