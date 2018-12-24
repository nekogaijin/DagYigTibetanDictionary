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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDagYigPref));
            this.label1 = new System.Windows.Forms.Label();
            this.lbltop = new System.Windows.Forms.Label();
            this.cbScanFonts = new System.Windows.Forms.ComboBox();
            this.UpDownScan = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.ckHotKey = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbScanDefFonts = new System.Windows.Forms.ComboBox();
            this.UpDownScanDef = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbDictTibFonts = new System.Windows.Forms.ComboBox();
            this.UpDownDictTib = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbDictInputFonts = new System.Windows.Forms.ComboBox();
            this.UpDownDictInput = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cbDictDefFonts = new System.Windows.Forms.ComboBox();
            this.UpDownDictDef = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownScanDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownDictTib)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownDictInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownDictDef)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Top Window Font Size";
            // 
            // lbltop
            // 
            this.lbltop.AutoSize = true;
            this.lbltop.Location = new System.Drawing.Point(22, 34);
            this.lbltop.Name = "lbltop";
            this.lbltop.Size = new System.Drawing.Size(68, 13);
            this.lbltop.TabIndex = 43;
            this.lbltop.Text = "Top Window";
            this.lbltop.Click += new System.EventHandler(this.lbltop_Click);
            // 
            // cbScanFonts
            // 
            this.cbScanFonts.FormattingEnabled = true;
            this.cbScanFonts.Location = new System.Drawing.Point(25, 50);
            this.cbScanFonts.Name = "cbScanFonts";
            this.cbScanFonts.Size = new System.Drawing.Size(214, 21);
            this.cbScanFonts.TabIndex = 42;
            this.cbScanFonts.SelectedIndexChanged += new System.EventHandler(this.ScanFont_SelectedIndexChanged);
            // 
            // UpDownScan
            // 
            this.UpDownScan.Location = new System.Drawing.Point(259, 51);
            this.UpDownScan.Name = "UpDownScan";
            this.UpDownScan.Size = new System.Drawing.Size(37, 20);
            this.UpDownScan.TabIndex = 41;
            this.UpDownScan.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.UpDownScan.ValueChanged += new System.EventHandler(this.ScanSize_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 303);
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
            this.ckHotKey.Location = new System.Drawing.Point(25, 198);
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
            this.label2.Location = new System.Drawing.Point(256, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Definition Font Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "Definition Window";
            // 
            // cbScanDefFonts
            // 
            this.cbScanDefFonts.FormattingEnabled = true;
            this.cbScanDefFonts.Location = new System.Drawing.Point(25, 105);
            this.cbScanDefFonts.Name = "cbScanDefFonts";
            this.cbScanDefFonts.Size = new System.Drawing.Size(214, 21);
            this.cbScanDefFonts.TabIndex = 48;
            this.cbScanDefFonts.SelectedIndexChanged += new System.EventHandler(this.ScanDefFont_SelectedIndexChanged);
            // 
            // UpDownScanDef
            // 
            this.UpDownScanDef.Location = new System.Drawing.Point(259, 106);
            this.UpDownScanDef.Name = "UpDownScanDef";
            this.UpDownScanDef.Size = new System.Drawing.Size(37, 20);
            this.UpDownScanDef.TabIndex = 47;
            this.UpDownScanDef.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.UpDownScanDef.ValueChanged += new System.EventHandler(this.ScanDefSize_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(314, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "HotKey for looping through prior values. Example X   (CTL ALT X)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "CTL  ALT ";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(88, 266);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(36, 20);
            this.txtKey.TabIndex = 53;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Fonts for Scanner";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(439, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Fonts for Dictionary";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(673, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 13);
            this.label8.TabIndex = 63;
            this.label8.Text = "Tibetan Column Font Size";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(439, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 62;
            this.label9.Text = "Tibetan Column";
            // 
            // cbDictTibFonts
            // 
            this.cbDictTibFonts.FormattingEnabled = true;
            this.cbDictTibFonts.Location = new System.Drawing.Point(442, 106);
            this.cbDictTibFonts.Name = "cbDictTibFonts";
            this.cbDictTibFonts.Size = new System.Drawing.Size(214, 21);
            this.cbDictTibFonts.TabIndex = 61;
            this.cbDictTibFonts.SelectedIndexChanged += new System.EventHandler(this.DictTibFont_SelectedIndexChanged);
            // 
            // UpDownDictTib
            // 
            this.UpDownDictTib.Location = new System.Drawing.Point(676, 107);
            this.UpDownDictTib.Name = "UpDownDictTib";
            this.UpDownDictTib.Size = new System.Drawing.Size(37, 20);
            this.UpDownDictTib.TabIndex = 60;
            this.UpDownDictTib.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.UpDownDictTib.ValueChanged += new System.EventHandler(this.DictTibSize_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(673, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 59;
            this.label10.Text = "Input Box  Font Size";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(439, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 58;
            this.label11.Text = "Input Box";
            // 
            // cbDictInputFonts
            // 
            this.cbDictInputFonts.FormattingEnabled = true;
            this.cbDictInputFonts.Location = new System.Drawing.Point(442, 51);
            this.cbDictInputFonts.Name = "cbDictInputFonts";
            this.cbDictInputFonts.Size = new System.Drawing.Size(214, 21);
            this.cbDictInputFonts.TabIndex = 57;
            this.cbDictInputFonts.SelectedIndexChanged += new System.EventHandler(this.DictInputFont_SelectedIndexChanged);
            // 
            // UpDownDictInput
            // 
            this.UpDownDictInput.Location = new System.Drawing.Point(676, 52);
            this.UpDownDictInput.Name = "UpDownDictInput";
            this.UpDownDictInput.Size = new System.Drawing.Size(37, 20);
            this.UpDownDictInput.TabIndex = 56;
            this.UpDownDictInput.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.UpDownDictInput.ValueChanged += new System.EventHandler(this.DictInputSize_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(673, 143);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(136, 13);
            this.label14.TabIndex = 67;
            this.label14.Text = "Definition Column Font Size";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(439, 143);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 66;
            this.label15.Text = "Definition Column";
            // 
            // cbDictDefFonts
            // 
            this.cbDictDefFonts.FormattingEnabled = true;
            this.cbDictDefFonts.Location = new System.Drawing.Point(442, 159);
            this.cbDictDefFonts.Name = "cbDictDefFonts";
            this.cbDictDefFonts.Size = new System.Drawing.Size(214, 21);
            this.cbDictDefFonts.TabIndex = 65;
            this.cbDictDefFonts.SelectedIndexChanged += new System.EventHandler(this.DictDefFont_SelectedIndexChanged);
            // 
            // UpDownDictDef
            // 
            this.UpDownDictDef.Location = new System.Drawing.Point(676, 160);
            this.UpDownDictDef.Name = "UpDownDictDef";
            this.UpDownDictDef.Size = new System.Drawing.Size(37, 20);
            this.UpDownDictDef.TabIndex = 64;
            this.UpDownDictDef.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.UpDownDictDef.ValueChanged += new System.EventHandler(this.DictDefSize_ValueChanged);
            // 
            // frmDagYigPref
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 424);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cbDictDefFonts);
            this.Controls.Add(this.UpDownDictDef);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbDictTibFonts);
            this.Controls.Add(this.UpDownDictTib);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbDictInputFonts);
            this.Controls.Add(this.UpDownDictInput);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbScanDefFonts);
            this.Controls.Add(this.UpDownScanDef);
            this.Controls.Add(this.ckHotKey);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbltop);
            this.Controls.Add(this.cbScanFonts);
            this.Controls.Add(this.UpDownScan);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDagYigPref";
            this.Text = "DagYig Preferences";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.UpDownScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownScanDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownDictTib)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownDictInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownDictDef)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbltop;
        private System.Windows.Forms.ComboBox cbScanFonts;
        private System.Windows.Forms.NumericUpDown UpDownScan;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox ckHotKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbScanDefFonts;
        private System.Windows.Forms.NumericUpDown UpDownScanDef;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbDictTibFonts;
        private System.Windows.Forms.NumericUpDown UpDownDictTib;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbDictInputFonts;
        private System.Windows.Forms.NumericUpDown UpDownDictInput;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbDictDefFonts;
        private System.Windows.Forms.NumericUpDown UpDownDictDef;
    }
}