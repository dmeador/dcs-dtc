using DTC.Models.DCS;
using DTC.Models.F16.Waypoints;
using System.Text;
using System;

namespace DTC.Models.F16.Upload
{
	public class WaypointBuilder : BaseBuilder
	{
		private F16Configuration _cfg;

		public WaypointBuilder(F16Configuration cfg, IAircraftDeviceManager aircraft, StringBuilder sb) : base(aircraft, sb)
		{
			_cfg = cfg;
		}

		public override void Build()
		{
			var wpts = _cfg.Waypoints.Waypoints;
			var wptStart = _cfg.Waypoints.SteerpointStart;
			
			var wptEnd = wptStart + wpts.Count;
			
			if(_cfg.Waypoints.SteerpointEnd < wptEnd)
			{
				wptEnd = _cfg.Waypoints.SteerpointEnd;
			}
			if (wpts.Count == 0)
			{
				return;
			}

			var wptDiff = wptEnd - wptStart + 1;

			var ufc = _aircraft.GetDevice("UFC");

			AppendCommand(ufc.GetCommand("RTN"));
			AppendCommand(ufc.GetCommand("RTN"));
			AppendCommand(ufc.GetCommand("4"));

			for (var i = 0; i < wptDiff; i++)
			{
				Waypoint wpt;
				if (i < wpts.Count)
				{
					wpt = wpts[i];
				}
				else
				{
					// before //Repeats the last waypoint till it fills
					//wpt = wpts[wpts.Count - 1];
					// create a blank waypoint to fill in or overwrite data
					wpt = new Waypoint(i, "", "", "", 0, "");
				}

				if (wpt.Blank)
				{
					// enter dummy lat lon
					//continue;
					AppendCommand(BuildDigits(ufc, (i + wptStart).ToString()));
					AppendCommand(ufc.GetCommand("ENTR"));
					AppendCommand(ufc.GetCommand("DOWN"));
					AppendCommand(ufc.GetCommand("DOWN"));

					AppendCommand(BuildCoordinate(ufc, "N 00 00.000"));
					AppendCommand(ufc.GetCommand("ENTR"));
					AppendCommand(ufc.GetCommand("DOWN"));

					AppendCommand(BuildCoordinate(ufc, "W 00 00.000"));
					AppendCommand(ufc.GetCommand("ENTR"));
					AppendCommand(ufc.GetCommand("DOWN"));

					AppendCommand(BuildDigits(ufc, "0"));  // elev
					AppendCommand(ufc.GetCommand("ENTR")); // elev done
					AppendCommand(ufc.GetCommand("DOWN"));
					AppendCommand(BuildDigits(ufc, "000000"));
					AppendCommand(ufc.GetCommand("ENTR")); // TOS done
					AppendCommand(ufc.GetCommand("DOWN"));

					Console.WriteLine($"WaypointBuilder.Build - blank - {(i + wptStart):d}, N 00 00.000, W 00 00.000, 0, 000000 ");
				}
                else
                {
					if (wpt.Latitude.Length == 0)
					{
						continue;
					}
					if (wpt.Longitude.Length == 0)
					{
						continue;
					}

					AppendCommand(BuildDigits(ufc, (i + wptStart).ToString()));
					AppendCommand(ufc.GetCommand("ENTR"));
					AppendCommand(ufc.GetCommand("DOWN"));
					AppendCommand(ufc.GetCommand("DOWN"));
					if(wpt.Latitude.Length == 0)
                    {
						AppendCommand(BuildCoordinate(ufc, "N 00 00.000"));
					}
					else
                    {
						AppendCommand(BuildCoordinate(ufc, wpt.Latitude));

					}
					AppendCommand(ufc.GetCommand("ENTR"));
					AppendCommand(ufc.GetCommand("DOWN"));

					if (wpt.Longitude.Length == 0)
					{
						AppendCommand(BuildCoordinate(ufc, "W000 00.000"));
					}
					else
					{
						AppendCommand(BuildCoordinate(ufc, wpt.Longitude));
					}
					AppendCommand(ufc.GetCommand("ENTR"));
					AppendCommand(ufc.GetCommand("DOWN"));

					AppendCommand(BuildDigits(ufc, wpt.Elevation.ToString()));
					AppendCommand(ufc.GetCommand("ENTR")); 
					AppendCommand(ufc.GetCommand("DOWN"));

					String totString = Waypoint.SanitizeTosString(wpt.TimeOnStation);
					// strip and fixup TOS format to 6-digit integer string

					AppendCommand(BuildDigits(ufc, totString));
					AppendCommand(ufc.GetCommand("ENTR"));
					AppendCommand(ufc.GetCommand("DOWN"));
					Console.WriteLine($"WaypointBuilder.Build - {(i + wptStart):d}, {wpt.Latitude},  {wpt.Latitude}, {wpt.Elevation:d}, {wpt.TimeOnStation} ");
				}

			}

			AppendCommand(ufc.GetCommand("1"));
			AppendCommand(ufc.GetCommand("ENTR"));
			AppendCommand(ufc.GetCommand("RTN"));
		}

		private string BuildCoordinate(Device ufc, string coord)
		{
			var sb = new StringBuilder();

			var latStr = RemoveSeparators(coord.Replace(" ", ""));

			foreach (var c in latStr.ToCharArray())
			{
				if (c == 'N')
				{
					sb.Append(ufc.GetCommand("2"));
				}
				else if (c == 'S')
				{
					sb.Append(ufc.GetCommand("8"));
				}
				else if (c == 'E')
				{
					sb.Append(ufc.GetCommand("6"));
				}
				else if (c == 'W')
				{
					sb.Append(ufc.GetCommand("4"));
				}
				else
				{
					sb.Append(ufc.GetCommand(c.ToString()));
				}
			}

			Console.WriteLine("WaypointBuilder.Build - buildcoord - " + sb.ToString());
			return sb.ToString();
		}
	}
}
