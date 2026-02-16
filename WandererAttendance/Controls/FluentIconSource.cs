using Avalonia.Media;
using FluentAvalonia.UI.Controls;

namespace WandererAttendance.Controls;

/// <summary>
/// Fluent Icon 图标源
/// </summary>
public class FluentIconSource : FontIconSource
{
    public FluentIconSource()
    {
        FontFamily = new FontFamily("avares://WandererAttendance/Assets/Fonts/#FluentSystemIcons-Resizable");
    }
    
    public FluentIconSource(string glyph) : this()
    {
        Glyph = glyph;
    }

    public FluentIconSource ProvideValue() => this;
}