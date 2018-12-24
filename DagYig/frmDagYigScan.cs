/* File: frmDagYigScan.cs
 * Date: 11.10.2010
 * Desc: pick up from clipboard and look up
 * Auth: © Al Gallo 2009 */
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using DagYig.Properties;

namespace DagYig
{
    public partial class frmDagYigScan : Form
    {
        const uint CF_UNICODETEXT = 13;
        const uint CF_TEXT = 1;
        const uint CF_LOCALE = 16;

        #region ClipBoard
        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        protected static extern IntPtr GetClipboardData(uint uFormat);
      
        [DllImport("user32.dll")]
        protected static extern IntPtr SetClipboardData(uint uFormat);
    

        IntPtr nextClipboardViewer;
        #endregion
       
 
       
        private Font scanFont;
        private Font scanDefFont;
        private string clipBoardText = ""; 
        private string PriorLookup = "";
        private string TibValue = "";
        private string WylieValue = "";
        private string TransValue = "";
       
        private WordDefCollection wdColl = new WordDefCollection();
        bool blnFirstTimeThrough = true;
        bool blnScanStarted = true;
       
        #region capturekey
        GlobalKeyboardHook gkh = new GlobalKeyboardHook();
        private string keylist = "";
        bool IsControlDown = false;
        bool IsShiftDown = false;
        bool IsSpecialKeyDown = false;
        bool IsMenuKeyDown = false; // ALT KEY
        string specialKey = "X";
        bool blnHookKey = true; // does user want to use a key for looping through prior captures
        #endregion


        #region public preferences 
        public string SpecialKey
        {
            get { return specialKey; }
            set { specialKey = value; }
        }
        public Font ScanFont
        {
            get { return scanFont; }
            set { scanFont = value; }
        }
       
        public Font DefFont
        {
            get { return scanDefFont; }
            set { scanDefFont = value; }
        }

        public void ChangeFont(Font sFont, Font dFont)
        {
            txtScan.Font = sFont;
            txtScanDef.Font = dFont;
        }


        public bool BlnHookKey
        {
            get { return blnHookKey; }
            set { blnHookKey = value; }
        }
    
     
        public void SetBlnHookKey(bool YN)
        {
            if (YN)
            {
                if (!blnHookKey) //if it wasn't already registered, register it
                {
                    gkh.Hook();
                    gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
                    gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
                }
                blnHookKey = true;
            }
            else
            {
                if (blnHookKey) //if it was already registered, unregister it
                {
                    gkh.Unhook();
                }

                blnHookKey = false;
            }
        }

        public void RegisterClipBoardViewer()
        {
            nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
        }
        #endregion 

      
        #region OnLoad
        public frmDagYigScan()
        {
            InitializeComponent();
          
            nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
           
            this.TopMost = true;
        }
       
        private void frmClipBoard_Load(object sender, EventArgs e)
        {
            LoadPrefs();
            if (blnHookKey)
            {
                gkh.Hook();
                gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
                gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
            }
        }

        private void LoadPrefs()
        {
            scanFont = Settings.Default.ScanFont;
            scanDefFont = Settings.Default.ScanDefFont;
            blnHookKey = Settings.Default.UseHotKey == "Y" ? true : false;
            specialKey = Settings.Default.LoopHotKey;
            if (String.IsNullOrEmpty(specialKey)) { blnHookKey = false; }
            txtScan.Font =scanFont;
            txtScanDef.Font = scanDefFont;

            // Set window size

            if (Settings.Default.ClipBoardWindowSize != null)
            {
                this.Size = Settings.Default.ClipBoardWindowSize;
            }
        }

        #endregion

        #region keyUpDownevents
        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            keylist = "Up\t" + e.KeyCode.ToString();
            string k = e.KeyCode.ToString();
            switch (e.KeyCode.ToString())
            {
                case "LControlKey":
                case "RControlKey":
                    IsControlDown = false;
                    break;
                case "LShiftKey":
                case "RShiftKey":
                    IsShiftDown = false;
                    break;
                case "LMenu":
                case "RMenu":
                    IsMenuKeyDown = false;
                    break;

            }
            if (k == specialKey)  // 3rd key
            {
                IsSpecialKeyDown = false;
            }
       
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            keylist = "Down\t" + e.KeyCode.ToString();
           
            string k = e.KeyCode.ToString();
            switch (e.KeyCode.ToString())
            {
                case "LControlKey":
                case "RControlKey":
                    IsControlDown = true;
                    break;
                case "LShiftKey":
                case "RShiftKey":
                    IsShiftDown = true;
                    break;
                case "LMenu":
                case "RMenu":
                    IsMenuKeyDown = true;
                    break;

            }
            if (k == specialKey) //3rd key - last key will always be only one to register down
            {
                IsSpecialKeyDown = true;
            }
            if (IsControlDown && IsMenuKeyDown && IsSpecialKeyDown)
            {
                ShowPrior();
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        #endregion
   
     
        // loop through and bring up prior search. Called on hotkey CTRL + ALT + special key
        private void ShowPrior()
        {
            WordDef wd = (WordDef)wdColl.GetPriorElement();
            StringBuilder strData = new StringBuilder();
            string t = wd.Tibetan;
            strData.Append(wd.Wylie);
            strData.Append(" - ");
            strData.AppendLine(wd.Trans);
            wd = null;
            txtScan.Text = t;
            txtScanDef.Text = strData.ToString();
            strData = null;
        }
       
   
      
        private void GetClipboardFromData()
        {
            if (blnFirstTimeThrough)  // we don't want to throw clipboard data in there from somewhere else
            {
                blnFirstTimeThrough = false;
                return;
            }
            
            IDataObject iData = new DataObject();
          
            try {    iData = Clipboard.GetDataObject();   }
            catch (System.Runtime.InteropServices.ExternalException externEx)
            {
               // ErrorLog.WriteLog(DateTime.Now + "Error frmDagYigScan, GetClipboardData, System.Runtime.InteropServices.ExternalException ERR: " + externEx.Message);
                MessageBox.Show("There was a problem capturing this data: " + externEx.ToString());
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem capturing this data: " + ex.ToString());
                return;
            }
            
            if (!iData.GetDataPresent(DataFormats.Text) && !iData.GetDataPresent(DataFormats.Rtf)
                && !iData.GetDataPresent(DataFormats.UnicodeText))
            {  
                  return;
            }
           
            // check if found and if it's a brand new word (may already be in word collection)
            // and if it is new, retrieve from DB
         //04/27/2012 What if clipboard picks up blank string randomly from somewhere else - why, where? not sure. 
            // we end up replacing whatever was put in the txtScan with "not found" 
            if(string.IsNullOrEmpty(GetClipBoardText(iData))) {    return;      }
            if (PriorLookup.Equals(clipBoardText)) { return; }

         //   if (ClipboardSearch(iData))
            if (RetrievedDefinition())
            {
                System.Text.StringBuilder strData = new System.Text.StringBuilder();
                WordDef wd = (WordDef)wdColl.Current;
                string t = wd.Tibetan;
                strData.Append(wd.Wylie);
                strData.Append(" - ");
                strData.AppendLine(wd.Trans);
                wd = null;
                txtScan.Text = t;
                txtScanDef.Text = strData.ToString();
                strData = null;
            }
            else
            {
               
                txtScan.Text = clipBoardText; // global string. Filled in by ClipboardSearch() method
                txtScanDef.Text = "Not Found";
                  
               
            }
        }

        // this will fill global with value of clipboard 
        private string GetClipBoardText(IDataObject iData)
        {
            clipBoardText = "";
            try
            {
                clipBoardText = (string)iData.GetData(DataFormats.UnicodeText, true);
                clipBoardText = clipBoardText.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem retrieving data " + ex.ToString());
                return "";
            }
            //if (!clipBoardText.StartsWith("?")) // unicode
            //{
            //    try
            //    {
            //        clipBoardText = iData.GetData(DataFormats.UnicodeText).ToString();
            //        clipBoardText = clipBoardText.Trim();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("There was a problem retrieving data " + ex.ToString());
            //        return "";
            //    }

            //}
            //else
            //{
            //    try
            //    {
            //        clipBoardText = (string)iData.GetData(DataFormats.Text, true);
            //        clipBoardText = clipBoardText.Trim();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("There was a problem retrieving data " + ex.ToString());
            //        return "";
            //    }
            //}

            return clipBoardText;
        }

        private bool RetrievedDefinition()
        {
            // bool FoundNewWord = false;
           bool FoundNewWord = false;  //04/27/2012 have to fix this.. what if it's the same word as prior, so we don't go to dict.

           try
           {
               WordDef wd = GetTranslation(clipBoardText);
               if (wd != null)
               {
                   wdColl.Add(wd);
                   wd = null;
                   FoundNewWord = true;
               }
               else  //04/27/2012
               {
                   FoundNewWord = false;
               }
               PriorLookup = clipBoardText;
           }
           catch (Exception e) { }
             
            return FoundNewWord;
        }

        private WordDef GetTranslation(string word)
        {

            try
            {
                if (CallDefDB(word))
                {
                    return new WordDef(WylieValue, TibValue, TransValue);
                }
            }
            catch { }
            return null;
        }

        private bool CallDefDB(string word)
        {
            TibValue = "";
            WylieValue = "";
            TransValue = "";
            bool blnFoundData = false;
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader sr = null;
            string sqlconn = "Server=.\\sqlexpress;Database=DagYig;Trusted_Connection=True;";
            try { sqlconn = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]; }
            catch (Exception connexc)
            {
                ErrorLog.WriteLog("Error - frmDagYig, InitializeFormItems, error pulling sqlconnection from config " + connexc.Message);
                ErrorLog.WriteLog("Using hard coded sql string and continuing.");
            }
        
            try
            {
                con = new SqlConnection(sqlconn);

                if (IsTibetan(word))
                {
                    word = cleanTib(word);
                    cmd = new SqlCommand("LookUpDefUsingTib", con);
               
                }
                else
                {
                    word = cleanLatin(word);
                    cmd = new SqlCommand("LookUpDefUsingWylie", con);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("List", word));

                con.Open();
                sr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (sr.Read())
                {
                    TibValue = sr["Tibetan"].ToString();
                    WylieValue = sr["Wylie"].ToString();
                    TransValue = sr["Definition"].ToString();
                    blnFoundData = true;
                }
                if (String.IsNullOrEmpty(TibValue))
                {
                    TibValue = "Not Found";
                }
               
            }
            catch (Exception e)
            {
                ErrorLog.WriteLog(DateTime.Now + " Error: frmDagYigScan CallDefDB " + e.Message);

            }
            finally
            {
                try
                {
                    if (sr != null) { sr.Close(); }
                    if (cmd != null) { cmd = null; }
                    if (con != null) { con.Close(); } 
                }
                catch { }
               }

            return blnFoundData;
        }

       

        #region helpers
        #region clipboard
        private void UnregisterClipboardViewer()
        {
            ChangeClipboardChain(this.Handle, nextClipboardViewer);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    GetClipboardFromData();
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == nextClipboardViewer)
                        nextClipboardViewer = m.LParam;
                    else
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion
        private bool IsTibetan(string word)
        {
            char c = word[0];
            int ai = (int)c;
            if (ai > 3800 && ai < 4500)  {  return true;  }
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
            word = word.Replace("‘", "'");
            word = word.Replace(",", "");
            word = word.Replace("{", "");
            word = word.Replace("}", "");
            word = word.Replace("(", "");
            word = word.Replace(")", "");
            word = word.Replace("\r", "");
            word = word.Replace("\n", "");
            word = word.Replace("།", "་");
            //	(int)'་'	3851	int
            int l = word.Length - 1;
            int m = (int)word[l];
            if (m != 3851)
            {
                word += "་";
            }
            return word;
        }
        #endregion
        #region forms

        private void btnPreferences_OnClick(object sender, EventArgs e)
        {
           
             bool found = false;
             foreach(Form f in Application.OpenForms)
             {
                if(f.GetType() == typeof(frmDagYigPref))
                {
                    found = true;
                    break;
                }
             }
             if (!found)
             {
                frmDagYigPref cbp   = new frmDagYigPref();
                 cbp.MyParentForm = this;
                 cbp.Show();
             }
        }
       

        private void btnDictionary_OnClick(object sender, EventArgs e)
        {

             bool found = false;
             foreach(Form f in Application.OpenForms)
             {
                if(f.GetType() == typeof(frmDagYig))
                {
                    found = true;
                    ((frmDagYig)f).BringToFront();
                    break;
                }
             }
            if (!found) {
             frmDagYig   dict = new frmDagYig("");
             dict.BringToFront();
            // UnregisterClipboardViewer();
             dict.MyParentForm = this;
             dict.Show();
            }
        }

        private void btnHelp_OnClick(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "http://TibetanLanguage.BlogSpot.com");

        }

        // look this up in dictionary 
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtScan.Text)) { btnDictionary_OnClick(sender, e); return; }
            bool found = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmDagYig))
                {
                    found = true;
                    ((frmDagYig)f).BringToFront();
                    ((frmDagYig)f).LookUpOnOpen(txtScan.Text);
                    break;
                }
            }
            if (!found)
            {
                frmDagYig dict = new frmDagYig(txtScan.Text);
                // UnregisterClipboardViewer();
                dict.MyParentForm = this;
                dict.Show();
                
            }

        }

        private void frmDagYigScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool found = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmDagYig))
                {
                    found = true;
                    break;
                }
            }
            if (found)
            {
                if (DialogResult.Yes != MessageBox.Show(

                "The Dictionary is still open. If you shut down this application, the Dictionary will also shut down. Close the application anyway?",

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
            if (blnHookKey)
            {
                gkh.Unhook();
            }
            if (blnScanStarted)
            {
                UnregisterClipboardViewer();
            }
        }

        private void SavePrefs()
        {

            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Default.ClipBoardWindowSize = this.Size;
            }
            else
            {
                Settings.Default.ClipBoardWindowSize = this.RestoreBounds.Size;
            }

            // Save settings

            Settings.Default.Save();
        }
        #endregion

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            if (blnScanStarted)
            {
                UnregisterClipboardViewer();
                blnScanStarted = false;
                ((ToolStripButton)sender).BackColor = Color.Red;
                ((ToolStripButton)sender).ToolTipText = "Start Scanning";
            }
            else
            {
                RegisterClipBoardViewer();
                blnScanStarted = true;
                ((ToolStripButton)sender).BackColor = Color.Green;
                ((ToolStripButton)sender).ToolTipText = "Sop Scanning";
            }
        }

       




    }
}
