using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTC.UI.CommonPages
{
    public partial class FormTextDialog : Form
    {
        public DialogResult Result = DialogResult.Cancel;
        public Action<DialogResult> DialogResultCallback;
        public string FormText;

        public FormTextDialog()
        {
            InitializeComponent();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormTextDialog_Load(object sender, EventArgs e)
        {
            textBoxData.Text = FormText;
            textBoxData.HideSelection = true;
        }
    }
}
