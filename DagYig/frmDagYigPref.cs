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

        private float pScanFontSize;
        private Font pScanFont;
        private float pScanDefFontSize;
        private Font pScanDefFont;
     
        private float pDictInputFontSize;
        private Font pDictInputFont;
        private float pDictDefFontSize;
        private Font pDictDefFont;
        private float pDictTibFontSize;
        private Font pDictTibFont;

        private bool blnScanChangeCalled = false; // not used to windows forms.. how do we get font.SIZE without 
        private bool blnScanDefChangeCalled = false; // creating whole new font? 
        private bool blnDictTibChangeCalled = false;
        private bool blnDictDefChangeCalled = false;
        private bool blnDictInputChangeCalled = false; 

        public frmDagYigScan MyParentForm;

        public Font PScanFont
        {
            get { return pScanFont; }
        }
        public float PScanFontSize
        {
            get { return pScanFontSize; }
        }
    
        public Font PScanDefFont
        {
            get { return pScanDefFont ; }
        }
        public float PDefScanFontSize
        {
            get { return pScanDefFontSize; }
        }


        public Font PDictInputFont
        {
            get { return pDictInputFont; }
        }
        public float PDictInputFontSize
        {
            get { return pDictInputFontSize; }
        }


        public Font PDictDefFont
        {
            get { return pDictDefFont; }
        }
        public float PDefFontSize
        {
            get { return pDictDefFontSize; }
        }

        public Font PDictTibFont
        {
            get { return pDictTibFont; }
        }
        public float PDictTibFontSize
        {
            get { return pDictTibFontSize; }
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

            pScanFont = Settings.Default.ScanFont;
            pScanDefFont = Settings.Default.ScanDefFont;
            pDictInputFont = Settings.Default.DictInputFont;
            pDictDefFont = Settings.Default.DictDefFont;
            pDictTibFont = Settings.Default.DictTibFont;

            UpDownScan.Value = (decimal)Settings.Default.ScanFontSize;
            UpDownScanDef.Value = (decimal)Settings.Default.ScanDefFontSize;

            UpDownDictTib.Value = (decimal)Settings.Default.DictTibFontSize;
            UpDownDictDef.Value = (decimal)Settings.Default.DictDefFontSize;
            UpDownDictInput.Value = (decimal)Settings.Default.DictInputFontSize;


            LoopThroughScanSystemFonts();
            LoopThroughScanDefSystemFonts();
            
            LoopThroughDictTibSystemFonts();
            LoopThroughDictDefSystemFonts();
            LoopThroughDictInputSystemFonts();

        }
        
        #region fill font boxes
        private void LoopThroughDictTibSystemFonts()
        {
            FontFamily[] families = FontFamily.Families;
            //Loop Through System Fonts for Tibetan
            foreach (FontFamily family in families)
            {
               if (family.IsStyleAvailable(FontStyle.Regular))
                {
                   cbDictTibFonts.Items.Add(family.Name);
                }

                if (family.Name == pDictTibFont.FontFamily.Name)
                {
                    cbDictTibFonts.SelectedIndex = cbDictTibFonts.Items.Count - 1;
                }
            }
        }
        private void LoopThroughDictDefSystemFonts()
        {
            FontFamily[] families = FontFamily.Families;
            //Loop Through System Fonts for Tibetan
            foreach (FontFamily family in families)
            {
                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    cbDictDefFonts.Items.Add(family.Name);
                }

                if (family.Name == pDictDefFont.FontFamily.Name)
                {
                    cbDictDefFonts.SelectedIndex = cbDictDefFonts.Items.Count - 1;
                }
            }
        }
        private void LoopThroughDictInputSystemFonts()
        {
            FontFamily[] families = FontFamily.Families;
            //Loop Through System Fonts for Tibetan
            foreach (FontFamily family in families)
            {
                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    cbDictInputFonts.Items.Add(family.Name);
                }

                if (family.Name == pDictInputFont.FontFamily.Name)
                {
                    cbDictInputFonts.SelectedIndex = cbDictInputFonts.Items.Count - 1;
                }
            }
        }
        private void LoopThroughScanSystemFonts()
        {
            FontFamily[] families = FontFamily.Families;
            //Loop Through System Fonts for Tibetan
            foreach (FontFamily family in families)
            {
                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    cbScanFonts.Items.Add(family.Name);
                }

                if (family.Name == pScanFont.FontFamily.Name)
                {
                    cbScanFonts.SelectedIndex = cbScanFonts.Items.Count - 1;
                }
            }
        }
        private void LoopThroughScanDefSystemFonts()
        {
            FontFamily[] families = FontFamily.Families;
            //Loop Through System Fonts for Tibetan
            foreach (FontFamily family in families)
            {
                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    cbScanDefFonts.Items.Add(family.Name);
                }

                if (family.Name == pScanDefFont.FontFamily.Name)
                {
                    cbScanDefFonts.SelectedIndex = cbScanDefFonts.Items.Count - 1;
                }
            }
        }
#endregion
        #region Font Size Changed
        private void ScanSize_ValueChanged(object sender, EventArgs e)
        {
            blnScanChangeCalled = true;
            pScanFontSize = (float)((NumericUpDown)sender).Value;
            if (pScanFontSize < 1) { pScanFontSize = 1; }
            //  Settings.Default.TibFontSize = pTibFontSize;
            pScanFont = ChangeFont(pScanFont, pScanFontSize);
            Settings.Default.ScanFont = pScanFont;
            Settings.Default.ScanFontSize = pScanFontSize;
            LoopThroughScanSystemFonts();
            blnScanChangeCalled = false;
        }

        private void ScanDefSize_ValueChanged(object sender, EventArgs e)
        {
            blnScanDefChangeCalled = true;
            pScanDefFontSize = (float)((NumericUpDown)sender).Value;
            if (pScanDefFontSize < 1) { pScanDefFontSize = 1; }
            pScanDefFont = ChangeFont(pScanDefFont, pScanDefFontSize);
            Settings.Default.ScanDefFont = pScanDefFont;
            Settings.Default.ScanDefFontSize = pScanDefFontSize;
            LoopThroughScanDefSystemFonts();
            blnScanDefChangeCalled = false;
        }

        private void DictInputSize_ValueChanged(object sender, EventArgs e)
        {
            blnDictInputChangeCalled = true;
            pDictInputFontSize = (float)((NumericUpDown)sender).Value;
            if (pDictInputFontSize < 1) { pDictInputFontSize = 1; }
            pDictInputFont = ChangeFont(pDictInputFont, pDictInputFontSize);
            Settings.Default.DictInputFont = pDictInputFont;
            Settings.Default.DictInputFontSize = pDictInputFontSize;
            LoopThroughDictInputSystemFonts();
            blnDictInputChangeCalled = false;
        }

        private void DictTibSize_ValueChanged(object sender, EventArgs e)
        {
            blnDictTibChangeCalled = true;
            pDictTibFontSize = (float)((NumericUpDown)sender).Value;
            if (pDictTibFontSize < 1) { pDictTibFontSize = 1; }
            pDictTibFont = ChangeFont(pDictTibFont, pDictTibFontSize);
            Settings.Default.DictTibFont = pDictTibFont;
            Settings.Default.DictTibFontSize = pDictTibFontSize;
            LoopThroughDictTibSystemFonts();
            blnDictTibChangeCalled = false;
        }

        private void DictDefSize_ValueChanged(object sender, EventArgs e)
        {
            blnDictDefChangeCalled = true;
            pDictDefFontSize = (float)((NumericUpDown)sender).Value;
            if (pDictDefFontSize < 1) { pDictDefFontSize = 1; }
            pDictDefFont = ChangeFont(pDictDefFont, pDictDefFontSize);
            Settings.Default.DictDefFont = pDictDefFont;
            Settings.Default.DictDefFontSize = pDictDefFontSize;
            LoopThroughDictDefSystemFonts();
            blnDictDefChangeCalled = false;
        }
      
        #endregion

        #region Font Selected Index Changed
        private void ScanFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnScanChangeCalled) { return; }
            string fontName = (string)((ComboBox)sender).SelectedItem;
            pScanFont = ChangeFont(fontName, pScanFont.Size);
            Settings.Default.ScanFont = pScanFont;
        }
        private void ScanDefFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnScanDefChangeCalled) { return; }
            string fontName = (string)((ComboBox)sender).SelectedItem;
            pScanDefFont = ChangeFont(fontName, pScanDefFont.Size);
            Settings.Default.ScanDefFont= pScanDefFont;
        }

        private void DictInputFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnDictInputChangeCalled) { return; }
            string fontName = (string)((ComboBox)sender).SelectedItem;
            pDictInputFont = ChangeFont(fontName, pDictInputFont.Size);
            Settings.Default.DictInputFont = pDictInputFont;
        }

        private void DictTibFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnDictTibChangeCalled) { return; }
            string fontName = (string)((ComboBox)sender).SelectedItem;
            pDictTibFont = ChangeFont(fontName, pDictTibFont.Size);
            Settings.Default.DictTibFont = pDictTibFont;
        }

        private void DictDefFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnDictDefChangeCalled) { return; }
            string fontName = (string)((ComboBox)sender).SelectedItem;
            pDictDefFont = ChangeFont(fontName, pDictDefFont.Size);
            Settings.Default.DictDefFont = pDictTibFont;
        }

       
#endregion

        private void button1_Click(object sender, EventArgs e)
        {

               if (ckHotKey.Checked && String.IsNullOrEmpty(txtKey.Text))
            {

                MessageBox.Show("If Use Hotkey is checked, you must enter a value for the hotkey.");
                return;
            }
               Settings.Default.LoopHotKey = txtKey.Text;
               Settings.Default.UseHotKey = ckHotKey.Checked == true ? "Y" : "N";

               MyParentForm.ChangeFont(pScanFont, pScanDefFont);
               MyParentForm.SetBlnHookKey(ckHotKey.Checked);
               MyParentForm.SpecialKey = txtKey.Text;

               Settings.Default.Save();

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

        private void lbltop_Click(object sender, EventArgs e)
        {

        }

       

     
    }
}
