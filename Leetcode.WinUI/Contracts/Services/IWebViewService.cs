﻿using System;

using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace Leetcode.WinUI.Contracts.Services;

public interface IWebViewService
{
    bool CanGoBack
    {
        get;
    }

    bool CanGoForward
    {
        get;
    }

    event EventHandler<CoreWebView2WebErrorStatus> NavigationCompleted;

    void Initialize(WebView2 webView);

    void GoBack();

    void GoForward();

    void Reload();

    void UnregisterEvents();
}
