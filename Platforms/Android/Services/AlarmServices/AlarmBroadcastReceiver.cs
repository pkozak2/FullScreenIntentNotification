using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

namespace FullScreenIntentNotification.Platforms.Android.Services.AlarmServices;
[BroadcastReceiver(Enabled = true, Exported = false)]
public class AlarmBroadcastReceiver : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        var fullScreenIntent = new Intent(context, typeof(MainActivity));
        fullScreenIntent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);
        var fullScreenPendingIntent = PendingIntent.GetActivity(context, 0, fullScreenIntent, PendingIntentFlags.Immutable);
        var mBuilder = new NotificationCompat.Builder(context, "69420")
            .SetContentTitle("New full screen intent notification")
            .SetAutoCancel(true)
            .SetContentTitle("A notification has arrived!")
            .SetContentText("A new full screen intent notification has arrived")
            .SetPriority((int)NotificationPriority.Max)
            .SetVibrate(new long[0])
            .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
            .SetVisibility((int)NotificationVisibility.Public)
            .SetSmallIcon(_Microsoft.Android.Resource.Designer.ResourceConstant.Drawable.notification_template_icon_bg)
            .SetShowWhen(true)
            .SetFullScreenIntent(fullScreenPendingIntent, true);

        var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var importance = NotificationImportance.Max;
            var notificationChannel = new NotificationChannel("69420", "title", importance);
            notificationChannel.EnableLights(true);
            notificationChannel.EnableVibration(true);
            notificationChannel.SetShowBadge(true);
            notificationChannel.Importance = NotificationImportance.High;
            notificationChannel.LockscreenVisibility = NotificationVisibility.Public;
            notificationChannel.ShouldVibrate();
            notificationChannel.ShouldShowLights();
            notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

            if (notificationManager != null)
            {
                mBuilder.SetChannelId("69420");
                notificationManager.CreateNotificationChannel(notificationChannel);
            }
        }

        notificationManager.Notify(Guid.NewGuid().GetHashCode(), mBuilder.Build());
    }
}
