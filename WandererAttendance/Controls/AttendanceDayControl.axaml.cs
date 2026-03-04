using System;
using Avalonia;
using Avalonia.Controls;

namespace WandererAttendance.Controls;

public partial class AttendanceDayControl : UserControl
{
    public static readonly StyledProperty<DateTime> DateProperty =
        AvaloniaProperty.Register<AttendanceDayControl, DateTime>(nameof(Date));

    public DateTime Date
    {
        get => GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }

    
    public AttendanceDayControl()
    {
        InitializeComponent();
    }
}