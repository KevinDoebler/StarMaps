using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StarMaps.Models;

namespace StarMaps.Services
{
	public class LocationDataService
	{
		public List<LocationModel> GetLocations()
		{
			return new List<LocationModel>();
		}

		public List<LocationModel> GetFakeLocations()
		{
			LocationModel location1 = new LocationModel();
			location1.ID = 1;
			location1.Name = "Guardian Buildling";
			location1.Address1 = "500 Griswold St.";
			location1.Address2 = "";
			location1.City = "Detroit";
			location1.State = "MI";
			location1.Zip = "48226-3480";
			location1.Latitude = 42.329749;
			location1.Longitude = -83.046123;
			location1.Radius = 100;

			LocationModel location2 = new LocationModel();
			location2.ID = 2;
			location2.Name = "Campus Martius Park";
			location2.Address1 = "800 Woodward Ave.";
			location2.Address2 = "";
			location2.City = "Detroit";
			location2.State = "MI";
			location2.Zip = "48226-3580";
			location2.Latitude = 42.331746;
			location2.Longitude = -83.046846;
			location2.Radius = 100000;

			LocationModel location3 = new LocationModel();
			location3.ID = 3;
			location3.Name = "Masonic Temple";
			location3.Address1 = "500 Temple St";
			location3.City = "Detroit";
			location3.State = "MI";
			location3.Zip = "48201-2659";

			location3.Latitude = 42.342159;
			location3.Longitude = -83.059850;
			location3.Radius = 100;

			LocationModel location4 = new LocationModel();
			location4.ID = 4;
			location4.Name = "Doebville!";
			location4.Latitude = 42.562647;
			location4.Longitude = -83.008016;
			location4.Radius = 1000;

			List<LocationModel> locations = new List<LocationModel>();
			locations.Add(location1);
			locations.Add(location2);
			locations.Add(location3);
			locations.Add(location4);

			return locations;
		}
	}
}