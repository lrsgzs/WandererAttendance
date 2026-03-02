using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiniExcelLibs;

namespace WandererAttendance.Views.MainPages;

public partial class ProfilePage
{
    private static List<List<string>> LoadFromTxt(Stream stream)
    {
        var reader = new StreamReader(stream);
        
        List<List<string>> lines = [];
        while (reader.ReadLine() is { } line)
        {
            lines.Add((List<string>)[line.Trim()]);
        }
        
        return lines;
    }

    private static List<List<string>> LoadFromCsv(Stream stream)
    {
        var reader = new StreamReader(stream);
        
        List<List<string>> lines = [];
        while (reader.ReadLine() is { } line)
        {
            lines.Add(line.Split(",")
                .Select(item => item.Trim())
                .ToList());
        }
        
        return lines;
    }

    private static List<List<string>> LoadFromExcel(Stream stream)
    {
        return stream
            .Query()
            .Select(row => (IDictionary<string, object?>)row)
            .Select(dict => dict
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value?.ToString() ?? "")
                .ToList())
            .ToList();
    }
}