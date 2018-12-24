/* File: DagYigPref.cs
 * Date: 12.10.2010
 * Desc: 
 * Auth: © Al Gallo 2009 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DagYig.Properties;
namespace DagYig
{
    public partial class frmDagYigPref : Form
    {
        public frmDagYigPref()
        {
            InitializeComponent();
            init();
        }

        private float pTibFontSize;
        private Font pTibFont;
        private float pEngFontSize;
        private Font pEngFont;
        private bool blnTibChangeCalled = false; // not used to windows forms.. how do we get font.SIZE without 
        private bool blnEngChangeCalled = false; // creating whole new font? 
       
        public frmDagYigScan MyParentForm;

        public Font PTibFont
        {
            get { return pTibFont; }
        }

        public float PTibFontSize
        {
            get { return pTibFontSize; }
        }
        public Font PEngFont
        {
            get { return pEngFont; }
        }

        public float PEngFontSize
        {
            get { return pTibFontSize; }
        }
        private void frmClipBoardPref_Load(object sender, System.EventArgs e)
        {
            //string clr = ((frmClipBoard)MyParentForm).txtTibetan.Text;
            //this.textBox1.Text= clr;
        }
        private void init()
        {

            this.TopMost = true;

            string k = Settings.Default.LoopHotKey;
            if (!String.IsNullOrEmpty(k))
            {
                txtKey.Text = k;
                ckHotKey.Checked = Settings.Default.UseHotKey == "Y" ? true : false;
            }

            pEngFont = Settings.Default.EngFont;
            pTibFont = Settings.Default.TibFont;
            //pEngFontSize = Settings.Default.EngFontSize;
            //pTibFontSize = Settings.Default.TibFontSize;
            //pEngFontSize = PEngFont.Size;
            //pTibFontSize = pTibFont.Size;
            numericUpDownEng.Value = (decimal)pEngFont.Size;
            numericUpDownTib.Value = (decimal)pTibFont.Size;
            LoopThroughEnglishSystemFonts();
            LoopThroughTibetanSystemFonts();

        }
  
        private void LoopThroughTibetanSystemFonts()
        {
            FontFamily[] families = FontFamily.Families;
            //Loop Through System Fonts for Tibetan
            foreach (FontFamily family in families)
            {
               

                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    cbTibFonts.Items.Add(family.Name);
                }
               
                if (family.Name == pTibFont.FontFamily.Name)
                {
                    cbTibFonts.SelectedIndex = cbTibFonts.Items.Count - 1;

                }
            }
        }
        private void LoopThroughEnglishSystemFonts() {
               //Loop Through System Fonts
            FontFamily[] families = FontFamily.Families;
            foreach (FontFamily family in families)
            {
                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    cbEngFonts.Items.Add(family.Name);
                }
               
                if (family.Name == pEngFont.FontFamily.Name)
                {
                    cbEngFonts.SelectedIndex = cbEngFonts.Items.Count - 1;

                }
            }
           
          
        }
    
        private void TibFontSize_ValueChanged(object sender, EventArgs e)
        {
            blnTibChangeCalled = true;
            pTibFontSize = (float)((NumericUpDown)sender).Value;
            if (pTibFontSize < 1) { pTibFontSize = 1; }
          //  Settings.Default.TibFontSize = pTibFontSize;
            pTibFont = ChangeFont(pTibFont, pTibFontSize);
            Settings.Default.TibFont = pTibFont;
            LoopThroughTibetanSystemFonts();
            blnTibChangeCalled = false;
        }

        private void EngFontSize_ValueChanged(object sender, EventArgs e)
        {
            blnEngChangeCalled = true;
            if (pEngFontSize < 1) { pEngFontSize = 1; }
            pEngFontSize = (float)((NumericUpDown)sender).Value;
            //Settings.Default.EngFontSize = pEngFontSize;
            pEngFont = ChangeFont(pEngFont, pEngFontSize);
            Settings.Default.EngFont = pEngFont;
            LoopThroughEnglishSystemFonts();
            blnEngChangeCalled = false;
        }

        private void TibFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnTibChangeCalled) { return; }
            string fontName = (string)((ComboBox)sender).SelectedItem;
            pTibFont = ChangeFont(fontName, pTibFont.Size);
            Settings.Default.TibFont = pTibFont;

        }
        private void EngFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnEngChangeCalled) { return; }
            string fontName = (string)((ComboBox)sender).SelectedItem;
            pEngFont = ChangeFont(fontName, pEngFont.Size);
            Settings.Default.EngFont = pEngFont;

        }
     
        private void button1_Click(object sender, EventArgs e)
        {

               if (ckHotKey.Checked && String.IsNullOrEmpty(txtKey.Text))
            {

                MessageBox.Show("If Use Hotkey is checked, you must enter a value for the hotkey.");
                return;
            }
               Settings.Default.LoopHotKey = txtKey.Text;
               Settings.Default.UseHotKey = ckHotKey.Checked == true ? "Y" : "N";
                
               MyParentForm.ChangeFont(pTibFont, pEngFont);
               MyParentForm.SetBlnHookKey(ckHotKey.Checked);
               MyParentForm.SpecialKey = txtKey.Text;
       
            Close();
            
        }
        private Font ChangeFont(string fontName, float fontSize) {

           return new Font(fontName, fontSize,
                FontStyle.Regular, GraphicsUnit.Point);
             
           
        }
        private Font ChangeFont (Font f, float fs) {
                return new Font(f.FontFamily,  fs,
                     f.Style, GraphicsUnit.Point,
                     f.GdiCharSet, f.GdiVerticalFont);
        }

        private void ckHotKey_Changed(object sender, EventArgs e)
        {
            if (ckHotKey.Checked && String.IsNullOrEmpty(txtKey.Text))
            {

                MessageBox.Show("If Use Hotkey is checked, you must enter a value for the hotkey.");
                return;
            }

        }
    }
}
