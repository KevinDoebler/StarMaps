using System;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.Util;
using Android.Widget;

namespace StarMaps
{
	[BroadcastReceiver]
	public class ProximityIntentReceiver : BroadcastReceiver
	{
		private const int NOTIFICATION_ID = 1000;

		public override void OnReceive(Context context, Intent intent)
		{
			// Get extras from broadcast
			String name = intent.GetStringExtra("Name");
			String key = LocationManager.KeyProximityEntering;

			// Are we entering or exiting the proximity/location
			Boolean entering = intent.GetBooleanExtra(key, false);

			Log.Info("Vegas", "Broadcast Intent Received: " + intent.GetStringExtra("Name"));
			Log.Info("Vegas", "Broadcast Intent Entering? " + entering);

			PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);
			String notificationText;

			if (entering)
			{
				notificationText = "Welcome to " + name;
				Toast.MakeText(context, notificationText, ToastLength.Short).Show();
			}
			else
			{
				notificationText = "Thank you for visiting " + name;
				Toast.MakeText(context, notificationText, ToastLength.Short).Show();
			}

			// Show notification
			try
			{
				Utilities notificationUtility = new Utilities(context);
				notificationUtility.ShowNotification(notificationText, notificationText, NOTIFICATION_ID, pendingIntent);
			}
			catch (Java.Lang.IllegalArgumentException ex)
			{
				Log.Info("Vegas", "ERROR!!!!! " + ex.Message);
				if (ex.InnerException != null)
				{
					Log.Info("Vegas", "INNER EXCEPTION!!! " + ex.InnerException);
				}
			}

			PendingIntent proximityIntent = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
			proximityIntent.Cancel();
		}
	}
}