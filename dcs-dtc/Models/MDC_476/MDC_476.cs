using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DTC.Models.MDC_476.Waypoints;
using System.Windows.Forms;
using System.IO;
using System.Text;
using DTC.Models.Base;

namespace DTC.Models.MDC_476
{
	public class MDC_476
	{
		// waypointRegex fields
		// 1 = Type; 2 = Name; 3 = Fix/Point; 4 = Location / LatLong; 5 = elev; 6=Alt; 7=SPD; 8= ETE/TOT; 9=Leg Fuel
		//private static Regex waypointRegex = new Regex(@"^([0-9]+)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)$");
		private static Regex waypointLineRegex =
		new Regex(@"(.*)(?:\t)");

		private static Regex waypointRegex =
			//new Regex(@"^([0-9]+)\t([a-zA-Z0-9\. ]*)\t([a-zA-Z0-9\.\-_ ]*)\t([a-zA-Z0-9\.\-_ ]*)\t([0-9NSEWnsew°'\. ]*)\t([0-9FL<>\. ]*)\t([0-9FL<>\. ]*)\t(.*)\t(.*)[\t\r\n]$");
		new Regex(@"^([0-9]+)\t([a-zA-Z0-9\. ]*)\t([a-zA-Z0-9\.\-_ ]*)\t([a-zA-Z0-9\.\-_ ]*)\t([0-9NSEWnsew°'\. ]*)\t([0-9FL<>\. ]*)\t([0-9FL<>\. ]*).*[\t\r\n]$");
		private static Regex coordRegex = new Regex(@"^([N|S][ \t]*[0-9]+[°]?[ \t]*[0-9]+\.[0-9]+)['`]?[ \t]*([E|W][ \t]*[0-9]+[°]?[ \t]*[0-9]+\.[0-9]+)['`]?");
		private static Regex coord2Regex = new Regex(@"^([N|S|E|W])[ \t]*([0-9]+)[°]?[ \t]*([0-9]+)\.([0-9]+)['`]?");
		private static Regex totRegex = new Regex(@"^([0-9]{2}$:[0-9]{2}$)");


		public WaypointSystem Waypoints;
		public string MDC_data;

		public MDC_476()
		{
			Waypoints = new WaypointSystem();
		}

		public string GetClipboard()
        {
			MDC_data = Clipboard.GetText();
			return MDC_data;
		}

		public bool ParseFlightPlan_to_Waypoints()
        {
			StringBuilder stringToRead = new StringBuilder();
			stringToRead.Append(MDC_data);
			// find "Flight Plan"
			// find "# <> Type <> Name <> Fix/Point <> Location <> Elev <> Alt <> SPD <> ETE/TOT <> Leg Fuel
			using (StringReader reader = new StringReader(stringToRead.ToString()))
            {
				int mode_search = 0;
				string line = reader.ReadLine();
				while( line != null)
                {
					if(mode_search == 0)
                    {
						if (line.Contains("Flight Plan"))
						{
							mode_search = 1;
						}
						else if(line.Contains("Type") && line.Contains("Name") && line.Contains("Fix"))
                        {
							mode_search = 2;
						}
						line = reader.ReadLine(); // read another line
						continue;
					}
					if (mode_search == 1)
					{
						if((line.Contains("Type") && line.Contains("Name") && line.Contains("Fix")))
                        {
							mode_search = 2;
						}
						else
                        {
							// this else would be strange!
                        }
						line = reader.ReadLine(); // read another line
						continue;
					}
					if (mode_search == 2)
					{
						if (IsFlightPlanLineValid(line))
						{
							//mode_search = 3;

							// parse & add waypoint!!
							Waypoint wpt = WaypointFromLine(line);
							Waypoints.Add(wpt);
						}
						else
						{
							// done with flight plan
							break;
						}
						line = reader.ReadLine(); // read another line
						continue;
					}
					// otherwise we may just have some waypoints -- check for a valid waypoint line format
					//1	TRN	<name>	CRAIG	N 36°15.250' W 115°08.267'	<elev>	5500	350	<ete/tot>	<leg fuel>
					//2	TRN	<name>	MINTT	N 36°42.733' W 115°06.283'	<elev>	17000	350	<ete/tot>>	<leg fuel>	
				}
			}
			Waypoints.RemoveTrainingEmpty();
			return true;
        }

		public static string ConvertAltitude(string altitude)
        {
			string valid_altitude;
			int new_altitude = 0;
			if (altitude.Contains("FL"))
			{
				// convert FL### to feet
				int location = altitude.IndexOf("FL");
				location += 2;
				string fl_string = altitude.Substring(location, 3);
				new_altitude = Convert.ToInt32(fl_string) * 100;
				valid_altitude = new_altitude.ToString();
				new_altitude = Convert.ToInt32(valid_altitude);
				if(altitude.Length > 0)
					Console.WriteLine("convert " + altitude + " " + new_altitude.ToString());

			}
			else if (altitude.Contains("."))
			{
				// convert FL### to feet
				int location = altitude.IndexOf(".");
				string left = altitude.Substring(0, location);
				string right= altitude.Substring(location);
				new_altitude = Convert.ToInt32(left)*1000 + Convert.ToInt32(Convert.ToDouble(right)*100); 
				valid_altitude = new_altitude.ToString();
				if(valid_altitude.Length > 0)
					Console.WriteLine("convert " + altitude + " " + valid_altitude);
			}
			else
			{
				//string invalidChars = @"[^0-9]";
				var result = Regex.Replace(altitude, @"[^0-9]", string.Empty); // replace invalid chars with "" nothing.
				if(altitude.Length > 0)
					Console.WriteLine("convert " + altitude + " " + result);
				valid_altitude = result;
			}
			return valid_altitude;
		}

		public static Waypoint WaypointFromLine(string line)
		{
			// waypointRegex fields
			//  1 = Number; 2 = Type; 3 = Name; 4 = Fix/Point; 5 = Location / LatLong; 6 = elev; 7=Alt; 8=SPD; 9= ETE/TOT; 10=Leg Fuel
			line = line.ToUpper();
			line = line.Replace('°', ' ');
			//var match = waypointRegex.Match(line);
			//string str_num = match.Groups[1].Value;
			/*
			int wpt_num = Convert.ToInt32(match.Groups[1].Value.Trim());
			string name = match.Groups[3].Value.Trim();
			string fix_point = match.Groups[4].Value.Trim();
			string lat_long = match.Groups[5].Value.Trim();
			string str_elevation = match.Groups[6].Value.Trim();
			string str_altitude = match.Groups[7].Value.Trim();
			string str_speed = match.Groups[8].Value.Trim();
			string str_tot = match.Groups[9].Value.Trim();
			*/
			int wpt_num = 0;
			string wptype = "";
			string name = ""; 
			string fix_point = "";  
			string lat_long = "";
			string str_elevation = "";
			string str_altitude = "";
			string str_speed = "";
			string str_tot = "";

			string[] token = line.Split('\t'); ;
			for (int i = 0; i < token.Length; i++)
			{
				string field = token[i];
				switch (i+1)
                {
					case 1:
						wpt_num = Convert.ToInt32(field.Trim());
						break;
					case 2:
						wptype = field.Trim();
						break;
					case 3:
						name = field.Trim();
						break;
					case 4:
						fix_point = field.Trim();
						break;
					case 5:
						lat_long = field.Trim();
						break;
					case 6:
						str_elevation = field.Trim();
						break;
					case 7:
						str_altitude = field.Trim();
						break;
					case 8:
						str_speed = field.Trim();
						break;
					case 9:
						str_tot = field.Trim();
						break;
				}
			}

			int elevation = 0;
			if (str_elevation.Length > 0)
            {
				elevation = Convert.ToInt32(str_elevation);
			}
			int altitude = 0;
			// Load Altitude as Elevation when configured
			if(Settings.AltitudeAsElev > 0)
            {
				str_altitude = ConvertAltitude(str_altitude);

				if (str_altitude.Length > 0)
				{
					altitude = Convert.ToInt32(str_altitude);
				}
				if(elevation == 0 && altitude != 0)
				{
					elevation = altitude;

				}
			}

			var matchCoord = coordRegex.Match(lat_long);
			string lat = "";
			string lon = "";

			if (matchCoord.Success)
            {
				lat = matchCoord.Groups[1].Value.Trim();
				lon = matchCoord.Groups[2].Value.Trim();

				// rewrite lat & lon to match desired DTC format
				var matchLat = coord2Regex.Match(lat);
				if(matchLat.Success)
                {
					lat = matchLat.Groups[1].Value + " " + matchLat.Groups[2].Value + "." + matchLat.Groups[3].Value + "." + matchLat.Groups[4].Value;
				}
				var matchLon = coord2Regex.Match(lon);
				if (matchLon.Success)
				{
					lon = matchLon.Groups[1].Value + " " + matchLon.Groups[2].Value + "." + matchLon.Groups[3].Value + "." + matchLon.Groups[4].Value;
				}
			}

			if (name.Length == 0)
            {
				name = fix_point;
            }

			var wpt = new Waypoint(wpt_num, name, lat, lon, elevation, str_tot);
			Console.WriteLine("waypoint #" + wpt_num + " name:" + name + " Lat:" + lat + " Lon:" + lon  + " Elev:" + elevation.ToString() + " Alt:" + str_altitude 
				+ " Spd:" + str_speed + " TOT:" + str_tot);
			return wpt;
		}

		public static bool IsFlightPlanLineValid(string line)
		{
			bool result = false;
			string[] token = line.Split('\t'); ;
			for(int i = 0; i< token.Length; i++)
			{
				string field = token[i];
				if(i>=5)
                {
					result = true;
                }
			} 
			return result;
		}

		public static bool IsCoordinateValid(string coord)
		{
			var match = waypointRegex.Match(coord);
			return match.Success;
		}
	}
}
