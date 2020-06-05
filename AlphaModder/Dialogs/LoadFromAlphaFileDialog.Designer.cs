namespace AlphaModder.Dialogs
{
    partial class LoadFromAlphaFileDialog
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
            this.buttonAlpha = new System.Windows.Forms.Button();
            this.buttonAlphaX = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonAlpha
            // 
            this.buttonAlpha.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAlpha.Location = new System.Drawing.Point(66, 27);
            this.buttonAlpha.Name = "buttonAlpha";
            this.buttonAlpha.Size = new System.Drawing.Size(185, 78);
            this.buttonAlpha.TabIndex = 13;
            this.buttonAlpha.Text = "Load from alpha.txt";
            this.buttonAlpha.UseVisualStyleBackColor = true;
            this.buttonAlpha.Click += new System.EventHandler(this.ButtonAlpha_Click);
            // 
            // buttonAlphaX
            // 
            this.buttonAlphaX.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAlphaX.Location = new System.Drawing.Point(97, 136);
            this.buttonAlphaX.Name = "buttonAlphaX";
            this.buttonAlphaX.Size = new System.Drawing.Size(128, 57);
            this.buttonAlphaX.TabIndex = 14;
            this.buttonAlphaX.Text = "Load from alphax.txt";
            this.buttonAlphaX.UseVisualStyleBackColor = true;
            this.buttonAlphaX.Click += new System.EventHandler(this.ButtonAlphaX_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(97, 199);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(128, 57);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // LoadFromAlphaFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 293);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAlphaX);
            this.Controls.Add(this.buttonAlpha);
            this.Name = "LoadFromAlphaFileDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAlpha;
        private System.Windows.Forms.Button buttonAlphaX;
        private System.Windows.Forms.Button buttonCancel;
    }
}