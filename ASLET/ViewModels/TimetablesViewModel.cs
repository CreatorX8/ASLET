﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ASLET.Models;
using ASLET.Services;
using ASLET.Services.Handlers;
using ASLET.Services.Objects;
using ReactiveUI;

namespace ASLET.ViewModels;

public class TimetablesViewModel : ViewModelBase, IRoutableViewModel
{
    #region Routing

    private static TimetablesViewModel? _instance;

    public static TimetablesViewModel GetInstance(IScreen? hostScreen)
    {
        if (_instance == null)
        {
            _instance = new TimetablesViewModel(hostScreen!);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Timetables";
    
    public IScreen HostScreen { get; }

    #endregion

    #region Logic

    public ICommand GenerateTimetableCommand { get; }

    private bool _hasGeneratedTimetable = false;

    public ObservableCollection<ClassModel> Classes { get; } = new();

    private ClassModel _selectedClass;

    public ClassModel SelectedClass
    {
        get => _selectedClass;
        private set
        {
            this.RaiseAndSetIfChanged(ref _selectedClass, value);
            UpdateTimetable();
        }
    }

    private ObservableCollection<TimetableModel> _timetable = new();

    public ObservableCollection<TimetableModel> Timetable
    {
        get => _timetable;
        private set => this.RaiseAndSetIfChanged(ref _timetable, value);
    }

    public void FillClasses()
    {
        Classes.Clear();
        foreach (ClassModel @class in TimetableService.GetClasses())
        {
            Classes.Add(@class);
        }
    }

    private void UpdateTimetable()
    {
        if (!_hasGeneratedTimetable) return;
        Timetable.Clear();
        
        foreach (TimetableModel timetableModel in ScheduleFabric.algControll.GetData(_selectedClass.ClassId))
        {
            Timetable.Add(timetableModel);
        }
    }
    
    #endregion
    
    #region Parent-child relations

    private static MainWindowViewModel? _parent;
    public static void SetParent(MainWindowViewModel? parent) => _parent = parent;

    #endregion
    
    #region DarkMode

    private bool _darkMode;
    public bool DarkMode
    {
        get => _darkMode;
        set => this.RaiseAndSetIfChanged(ref _darkMode, value);
    }
    #endregion

    public TimetablesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        GenerateTimetableCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            TimetableService.GenerateTimetable();
            _hasGeneratedTimetable = true;
            UpdateTimetable();
        });
        
        FillClasses();
        if (Classes.Count > 0) SelectedClass = Classes[0];
    }
    
}