namespace DagYig
{
    partial class frmDagYigPref
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbltop = new System.Windows.Forms.Label();
            this.cbTibFonts = new System.Windows.Forms.ComboBox();
            this.numericUpDownTib = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.ckHotKey = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbEngFonts = new System.Windows.Forms.ComboBox();
            this.numericUpDownEng = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTib)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEng)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Tibetan Font Size";
            // 
            // lbltop
            // 
            this.lbltop.AutoSize = true;
            this.lbltop.Location = new System.Drawing.Point(22, 9);
            this.lbltop.Name = "lbltop";
            this.lbltop.Size = new System.Drawing.Size(67, 13);
            this.lbltop.TabIndex = 43;
            this.lbltop.Text = "Tibetan Font";
            // 
            // cbTibFonts
            // 
            this.cbTibFonts.FormattingEnabled = true;
            this.cbTibFonts.Location = new System.Drawing.Point(25, 25);
            this.cbTibFonts.Name = "cbTibFonts";
            this.cbTibFonts.Size = new System.Drawing.Size(214, 21);
            this.cbTibFonts.TabIndex = 42;
            this.cbTibFonts.SelectedIndexChanged += new System.EventHandler(this.TibFont_SelectedIndexChanged);
            // 
            // numericUpDownTib
            // 
            this.numericUpDownTib.Location = new System.Drawing.Point(259, 26);
            this.numericUpDownTib.Name = "numericUpDownTib";
            this.numericUpDownTib.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownTib.TabIndex = 41;
            this.numericUpDownTib.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownTib.ValueChanged += new System.EventHandler(this.TibFontSize_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "Update Preferences";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ckHotKey
            // 
            this.ckHotKey.AutoSize = true;
            this.ckHotKey.Location = new System.Drawing.Point(25, 122);
            this.ckHotKey.Name = "ckHotKey";
            this.ckHotKey.Size = new System.Drawing.Size(83, 17);
            this.ckHotKey.TabIndex = 46;
            this.ckHotKey.Text = "Use HotKey";
            this.ckHotKey.UseVisualStyleBackColor = true;
            this.ckHotKey.CheckedChanged += new System.EventHandler(this.ckHotKey_Changed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "English Font Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "English Font";
            // 
            // cbEngFonts
            // 
            this.cbEngFonts.FormattingEnabled = true;
            this.cbEngFonts.Location = new System.Drawing.Point(25, 80);
            this.cbEngFonts.Name = "cbEngFonts";
            this.cbEngFonts.Size = new System.Drawing.Size(214, 21);
            this.cbEngFonts.TabIndex = 48;
            this.cbEngFonts.SelectedIndexChanged += new System.EventHandler(this.EngFont_SelectedIndexChanged);
            // 
            // numericUpDownEng
            // 
            this.numericUpDownEng.Location = new System.Drawing.Point(259, 81);
            this.numericUpDownEng.Name = "numericUpDownEng";
            this.numericUpDownEng.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownEng.TabIndex = 47;
            this.numericUpDownEng.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.numericUpDownEng.ValueChanged += new System.EventHandler(this.EngFontSize_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(314, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "HotKey for looping through prior values. Example X   (CTL ALT X)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "CTL  ALT ";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(88, 190);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(36, 20);
            this.txtKey.TabIndex = 53;
            // 
            // frmClipBoardPref
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 262);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbEngFonts);
            this.Controls.Add(this.numericUpDownEng);
            this.Controls.Add(this.ckHotKey);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbltop);
            this.Controls.Add(this.cbTibFonts);
            this.Controls.Add(this.numericUpDownTib);
            this.Name = "frmClipBoardPref";
            this.Text = "ClipBoardPref";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTib)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEng)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbltop;
        private System.Windows.Forms.ComboBox cbTibFonts;
        private System.Windows.Forms.NumericUpDown numericUpDownTib;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox ckHotKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbEngFonts;
        private System.Windows.Forms.NumericUpDown numericUpDownEng;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKey;
    }
}