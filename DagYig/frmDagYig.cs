/* File: frmDagYig.cs
 * Date: 06.10.2009
 * Desc: Tibetan Dictionary 
 * Auth: © Al Gallo 2009 */
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using DagYig.Properties;

namespace DagYig
{
    public partial class frmDagYig : Form
    {
        private string sqlconn = "Server=.\\sqlexpress;Database=DagYig;Trusted_Connection=True;";
        private DataSet ds = new DataSet();
        private bool blnSearchEnglishDefinition = true;
        private bool blnGridLoading = true;
        InputLanguage[] lang = new InputLanguage[InputLanguage.InstalledInputLanguages.Count];
        private Font tibFont;
        private Font defFont;
        private Font inputFont;
        // col sizes
        private int gridHeight;
        private int tibColW;
      
        private int defColW;
     
        private int wylieColW;

        private bool blnFirstTimeThrough = true;// easy way to keep the load prefs from mucking with the on change of size and font
        public frmDagYigScan MyParentForm;
        public frmDagYig( string passedIn)
        {
            InitializeComponent();
            InitializeFormItems();
            blnFirstTimeThrough = false;
            if (!String.IsNullOrEmpty(passedIn)) {
                 LookUpOnOpen(passedIn);
            }
        }
       
    
        private void InitializeFormItems()
        { 
            try { sqlconn = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]; }
            catch (Exception connexc)
            {
                ErrorLog.WriteLog("Error - frmDagYig, InitializeFormItems, error pulling sqlconnection from config " + connexc.Message);
                ErrorLog.WriteLog("Using hard coded sql string and continuing.");
            }

            LoadPrefs();
          
            InputLanguage.InstalledInputLanguages.CopyTo(lang, 0);
            foreach (InputLanguage l in lang)
            {
                cbLang.Items.Add(l.Culture.DisplayName);
            }
            
            cbLang.SelectedIndex = cbLang.Items.IndexOf(InputLanguage.DefaultInputLanguage.Culture.DisplayName);
            cbLang.SelectedItem = InputLanguage.DefaultInputLanguage.Culture.DisplayName;
                       
            if (InputLanguage.CurrentInputLanguage.Culture.Name != "en-US")
            {
                txtDicLookUp.Font = tibFont;
            }
            else
            {
                txtDicLookUp.Font = inputFont;
            }

        }

        private void LoadPrefs()
        {
            inputFont = Settings.Default.DictInputFont;
            tibFont = Settings.Default.DictTibFont;
            defFont = Settings.Default.DictDefFont;
          
            numericUpDownTib.Value = (decimal)tibFont.Size;
            numericUpDownDef.Value = (decimal)defFont.Size;
            numericUpDownInput.Value = (decimal)inputFont.Size;

            if (Settings.Default.DictionaryWindowSize != null)
            {
                this.Size = Settings.Default.DictionaryWindowSize;
            }

            gridHeight = Settings.Default.GridRowHeight;
            tibColW = Settings.Default.TibColSizeW;
            defColW = Settings.Default.DefColSizeW;
            wylieColW = Settings.Default.WylieColSizeW;
            if (gridHeight < 5) { gridHeight = 5; }
            if (tibColW < 5) { tibColW = 5; }
            if (defColW < 5) { defColW = 5; }
            if (wylieColW < 5) { wylieColW = 5; }
        }

        private void SavePrefs()
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Default.DictionaryWindowSize = this.Size;
            }
            else
            {
                Settings.Default.DictionaryWindowSize = this.RestoreBounds.Size;
            }

            SaveColumnWidths();

           

            //if (size < 1) { size = 1; }
            //Settings.Default.DictInputFontSize = size;
            if (inputFont != null)
            {
                Settings.Default.DictInputFont = inputFont;
                Settings.Default.DictInputFontSize = inputFont.Size;
            }
           
            if (tibFont != null)
            {
                Settings.Default.DictTibFont = tibFont;
                Settings.Default.DictTibFontSize = tibFont.Size;
            }
            if (defFont != null)
            {
                Settings.Default.DictDefFont = defFont;
                Settings.Default.DictDefFontSize = defFont.Size;
            }

               
            Settings.Default.Save();
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

        private string CheckValidTibetan(string t) {
           
            if (String.IsNullOrEmpty(t))
            {
                return "Missing Tibetan.\r\n";
            }
            foreach (char c in t)
            {
                if (!IsTibetan(c))
                {
                    return t + " is Not Valid Tibetan.\r\n";
                }
            }
            return "";
        }

        private string CheckValidWylie(string t)
        {
            if (String.IsNullOrEmpty(t))
            {
                return "Missing Wylie Value.\r\n";
            }
            foreach (char c in t)
            {
                if (IsTibetan(c))
                {
                    return t + " Wylie cannot contain Tibetan Fonts.\r\n";
                }
            }
            return "";
        }
        private string CheckValidDef(string t)
        {
            if (String.IsNullOrEmpty(t))
            {
                return "Missing Definition Value";
            }
            //foreach (char c in t)
            //{
            //    if (IsTibetan(c))
            //    {
            //        return " Definition cannot contain Tibetan Fonts.\r\n";
            //    }
            //}
            return "";
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
            word = word.Replace("'", "''");
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

                Settings.Default.DictTibFont = tibFont;

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
        // definition
        private void FontSizeDef_ValueChanged(object sender, EventArgs e)
        {
            if (blnFirstTimeThrough) { return; }
            float size = (float)((NumericUpDown)sender).Value;
            if (size < 1.0f) { size = 1.0f; }
            try
            {
                // definitely change the english definition
                defFont = ChangeFontSize(defFont, size, GraphicsUnit.Point);
                Settings.Default.DictDefFont = defFont;
                DefGridView.Columns["Definition"].DefaultCellStyle.Font = defFont;

                //if (InputLanguage.CurrentInputLanguage.Culture.Name == "en-US") // not tibetan, change it
                //{
                //    txtDicLookUp.Font = engFont;// ChangeFontSize(txtDicLookUp.Font, fnt, GraphicsUnit.Pixel);
                //}
                //if (!String.IsNullOrEmpty(txtDicLookUp.Text))  // if it's not tibetan, then change it
                //{
                //    if (!IsTibetan(txtDicLookUp.Text))
                //    {
                //        txtDicLookUp.Font = engFont;

                //    }
                //}
            }
            catch { }

        }
        // input
        private void FontSizeInput_ValueChanged(object sender, EventArgs e)
        {
            if (blnFirstTimeThrough) { return; }
            float size = (float)((NumericUpDown)sender).Value;
            if (size < 1.0f) { size = 1.0f; }
            try
            {
                // definitely change the english definition
                inputFont = ChangeFontSize(inputFont, size, GraphicsUnit.Point);
                Settings.Default.DictInputFont = inputFont;
                txtDicLookUp.Font = inputFont; 
                //if (InputLanguage.CurrentInputLanguage.Culture.Name == "en-US") // not tibetan, change it
                //{
                //    txtDicLookUp.Font = engFont;// ChangeFontSize(txtDicLookUp.Font, fnt, GraphicsUnit.Pixel);
                //}
                //if (!String.IsNullOrEmpty(txtDicLookUp.Text))  // if it's not tibetan, then change it
                //{
                //    if (!IsTibetan(txtDicLookUp.Text))
                //    {
                //        txtDicLookUp.Font = engFont;
                        
                //    }
                //}
             }
             catch { }

        }
     
        private void cbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = lang[cbLang.SelectedIndex];
          
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
            SaveColumnWidths();
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
                            ErrorLog.WriteLog(DateTime.Now + "Error: frmDagYig, btnInsertNewDef_Click " + ex.Message);
                            msg += "Error: " + dt1.Rows[i]["DictID"].ToString();
                        }
                    }
                }
                if (msg != "")
                {
                    lblMsg.Text = "Update Failed!";
                    lblMsg.Visible = true;
                    MessageBox.Show(msg);
                }
                else
                {
                    lblMsg.Text = "Definition Updated.";
                    lblMsg.Visible = true;
                    CallDefDB(newword);
                }

            }
        }
#endregion
    
        #region DataGrid
        private void SaveColumnWidths()
        {
            if (DefGridView.Columns["Tibetan"] == null || DefGridView.Columns["Wylie"] == null || DefGridView.Columns["Definition"] == null) { return;}
         
            defColW = DefGridView.Columns["Definition"].Width;
            tibColW = DefGridView.Columns["Tibetan"].Width;
            wylieColW = DefGridView.Columns["Wylie"].Width;
            gridHeight = DefGridView.Rows[0].Height;
            if (tibColW < 5 || tibColW > 2800) { tibColW = 50; }
            if (defColW < 5 || defColW > 2800) { defColW = 50; }
            if (wylieColW < 5 || wylieColW > 2800) { wylieColW = 50; }
            if (gridHeight <5 || gridHeight > 500) {gridHeight = 50;}
            Settings.Default.GridRowHeight = gridHeight;
            Settings.Default.TibColSizeW = tibColW;
            Settings.Default.DefColSizeW = defColW;
            Settings.Default.WylieColSizeW = wylieColW;
           // Settings.Default.Save();
        }
        private void defGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //this.DefGridView.Columns["Tibetan"].DefaultCellStyle.Font =
            //        ChangeFontSize(DefGridView.DefaultCellStyle.Font, 25, GraphicsUnit.Pixel);
            this.DefGridView.Columns["Tibetan"].Width = tibColW;
            this.DefGridView.Columns["Definition"].Width = defColW;
            this.DefGridView.Columns["Wylie"].Width = wylieColW;
           
            this.DefGridView.Columns["DictID"].Visible = false;
            this.DefGridView.Columns["DictID"].ReadOnly = true;
             //this.DefGridView.Columns["Reference"].Visible = false;
             //this.DefGridView.Columns["Reference"].ReadOnly = true;
             this.DefGridView.Columns["Comment"].Visible = false;
             this.DefGridView.Columns["Comment"].ReadOnly = true;
             //this.DefGridView.Columns["Uni"].Visible = false;
             //this.DefGridView.Columns["Uni"].ReadOnly = true;
            this.DefGridView.Columns["Definition"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            Padding newPadding = new Padding(0, 0, 0,5);
            this.DefGridView.Columns["Tibetan"].DefaultCellStyle.Padding = newPadding;
            //Font tibetanFont = new Font(cbFonts.SelectedText, (float)numericUpDown1.Value, GraphicsUnit.Pixel);
            this.DefGridView.Columns["Tibetan"].DefaultCellStyle.Font = tibFont;
            //Font definitionFont = new Font(cbFonts.SelectedText, (float)numericUpDown2.Value, GraphicsUnit.Pixel);
            this.DefGridView.Columns["Definition"].DefaultCellStyle.Font = defFont; // engFont;

           // ChangeRowHeight();
            ////  DefGridView.Rows[DefGridView.Rows.Count - 1].Cells["Word"].ReadOnly = false;
            //DataGridViewRow row = this.DefGridView.RowTemplate;
            //row.Height = 80;
            blnGridLoading = false;
        }

        #endregion
        public void ChangeRowHeight()
        {
            this.DefGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            if (this.DefGridView.Rows[0].Height != gridHeight)
            {
             //   this.DefGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                int numRows = this.DefGridView.Rows.Count;
                for (int i = 0; i < numRows; i++)
                {
                    this.DefGridView.Rows[i].Height = gridHeight;

                }
            }
        }
        // if scanner passed in a value to look up
        public void LookUpOnOpen(string lookup)
        {
            blnSearchEnglishDefinition = false;
            txtDicLookUp.Text = lookup;
            SearchSpecific(lookup);
        }

       
        private void SearchSpecific(string val)
        {

            if (ds.HasChanges())
            {
                if (DialogResult.Yes != MessageBox.Show(

               "If you search now, you will lose your changes. Continue to Search?",

               "Continue to Search?",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question,

                MessageBoxDefaultButton.Button2))
                {
                    return;

                }
            }

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

            if (IsTibetan(val))   {  cleanTib(val);     }
            else  {  cleanLatin(val);  }
           
            
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
            catch (Exception eee) { ErrorLog.WriteLog("Error: FrmDagYig, SearchSpecific " + eee.Message); }

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
            SqlDataAdapter dataAdapter = null;
                   
            try
            {

                con = new SqlConnection(sqlconn);
                if (blnSearchEnglishDefinition)
                {
                    if (word.Length > 300)
                    {
                        word = word.Substring(0, 300);
                    }
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
                        // limit of 300 chars in db field for the wylie. (largest phrase we have is 250)
                        // but allowing a 500 char limit in proc just in case user sends a huge phrase
                        // of that 500 char, we take first 8 words.
                        if (word.Length > 500)
                        {
                            word = word.Substring(0, 500);
                        }
                        cmd = new SqlCommand("LookUpDef", con);
                    }
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("List", word));

                con.Open();

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter(cmd);

                // Populate a new data table and bind it to the BindingSource.
                dataAdapter.Fill(ds);
            }
            catch (Exception e)
            {
                ErrorLog.WriteLog("Error: FrmDagYig, CallDefDB " + e.Message); 

            }
            finally { if (con != null) { con.Close(); } }
            ds.Tables[0].DefaultView.Sort = String.Empty;
            return ds.Tables[0];

        }

        private string UpdateDR(DataRow dr)
        {
            string msg = "";
            StringBuilder sb = new StringBuilder();
            SqlConnection con = null;
            SqlCommand cmd = null;
            // make sure values are valid
            string def = dr["Definition"].ToString().Trim();
            string wyl = dr["Wylie"].ToString().Trim();
            string tib = dr["Tibetan"].ToString().Trim();
            if (string.IsNullOrEmpty(wyl))
            {
                wyl = "";
                sb.Append("Wylie cannot be empty.\r\n");
            }
            else
            {
                sb.Append(CheckValidWylie(wyl));
            }
            if (string.IsNullOrEmpty(tib))
            {
                tib = "";
                sb.Append("Tibetan cannot be empty.\r\n");
            }
            else
            {
                sb.Append(CheckValidTibetan(tib));
           
            }
            if (string.IsNullOrEmpty(def))
            {
                def = "";
                sb.Append("Definition cannot be empty.\r\n");
            }
            else
            {
                sb.Append(CheckValidDef(def));
            }
            
            if (sb.Length > 0)
            {
                sb.Insert(0, "Problem with Entry: " + tib + " " + wyl + " " + def + " \r\n");
                return sb.ToString();
            }
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
            catch (Exception ee) { msg = "An Error Occurred. ";
                ErrorLog.WriteLog("Error: FrmDagYig, UpdateDR " + ee.Message);
            }  
            finally { if (con != null) { con.Close(); } }
           
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
            catch (Exception ex) { ErrorLog.WriteLog("Error: FrmDagYig, backgroundWorker1_DoWork " + ex.Message); }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result == null)
                {
                    ErrorLog.WriteLog("Error: FrmDagYig, backgroundWorker1_RunWorkerCompleted, e.Result was null");
                    lblMsg.Text = "Error Processing Search";
                    return;
                }
                DataTable dt = (DataTable)e.Result;

                try
                {
                    DefGridView.DataSource = ds.Tables[0]; // bindingSource1;
                }
                catch (Exception eee)
                {
                    ErrorLog.WriteLog("Error: FrmDagYig, backgroundWorker1_RunWorkerCompleted " + eee.Message);
                }
               
                
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
            //string test = txtDicLookUp.Text;
            //if (String.IsNullOrEmpty(test)) { return; }
            //char c = test[0];
            //if (IsTibetan(c)) { txtDicLookUp.Font = Settings.Default.TibFont; }
            //else { txtDicLookUp.Font = Settings.Default.EngFont; }
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

        private void frmDagYig_Closing(object sender, FormClosingEventArgs e)
        {
            if (ds.HasChanges())
            {
                if (DialogResult.Yes != MessageBox.Show(

               "You have not yet saved your changes. Close the application anyway?",

               "Close Application?",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question,

                MessageBoxDefaultButton.Button2))
                {

                    e.Cancel = true;
                    return;

                }
            }

            SavePrefs();
            // clean up everything and anything. overkill I'm sure.
            try
            {
                if (lang != null) { lang = null; }
                if (ds != null) { ds = null; }
                if (tibFont != null) { tibFont = null; }
                if (inputFont != null) { inputFont = null; }
                if (defFont != null) { defFont = null; }
            }
            catch { }

            
         //   MyParentForm.RegisterClipBoardViewer();
        }

        private void ContextMenuCopy_Click(object sender, EventArgs e)
        {
            txtDicLookUp.Copy();
        }

        private void ContextMenuPaste_Click(object sender, EventArgs e)
        {
            txtDicLookUp.Paste();
        }

      
        private void txtDicLookUp_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(txtDicLookUp, e.Location);
            }
        }

        private void DefGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ds.HasChanges())
            {
                if (DialogResult.Yes != MessageBox.Show(

               "If you sort now, you will lose your changes. Sort the columns?",

               "Sort the columns?",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question,

                MessageBoxDefaultButton.Button2))
                {
                    return;

                }
            }

        }

        private void DefGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //if (blnGridLoading) { return; }
            //if (DefGridView.Columns["Tibetan"] == null || DefGridView.Columns["Wylie"] == null || DefGridView.Columns["Definition"] == null) { return;}
            //defColW = DefGridView.Columns["Definition"].Width;
            //tibColW = DefGridView.Columns["Tibetan"].Width;
            //wylieColW = DefGridView.Columns["Wylie"].Width;

            //if (tibColW < 5 || tibColW > 1800) { tibColW = 50; }
            //if (defColW < 5 || defColW > 1800) { defColW = 50; }
            //if (wylieColW < 5 || wylieColW > 1800) { wylieColW = 50; }

            //// Settings.Default.GridRowHeight = this.DefGridView.RowTemplate.Height;
            //Settings.Default.TibColSizeW = tibColW;
            //Settings.Default.DefColSizeW = defColW;
            //Settings.Default.WylieColSizeW = wylieColW;
            //Settings.Default.Save();

        }

        private void DefGridView_RowDividerHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            string x = "";
        }

        private void DefGridView_SizeChanged(object sender, EventArgs e)
        {
            string x = "";
        }

        private void DefGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
           
          //  this.DefGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            int numRows = this.DefGridView.Rows.Count;
            for (int i = 0; i < numRows; i++)
            {
                this.DefGridView.Rows[i].Height = gridHeight;

            }
        }

        private void DefGridView_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Height > 5)
            {
                gridHeight = e.Row.Height;
                Settings.Default.GridRowHeight =gridHeight;
            }
        }

     
      
       
      
    }

       
 }

