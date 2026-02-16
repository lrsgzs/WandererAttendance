using System;
using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using DynamicData;
using FluentAvalonia.UI.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WandererAttendance.Abstraction;
using WandererAttendance.Attributes;
using WandererAttendance.Controls;
using WandererAttendance.Services;
using WandererAttendance.Services.Config;
using WandererAttendance.ViewModels;

namespace WandererAttendance.Views;

public partial class MainView : UserControl, INavigationPageFactory
{
    public MainViewModel ViewModel { get; } = IAppHost.GetService<MainViewModel>();
    private ILogger<MainView> Logger { get; } = IAppHost.GetService<ILogger<MainView>>();
    private const string DefaultMainPageId = "home";
    
    private AppToastAdorner? _appToastAdorner;
    private bool _isAdornerAdded;
    
    public MainView()
    {
        DataContext = this;
        InitializeComponent();
        
        ViewModel.PropertyChanged += ViewModelOnPropertyChanged;

        NavigationFrame.NavigationPageFactory = this;
        BuildNavigationMenuItems();
        
        RenderOptions.SetTextRenderingMode(this, TextRenderingMode.Antialias);
        RenderOptions.SetBitmapInterpolationMode(this, BitmapInterpolationMode.HighQuality);
        RenderOptions.SetEdgeMode(this, EdgeMode.Antialias);
    }

    private void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.IsPinned))
        {
            if (!App.IsDesktop)
            {
                return;
            }
        
            Logger.LogInformation("修改置顶状态为 {IS_PINNED}", ViewModel.IsPinned);
            App.MainWindow?.Topmost = ViewModel.IsPinned;
        }
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        SelectNavigationItemById(DefaultMainPageId);
        
        if (Content is not Control element || _isAdornerAdded)
        {
            return;
        }

        var layer = AdornerLayer.GetAdornerLayer(element);
        var appToastAdorner = _appToastAdorner = new AppToastAdorner(this);
        layer?.Children.Add(appToastAdorner);
        AdornerLayer.SetAdornedElement(appToastAdorner, this);
        _isAdornerAdded = true;
    }
    
    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        DataContext = null;
        IAppHost.GetService<MainConfigHandler>().Save();
    }

    private void BuildNavigationMenuItems()
    {
        Logger.LogInformation("构建导航项目");
        ViewModel.NavigationViewItems.Clear();
        ViewModel.NavigationViewFooterItems.Clear();
        
        ViewModel.NavigationViewItems
            .AddRange(MainPagesRegistryService.Items
                .Select(info => info.ToNavigationViewItemBase()));
        
        ViewModel.NavigationViewFooterItems
            .AddRange(MainPagesRegistryService.FooterItems
                .Select(info => info.ToNavigationViewItemBase()));
    }

    public void SelectNavigationItemById(string id)
    {
        var info = MainPagesRegistryService.Items.FirstOrDefault(info => info.Id == id) ??
                   MainPagesRegistryService.FooterItems.FirstOrDefault(info => info.Id == id);
        
        if (info != null)
        {
            CoreNavigate(info);
        }
    }
    
    private void SelectNavigationItem(MainPageInfo info)
    {
        var item = ViewModel.NavigationViewItems.FirstOrDefault(item => Equals(item.Tag, info)) ??
                   ViewModel.NavigationViewFooterItems.FirstOrDefault(item => Equals(item.Tag, info));
        ViewModel.SelectedNavigationViewItem = item;
    }

    private void CoreNavigate(MainPageInfo info)
    {
        Logger.LogInformation("导航到 [{ID}]{NAME}", info.Id, info.Name);
        ViewModel.FrameContent = null;
        SelectNavigationItem(info);
        ViewModel.SelectedPageInfo = info;
        NavigationFrame.NavigateFromObject(info);
    }
    
    private void NavigationView_OnItemInvoked(object? sender, NavigationViewItemInvokedEventArgs e)
    {
        if (e.InvokedItemContainer is NavigationViewItem { Tag: MainPageInfo info })
        {
            CoreNavigate(info);
        }
    }

    private void TogglePaneButton_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationView.IsPaneOpen = !NavigationView.IsPaneOpen;
    }

    public Control? GetPage(Type srcType)
    {
        return Activator.CreateInstance(srcType) as Control;
    }

    public Control? GetPageFromObject(object target)
    {
        if (target is not MainPageInfo info)
        {
            return null;
        }
        
        return IAppHost.Host!.Services.GetKeyedService<UserControl>(info.Id);
    }
}