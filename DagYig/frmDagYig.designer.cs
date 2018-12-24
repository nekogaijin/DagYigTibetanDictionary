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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDagYig));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.numericUpDownDef = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDicLookUp = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDownInput = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLang = new System.Windows.Forms.ComboBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownTib = new System.Windows.Forms.NumericUpDown();
            this.btnLookUpTibetan = new System.Windows.Forms.Button();
            this.btnDictLookupEnglish = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.DefGridView = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Menu1 = new System.Windows.Forms.MenuStrip();
            this.conversionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wylieToTibetanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDef)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTib)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefGridView)).BeginInit();
            this.Menu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownDef);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtDicLookUp);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownInput);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.cbLang);
            this.splitContainer1.Panel1.Controls.Add(this.lblMsg);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownTib);
            this.splitContainer1.Panel1.Controls.Add(this.btnLookUpTibetan);
            this.splitContainer1.Panel1.Controls.Add(this.btnDictLookupEnglish);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DefGridView);
            this.splitContainer1.Size = new System.Drawing.Size(1154, 546);
            this.splitContainer1.SplitterDistance = 98;
            this.splitContainer1.TabIndex = 0;
            // 
            // numericUpDownDef
            // 
            this.numericUpDownDef.Location = new System.Drawing.Point(990, 57);
            this.numericUpDownDef.Name = "numericUpDownDef";
            this.numericUpDownDef.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownDef.TabIndex = 60;
            this.numericUpDownDef.ValueChanged += new System.EventHandler(this.FontSizeDef_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(966, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "Definition Font Size";
            // 
            // txtDicLookUp
            // 
            this.txtDicLookUp.ContextMenuStrip = this.contextMenuStrip1;
            this.txtDicLookUp.Location = new System.Drawing.Point(12, 27);
            this.txtDicLookUp.Name = "txtDicLookUp";
            this.txtDicLookUp.Size = new System.Drawing.Size(379, 65);
            this.txtDicLookUp.TabIndex = 58;
            this.txtDicLookUp.Text = "";
            this.txtDicLookUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtDicLookUp_MouseClick);
            this.txtDicLookUp.TextChanged += new System.EventHandler(this.txtDicLookup_OnTextChanged);
            this.txtDicLookUp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDic_OnKeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.ContextMenuCopy_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.ContextMenuPaste_Click);
            // 
            // numericUpDownInput
            // 
            this.numericUpDownInput.Location = new System.Drawing.Point(899, 57);
            this.numericUpDownInput.Name = "numericUpDownInput";
            this.numericUpDownInput.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownInput.TabIndex = 57;
            this.numericUpDownInput.ValueChanged += new System.EventHandler(this.FontSizeInput_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(882, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Input Font Size";
            // 
            // cbLang
            // 
            this.cbLang.FormattingEnabled = true;
            this.cbLang.Location = new System.Drawing.Point(561, 59);
            this.cbLang.Name = "cbLang";
            this.cbLang.Size = new System.Drawing.Size(178, 21);
            this.cbLang.TabIndex = 54;
            this.cbLang.SelectedIndexChanged += new System.EventHandler(this.cbLang_SelectedIndexChanged);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(691, 37);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(35, 13);
            this.lblMsg.TabIndex = 53;
            this.lblMsg.Text = "label2";
            this.lblMsg.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(786, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Tibetan Font Size";
            // 
            // numericUpDownTib
            // 
            this.numericUpDownTib.Location = new System.Drawing.Point(813, 57);
            this.numericUpDownTib.Name = "numericUpDownTib";
            this.numericUpDownTib.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownTib.TabIndex = 51;
            this.numericUpDownTib.ValueChanged += new System.EventHandler(this.FontSizeTib_ValueChanged);
            // 
            // btnLookUpTibetan
            // 
            this.btnLookUpTibetan.Location = new System.Drawing.Point(412, 27);
            this.btnLookUpTibetan.Name = "btnLookUpTibetan";
            this.btnLookUpTibetan.Size = new System.Drawing.Size(133, 23);
            this.btnLookUpTibetan.TabIndex = 50;
            this.btnLookUpTibetan.Text = "Search Tibetan";
            this.btnLookUpTibetan.UseVisualStyleBackColor = true;
            this.btnLookUpTibetan.Click += new System.EventHandler(this.btnDictLookupTibetan_Click);
            // 
            // btnDictLookupEnglish
            // 
            this.btnDictLookupEnglish.Location = new System.Drawing.Point(412, 57);
            this.btnDictLookupEnglish.Name = "btnDictLookupEnglish";
            this.btnDictLookupEnglish.Size = new System.Drawing.Size(133, 23);
            this.btnDictLookupEnglish.TabIndex = 49;
            this.btnDictLookupEnglish.Text = "Search English Definition";
            this.btnDictLookupEnglish.UseVisualStyleBackColor = true;
            this.btnDictLookupEnglish.Click += new System.EventHandler(this.btnDictLookupEnglish_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(561, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 48;
            this.button1.Text = "Update Dictionary";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnInsertNewDef_Click);
            // 
            // DefGridView
            // 
            this.DefGridView.AllowUserToDeleteRows = false;
            this.DefGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DefGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DefGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DefGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DefGridView.Location = new System.Drawing.Point(0, 0);
            this.DefGridView.Name = "DefGridView";
            this.DefGridView.RowTemplate.Height = 65;
            this.DefGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DefGridView.Size = new System.Drawing.Size(1154, 444);
            this.DefGridView.TabIndex = 12;
            this.DefGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DefGridView_ColumnHeaderMouseClick);
            this.DefGridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DefGridView_ColumnWidthChanged);
            this.DefGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.defGridView_DataBindingComplete);
            this.DefGridView.RowDividerHeightChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.DefGridView_RowDividerHeightChanged);
            this.DefGridView.RowHeightChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.DefGridView_RowHeightChanged);
            this.DefGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DefGridView_RowPostPaint);
            this.DefGridView.SizeChanged += new System.EventHandler(this.DefGridView_SizeChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Menu1
            // 
            this.Menu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conversionsToolStripMenuItem});
            this.Menu1.Location = new System.Drawing.Point(0, 0);
            this.Menu1.Name = "Menu1";
            this.Menu1.Size = new System.Drawing.Size(1154, 24);
            this.Menu1.TabIndex = 47;
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
            this.ClientSize = new System.Drawing.Size(1154, 546);
            this.Controls.Add(this.Menu1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDagYig";
            this.Text = "frmDagYigNew";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDagYig_Closing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDef)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTib)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefGridView)).EndInit();
            this.Menu1.ResumeLayout(false);
            this.Menu1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.MenuStrip Menu1;
        private System.Windows.Forms.ToolStripMenuItem conversionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wylieToTibetanToolStripMenuItem;
        private System.Windows.Forms.RichTextBox txtDicLookUp;
        private System.Windows.Forms.NumericUpDown numericUpDownInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLang;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownTib;
        private System.Windows.Forms.Button btnLookUpTibetan;
        private System.Windows.Forms.Button btnDictLookupEnglish;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView DefGridView;
        private System.Windows.Forms.NumericUpDown numericUpDownDef;
        private System.Windows.Forms.Label label2;
    }
}