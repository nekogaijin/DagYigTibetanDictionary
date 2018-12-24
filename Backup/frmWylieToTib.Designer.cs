namespace DagYig
{
    partial class frmWylieToTib
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
            this.btnConvertToTib = new System.Windows.Forms.Button();
            this.txtConvert = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnConvertToTib
            // 
            this.btnConvertToTib.Location = new System.Drawing.Point(12, 5);
            this.btnConvertToTib.Name = "btnConvertToTib";
            this.btnConvertToTib.Size = new System.Drawing.Size(144, 21);
            this.btnConvertToTib.TabIndex = 28;
            this.btnConvertToTib.Text = "Convert Wylie ToTibetan";
            this.btnConvertToTib.UseVisualStyleBackColor = true;
            this.btnConvertToTib.Click += new System.EventHandler(this.btnConvertToTib_Click);
            // 
            // txtConvert
            // 
            this.txtConvert.Location = new System.Drawing.Point(12, 32);
            this.txtConvert.Multiline = true;
            this.txtConvert.Name = "txtConvert";
            this.txtConvert.Size = new System.Drawing.Size(487, 368);
            this.txtConvert.TabIndex = 29;
            this.txtConvert.TextChanged += new System.EventHandler(this.txtConvert_OnTextChanged);
            // 
            // frmWylieToTib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 412);
            this.Controls.Add(this.txtConvert);
            this.Controls.Add(this.btnConvertToTib);
            this.Name = "frmWylieToTib";
            this.Text = "frmWylieToTib";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConvertToTib;
        private System.Windows.Forms.TextBox txtConvert;

    }
}