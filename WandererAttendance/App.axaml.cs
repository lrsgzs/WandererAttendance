using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using WandererAttendance.Abstraction;
using WandererAttendance.Extensions.Registry;
using WandererAttendance.Services.Config;
using WandererAttendance.Services.Logging;
using WandererAttendance.ViewModels;
using WandererAttendance.ViewModels.MainPages;
using WandererAttendance.Views;
using WandererAttendance.Views.MainPages;

namespace WandererAttendance;

public partial class App : Application
{
    public static bool IsDesktop { get; private set; } = false;
    public static MainWindow? MainWindow { get; private set; } = null;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        BuildHost();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            
            IsDesktop = true;
            MainWindow = new MainWindow
            {
                Content = IAppHost.GetService<MainView>()
            };
            desktop.MainWindow = MainWindow;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            IsDesktop = false;
            singleViewPlatform.MainView = IAppHost.GetService<MainView>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private void BuildHost()
    {
        IAppHost.Host = Host
            .CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureServices(services =>
            {
                // 日志
                services.AddLogging(builder =>
                {
                    builder.AddConsoleFormatter<ClassIslandConsoleFormatter, ConsoleFormatterOptions>();
                    builder.AddConsole(console => { console.FormatterName = "classisland"; });
#if DEBUG
                    builder.SetMinimumLevel(LogLevel.Trace);
#endif
                });
                
                // 配置
                services.AddSingleton<ConfigServiceBase, DesktopConfigService>();
                services.AddSingleton<ConfigHandler>();
                
                // 服务
                
                // 主窗口
                services.AddSingleton<MainView>();
                services.AddTransient<MainViewModel>();
                
                // 界面 Views
                services.AddMainPage<HomePage>();
                services.AddMainPageSeparator();
                services.AddMainPage<ProfilePage>();

                services.AddMainPageFooter<SettingsPage>();
                services.AddMainPageFooter<AboutPage>();
                services.AddMainPageFooterSeparator();
                services.AddMainPageFooter<DebugPage>();
                
                // 界面 ViewModels
                services.AddTransient<HomePageViewModel>();
            })
            .Build();

        var logger = IAppHost.GetService<ILogger<App>>();
        logger.LogInformation("WandererAttendance Copyright by lrs2187(2026) Licensed under GPL3.0");
        logger.LogInformation("Host built.");
        IAppHost.GetService<ConfigHandler>();
    }
}