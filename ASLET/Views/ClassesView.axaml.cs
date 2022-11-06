﻿using ASLET.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace ASLET.Views;

public partial class ClassesView : ReactiveUserControl<ClassesViewModel>
{
    private static ClassesView? _instance;

    public static ClassesView? GetInstance(ClassesViewModel viewModel)
    {
        if (_instance == null)
        {
            _instance = new ClassesView
            {
                DataContext = viewModel
            };
        }

        return _instance;
    }

    public ClassesView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}