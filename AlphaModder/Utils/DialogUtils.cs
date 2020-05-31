using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaModder.Utils
{
    class DialogUtils
    {
        public static void messageBox(String message)
        {
            MessageBox.Show(message);
        }

        public static bool messageBoxOkCancel(String message)
        {
            DialogResult dialogResult = MessageBox.Show(message, "", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
                return true;
            return false;
        }

    }
}
