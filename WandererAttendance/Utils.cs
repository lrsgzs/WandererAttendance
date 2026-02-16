using System;
using System.IO;

namespace WandererAttendance;

public static partial class Utils
{
    public static string GetFilePath(params string[] strings)
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name ?? "WandererAttendance";
        var path = Path.Combine([appData, appName, ..strings]);
        
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        return path;
    }
}