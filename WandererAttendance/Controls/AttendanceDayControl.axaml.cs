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

public partial class AttendanceDayControl : UserControl
{
    public static readonly StyledProperty<DateTime> DateProperty =
        AvaloniaProperty.Register<AttendanceDayControl, DateTime>(nameof(Date));

    public DateTime Date
    {
        get => GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }
    
    public static readonly StyledProperty<string> SimpleTextProperty =
        AvaloniaProperty.Register<AttendanceDayControl, string>(nameof(SimpleText), string.Empty);

    public string SimpleText
    {
        get => GetValue(SimpleTextProperty);
        set => SetValue(SimpleTextProperty, value);
    }
    
    public static readonly StyledProperty<bool> ShowSimpleTextProperty =
        AvaloniaProperty.Register<AttendanceDayControl, bool>(nameof(ShowSimpleText));

    public bool ShowSimpleText
    {
        get => GetValue(ShowSimpleTextProperty);
        set => SetValue(ShowSimpleTextProperty, value);
    }
    
    public ProfileConfigHandler ProfileConfigHandler { get; } = IAppHost.GetService<ProfileConfigHandler>();
    public ObservableCollection<StatusAndCount> Data { get; } = [];
    
    static AttendanceDayControl()
    {
        DateProperty.Changed.AddClassHandler<AttendanceDayControl>((x, e) => x.OnDateChanged(e));
    }
    
    public AttendanceDayControl()
    {
        InitializeComponent();
    }

    private void Control_OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        ShowSimpleText = e.NewSize.Height <= 96 || e.NewSize.Width <= 96;
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
        
        // 简略文本
        if (config.Statuses.GetValueOrDefault(date) == null)
        {
            SimpleText = "无记录";
            return;
        }

        var firstStatus = config.Profile.Statuses.FirstOrDefault(s => s.IsDefault) ??
                          config.Profile.Statuses.FirstOrDefault();
        if (firstStatus == null)
        {
            SimpleText = "无状态";
            return;
        }

        var count = config.Profile.Persons
            .Count(p => attendanceStatus.Persons[p.Guid].Statuses.Contains(firstStatus.Guid));
        SimpleText = $"{firstStatus.Name} {count} 人";
    }
}