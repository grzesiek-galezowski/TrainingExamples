using Microsoft.Extensions.Logging;
using MutaSynthEngine;
using MutaSynthEngine.Platforms.Windows;

namespace MutaSynth;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        IAudioDriver audioDriver = new WindowsAudioService();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton(audioDriver);
        builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

