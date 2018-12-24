/* File: frmWylieToTib.cs
 * Date: 11.10.2010
 * Desc: Wylie to Tibetan conversion. 
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
    public partial class frmWylieToTib : Form
    {
        WylieToTib WtoT = new WylieToTib();
      
        public frmWylieToTib()
        {
            InitializeComponent();
            this.TopMost = true;
            txtConvert.Font = Settings.Default.EngFont;

        }

     
        private void btnConvertToTib_Click(object sender, EventArgs e)
        {
            string words = txtConvert.Text;// txtTibetan.SelectedText;
            string output = "";
          
            if (String.IsNullOrEmpty(words))
            {
                return;
            }
            words = words.Trim();

            StringBuilder sb = new StringBuilder();
       
            words = words.Replace("\r", "");
            words = words.Replace("\n", "");
            words = words.Replace("ṅ", "ng");
            words = words.Replace("ñ", "ny");
            words = words.Replace("ź", "zh");
            words = words.Replace("ś", "sh");
            words = words.Replace("||", "/");
            words = words.Replace("|", "/");


            string[] allText = words.Split(' ');

            output = WtoT.ConvertWord(words);
        
            txtConvert.Text = output;
            Clipboard.SetText(output);
               
            txtConvert.Font = Settings.Default.TibFont;
        }

        private bool IsTibetan(char c)
        {
           
            int ai = (int)c;
         
            if (ai > 3800 && ai < 4500)
            {
                return true;
            }
            return false;

        }

        private void txtConvert_OnTextChanged(object sender, EventArgs e)
        {
            string test = txtConvert.Text;
            if (String.IsNullOrEmpty(test)) { return; }
            char c = test[0];
            if (IsTibetan(c)) { txtConvert.Font = Settings.Default.TibFont; }
            else { txtConvert.Font = Settings.Default.EngFont; }
        }
    }
}
