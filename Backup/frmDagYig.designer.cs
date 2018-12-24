namespace DagYig
{
    partial class frmDagYig
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
            this.DefGridView = new System.Windows.Forms.DataGridView();
            this.btnDictLookupEnglish = new System.Windows.Forms.Button();
            this.txtDicLookUp = new System.Windows.Forms.TextBox();
            this.btnLookUpTibetan = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDownTib = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.cbLang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownEng = new System.Windows.Forms.NumericUpDown();
            this.Menu1 = new System.Windows.Forms.MenuStrip();
            this.conversionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wylieToTibetanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DefGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTib)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEng)).BeginInit();
            this.Menu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DefGridView
            // 
            this.DefGridView.AllowUserToDeleteRows = false;
            this.DefGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DefGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DefGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DefGridView.Location = new System.Drawing.Point(12, 89);
            this.DefGridView.Name = "DefGridView";
            this.DefGridView.RowTemplate.Height = 65;
            this.DefGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DefGridView.Size = new System.Drawing.Size(1056, 309);
            this.DefGridView.TabIndex = 11;
            this.DefGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.defGridView_DataBindingComplete);
            // 
            // btnDictLookupEnglish
            // 
            this.btnDictLookupEnglish.Location = new System.Drawing.Point(412, 48);
            this.btnDictLookupEnglish.Name = "btnDictLookupEnglish";
            this.btnDictLookupEnglish.Size = new System.Drawing.Size(133, 23);
            this.btnDictLookupEnglish.TabIndex = 34;
            this.btnDictLookupEnglish.Text = "Search English Definition";
            this.btnDictLookupEnglish.UseVisualStyleBackColor = true;
            this.btnDictLookupEnglish.Click += new System.EventHandler(this.btnDictLookupEnglish_Click);
            // 
            // txtDicLookUp
            // 
            this.txtDicLookUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDicLookUp.Font = new System.Drawing.Font("Jomolhari", 20.25F);
            this.txtDicLookUp.Location = new System.Drawing.Point(12, 27);
            this.txtDicLookUp.Multiline = true;
            this.txtDicLookUp.Name = "txtDicLookUp";
            this.txtDicLookUp.Size = new System.Drawing.Size(378, 44);
            this.txtDicLookUp.TabIndex = 35;
            this.txtDicLookUp.TextChanged += new System.EventHandler(this.txtDicLookup_OnTextChanged);
            this.txtDicLookUp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDic_OnKeyDown);
            // 
            // btnLookUpTibetan
            // 
            this.btnLookUpTibetan.Location = new System.Drawing.Point(412, 18);
            this.btnLookUpTibetan.Name = "btnLookUpTibetan";
            this.btnLookUpTibetan.Size = new System.Drawing.Size(133, 23);
            this.btnLookUpTibetan.TabIndex = 36;
            this.btnLookUpTibetan.Text = "Search Tibetan";
            this.btnLookUpTibetan.UseVisualStyleBackColor = true;
            this.btnLookUpTibetan.Click += new System.EventHandler(this.btnDictLookupTibetan_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(929, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Update Dictionary";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnInsertNewDef_Click);
            // 
            // numericUpDownTib
            // 
            this.numericUpDownTib.Location = new System.Drawing.Point(582, 40);
            this.numericUpDownTib.Name = "numericUpDownTib";
            this.numericUpDownTib.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownTib.TabIndex = 37;
            this.numericUpDownTib.ValueChanged += new System.EventHandler(this.FontSizeTib_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(563, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Tibetan Font Size";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(778, 67);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(35, 13);
            this.lblMsg.TabIndex = 41;
            this.lblMsg.Text = "label2";
            this.lblMsg.Visible = false;
            // 
            // cbLang
            // 
            this.cbLang.FormattingEnabled = true;
            this.cbLang.Location = new System.Drawing.Point(854, 20);
            this.cbLang.Name = "cbLang";
            this.cbLang.Size = new System.Drawing.Size(214, 21);
            this.cbLang.TabIndex = 42;
            this.cbLang.SelectedIndexChanged += new System.EventHandler(this.cbLang_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(851, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Language";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(659, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "English Font Size";
            // 
            // numericUpDownEng
            // 
            this.numericUpDownEng.Location = new System.Drawing.Point(671, 40);
            this.numericUpDownEng.Name = "numericUpDownEng";
            this.numericUpDownEng.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownEng.TabIndex = 45;
            this.numericUpDownEng.ValueChanged += new System.EventHandler(this.FontSizeEng_ValueChanged);
            // 
            // Menu1
            // 
            this.Menu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conversionsToolStripMenuItem});
            this.Menu1.Location = new System.Drawing.Point(0, 0);
            this.Menu1.Name = "Menu1";
            this.Menu1.Size = new System.Drawing.Size(1085, 24);
            this.Menu1.TabIndex = 46;
            this.Menu1.Text = "menuStrip1";
            // 
            // conversionsToolStripMenuItem
            // 
            this.conversionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wylieToTibetanToolStripMenuItem});
            this.conversionsToolStripMenuItem.Name = "conversionsToolStripMenuItem";
            this.conversionsToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.conversionsToolStripMenuItem.Text = "Conversions";
            // 
            // wylieToTibetanToolStripMenuItem
            // 
            this.wylieToTibetanToolStripMenuItem.Name = "wylieToTibetanToolStripMenuItem";
            this.wylieToTibetanToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.wylieToTibetanToolStripMenuItem.Text = "Wylie To Tibetan";
            this.wylieToTibetanToolStripMenuItem.Click += new System.EventHandler(this.wylieToTibetanToolStripMenuItem_Click);
            // 
            // frmDagYig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 418);
            this.Controls.Add(this.numericUpDownEng);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbLang);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownTib);
            this.Controls.Add(this.btnLookUpTibetan);
            this.Controls.Add(this.txtDicLookUp);
            this.Controls.Add(this.btnDictLookupEnglish);
            this.Controls.Add(this.DefGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Menu1);
            this.MainMenuStrip = this.Menu1;
            this.Name = "frmDagYig";
            this.Text = "DagYig";
            ((System.ComponentModel.ISupportInitialize)(this.DefGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTib)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEng)).EndInit();
            this.Menu1.ResumeLayout(false);
            this.Menu1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DefGridView;
        private System.Windows.Forms.Button btnDictLookupEnglish;
        private System.Windows.Forms.TextBox txtDicLookUp;
        private System.Windows.Forms.Button btnLookUpTibetan;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDownTib;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ComboBox cbLang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownEng;
        private System.Windows.Forms.MenuStrip Menu1;
        private System.Windows.Forms.ToolStripMenuItem conversionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wylieToTibetanToolStripMenuItem;
    }
}