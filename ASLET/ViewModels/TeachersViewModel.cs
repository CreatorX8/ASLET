﻿using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
using ASLET.Services;
using ReactiveUI;

namespace ASLET.ViewModels;

public class TeachersViewModel : ViewModelBase, IRoutableViewModel
{
    private static TeachersViewModel? _instance;

    public static TeachersViewModel GetInstance(IScreen? hostScreen)
    {
        if (_instance == null)
        {
            _instance = new TeachersViewModel(hostScreen!);
        }
        
        return _instance;
    }

    public string? UrlPathSegment => "Teachers";
    public IScreen HostScreen { get; }

    public ICommand AddTeacherCommand { get; }

    public Interaction<TeachersDialogViewModel, TeacherModel?> AddTeacher { get; }

    private TeacherModel _selectedTeacher;

    public TeacherModel SelectedTeacher
    {
        get => _selectedTeacher;
        private set => this.RaiseAndSetIfChanged(ref _selectedTeacher, value);
    }

    private ObservableCollection<TeacherModel> _teachers = new();

    public ObservableCollection<TeacherModel> Teachers
    {
        get => _teachers;
        private set => this.RaiseAndSetIfChanged(ref _teachers, value);
    }

    public ICommand DeleteTeacherCommand { get; }

    public TeachersViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddTeacher = new Interaction<TeachersDialogViewModel, TeacherModel?>();
        AddTeacherCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            TeacherModel? result = await AddTeacher.Handle(new TeachersDialogViewModel());

            if (result != null)
            {
                TimetableService.AddTeacher(result);
            }
        });

        DeleteTeacherCommand = ReactiveCommand.CreateFromTask((TeacherModel selectedTeacher) =>
        {
            TimetableService.RemoveTeacher(selectedTeacher);
            return Task.CompletedTask;
        });
    }

    public void UpdateTeachers(ref ObservableCollection<TeacherModel> teachers)
    {
        Teachers = teachers;
    }
}