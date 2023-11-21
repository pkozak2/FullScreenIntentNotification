using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FullScreenIntentNotification.Interfaces;

namespace FullScreenIntentNotification;
public partial class MainPageViewModel : ObservableObject
{
    private readonly IAlarmScheduler _alarmScheduler;

    public MainPageViewModel(IAlarmScheduler alarmScheduler)
    {
        _alarmScheduler = alarmScheduler;
    }

    [RelayCommand]
    public void ScheduleAlarm()
    {
        _alarmScheduler.ScheduleAlarm(DateTime.Now.AddSeconds(5));
    }
}
