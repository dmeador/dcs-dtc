using System;
using System.Text.RegularExpressions;

using System.Collections.Generic;
namespace DTC.Models.F16.Waypoints
{
	public class Waypoint
	{
		private static Regex coordRegex = new Regex("^([N|S] \\d\\d\\.\\d\\d\\.\\d\\d\\d) ([E|W] \\d\\d\\d\\.\\d\\d\\.\\d\\d\\d)$");
		private static Regex tosRegex = new Regex("^([0-9 ] +):([0-9 ]+):([0-9 ]*)$");

		public int Sequence { get; set; }
		public string Name { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public int Elevation { get; set; }
		public string TimeOnStation { get; set; }

		public bool Blank {
			get
			{
				var tmp = Latitude.Replace("N", "").Replace("S", "").Replace(".", "");
				if (int.TryParse(tmp, out int latInt))
				{
					if (latInt == 0)
					{
						return true;
					}
				}
				return false;
			}
		}

		public Waypoint(int seq, string name, string latitude, string longitude, int elevation, string tos)
		{
			Sequence = seq;
			Name = name;
			Latitude = latitude;
			Longitude = longitude;
			Elevation = elevation;
			TimeOnStation = tos;
		}

		public static Waypoint FromStrings(string name, string coord, string elevation, string tos)
		{
			var match = coordRegex.Match(coord);
			var wpt = new Waypoint(0, name, match.Groups[1].Value, match.Groups[2].Value, int.Parse(elevation), tos);
			return wpt;
		}

		public static bool IsCoordinateValid(string coord)
		{
			var match = coordRegex.Match(coord);
			return match.Success;
		}

		private static List<int> GetTimeOnStation(string tos)
	{
			List<int> tos_result = new List<int>();
			//var match = tosRegex.Match(tos);
			//string pattern = @"([0-9 ]+):([0-9 ]+):([0-9 ]*)";
			string[] tokens = tos.Split(':');

			if(tokens.Length > 0)
			{
				for ( int i = 0; i<  tokens.Length; i++)
                {
					string field = tokens[i];
					int value = 0;
					if (field.Trim() != "")
					{ 
						value = Convert.ToInt32(field);
					}
					tos_result.Add(value);
				}
			}
			return tos_result;
		}

		public static string SanitizeTosString(string tos)
		{
			string str_Result = "";
			if(tos.Length > 0)
            {
				List<int> tos_array = GetTimeOnStation(tos);
				if (tos_array.Count == 0)
				{
					// insert a "00" in slot 1
					tos_array.Insert(0, 0);
				}
				if (tos_array.Count == 1)
				{
					// insert a "00" in slot 1
					tos_array.Insert(0, 0);
				}
				if (tos_array.Count == 2)
				{
					// insert a "00" in slot 1
					tos_array.Insert(0, 0);
				}
				for (int i = 0; i < 3; i++)
				{
					str_Result += tos_array[i].ToString().PadLeft(2, '0');
				}
			}
			return str_Result;
		}


	}
}
