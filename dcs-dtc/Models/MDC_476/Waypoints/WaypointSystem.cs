using System.Collections.Generic;

namespace DTC.Models.MDC_476.Waypoints
{
	public class WaypointSystem
	{
		public List<Waypoint> Waypoints { get; set; }
		public int SteerpointStart { get; set; }
		public int SteerpointEnd { get; set; }
		public bool EnableUpload { get; set; }

		public WaypointSystem()
		{
			Waypoints = new List<Waypoint>();
			SteerpointStart = 1;
			SteerpointEnd = 20;
			EnableUpload = true;
		}

		public Waypoint Add(Waypoint wpt)
		{
			var seq = Waypoints.Count + 1;
			wpt.Sequence = seq;
			Waypoints.Add(wpt);
			return wpt;
		}

		public void RemoveTrainingEmpty()
		{
			if(Waypoints.Count > 0) 
			for (int i = Waypoints.Count - 1; i >= 0; i--)
			{
				Waypoint wpt = Waypoints[i];
				if (wpt.Latitude.Length == 0 && wpt.Longitude.Length == 0)
                {
					Waypoints.RemoveAt(i);
                }
                else
                {
					break;
                }
			}
		}
		public void SetSteerpointStart(int v)
		{
			if (v >= 1 && v <= SteerpointEnd)
			{
				SteerpointStart = v;
			}
		}

		public void SetSteerpointEnd(int v)
		{
			if (v >= SteerpointStart && v <= 127)
			{
				SteerpointEnd = v;
			}
		}

		public void Remove(Waypoint wpt)
		{
			Waypoints.Remove(wpt);
			RecalculateSequence();
		}

		public void Reorder(int idxFrom, int idxTo)
		{
			var wpt = Waypoints[idxFrom];
			Waypoints.Remove(wpt);
			Waypoints.Insert(idxTo, wpt);
			RecalculateSequence();
		}

		private void RecalculateSequence()
		{
			for (int i = 0; i < Waypoints.Count; i++)
			{
				Waypoint wpt = Waypoints[i];
				wpt.Sequence = i + 1;
			}
		}

		public DTC.Models.F16.TOS.TOSSystem GetTimeOnTargetsSetting()
        {
			DTC.Models.F16.TOS.TOSSystem TOSSettings = new DTC.Models.F16.TOS.TOSSystem();
			for (int i = 0; i < Waypoints.Count; i++)
			{
				TOSSettings.SetTime(i+1, Waypoints[i].TimeOnTarget);
			}
			return TOSSettings;
		}
	}
}
