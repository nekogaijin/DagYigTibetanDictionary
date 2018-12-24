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
    public partial class frmClipBoard : Form
    {
        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern IntPtr GetClipboardData(uint uFormat);

       
        IntPtr nextClipboardViewer;
        const uint CF_UNICODETEXT = 13;

        private int qInt;
        private string  strClipboardText = "";
        private string PriorLookup = "";
        private string TibValue = "";
        private string WylieValue = "";
        private  string TransValue = "";
        private Queue _lookup = new Queue();
       // private List<WordDef> wordList = new List<WordDef>();
        private WordDefCollection wdColl = new WordDefCollection();
        bool blnFirstTimeThrough = true;
        #region capturekey
        GlobalKeyboardHook gkh = new GlobalKeyboardHook();
        private string keylist = "";
        bool IsControlDown = false;
        bool IsShiftDown = false;
        bool IsSpecialKeyDown = false;
        bool IsMenuKeyDown = false;
        string specialKey = "X";
        bool blnHookKey = true;
        #endregion

        /// <summary>
        /// Remove this form from the Clipboard Viewer list
        /// </summary>
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


        private frmClipBoardPref cbp;

        private float tibFontSize;
        private Font tibFont;

        public Font TibFont
        {
            get { return tibFont; }
            set { tibFont = value; }
        }
        public float TibFontSize
        {
            get { return tibFontSize; }
            set { tibFontSize = value; }
        }
       
        public frmClipBoard()
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
            if (k == specialKey)
            {
                IsSpecialKeyDown = false;
            }
            
            //if (IsControlDown && IsMenuKeyDown && IsSpecialKeyDown)
            //{
            //    ShowPrior();
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Handled = false;
            //}
            //if (IsControlDown && IsMenuKeyDown && k == "B")
            //{
            //    gkh.Unhook();
            //}
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
            if (k == specialKey)
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
        private void ShowPrior()
        {
            WordDef wd = (WordDef)wdColl.GetPriorElement();
            StringBuilder strBalloon = new StringBuilder();
            string t = wd.Tibetan;
            strBalloon.Append(wd.Wylie);
            strBalloon.Append(" - ");
            strBalloon.AppendLine(wd.Trans);
            wd = null;
            //FormatMenuBuild(iData);
            //SupportedMenuBuild(iData);
            //ContextMenuBuild();

            //if (_lookup.Count > 0)
            //{
            txtTibetan.Text = t;
            txtTrans.Text = strBalloon.ToString();
        }
        private void btnPreferences_OnClick(object sender, EventArgs e)
        {
            Form fcbp = new frmClipBoardPref();
            fcbp.Show();
            LoadPrefs();

        }
        private void LoadPrefs()
        {
          
          
            if (Settings.Default.EngFont != null)
            {
                txtTibetan.Font = Settings.Default.EngFont;
            }

            if (Settings.Default.EngFontSize != null)
            {
                float fontsize = Settings.Default.EngFontSize;
                Font font = new Font(txtTibetan.Font.FontFamily, fontsize,
                        txtTibetan.Font.Style, GraphicsUnit.Pixel,
                        txtTibetan.Font.GdiCharSet, txtTibetan.Font.GdiVerticalFont);

            }
            // Set window size

            if (Settings.Default.ClipBoardWindowSize != null)
            {
                this.Size = Settings.Default.ClipBoardWindowSize;
            }
        }
        private void SavePrefs()
        {

            Settings.Default.EngFont = txtTrans.Font;
            Settings.Default.EngFontSize = txtTrans.Font.Size;
            Settings.Default.TibFont = txtTibetan.Font;
            Settings.Default.TibFontSize = txtTibetan.Font.Size;
            // Copy window size to app settings

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
        private void frmClipBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Copy window location to app settings
            SavePrefs();
            if (blnHookKey)
            {
                UnregisterClipboardViewer();
            }
        }

              
        // preferences   

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbp == null)
            {
                cbp = new frmClipBoardPref();
            }
            cbp.MyParentForm = this;
            cbp.Show();
        }
        public bool BlnHookKey {
            get { return blnHookKey; }
            set { blnHookKey = value; }
        }
        // called from preferences diag form
        public void ChangeFont(Font tf, float tfs, Font ef, float efs)
        {
            Font tfont = new Font(tf.FontFamily, tfs,
                              txtTibetan.Font.Style, GraphicsUnit.Point,
                              txtTibetan.Font.GdiCharSet, txtTibetan.Font.GdiVerticalFont);
            txtTibetan.Font = tfont;
            Font efont = new Font(ef.FontFamily, efs,
                              txtTrans.Font.Style, GraphicsUnit.Point,
                              txtTrans.Font.GdiCharSet, txtTrans.Font.GdiVerticalFont);
            txtTrans.Font = efont;
            cbp = null;
            SavePrefs();
        }
       
        private unsafe static string ConvertToString(IntPtr src)
        {
            int x;
            char* pSrc = (char*)src.ToPointer();
            StringBuilder sb = new StringBuilder();

            for (x = 0; pSrc[x] != '\0'; x++)
            {
                sb.Append(pSrc[x]);
            }
            return sb.ToString();
        }

        private void GetClipboardData()
        {
            if (blnFirstTimeThrough)
            {
                blnFirstTimeThrough = false;
                return;
            }
            //
            // Data on the clipboard uses the 
            // IDataObject interface
            //
            strClipboardText = "";
            string sText;
            IDataObject iData = new DataObject();
            string strText = "clipmon";
            IntPtr hData;

            try
            {
              
                iData = Clipboard.GetDataObject();
               
                //if ( ((string)iData.GetData(DataFormats.Text)).StartsWith("?")  )
                //{
                //    if (iData.GetDataPresent(DataFormats.UnicodeText))
                //    {
                //        //ctlClipboardText.Rtf = (string)iData.GetData(DataFormats.Rtf);
                //        string ss = (string)iData.GetData(DataFormats.UnicodeText);

                //    }


                //    //hData = GetClipboardData(CF_UNICODETEXT);
                //    //if (hData.ToInt32() == 0)
                //    //{
                //    //    return;
                //    //}

                //    //sText = ConvertToString(hData);
                //}

                //Clipboard.GetDataObject();
            }
            catch (System.Runtime.InteropServices.ExternalException externEx)
            {
                // Copying a field definition in Access 2002 causes this sometimes?
                Debug.WriteLine("InteropServices.ExternalException: {0}", externEx.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            // 
            // Get RTF if it is present
            //
            if (iData.GetDataPresent(DataFormats.Text))
            {
                //ctlClipboardText.Rtf = (string)iData.GetData(DataFormats.Rtf);

                //if(iData.GetDataPresent(DataFormats.Text))
                //{
                //    strText = "RTF";
                //}
            }
            else
            {
                // 
                // Get Text if it is present
                //
                if (iData.GetDataPresent(DataFormats.UnicodeText))
                {
                    //ctlClipboardText.Text = (string)iData.GetData(DataFormats.Text);

                    //strText = "Text"; 

                    //Debug.WriteLine((string)iData.GetData(DataFormats.Text));
                }
                else
                {
                    //
                    // Only show RTF or TEXT
                    //
                   txtTrans.Text = "(cannot display this format)";
                    return;
                }
            }

         //   notAreaIcon.Tooltip = strText;

            if (ClipboardSearch(iData))
            {
                //
                // New word go look up.
                //
                System.Text.StringBuilder strBalloon = new System.Text.StringBuilder(100);

                //foreach (string objLink in _lookup)
                //{
                //    strBalloon.Append(objLink.ToString()  + "\n");
                //}

                //    WordDef wd = (WordDef)_lookup.Dequeue();

                WordDef wd = (WordDef)wdColl.Current;

                string t = wd.Tibetan;
                strBalloon.Append(wd.Wylie);
                strBalloon.Append(" - ");
                strBalloon.AppendLine(wd.Trans);
                wd = null;
                //FormatMenuBuild(iData);
                //SupportedMenuBuild(iData);
                //ContextMenuBuild();

                //if (_lookup.Count > 0)
                //{
                txtTibetan.Text = t;
                txtTrans.Text = strBalloon.ToString();
                //   notAreaIcon.BalloonDisplay(NotificationAreaIcon.NOTIFYICONdwInfoFlags.NIIF_INFO, "Links", strBalloon.ToString());
                //}
            }
            else
            {
                txtTibetan.Text =  strClipboardText ;
                txtTrans.Text = "Not Found";
            }
        }

        private bool ClipboardSearch(IDataObject iData)
        {

            //bool FoundNewLinks = false;
            bool FoundNewWord = false;
            //
            // If it is not text then quit
            // cannot search bitmap etc
            //

            
            byte[] unicodeData = null;


            


                try
                {
                    strClipboardText = (string)iData.GetData(DataFormats.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                if (strClipboardText.StartsWith("?"))
                {
                    try
                    {
                        //unicodeData = iData.GetData(DataFormats.UnicodeText).;
                        strClipboardText = iData.GetData(DataFormats.UnicodeText).ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return false;
                    }

                }
            

            //if (String.IsNullOrEmpty(PriorLookup))
            //{
            //    PriorLookup = strClipboardText;
                
            //}

            if (!PriorLookup.Equals(strClipboardText))
            {

                try
                {
                    WordDef wd = GetTranslation(strClipboardText);
                    if (wd != null)
                    {
                      //  _lookup.Enqueue(wd);
                        wdColl.Add(wd);
                        wd = null;
                        FoundNewWord = true;
                    }

                    PriorLookup = strClipboardText;

                }
                catch (Exception e) { }
            }

            //if (!_lookup.Contains(strClipboardText))
            //{
            //    _lookup.Enqueue(strClipboardText);
            //    FoundNewWord = true;
            //}
            // Hyperlinks e.g. http://www.server.com/folder/file.aspx
            //Regex rxURL = new Regex(@"(\b(?:http|https|ftp|file)://[^\s]+)", RegexOptions.IgnoreCase);
            //rxURL.Match(strClipboardText);

            //foreach (Match rm in rxURL.Matches(strClipboardText))
            //{
            //    if(!_hyperlink.Contains(rm.ToString()))
            //    {
            //        _hyperlink.Enqueue(rm.ToString());
            //        FoundNewLinks = true;
            //    }
            //}

            // Files and folders - \\servername\foldername\
            // TODO needs work
            //Regex rxFile = new Regex(@"(\b\w:\\[^ ]*)", RegexOptions.IgnoreCase);
            //rxFile.Match(strClipboardText);

            //foreach (Match rm in rxFile.Matches(strClipboardText))
            //{
            //    if(!_hyperlink.Contains(rm.ToString()))
            //    {
            //        _hyperlink.Enqueue(rm.ToString());
            //        FoundNewLinks = true;
            //    }
            //}

            // UNC Files 
            // TODO needs work
            //Regex rxUNC = new Regex(@"(\\\\[^\s/:\*\?\" + "\"" + @"\<\>\|]+)", RegexOptions.IgnoreCase);
            //rxUNC.Match(strClipboardText);

            //foreach (Match rm in rxUNC.Matches(strClipboardText))
            //{
            //    if(!_hyperlink.Contains(rm.ToString()))
            //    {
            //        _hyperlink.Enqueue(rm.ToString());
            //        FoundNewLinks = true;
            //    }
            //}

            // UNC folders
            // TODO needs work
            //Regex rxUNCFolder = new Regex(@"(\\\\[^\s/:\*\?\" + "\"" + @"\<\>\|]+\\)", RegexOptions.IgnoreCase);
            //rxUNCFolder.Match(strClipboardText);

            //foreach (Match rm in rxUNCFolder.Matches(strClipboardText))
            //{
            //    if(!_hyperlink.Contains(rm.ToString()))
            //    {
            //        _hyperlink.Enqueue(rm.ToString());
            //        FoundNewLinks = true;
            //    }
            //}

            // Email Addresses
            //Regex rxEmailAddress = new Regex(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)", RegexOptions.IgnoreCase);
            //rxEmailAddress.Match(strClipboardText);

            //foreach (Match rm in rxEmailAddress.Matches(strClipboardText))
            //{
            //    if(!_hyperlink.Contains(rm.ToString()))
            //    {
            //        _hyperlink.Enqueue("mailto:" + rm.ToString());
            //        FoundNewLinks = true;
            //    }
            //}

            return FoundNewWord;
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
            //string pattern = @"[^a-zA-Z0-9']";
            //return Regex.Replace(word, pattern, "", RegexOptions.Multiline);
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
            string pattern = @"[a-zA-Z0-9']";
            //		(int)'་'	3851	int
            int l = word.Length-1;
            int m = (int)word[l];
            if (m != 3851)
            {
                word += "་";
            }
           // int w = (int)w[word.Length - 1];
           //if(w != "་") {
           //     word += "་";
           // }
            return Regex.Replace(word, pattern, "", RegexOptions.Multiline);

        }



        private WordDef GetTranslation(string word)
        {


            if (IsTibetan(word))
            {
                // search using tib
                word = cleanTib(word);
            }
            else
            {
                // search using wylie
                word = cleanLatin(word);

            }
            try
            {
                // backgroundWorker1.RunWorkerAsync(word);//this invokes the DoWork event
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
            bool blnTibetan = true;
            char[] a = word.ToCharArray();
            int ai = (int)a[0];

            if (ai < 170)
            {
                blnTibetan = false;
            }


            SqlConnection con = null;
            SqlCommand cmd = null;

            string sqlconn = "Server=.\\sqlexpress;Database=DagYig;Trusted_Connection=True;";


            string msg = "";
            string def = "";
            try
            {


                con = new SqlConnection(sqlconn);

                if (blnTibetan)
                {
                    cmd = new SqlCommand("LookUpDefUsingTib", con);
               
                }
                else
                {
                    cmd = new SqlCommand("LookUpDefUsingWylie", con);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("List", word));

                con.Open();
                SqlDataReader sr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            finally { if (con != null) { con.Close(); } }

            return blnFoundData;
        }

        private bool IsTibetan(string word)
        {
            char c = word[0];
            int ai = (int)c;
            //bool blnTibetan = true;
            if (ai > 3800 && ai < 4500)
            {
                return true;
            }
            return false;

        }


       

  
        private void button2_Click_1(object sender, EventArgs e)
        {
            Form dy = new frmDagYig();
            dy.Show();
        }
    }
}
