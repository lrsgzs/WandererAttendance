using System.Collections.Generic;
using Avalonia.Media;

namespace WandererAttendance;

public static class GlobalConstants
{
    public static FontFamily FluentIconsFontFamily =
        new("avares://WandererAttendance/Assets/Fonts/#FluentSystemIcons-Resizable");
    
    public static class ImportSheetStaticTexts
    {
        public static readonly List<string> NameHeaderTexts = ["姓名", "名字", "name"];
        public static readonly List<string> IdHeaderTexts = ["编号", "学号", "考号", "id"];
        public static readonly List<string> SexHeaderTexts = ["性别", "sex"];

        public static class SexTexts
        {
            public static readonly List<string> Male = ["男", "male", "boy", "man", "1"];
            public static readonly List<string> Female = ["女", "female", "girl", "woman", "0"];
        }
    }
}