using AlphaModder.Model;
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

        public static bool messageBoxOkCancel(String message, MessageBoxType messageBoxType)
        {


            DialogResult dialogResult = MessageBox.Show(message, "", MessageBoxButtons.OKCancel, getMessageBoxButtonsFromType(messageBoxType));
            if (dialogResult == DialogResult.OK)
                return true;
            return false;
        }


        private static MessageBoxIcon getMessageBoxButtonsFromType(MessageBoxType messageBoxType)
        {
            if (messageBoxType == MessageBoxType.INFO)
                return MessageBoxIcon.Information;
            if (messageBoxType == MessageBoxType.QUESTION)
                return MessageBoxIcon.Question;
            if (messageBoxType == MessageBoxType.ALERT)
                return MessageBoxIcon.Warning;
            if (messageBoxType == MessageBoxType.ERROR)
                return MessageBoxIcon.Error;
            return MessageBoxIcon.None;
        }

    }
}
