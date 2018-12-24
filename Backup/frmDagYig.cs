/* File: frmDagYig.cs
 * Date: 06.10.2009
 * Desc: Tibetan Dictionary 
 * Auth: © Al Gallo 2009 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using DagYig.Properties;

namespace DagYig
{
    public partial class frmDagYig : Form
    {
        private string sqlconn = "Server=.\\sqlexpress;Database=DagYig;Trusted_Connection=True;";
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private bool blnSearchEnglishDefinition = true;
        InputLanguage[] lang = new
               InputLanguage[InputLanguage.InstalledInputLanguages.Count];
        private Font tibFont;
        private Font engFont;
        private bool blnFirstTimeThrough = true;// easy way to keep the load prefs from mucking with the on change of size and font
        
        public frmDagYig()
        {
            InitializeComponent();
            InitializeFormItems();
            blnFirstTimeThrough = false;
        }
       
        // on load
        private void InitializeFormItems()
        {
            LoadPrefs();
            InputLanguage.InstalledInputLanguages.CopyTo(lang, 0);

            foreach (InputLanguage l in lang)
            {
                cbLang.Items.Add(l.Culture.DisplayName);
            }
            
            cbLang.SelectedIndex =
            cbLang.Items.IndexOf(InputLanguage.DefaultInputLanguage.Culture.DisplayName);
            cbLang.SelectedItem =
            InputLanguage.DefaultInputLanguage.Culture.DisplayName;

           
            if (InputLanguage.CurrentInputLanguage.Culture.Name != "en-US")
            {
                txtDicLookUp.Font = tibFont;
            }
            else
            {
                txtDicLookUp.Font = engFont;
            }

        }
        private void LoadPrefs()
        {
            engFont = Settings.Default.EngFont;
            tibFont = Settings.Default.TibFont;
            numericUpDownTib.Value = (decimal)tibFont.Size;
            numericUpDownEng.Value = (decimal)engFont.Size;    
        
    
        }
        private void SavePrefs()
        {
            // not implemented

            // Copy window size to app settings

            // Save settings

           // Settings.Default.Save();
        }

        #region helper methods
        private bool IsTibetan(string word)
        {
            char c = word[0];
            int ai = (int)c;
            if (ai > 3800 && ai < 4500)
            {
                return true;
            }
            return false;

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
        // get rid of garbage
        private string cleanLatin(string word)
        {
            word = word.Replace(",", "");
            word = word.Replace("‘", "'");
            word = word.Replace("{", "");
            word = word.Replace("}", "");
            word = word.Replace("(", "");
            word = word.Replace(")", "");
            word = word.Replace("\r", "");
            word = word.Replace("\n", "");
            word = word.Replace("ṅ", "ng");
            word = word.Replace("ñ", "ny");
            word = word.Replace("ź", "zh");
            word = word.Replace("ś", "sh");
            word = word.Replace("||", "/");
            word = word.Replace("|", "/");
            word = word.Replace("’", "'");
            return word;
        }

        // get rid of garbage
        private string cleanTib(string word)
        {
            word = word.Replace("’", "'");
            word = word.Replace("‘", "'");
            word = word.Replace(",", "");
            word = word.Replace("{", "");
            word = word.Replace("}", "");
            word = word.Replace("(", "");
            word = word.Replace(")", "");
            word = word.Replace("\r", "");
            word = word.Replace("\n", "");
            word = word.Replace("།", "་");
            //		(int)'་'	3851	int
            int l = word.Length - 1;
            int m = (int)word[l];
            if (m != 3851)
            {
                word += "་";
            }
           return word;
        }
        #endregion

        #region clicks
        #region fonts

        private Font ChangeFontSize(Font font, float fontSize, GraphicsUnit unit)
        {
            if (font != null)
            {
                float currentSize = font.Size;
                if (currentSize != fontSize)
                {
                    font = new Font(font.Name, fontSize,
                        font.Style, unit,
                        font.GdiCharSet, font.GdiVerticalFont);
                }
            }
            return font;
        }
        
        // tibetan
        private void FontSizeTib_ValueChanged(object sender, EventArgs e)
        {
            if (blnFirstTimeThrough) {return;}
            float size = (float)((NumericUpDown)sender).Value;
            if (size < 1.0f) { size = 1.0f; }
            try
            {
                tibFont = ChangeFontSize(tibFont, size, GraphicsUnit.Point);
             
                if (InputLanguage.CurrentInputLanguage.Culture.Name != "en-US") // not tibetan then change it

                {
                    txtDicLookUp.Font = ChangeFontSize(txtDicLookUp.Font, size, GraphicsUnit.Pixel);
                }
                if (!String.IsNullOrEmpty(txtDicLookUp.Text))  // if it's not tibetan, then change it
                {
                    if (IsTibetan(txtDicLookUp.Text.Trim())){
                        txtDicLookUp.Font = tibFont;
                    }
                }
           
               // definitely change the Tibetan text
              DefGridView.Columns["Tibetan"].DefaultCellStyle.Font = tibFont;
            }
            catch { }
         }

        // english
        private void FontSizeEng_ValueChanged(object sender, EventArgs e)
        {
            if (blnFirstTimeThrough) { return; }
            float size = (float)((NumericUpDown)sender).Value;
            if (size < 1.0f) { size = 1.0f; }
            try
            {
                // definitely change the english definition
                engFont = ChangeFontSize(engFont, size, GraphicsUnit.Point);
                DefGridView.Columns["Definition"].DefaultCellStyle.Font = engFont;
         
                if (InputLanguage.CurrentInputLanguage.Culture.Name == "en-US") // not tibetan, change it
                {
                    txtDicLookUp.Font = engFont;// ChangeFontSize(txtDicLookUp.Font, fnt, GraphicsUnit.Pixel);
                }
                if (!String.IsNullOrEmpty(txtDicLookUp.Text))  // if it's not tibetan, then change it
                {
                    if (!IsTibetan(txtDicLookUp.Text))
                    {
                        txtDicLookUp.Font = engFont;
                        
                    }
                }
             }
             catch { }

        }
     
        private void cbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage =
                lang[cbLang.SelectedIndex];
          
        }
        #endregion
      
        
        private void btnDictLookupEnglish_Click(object sender, EventArgs e)
        {
            string lookup = txtDicLookUp.Text;
           
            if (String.IsNullOrEmpty(lookup))
            {
                return;
            }
            blnSearchEnglishDefinition = true;
            SearchSpecific(lookup);
        }
        private void btnDictLookupTibetan_Click(object sender, EventArgs e)
        {
            string lookup = txtDicLookUp.Text;

            if (String.IsNullOrEmpty(lookup))
            {
                return;
            }
            blnSearchEnglishDefinition = false;
            SearchSpecific(lookup);
        }
        private void btnInsertNewDef_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            lblMsg.Text = "";
            string msg = "";
            string newword = "";
            if (ds.HasChanges())
            {

                DataTable dt1 = ds.Tables[0];
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i].RowState == DataRowState.Modified ||
                            dt1.Rows[i].RowState == DataRowState.Added)
                    {
                        try
                        {
                            msg += UpdateDR(dt1.Rows[i]);
                            newword = dt1.Rows[i]["Wylie"].ToString();
                        }
                        catch (Exception ex)
                        {
                            msg += "Error: " + dt1.Rows[i]["DictID"].ToString();
                        }
                    }
                }
                if (msg != "")
                {
                    lblMsg.Text = msg;
                    lblMsg.Visible = true;
                }
                else
                {
                    lblMsg.Text = "New Definition Entered.";
                    lblMsg.Visible = true;
                    CallDefDB(newword);
                }

            }
        }
#endregion
    
        #region DataGrid
        private void defGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //this.DefGridView.Columns["Tibetan"].DefaultCellStyle.Font =
            //        ChangeFontSize(DefGridView.DefaultCellStyle.Font, 25, GraphicsUnit.Pixel);
            this.DefGridView.Columns["Tibetan"].Width = 200;
            this.DefGridView.Columns["Definition"].Width = 475;
            this.DefGridView.Columns["Wylie"].Width = 100;

            this.DefGridView.Columns["DictID"].Visible = false;
            this.DefGridView.Columns["DictID"].ReadOnly = true;
             this.DefGridView.Columns["Reference"].Visible = false;
             this.DefGridView.Columns["Reference"].ReadOnly = true;
             this.DefGridView.Columns["Comment"].Visible = false;
             this.DefGridView.Columns["Comment"].ReadOnly = true;
             this.DefGridView.Columns["Uni"].Visible = false;
             this.DefGridView.Columns["Uni"].ReadOnly = true;
            this.DefGridView.Columns["Definition"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            Padding newPadding = new Padding(0, 0, 0,5);
            this.DefGridView.Columns["Tibetan"].DefaultCellStyle.Padding = newPadding;
            //Font tibetanFont = new Font(cbFonts.SelectedText, (float)numericUpDown1.Value, GraphicsUnit.Pixel);
            this.DefGridView.Columns["Tibetan"].DefaultCellStyle.Font = tibFont;
            //Font definitionFont = new Font(cbFonts.SelectedText, (float)numericUpDown2.Value, GraphicsUnit.Pixel);
            this.DefGridView.Columns["Definition"].DefaultCellStyle.Font = engFont;

            ////  DefGridView.Rows[DefGridView.Rows.Count - 1].Cells["Word"].ReadOnly = false;
            //DataGridViewRow row = this.DefGridView.RowTemplate;
            //row.Height = 80;

        }

        #endregion
      
        private void SearchSpecific(string val)
        {
            lblMsg.Text = "";
            lblMsg.Visible = false;
          
            if (String.IsNullOrEmpty(val))
            {
                lblMsg.Text = "Please enter a valid search term.";
                return;
            }
            val = val.Trim();
            if (val.StartsWith(","))
            {
                val = val.Substring(1);
            }

            if (IsTibetan(val))
            {
                cleanTib(val);
            }
            else
            {
                cleanLatin(val);
            }
           
            
            // clear out prior ds
            ds.Clear();
            DefGridView.DataSource = null;
            lblMsg.Text = "Searching";
            lblMsg.Visible = true;
            // todo , put in a busy 
            try
            {
                backgroundWorker1.RunWorkerAsync(val);//this invokes the DoWork event
            }
            catch { }

        }

        #region DB
        private DataTable CallDefDB(string word)
        {
                       
            bool blnTibetan = true;
            word = word.Trim();

            blnTibetan = IsTibetan(word);
           
            ds.Clear();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string msg = "";
        
            try
            {

                con = new SqlConnection(sqlconn);
                if (blnSearchEnglishDefinition)
                {
                     cmd = new SqlCommand("LookUpEnglish", con);
                   
                }
                else
                {
                    if (blnTibetan)
                    {
                        cmd = new SqlCommand("LookUpDefTib", con);
                    }
                    else
                    {
                        cmd = new SqlCommand("LookUpDef", con);
                    }
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("List", word));

                con.Open();

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter(cmd);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand. These are used to
                // update the database.
                // Populate a new data table and bind it to the BindingSource.
                dataAdapter.Fill(ds);
             
               
            }
            catch (Exception e)
            {
                msg += "Error " + e.Message;

            }
            finally { if (con != null) { con.Close(); } }

            return ds.Tables[0];

        }
        private string UpdateDR(DataRow dr)
        {
            string msg = "";
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(sqlconn);
                cmd = new SqlCommand("UpdateDef", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("DictID", dr["DictID"].ToString()));
                cmd.Parameters.Add(new SqlParameter("Definition", dr["Definition"].ToString()));
                cmd.Parameters.Add(new SqlParameter("Wylie", dr["Wylie"].ToString()));
                cmd.Parameters.Add(new SqlParameter("Tibetan", dr["Tibetan"].ToString()));

                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ee) { msg = "An Error Occurred. "; } //+ dr["DictID"].ToString(); }
            finally { if (con != null) { con.Close(); } }
            if (msg == "")
            {

            }
            return msg;
        }
        #endregion

        #region background workers
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string val = (string)e.Argument;
            try
            {
                e.Result = CallDefDB(val);
            }
            catch (Exception ex) { string yadda = ex.Message; }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result == null)
                {
                    lblMsg.Text = "Error Processing Search";
                    return;
                }
                DataTable dt = (DataTable)e.Result;

                DefGridView.DataSource = ds.Tables[0]; // bindingSource1;

               
                
                if (dt.Rows.Count < 1)
                {
                    lblMsg.Text = "Not Found";
                    lblMsg.Visible = true;

                }
                else
                {
                    lblMsg.Text = "Found";
                    lblMsg.Visible = true;
              
                }

            }
        }
#endregion


        #region events
        private void txtDicLookup_OnTextChanged(object sender, EventArgs e)
        {
            string test = txtDicLookUp.Text;
            if (String.IsNullOrEmpty(test)) { return; }
            char c = test[0];
            if (IsTibetan(c)) { txtDicLookUp.Font = Settings.Default.TibFont; }
            else { txtDicLookUp.Font = Settings.Default.EngFont; }
        }

        private void txtDic_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDictLookupTibetan_Click(sender, e);
            }
        }

        private void wylieToTibetanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
             foreach(Form f in Application.OpenForms)
             {
                if(f.GetType() == typeof(frmWylieToTib))
                {
                    found = true;
                    break;
                }
             }
             if (!found)
             {

                 Form f = new frmWylieToTib();
                 f.Show();
             }
        }

        #endregion







    }
}
