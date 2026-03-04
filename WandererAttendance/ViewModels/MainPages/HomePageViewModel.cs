using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using WandererAttendance.Models;
using WandererAttendance.Models.Profile;
using WandererAttendance.Services.Config;

namespace WandererAttendance.ViewModels.MainPages;

public partial class HomePageViewModel : ObservableRecipient
{
    public DateOnly TodayDate { get; } = DateOnly.FromDateTime(DateTime.Now);
    
    public ProfileConfigHandler ProfileConfigHandler { get; }
    public ObservableCollection<StatusAndCount> AttendanceData { get; } = [];

    public HomePageViewModel(ProfileConfigHandler profileConfigHandler)
    {
        ProfileConfigHandler = profileConfigHandler;
        RefreshData();
    }
    
    public void RefreshData()
    {
        AttendanceData.Clear();
        var config = ProfileConfigHandler.Data;
        
        // 拉取数据
        var attendanceStatus = Utils.CopyObjectByJson(
            config.Statuses.GetValueOrDefault(TodayDate, new OneDayAttendanceStatus()));
        foreach (var person in config.Profile.Persons)
        {
            if (attendanceStatus.Students.GetValueOrDefault(person.Guid) != null) continue;

            var status = new AttendanceStatus();
            status.Statuses.AddRange(config.Profile.Statuses
                .Where(s => s.IsDefault)
                .Select(s => s.Guid));
            attendanceStatus.Students[person.Guid] = status;
        }
        
        // 统计数据
        AttendanceData
            .AddRange(config.Profile.Statuses
            .Select(s => new StatusAndCount
            {
                Status = s,
                Count = config.Profile.Persons
                    .Count(p => attendanceStatus.Students[p.Guid].Statuses.Contains(s.Guid)),
                Persons = config.Profile.Persons
                    .Where(p => attendanceStatus.Students[p.Guid].Statuses.Contains(s.Guid))
                    .ToList()
            }));
    }
}