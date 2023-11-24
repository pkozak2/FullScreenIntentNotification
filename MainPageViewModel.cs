using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FullScreenIntentNotification.Interfaces;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;

namespace FullScreenIntentNotification;
public partial class MainPageViewModel : ObservableObject
{
    private readonly IAlarmScheduler _alarmScheduler;
    private readonly INotificationService _notificationService;

    public MainPageViewModel(IAlarmScheduler alarmScheduler, INotificationService notificationService)
    {
        _alarmScheduler = alarmScheduler;
        _notificationService = notificationService;
    }

    [RelayCommand]
    public async Task ScheduleAlarm()
    {
        var randomIndex = Random.Shared.Next(14);
        var notificationIcon = new AndroidIcon { ResourceName = "notif_icon" };
        var notificationRequest = new NotificationRequest
        {
            Android = new AndroidOptions
            {
                ChannelId = "com.group10.healthmate",
                IconLargeName = notificationIcon,
                IconSmallName = notificationIcon,
                IsGroupSummary = true,
                LaunchApp = new AndroidLaunch(),
                Priority = AndroidPriority.Max,
                VibrationPattern = [200, 300, 200, 300, 200, 300],
                VisibilityType = AndroidVisibilityType.Public
            },
            CategoryType = NotificationCategoryType.Alarm,
            Description = "Test description",
            Group = "com.group10.healthmate",
            //Image = new NotificationImage
            //{
            //    FilePath
            //},
            NotificationId = Guid.NewGuid().GetHashCode(),
            Schedule = new NotificationRequestSchedule
            {
                Android = new AndroidScheduleOptions
                {
                    AlarmType = AndroidAlarmType.RtcWakeup,
                },
                NotifyTime = DateTime.Now.AddSeconds(5),
                RepeatType = NotificationRepeat.No
            },
            Subtitle = "subtitle",
            Title = "title"
        };

        await _notificationService.Show(notificationRequest);
    }
}
