using Android.App;
using Android.Content;
using Android.OS;
using FullScreenIntentNotification.Interfaces;
using Application = Android.App.Application;

namespace FullScreenIntentNotification.Platforms.Android.Services.AlarmServices;
public class AlarmScheduler : IAlarmScheduler
{
    public void ScheduleAlarm(DateTime alarmTime)
    {
        var intent = new Intent(Application.Context, typeof(AlarmBroadcastReceiver));
        var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.Immutable);
        var triggerTime = GetMillisecondsSinceEpoch(alarmTime);

        if (Application.Context.GetSystemService(Context.AlarmService) is AlarmManager alarmManager)
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, triggerTime, pendingIntent);
            else
                alarmManager.SetExact(AlarmType.RtcWakeup, triggerTime, pendingIntent);
    }

    private long GetMillisecondsSinceEpoch(DateTime dateTime)
    {
        var epoch = new DateTime(1970, 1, 1);
        var timeSpan = dateTime.ToUniversalTime() - epoch;
        return (long)timeSpan.TotalMilliseconds;
    }
}