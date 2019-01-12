using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;

namespace StarMaps
{
	class Utilities
	{
		private Context context;
		private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		static readonly string CHANNEL_ID = "location_notification";

		public Utilities(Context context)
		{
			this.context = context;
		}

		public void ShowNotification(string notificationTitle, string notificationText, int notificationID, PendingIntent pendingIntent)
		{
			CreateNotificationChannel();

			// Build the notification:
			var builder = new NotificationCompat.Builder(context, CHANNEL_ID)
						  .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
						  .SetContentIntent(pendingIntent) // Start up this activity when the user clicks the intent.
						  .SetContentTitle(notificationTitle) // Set the title
						  .SetNumber(1) // Display the count in the Content Info
						  .SetSmallIcon(Resource.Drawable.icon) // This is the icon to display
						  .SetContentText(notificationText); // the message to display.

			// Finally, publish the notification:
			var notificationManager = NotificationManagerCompat.From(context);
			notificationManager.Notify(notificationID, builder.Build());
		}

		void CreateNotificationChannel() 
		{
			if (Build.VERSION.SdkInt < BuildVersionCodes.O)
			{
				// Notification channels are new in API 26 (and not a part of the
				// support library). There is no need to create a notification
				// channel on older versions of Android.
				return;
			}

			var name = context.Resources.GetString(Resource.String.channel_name);
			var description = context.GetString(Resource.String.channel_description);
			var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
			{
				Description = description
			};

			var notificationManager = (NotificationManager)context.GetSystemService(Android.Content.Context.NotificationService);
			notificationManager.CreateNotificationChannel(channel);
		}
		

		public static long CurrentTimeMillis()
		{
			return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
		}

		private Notification createNotification()
		{
			Notification notification = new Notification();

			notification.When = CurrentTimeMillis();

			notification.Icon = Resource.Drawable.icon;

			notification.Flags |= NotificationFlags.AutoCancel;
			notification.Flags |= NotificationFlags.ShowLights;

			Boolean vibrate = true;
			if (vibrate)
			{
				notification.Defaults |= NotificationDefaults.Vibrate;
			}

			notification.Defaults |= NotificationDefaults.Lights;

			notification.LedARGB = Android.Graphics.Color.White;
			notification.LedOnMS = 1500;
			notification.LedOffMS = 1500;

			return notification;
		}
	}
}