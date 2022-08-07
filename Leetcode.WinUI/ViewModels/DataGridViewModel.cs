﻿using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Leetcode.WinUI.Contracts.ViewModels;
using Leetcode.WinUI.Core.Contracts.Services;
using Leetcode.WinUI.Core.Models;

namespace Leetcode.WinUI.ViewModels;

public class DataGridViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

    public DataGridViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetGridDataAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
