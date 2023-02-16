using DTC.Models.Presets;
using DTC.Models.Base;
using DTC.UI.Base.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTC.Models.F16;
using DTC.Models.F16.Waypoints;
using DTC.Models.FA18;
using DTC.Models.AH64;

namespace DTC.UI.CommonPages
{
	public class WaypointsEventArgs : EventArgs
	{
		public WaypointsEventArgs(WaypointSystem wpts)
		{
			Wpts = wpts;
		}
		public WaypointSystem Wpts { get; set; }
	}



	public partial class AircraftPage : Page
	{
		protected readonly Aircraft _aircraft;
		//protected readonly Preset _preset;
		protected Preset _preset;

		public delegate void WaypointChangedHandler(object o, WaypointsEventArgs e);
		public event WaypointChangedHandler EventWaypointsChanged;

		public override string PageTitle
		{
			get { return _preset.Name; }
		}


		public AircraftPage(Aircraft aircraft, Preset preset)
		{
			InitializeComponent();
			_aircraft = aircraft;
			_preset = preset;

			RefreshPages();
		}


		public virtual void OnWaypointsChanged(WaypointsEventArgs e)
		{
			// Safely raise the event for all subscribers
			//WaypointsChanged?.Invoke(this, e);

			if( EventWaypointsChanged != null)
            {
				EventWaypointsChanged(this, e);
			}
		}


		private void HandleWaypointChanged(object sender, WaypointsEventArgs e)
		{
			if(sender != null)
            {
				// handle waypoint change from this type
				((F16Configuration)_preset.Configuration).Waypoints = e.Wpts;
				((F16Configuration)_preset.Configuration).Waypoints.SteerpointStart = 1;
				((F16Configuration)_preset.Configuration).Waypoints.SteerpointEnd = e.Wpts.Waypoints.Count;
				//DataChangedCallback();
				foreach (AircraftSettingPage page in pnlMain.Controls) 
                {
					page.OnWaypointsChanged(e);
				}
			}
		}

		public AircraftSettingPage[] GetPages(IConfiguration configuration)
		{
			if (_aircraft.Model == AircraftModel.F16C)
			{
				var cfg = (F16Configuration)configuration;
				Aircrafts.F16.UploadToJetPage uploadPage = new Aircrafts.F16.UploadToJetPage(this, cfg);
				EventWaypointsChanged += new WaypointChangedHandler(uploadPage.HandleWaypointsChanged);
				return new AircraftSettingPage[]
				{

					uploadPage,
					new Aircrafts.F16.LoadSavePage(this, cfg),
					new Aircrafts.F16.WaypointsPage(this, cfg.Waypoints),
					new Aircrafts.F16.CMSPage(this, cfg.CMS),
					new Aircrafts.F16.RadioPage(this, cfg.Radios),
					new Aircrafts.F16.MFDPage(this, cfg.MFD),
					new Aircrafts.F16.HARMPage(this, cfg.HARM),
					new Aircrafts.F16.HTSPage(this, cfg.HTS),
					//new Aircrafts.F16.TOSPage(this, cfg.TOS),
					new Aircrafts.F16.MiscPage(this, cfg.Misc)
				};
			} else if (_aircraft.Model == AircraftModel.FA18C)
			{
				var cfg = (FA18Configuration)configuration;
				return new AircraftSettingPage[]
				{
					new Aircrafts.FA18.UploadToJetPage(this, cfg),
					new Aircrafts.FA18.LoadSavePage(this, cfg),
					new Aircrafts.FA18.WaypointsPage(this, cfg.Waypoints),
					new Aircrafts.FA18.SequencePage(this, cfg.Sequences),
					new Aircrafts.FA18.PrePlannedPage(this, cfg.PrePlanned),
					new Aircrafts.FA18.CMSPage(this, cfg.CMS),
					new Aircrafts.FA18.RadioPage(this, cfg.Radios),
					new Aircrafts.FA18.MiscPage(this, cfg.Misc)
				};
			}
			else if (_aircraft.Model == AircraftModel.AH64D)
			{
				var cfg = (AH64Configuration)configuration;
				return new AircraftSettingPage[]
				{
				    new Aircrafts.AH64.UploadToHeliPage(this, cfg),
				    new Aircrafts.AH64.LoadSavePage(this, cfg),
				    new Aircrafts.AH64.WaypointsPage(this, cfg.Waypoints),
				    new Aircrafts.AH64.RadioPage(this, cfg.Radios)
				};
			}
			throw new Exception();
		}

		private void SetPage(AircraftSettingPage page)
		{
			foreach (AircraftSettingPage ctl in pnlMain.Controls)
			{
				ctl.Visible = false;
			}
			page.Visible = true;
		}

		public AircraftSettingPage GetTosPage()
		{
			foreach (AircraftSettingPage ctl in pnlMain.Controls)
			{
				if( ctl.GetPageTitle() == "ToS")
                {
					return ctl;
				}
			}
			return null;
		}

		public void ToggleEnabled()
		{
			pnlLeft.Enabled = !pnlLeft.Enabled;
		}

		public void DataChangedCallback()
		{
			PresetsStore.PresetChanged(_aircraft, _preset);
		}

		internal void RefreshPages()
		{
			var pages = GetPages(_preset.Configuration);

			var lst = new List<AircraftSettingPage>(pages);
			lst.Reverse();

			pnlMain.Controls.Clear();
			pnlLeft.Controls.Clear();

			foreach (var page in lst)
			{
				page.Visible = false;
				var btn = new DTCButton();
				btn.Text = page.GetPageTitle();
				btn.Dock = DockStyle.Top;
				btn.Click += (object sender, EventArgs e) =>
				{
					SetPage(page);
				};

				page.Dock = DockStyle.Fill;
				page.Visible = false;
				pnlMain.Controls.Add(page);
				pnlLeft.Controls.Add(btn);
			}
		}
	}
}
