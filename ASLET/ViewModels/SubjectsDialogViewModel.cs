﻿using System.Reactive;
using System.Threading.Tasks;
using ASLET.Models;
using ReactiveUI;

namespace ASLET.ViewModels;

public class SubjectsDialogViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, SubjectModel> AddSubjectCommand { get; }
    public ReactiveCommand<Unit, SubjectModel?> CancelCommand { get; }

    private string _subjectName;

    public string SubjectName
    {
        get => _subjectName;
        set => this.RaiseAndSetIfChanged(ref _subjectName, value);
    }

    public SubjectsDialogViewModel()
    {
        // TODO CHECKERS FOR VALID INPUT
        AddSubjectCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(new SubjectModel(_subjectName)));

        CancelCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult<SubjectModel?>(null));
        
        SubjectName = "";
    }
}