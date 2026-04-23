using MutaSynthEngine;

#if WINDOWS
using Microsoft.Maui.Controls.Compatibility.Platform.UWP;
#endif

namespace MutaSynth;

public partial class App : Application
{
    private readonly IAudioDriver _audioDriver;
    private readonly MainPage _mainPage;

    public App(MainPage mainPage, IAudioDriver audioDriver)
    {
        InitializeComponent();
        _audioDriver = audioDriver;
        _mainPage = mainPage;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(_mainPage);
        window.Destroying += OnWindowDestroying;
        window.Stopped += OnWindowStopped;
        return window;
    }

    private void OnWindowDestroying(object? sender, EventArgs e)
    {
        _audioDriver.Dispose();
        QuitApplication();
    }

    private void OnWindowStopped(object? sender, EventArgs e)
    {
        _audioDriver.Dispose();
        QuitApplication();
    }

    private static void QuitApplication()
    {
#if WINDOWS
        MauiWinUIApplication.Current?.Exit();
#endif
    }
}
