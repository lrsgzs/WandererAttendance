using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using WandererAttendance.Models.Profile;

namespace WandererAttendance.Models;

public partial class PersonWithStatus : ObservableObject, IDisposable
{
    private readonly Guid _personGuid;
    private readonly OneDayAttendanceStatus _status;
    private readonly IList<Status> _allStatuses;
    private bool _isUpdatingFromService = false;

    public Person Person { get; }
    public ObservableCollection<Status> Statuses { get; } = [];

    public PersonWithStatus(Person person, IList<Status> allStatuses, OneDayAttendanceStatus status)
    {
        Person = person;
        
        _personGuid = person.Guid;
        _allStatuses = allStatuses;
        _status = status;

        LoadStatuses();

        Statuses.CollectionChanged += OnStatusesCollectionChanged;
        _status.Persons.CollectionChanged += OnPersonsDictionaryChanged;
    }

    private void LoadStatuses()
    {
        if (_isUpdatingFromService) return; // 防止递归
        _isUpdatingFromService = true;
        
        try
        {
            var statusEntry = _status.Persons.GetValueOrDefault(_personGuid);

            Statuses.Clear();
            if (statusEntry == null)
            {
                statusEntry = new AttendanceStatus();
                
                foreach (var s in _allStatuses.Where(s => s.IsDefault))
                {
                    Statuses.Add(s);
                    statusEntry.Statuses.Add(s.Guid);
                }
            }
            else
            {
                var statusGuids = statusEntry.Statuses;
                foreach (var guid in statusGuids)
                {
                    var status = _allStatuses.FirstOrDefault(st => st.Guid == guid);
                    Statuses.Add(status ?? new Status(guid, "???"));
                }
            }
        }
        finally
        {
            _isUpdatingFromService = false;
        }
    }

    private void OnPersonsDictionaryChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        LoadStatuses();
    }

    private void OnStatusesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (_isUpdatingFromService)
            return;

        Dispatcher.UIThread.Post(UpdateServiceStatuses);
    }

    private void UpdateServiceStatuses()
    {
        var statusEntry = _status.Persons.GetValueOrDefault(_personGuid);
        var flag = false;
        
        if (statusEntry == null)
        {
            statusEntry = new AttendanceStatus();
            flag = true;
        }

        statusEntry.Statuses.Clear();
        statusEntry.Statuses.AddRange(Statuses.Select(s => s.Guid));

        if (flag)
        {
            _status.Persons[_personGuid] = statusEntry;
        }
    }

    public void Dispose()
    {
        Statuses.CollectionChanged -= OnStatusesCollectionChanged;
        _status.Persons.CollectionChanged -= OnPersonsDictionaryChanged;
        GC.SuppressFinalize(this);
    }
}