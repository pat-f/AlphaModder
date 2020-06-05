using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaModder.Dialogs
{
    public partial class LoadFromAlphaFileDialog : Form
    {
        public String response = "";

        public LoadFromAlphaFileDialog()
        {
            InitializeComponent();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonAlpha_Click(object sender, EventArgs e)
        {
            response = "alpha";
            this.Close();
        }

        private void ButtonAlphaX_Click(object sender, EventArgs e)
        {
            response = "alphax";
            this.Close();
        }
    }
}
