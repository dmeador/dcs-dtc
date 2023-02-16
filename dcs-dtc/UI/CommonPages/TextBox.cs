using System;
using System.Windows.Forms;

namespace DTC.UI.CommonPages
{
	public partial class TextBox : UserControl
	{
		public Action<DialogResult> DialogResultCallback;

		public TextBox()
		{
			InitializeComponent();
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				if (txtName.Text.Length > 0)
				{
					DialogResultCallback(DialogResult.OK);
				}
			}

			if (e.KeyCode == Keys.Escape)
			{
				e.SuppressKeyPress = true;
				DialogResultCallback(DialogResult.Cancel);
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			DialogResultCallback(DialogResult.OK);
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResultCallback(DialogResult.Cancel);
		}
	}
}
