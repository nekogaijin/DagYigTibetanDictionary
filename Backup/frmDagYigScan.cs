/* File: frmDagYigScan.cs
 * Date: 11.10.2010
 * Desc: pick up from clipboard and look up
 * Auth: © Al Gallo 2009 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using DagYig.Properties;

namespace DagYig
{
    public partial class frmDagYigScan : Form
    {
      
        #region ClipBoard
        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern IntPtr GetClipboardData(uint uFormat);
       
        IntPtr nextClipboardViewer;
        #endregion
       
 
        const uint CF_UNICODETEXT = 13;

        private Font tibFont;
        private Font engFont;
        private string clipBoardText = ""; 
        private string PriorLookup = "";
        private string TibValue = "";
        private string WylieValue = "";
        private string TransValue = "";
       
        private WordDefCollection wdColl = new WordDefCollection();
        bool blnFirstTimeThrough = true;
    
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
        public Font TibFont
        {
            get { return tibFont; }
            set { tibFont = value; }
        }
       
        public Font EngFont
        {
            get { return EngFont; }
        }

        public void ChangeFont(Font tf, Font ef)
        {
            txtTibetan.Font = tf;
            txtTrans.Font = ef;
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
            engFont = Settings.Default.EngFont;
            tibFont = Settings.Default.TibFont;
            blnHookKey = Settings.Default.UseHotKey == "Y" ? true : false;
            specialKey = Settings.Default.LoopHotKey;
            if (String.IsNullOrEmpty(specialKey)) { blnHookKey = false; }
            txtTibetan.Font = tibFont;
            txtTrans.Font = engFont;

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
            txtTibetan.Text = t;
            txtTrans.Text = strData.ToString();
        }
       
   
      
        private void GetClipboardData()
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
                // Copying a field definition in Access 2002 causes this sometimes?
                Debug.WriteLine("InteropServices.ExternalException: {0}", externEx.Message);
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
             //   txtTrans.Text = "(cannot compute cannot compute)";
                return;
            }
           
            // check if it's a brand new word (may already be in word collection)
            // and if it is, retrieve from DB
            if (ClipboardSearch(iData))
            {
                System.Text.StringBuilder strData = new System.Text.StringBuilder();
                WordDef wd = (WordDef)wdColl.Current;

                string t = wd.Tibetan;
                strData.Append(wd.Wylie);
                strData.Append(" - ");
                strData.AppendLine(wd.Trans);
                wd = null;
                txtTibetan.Text = t;
                txtTrans.Text = strData.ToString();
            }
            else
            {
                txtTibetan.Text = clipBoardText; // global string. Filled in by ClipboardSearch() method
                txtTrans.Text = "Not Found";
            }
        }

        private bool ClipboardSearch(IDataObject iData)
        {
            bool FoundNewWord = false;
            clipBoardText = "";
            try
            {
                clipBoardText = (string)iData.GetData(DataFormats.Text);
                clipBoardText = clipBoardText.Trim();
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.ToString());
                 return false;
            }
            if (clipBoardText.StartsWith("?")) // unicode
            {
                try
                {
                    clipBoardText = iData.GetData(DataFormats.UnicodeText).ToString();
                    clipBoardText = clipBoardText.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }

            }

            if (!PriorLookup.Equals(clipBoardText)) // don't bother going to db if we've already captures this one
            {
                try
                {
                    WordDef wd = GetTranslation(clipBoardText);
                    if (wd != null)
                    {
                        wdColl.Add(wd);
                        wd = null;
                        FoundNewWord = true;
                    }

                    PriorLookup = clipBoardText;
                }
                catch (Exception e) { }
            }

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

            string msg = "";
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
                msg += "Error " + e.Message;

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
                    GetClipboardData();
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
                    break;
                }
             }
            if (!found) {
             frmDagYig   dict = new frmDagYig();
             dict.Show();
            }
        }

       
        private void frmDagYigScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePrefs();
            if (blnHookKey)
            {
                gkh.Unhook();
            }
            UnregisterClipboardViewer();
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

    }
}
