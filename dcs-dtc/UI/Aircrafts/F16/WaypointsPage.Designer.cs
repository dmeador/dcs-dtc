
using DTC.UI.Base;

namespace DTC.UI.Aircrafts.F16
{
	partial class WaypointsPage
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtcButtonInsert = new DTC.UI.Base.Controls.DTCButton();
            this.dtcButtonClear = new DTC.UI.Base.Controls.DTCButton();
            this.dtcButtonImport = new DTC.UI.Base.Controls.DTCButton();
            this.btnDelete = new DTC.UI.Base.Controls.DTCButton();
            this.btnAdd = new DTC.UI.Base.Controls.DTCButton();
            this.dgWaypoints = new DTC.UI.Base.Controls.DTCDataGrid();
            this.colSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLatitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLongitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colElevation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWaypoints)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtcButtonInsert);
            this.panel1.Controls.Add(this.dtcButtonClear);
            this.panel1.Controls.Add(this.dtcButtonImport);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(689, 35);
            this.panel1.TabIndex = 99;
            // 
            // dtcButtonInsert
            // 
            this.dtcButtonInsert.BackColor = System.Drawing.Color.DarkKhaki;
            this.dtcButtonInsert.FlatAppearance.BorderSize = 0;
            this.dtcButtonInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dtcButtonInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtcButtonInsert.Location = new System.Drawing.Point(103, 5);
            this.dtcButtonInsert.Name = "dtcButtonInsert";
            this.dtcButtonInsert.Size = new System.Drawing.Size(90, 25);
            this.dtcButtonInsert.TabIndex = 6;
            this.dtcButtonInsert.Text = "Insert";
            this.dtcButtonInsert.UseVisualStyleBackColor = false;
            this.dtcButtonInsert.Click += new System.EventHandler(this.dtcButtonInsert_Click);
            // 
            // dtcButtonClear
            // 
            this.dtcButtonClear.BackColor = System.Drawing.Color.DarkKhaki;
            this.dtcButtonClear.FlatAppearance.BorderSize = 0;
            this.dtcButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dtcButtonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtcButtonClear.Location = new System.Drawing.Point(486, 5);
            this.dtcButtonClear.Name = "dtcButtonClear";
            this.dtcButtonClear.Size = new System.Drawing.Size(120, 25);
            this.dtcButtonClear.TabIndex = 5;
            this.dtcButtonClear.Text = "Clear";
            this.dtcButtonClear.UseVisualStyleBackColor = false;
            this.dtcButtonClear.Click += new System.EventHandler(this.dtcButtonClear_Click);
            // 
            // dtcButtonImport
            // 
            this.dtcButtonImport.BackColor = System.Drawing.Color.DarkKhaki;
            this.dtcButtonImport.FlatAppearance.BorderSize = 0;
            this.dtcButtonImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dtcButtonImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtcButtonImport.Location = new System.Drawing.Point(337, 5);
            this.dtcButtonImport.Name = "dtcButtonImport";
            this.dtcButtonImport.Size = new System.Drawing.Size(120, 25);
            this.dtcButtonImport.TabIndex = 4;
            this.dtcButtonImport.Text = "Import 476";
            this.dtcButtonImport.UseVisualStyleBackColor = false;
            this.dtcButtonImport.Click += new System.EventHandler(this.dtcImport476_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDelete.Location = new System.Drawing.Point(199, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 25);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAdd.Location = new System.Drawing.Point(5, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 25);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgWaypoints
            // 
            this.dgWaypoints.AllowDrop = true;
            this.dgWaypoints.AllowUserToAddRows = false;
            this.dgWaypoints.AllowUserToDeleteRows = false;
            this.dgWaypoints.AllowUserToResizeColumns = false;
            this.dgWaypoints.AllowUserToResizeRows = false;
            this.dgWaypoints.BackgroundColor = System.Drawing.Color.Beige;
            this.dgWaypoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgWaypoints.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgWaypoints.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgWaypoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgWaypoints.ColumnHeadersHeight = 30;
            this.dgWaypoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgWaypoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSequence,
            this.colName,
            this.colLatitude,
            this.colLongitude,
            this.colElevation,
            this.colTOT});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgWaypoints.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgWaypoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgWaypoints.EnableHeadersVisualStyles = false;
            this.dgWaypoints.Location = new System.Drawing.Point(0, 35);
            this.dgWaypoints.Name = "dgWaypoints";
            this.dgWaypoints.ReadOnly = true;
            this.dgWaypoints.RowHeadersVisible = false;
            this.dgWaypoints.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgWaypoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgWaypoints.ShowCellToolTips = false;
            this.dgWaypoints.Size = new System.Drawing.Size(689, 448);
            this.dgWaypoints.TabIndex = 100;
            this.dgWaypoints.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgWaypoints_DataError);
            this.dgWaypoints.SelectionChanged += new System.EventHandler(this.dgWaypoints_SelectionChanged);
            this.dgWaypoints.DoubleClick += new System.EventHandler(this.dgWaypoints_DoubleClick);
            // 
            // colSequence
            // 
            this.colSequence.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSequence.DataPropertyName = "Sequence";
            this.colSequence.HeaderText = "Seq";
            this.colSequence.Name = "colSequence";
            this.colSequence.ReadOnly = true;
            this.colSequence.Width = 58;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colLatitude
            // 
            this.colLatitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLatitude.DataPropertyName = "Latitude";
            this.colLatitude.HeaderText = "Latitude";
            this.colLatitude.MinimumWidth = 120;
            this.colLatitude.Name = "colLatitude";
            this.colLatitude.ReadOnly = true;
            this.colLatitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colLatitude.Width = 120;
            // 
            // colLongitude
            // 
            this.colLongitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLongitude.DataPropertyName = "Longitude";
            this.colLongitude.HeaderText = "Longitude";
            this.colLongitude.MinimumWidth = 120;
            this.colLongitude.Name = "colLongitude";
            this.colLongitude.ReadOnly = true;
            this.colLongitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colLongitude.Width = 120;
            // 
            // colElevation
            // 
            this.colElevation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colElevation.DataPropertyName = "Elevation";
            this.colElevation.HeaderText = "Elevation";
            this.colElevation.MinimumWidth = 50;
            this.colElevation.Name = "colElevation";
            this.colElevation.ReadOnly = true;
            this.colElevation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colElevation.Width = 72;
            // 
            // colTOT
            // 
            this.colTOT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTOT.DataPropertyName = "TimeOnStation";
            this.colTOT.HeaderText = "TOT";
            this.colTOT.Name = "colTOT";
            this.colTOT.ReadOnly = true;
            this.colTOT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTOT.Width = 43;
            // 
            // WaypointsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.Controls.Add(this.dgWaypoints);
            this.Controls.Add(this.panel1);
            this.Name = "WaypointsPage";
            this.Size = new System.Drawing.Size(689, 483);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgWaypoints)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
		private Base.Controls.DTCButton btnDelete;
		private Base.Controls.DTCButton btnAdd;
		private Base.Controls.DTCDataGrid dgWaypoints;
        private Base.Controls.DTCButton dtcButtonImport;
        private Base.Controls.DTCButton dtcButtonClear;
        private Base.Controls.DTCButton dtcButtonInsert;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLatitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLongitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn colElevation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTOT;
    }
}
