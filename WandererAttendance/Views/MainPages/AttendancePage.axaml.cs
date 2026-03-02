using Avalonia.Controls;
using WandererAttendance.Attributes;

namespace WandererAttendance.Views.MainPages;

[MainPageInfo("考勤", "attendance", "\uE430")]
public partial class AttendancePage : UserControl
{
    public AttendancePage()
    {
        InitializeComponent();
    }
}