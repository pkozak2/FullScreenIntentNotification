using FullScreenIntentNotification.Interfaces;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace FullScreenIntentNotification;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseLocalNotification(options =>
            {
                options.AddAndroid(o =>
                {
                    // o.AddChannelGroup(new Plugin.LocalNotification.AndroidOption.NotificationChannelGroupRequest
                    // {
                    //     Group = "com.group10.healthmate",
                    //     Name = "com.group10.healthmate"
                    // });
                    o.AddChannel(new Plugin.LocalNotification.AndroidOption.NotificationChannelRequest
                    {
                        Id = "com.group10.healthmate1",
                        Name = "TEST",
                        Importance = Plugin.LocalNotification.AndroidOption.AndroidImportance.Max,
                        Description = "g",
                        ShowBadge = true,
                        //Group = "com.group10.healthmate",
                        LockScreenVisibility = Plugin.LocalNotification.AndroidOption.AndroidVisibilityType.Public,
                        CanBypassDnd = true,

                    });
                });
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

#if ANDROID
        builder.Services.AddSingleton<IAlarmScheduler, Platforms.Android.Services.AlarmServices.AlarmScheduler>();
#endif
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();
        //builder.Services.AddSingleton(_ => LocalNotificationCenter.Current);

        return builder.Build();
    }
}
