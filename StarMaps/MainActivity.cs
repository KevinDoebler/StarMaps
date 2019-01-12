using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Locations;
using Android.Content;
using Android.Util;
using System;
using StarMaps.Services;
using System.Collections.Generic;
using StarMaps.Models;
using Android.Runtime;

namespace StarMaps
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, ILocationListener
	{
		TextView textMessage;
		string ACTION_FILTER = "StarMaps.StarMaps.ProximityIntentReceiver";
		public LocationManager locationManager;
		public ProximityIntentReceiver proximityIntentReceiver;

		double lat, long1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			textMessage = FindViewById<TextView>(Resource.Id.message);
			BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
			navigation.SetOnNavigationItemSelectedListener(this);

			RegisterReceiver(new ProximityIntentReceiver(), new IntentFilter(ACTION_FILTER));

			locationManager = (LocationManager)GetSystemService(LocationService);

			//for debugging...
			try
			{
				locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 1000, 10, this);
			}
			catch (Exception ex)
			{
				Log.Error("Vegas", ex.Message.ToString());
			}

			AddLocationAlerts();
		}

		public bool OnNavigationItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.navigation_home:
					textMessage.SetText(Resource.String.title_home);
					return true;
				case Resource.Id.navigation_dashboard:

					textMessage.SetText(Resource.String.title_dashboard);
					// AddLocationAlerts();
					return true;

				case Resource.Id.navigation_notifications:
					textMessage.SetText(Resource.String.title_notifications);
					return true;
			}
			return false;
		}

		private void AddLocationAlerts()
		{
			LocationDataService locationService = new LocationDataService();

			List<LocationModel> locations = new List<LocationModel>();
			locations = locationService.GetFakeLocations();

			foreach (LocationModel location in locations)
			{
				//Setting up My Broadcast Intent
				AddProximityAlert(location.Name, location.Latitude, location.Longitude, location.Radius);
			}
		}

		public void AddProximityAlert(String name, double latitude, double longitude, float range)
		{
			Log.Info("Vegas", "Adding Location: " + name);
			Intent intent = new Intent(ACTION_FILTER);

			intent.PutExtra("Name", name);

			PendingIntent proximityIntent = PendingIntent.GetBroadcast(this, 0, intent, PendingIntentFlags.UpdateCurrent);

			locationManager.AddProximityAlert(latitude, longitude, range, 10000, proximityIntent);
		}



		void ILocationListener.OnLocationChanged(Location newLocation)
		{
			double currentLat, currentLong;

			currentLat = newLocation.Latitude;
			currentLong = newLocation.Longitude;
		}

		void ILocationListener.OnProviderDisabled(string provider)
		{
		}

		void ILocationListener.OnProviderEnabled(string provider)
		{
		}

		void ILocationListener.OnStatusChanged(string provider, Availability status, Bundle extras)
		{
		}
	}
}

