namespace AlphaModder
{
    partial class FormHurryCalculator
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxTurnsRemaining = new System.Windows.Forms.TextBox();
            this.textBoxHurryCost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPay2 = new System.Windows.Forms.Label();
            this.labelSave = new System.Windows.Forms.Label();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.labelPayPct = new System.Windows.Forms.Label();
            this.labelSavePct = new System.Windows.Forms.Label();
            this.labelPay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxTurnsRemaining
            // 
            this.textBoxTurnsRemaining.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxTurnsRemaining.Location = new System.Drawing.Point(176, 63);
            this.textBoxTurnsRemaining.Name = "textBoxTurnsRemaining";
            this.textBoxTurnsRemaining.Size = new System.Drawing.Size(100, 27);
            this.textBoxTurnsRemaining.TabIndex = 0;
            this.textBoxTurnsRemaining.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxHurryCost
            // 
            this.textBoxHurryCost.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxHurryCost.Location = new System.Drawing.Point(176, 105);
            this.textBoxHurryCost.Name = "textBoxHurryCost";
            this.textBoxHurryCost.Size = new System.Drawing.Size(100, 27);
            this.textBoxHurryCost.TabIndex = 1;
            this.textBoxHurryCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(47, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Turns remaining";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(87, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hurry cost";
            // 
            // labelPay2
            // 
            this.labelPay2.AutoSize = true;
            this.labelPay2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPay2.ForeColor = System.Drawing.Color.Green;
            this.labelPay2.Location = new System.Drawing.Point(126, 231);
            this.labelPay2.Name = "labelPay2";
            this.labelPay2.Size = new System.Drawing.Size(44, 25);
            this.labelPay2.TabIndex = 4;
            this.labelPay2.Text = "Pay";
            // 
            // labelSave
            // 
            this.labelSave.AutoSize = true;
            this.labelSave.Location = new System.Drawing.Point(143, 368);
            this.labelSave.Name = "labelSave";
            this.labelSave.Size = new System.Drawing.Size(32, 13);
            this.labelSave.TabIndex = 5;
            this.labelSave.Text = "Save";
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.buttonCalculate.Location = new System.Drawing.Point(176, 158);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(112, 35);
            this.buttonCalculate.TabIndex = 6;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.ButtonCalculate_Click);
            // 
            // labelPayPct
            // 
            this.labelPayPct.AutoSize = true;
            this.labelPayPct.Location = new System.Drawing.Point(223, 328);
            this.labelPayPct.Name = "labelPayPct";
            this.labelPayPct.Size = new System.Drawing.Size(41, 13);
            this.labelPayPct.TabIndex = 7;
            this.labelPayPct.Text = "PayPct";
            // 
            // labelSavePct
            // 
            this.labelSavePct.AutoSize = true;
            this.labelSavePct.Location = new System.Drawing.Point(223, 368);
            this.labelSavePct.Name = "labelSavePct";
            this.labelSavePct.Size = new System.Drawing.Size(48, 13);
            this.labelSavePct.TabIndex = 8;
            this.labelSavePct.Text = "SavePct";
            // 
            // labelPay
            // 
            this.labelPay.AutoSize = true;
            this.labelPay.Location = new System.Drawing.Point(145, 328);
            this.labelPay.Name = "labelPay";
            this.labelPay.Size = new System.Drawing.Size(25, 13);
            this.labelPay.TabIndex = 9;
            this.labelPay.Text = "Pay";
            // 
            // FormHurryCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 450);
            this.Controls.Add(this.labelPay);
            this.Controls.Add(this.labelSavePct);
            this.Controls.Add(this.labelPayPct);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.labelSave);
            this.Controls.Add(this.labelPay2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxHurryCost);
            this.Controls.Add(this.textBoxTurnsRemaining);
            this.Name = "FormHurryCalculator";
            this.Text = "Hurry Cost Calculator";
            this.Load += new System.EventHandler(this.FormHurryCalculator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTurnsRemaining;
        private System.Windows.Forms.TextBox textBoxHurryCost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelPay2;
        private System.Windows.Forms.Label labelSave;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Label labelPayPct;
        private System.Windows.Forms.Label labelSavePct;
        private System.Windows.Forms.Label labelPay;
    }
}