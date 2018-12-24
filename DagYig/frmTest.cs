using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace DagYig
{
    public partial class frmTest : Form
    {
        GlobalKeyboardHook gkh = new GlobalKeyboardHook();
        private string keylist = "";
        bool IsControlDown = false;
        bool IsShiftDown = false;
        bool IsSpecialKeyDown = false;
        bool IsMenuKeyDown = false;
        string specialKey = "A";

        public frmTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gkh.Hook();
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);

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
            label1.Text = keylist;
            if (IsControlDown && IsMenuKeyDown && IsSpecialKeyDown)
            {
                label1.Text = k + " handled";
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
           
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            keylist = "Down\t" + e.KeyCode.ToString();
            label1.Text = keylist;
            string k = e.KeyCode.ToString();
            switch (e.KeyCode.ToString()) {
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
                label1.Text = k + " handled";
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gkh.Unhook();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
           sb.Append("<html><body><h1>Table of Contents</h1><p style=\"text-indent:0pt\">");
            for (int i = 1; i < 623; i++)
            {
                sb.Append("<a href=\"page_");
                    sb.Append(i.ToString());
                    sb.Append(".html\">");
                sb.Append(i.ToString());
                sb.AppendLine("</a><br/>");

            }

       sb.Append("</p></body></html>");

       // create a writer and open the file
       TextWriter tw = new StreamWriter("date.txt");

       // write a line of text to the file
       tw.WriteLine(sb.ToString());

       // close the stream
       tw.Close();
        }

    }
}
