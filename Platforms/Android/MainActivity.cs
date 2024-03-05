using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using AndroidX.Core.Content;

namespace FullScreenIntentNotification;
[Activity(Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize |
    ConfigChanges.Orientation |
    ConfigChanges.UiMode |
    ConfigChanges.ScreenLayout |
    ConfigChanges.SmallestScreenSize |
    ConfigChanges.Density,
    ShowForAllUsers = true)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetShowWhenLocked(true);
        SetTurnScreenOn(true);
        RequestedPermission();
    }

    private void RequestedPermission()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.UpsideDownCake)
        {
            var nManager = (NotificationManager)MainApplication.Context.GetSystemService(Context.NotificationService);
            if (!nManager.CanUseFullScreenIntent())
            {
                var intent = new Intent();
                intent.SetAction(Settings.ActionManageAppUseFullScreenIntent);
                intent.SetData(Android.Net.Uri.Parse($"package:{MainApplication.Context.PackageName}"));
                intent.AddFlags(ActivityFlags.NoHistory);
                ContextCompat.StartActivity(MainApplication.Context, intent, null);
            }
        }
    }
}
