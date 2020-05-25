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
    public partial class FormHurryCalculator : Form
    {
        public FormHurryCalculator()
        {
            InitializeComponent();
        }

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // get turns remaining and hurry cost from the input boxes
                Double turnsRemaining = Double.Parse(textBoxTurnsRemaining.Text);
                //Console.WriteLine("turns remaining: " + turnsRemaining);
                Double hurryCost = Double.Parse(textBoxHurryCost.Text);
                //Console.WriteLine("hurry cost: " + hurryCost);
                Double payPct = (turnsRemaining - 1.0) / turnsRemaining;
                //Console.WriteLine("pay pct: " + payPct);
                Double payDecimal = payPct * hurryCost;
                
                Int32 pay = (Int32)(payDecimal);
                // if payDecimal is not whole number, add 1 to pay
                if (!(Math.Abs(payDecimal % 1) <= (Double.Epsilon * 100))) pay++;
                payPct = pay / hurryCost;
                //Console.WriteLine(pay);
                //Console.WriteLine((turnsRemaining - 1.0) / turnsRemaining);
                //Console.WriteLine((4.0 / 5.0));
                //Console.WriteLine("(" + (turnsRemaining - 1.0) + "/" + turnsRemaining + ") * " + hurryCost + " = " + pay);
                Double savePct = 1.0 - payPct;
                Double save = hurryCost - pay;

                Int32 payPctFormatted = (Int32)((payPct * 100) + 0.5);
                Int32 savePctFormatted = (Int32)((savePct * 100) + 0.5);

                labelPay2.Text = "Pay   " + pay;
                labelPay.Text = "Pay   " + pay;
                labelPayPct.Text = "(" + payPctFormatted + "%)";
                labelSave.Text = "Save   " + save;
                labelSavePct.Text = "(" + savePctFormatted + "%)";
            } catch(Exception ex)
            {

            }
        }

        private void FormHurryCalculator_Load(object sender, EventArgs e)
        {
            labelPay.Text = "";
            labelPay2.Text = "";
            labelSave.Text = "";
            labelPayPct.Text = "";
            labelSavePct.Text = "";
        }
    }
}
