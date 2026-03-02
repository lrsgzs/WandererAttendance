using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiniExcelLibs;

namespace WandererAttendance.Views.MainPages;

public partial class ProfilePage
{
    private static List<List<string>> LoadFromTxt(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        
        return lines
            .Select(line => (List<string>)[line.Trim()])
            .ToList();
    }

    private static List<List<string>> LoadFromCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        
        return lines
            .Select(line =>
                line.Split(",")
                    .Select(item => item.Trim())
                    .ToList())
            .ToList();
    }

    private static List<List<string>> LoadFromExcel(string filePath)
    {
        var rows = MiniExcel.Query(filePath); // 禁用表头解析

        return rows
            .Select(row => (IDictionary<string, object?>)row)
            .Select(dict => dict
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value?.ToString() ?? "")
                .ToList())
            .ToList();
    }
}