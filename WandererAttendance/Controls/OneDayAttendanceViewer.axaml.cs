using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using DynamicData;
using WandererAttendance.Abstraction;
using WandererAttendance.Models;
using WandererAttendance.Models.Profile;
using WandererAttendance.Services.Config;

namespace WandererAttendance.Controls;

public partial class OneDayAttendanceViewer : UserControl
{
    public static readonly StyledProperty<DateTime> DateProperty =
        AvaloniaProperty.Register<AttendanceDayControl, DateTime>(nameof(Date));

    public DateTime Date
    {
        get => GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }
    
    public ProfileConfigHandler ProfileConfigHandler { get; } = IAppHost.GetService<ProfileConfigHandler>();
    public ObservableCollection<StatusAndCount> Data { get; } = [];
    
    static OneDayAttendanceViewer()
    {
        DateProperty.Changed.AddClassHandler<OneDayAttendanceViewer>((x, e) => x.OnDateChanged(e));
    }

    public OneDayAttendanceViewer()
    {
        InitializeComponent();
        RefreshData();
    }

    private void OnDateChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is DateTime)
        {
            RefreshData();
        }
    }

    public void RefreshData()
    {
        Data.Clear();
        var date = DateOnly.FromDateTime(Date);
        var config = ProfileConfigHandler.Data;
        
        // 拉取数据
        var attendanceStatus = Utils.CopyObjectByJson(
            config.Statuses.GetValueOrDefault(date, new OneDayAttendanceStatus()));
        foreach (var person in config.Profile.Persons)
        {
            if (attendanceStatus.Persons.GetValueOrDefault(person.Guid) != null) continue;

            var status = new AttendanceStatus();
            status.Statuses.AddRange(config.Profile.Statuses
                .Where(s => s.IsDefault)
                .Select(s => s.Guid));
            attendanceStatus.Persons[person.Guid] = status;
        }
        
        // 统计数据
        Data.AddRange(config.Profile.Statuses
            .Select(s => new StatusAndCount
            {
                Status = s,
                Count = config.Profile.Persons
                    .Count(p => attendanceStatus.Persons[p.Guid].Statuses.Contains(s.Guid)),
                Persons = []  // 当前控件无需显示详细人员
            }));
    }
}