﻿using System;
using ASLET.ViewModels;
using ASLET.Views;
using ReactiveUI;

namespace ASLET;

public class ASLETViewLocator : IViewLocator
{
    public IViewFor? ResolveView<T>(T viewModel, string? contract = null)
    {
        if (viewModel is ClassesViewModel)
        {
            return viewModel switch
            {
                ClassesViewModel context => ClassesView.GetInstance(context),
                _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
            };
        } else if (viewModel is TeachersViewModel)
        {
            return viewModel switch
            {
                TeachersViewModel context => TeachersView.GetInstance(context),
                _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
            };
        }

        return null;
    }
}