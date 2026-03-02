using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DynamicData;
using Microsoft.Extensions.Logging;

namespace WandererAttendance.Services;

public class ProfileService
{
    public static string ProfileName = "EMPTY";
    public static string ProfilePath => Utils.GetFilePath("Profiles");
    
    private ILogger<ProfileService> Logger { get; }
    public ObservableCollection<string> Profiles { get; } = [];

    public ProfileService(ILogger<ProfileService> logger)
    {
        Logger = logger;
        
        RefreshProfiles();
    }
    
    public void RefreshProfiles()
    {
        Logger.LogInformation("刷新档案列表");
        Profiles.Clear();
        Profiles.AddRange(
            from i in Directory.GetFiles(ProfilePath) 
            where i.EndsWith(".json")
            select Path.GetFileName(i).Replace(".json", ""));
    }
}