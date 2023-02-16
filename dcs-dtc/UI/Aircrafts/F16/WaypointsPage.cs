using DTC.Models.F16.Waypoints;
using DTC.UI.Base;
using DTC.UI.CommonPages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DTC.Models.MDC_476;


namespace DTC.UI.Aircrafts.F16
{
	public partial class WaypointsPage : AircraftSettingPage
	{
		private WaypointSystem _waypoints;
		private WaypointEdit _wptEditDialog;

		public WaypointsPage(AircraftPage parent, WaypointSystem wpts) : base(parent)
		{
			InitializeComponent();

			_waypoints = wpts;
			_wptEditDialog = new WaypointEdit(_waypoints, this.WptDialogEditCallback);
			_wptEditDialog.Visible = false;
			dgWaypoints.ReorderCallback = Reorder;
			this.Controls.Add(this._wptEditDialog);

			RefreshList();
		}

		private void Reorder(int indexFrom, int indexTo)
		{
			_waypoints.Reorder(indexFrom, indexTo);
			RefreshList();
		}

		public override string GetPageTitle()
		{
			return "Waypoints";
		}

		private void WptDialogEditCallback(WaypointEdit.WaypointEditResult result, Waypoint wpt)
		{
			if (result != WaypointEdit.WaypointEditResult.Close) {
				WaypointsChanged();
				_parent.DataChangedCallback();
				this.RefreshList();
			}

			if (result != WaypointEdit.WaypointEditResult.Add)
			{
				this.ToggleEnabled();
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			ShowWptDialog();
		}

		private void ShowWptDialog(Waypoint wpt = null)
		{
			this.ToggleEnabled();

			this._wptEditDialog.Location = new Point(
				(this.Size.Width - this._wptEditDialog.Size.Width) / 2,
				(this.Size.Height - this._wptEditDialog.Size.Height) / 2);

			_wptEditDialog.ShowDialog(wpt);
			this.RefreshList();
		}

		private void ToggleEnabled()
		{
			_parent.ToggleEnabled();
			dgWaypoints.Enabled = !dgWaypoints.Enabled;
			btnAdd.Enabled = !btnAdd.Enabled;
			btnDelete.Enabled = dgWaypoints.Enabled && dgWaypoints.SelectedRows.Count > 0;
		}

		private void RefreshList()
		{
			dgWaypoints.RefreshList(_waypoints.Waypoints);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			var wptsToDelete = new List<Waypoint>();

			foreach (DataGridViewRow row in dgWaypoints.SelectedRows)
			{
				var wpt = (Waypoint)row.DataBoundItem;
				wptsToDelete.Add(wpt);
			}

			foreach(var wpt in wptsToDelete)
			{
				_waypoints.Remove(wpt);
			}
			WaypointsChanged();
			_parent.DataChangedCallback();
			RefreshList();
			dgWaypoints.Focus();
		}

		private void btnCapture_Click(object sender, EventArgs e)
		{
			var cap = new WaypointCaptureCrosshair();
			cap.Show();
		}

		private void dgWaypoints_SelectionChanged(object sender, EventArgs e)
		{
			btnDelete.Enabled = dgWaypoints.Enabled && dgWaypoints.SelectedRows.Count > 0;
		}

		private void dgWaypoints_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			Console.WriteLine("error");
		}

		private void dgWaypoints_DoubleClick(object sender, EventArgs e)
		{
			if (dgWaypoints.SelectedRows.Count > 0)
			{
				ShowWptDialog((Waypoint)dgWaypoints.SelectedRows[0].DataBoundItem);
			}
		}

		private void WaypointsChanged()
		{
			WaypointsEventArgs ev = new WaypointsEventArgs(_waypoints);
			base.OnWaypointsChanged(ev);
		}

        private void dtcImport476_Click(object sender, EventArgs e)
        {
			// parse clipboard data - 476 MDC webpage data
			MDC_476 MDC = new MDC_476();
			MDC.GetClipboard();
			DialogResult result;
			using (DTC.UI.CommonPages.FormTextDialog form = new DTC.UI.CommonPages.FormTextDialog())
			{
				form.Text = "476 MDC Import";
				form.FormText = MDC.MDC_data;
				result = form.ShowDialog();
				if(result == DialogResult.OK)
                {
					MDC.ParseFlightPlan_to_Waypoints();
				}
                else
                {
					return;
                }
			}
			// 
			foreach( DTC.Models.MDC_476.Waypoints.Waypoint wp in MDC.Waypoints.Waypoints )
            {
				var _wp = new Waypoint(wp.Sequence, wp.Name, wp.Latitude, wp.Longitude, wp.Elevation, wp.TimeOnTarget);

				_waypoints.Add(_wp);
			}

			WaypointsChanged();
			RefreshList();
			_parent.DataChangedCallback();
			// send TOS values to TOSPage
			//DTC.Models.F16.TOS.TOSSystem TOS = MDC.Waypoints.GetTimeOnTargetsSetting();
			//TOSPage tospage = (TOSPage)((AircraftPage)_parent).GetTosPage();
			//tospage.UpdateTOS(TOS);
		}

		private void dtcButtonClear_Click(object sender, EventArgs e)
        {
			_waypoints.Waypoints.Clear();
			RefreshList();
			DTC.Models.F16.TOS.TOSSystem TOS = new DTC.Models.F16.TOS.TOSSystem();
			//TOSPage tospage = (TOSPage)((AircraftPage)_parent).GetTosPage();
			//tospage.UpdateTOS(TOS);

		}

		private void dtcButtonInsert_Click(object sender, EventArgs e)
        {
			if (dgWaypoints.SelectedRows.Count > 1)
			{
                System.Windows.Forms.MessageBox.Show("Select only one entry.\nInserted item goes above/before selection.");
			}
			else if (dgWaypoints.SelectedRows.Count == 1)
			{
				Waypoint wpt_selected = null;
				int new_start_seq = 0;
				foreach (DataGridViewRow row in dgWaypoints.SelectedRows)
				{
					wpt_selected = (Waypoint)row.DataBoundItem;
					break;
				}
				int index = _waypoints.Waypoints.IndexOf(wpt_selected);
				new_start_seq = wpt_selected.Sequence + 1;
				var _wp = new Waypoint(wpt_selected.Sequence, "", "", "", 0, "");
				_waypoints.Waypoints.Insert(index, _wp);
				//renumber everything above
				for(int i = index+1; i< _waypoints.Waypoints.Count; i++)
                {
					_waypoints.Waypoints[i].Sequence = new_start_seq++;

				}
				RefreshList();
				WaypointsChanged();
				_parent.DataChangedCallback();
			}

		}
    }
}
