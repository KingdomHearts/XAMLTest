using Android.App;
using Android.Content;
using Android.OS;
using XAMLTest.Data;

[Service]
public class BackgroundProcess : Service
{
    public override IBinder OnBind(Intent intent)
    {
        return null;
    }

    public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
    {
        // From shared code or in your PCL


        return StartCommandResult.NotSticky;
    }
}
[BroadcastReceiver]
public class BackgroundReceiver : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
        PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "BackgroundReceiver");
        wakeLock.Acquire();

        // Run your code here
        var objectjson = CalendarMockData.GetJsonFileCalendar();



        wakeLock.Release();
    }
}