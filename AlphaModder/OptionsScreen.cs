using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaModder
{
    public partial class OptionsScreen : Form
    {
        public OptionsScreen()
        {
            InitializeComponent();
        }

        // when user clicks ok, set the sound option based on the check box, and close the window
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (checkBoxEnableSounds.Checked)
                Properties.Settings.Default.EnableSounds = true;
            else
                Properties.Settings.Default.EnableSounds = false;

            Properties.Settings.Default.Save();

            this.Close();
        }

        // when user clicks cancel, close the window without making changes
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
