using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace DagYig
{
    public partial class frmPhonetics : Form
    {
        string entry = "";
        public frmPhonetics()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                entry = textBox1.Text;
                Process();

            }
            else
            {
                label1.Text = "no entry";
                return;
            }

        }

        private void Process()
        {
            StringBuilder test = new StringBuilder();
            // divide entry up into syllables
           
            entry = entry.Replace("/", "");  // I'm just going to assume that this won't be multiple phrases. Deal with it another time.
            int l = entry.Length - 1;
            int m = (int)entry[l];
            // dump a final tsheg.
           // string[] arsyllables = entry.Split(' ');
           // ArrayList syllables = new ArrayList(entry.Split(' '));
            string[]  syllables = entry.Split(' ');
            string sib1 = "";
            string sib2 = "";
            string sib3 = "";

            //pa/ba/po/bo/mo med/ldan/bral/bya/can" not followed by pa/ba/po/bo.
            string reg1 = "/^(?:pa|ba|po|bo|mo)$/";
            string reg2 = "/^(?:med|ldan|bral|bya|can)$/";
            string reg3 = "/^[pb][ao](?:'i|s|'am|'ang|'o|r)?$/";

            for (int i=0; i < syllables.Length; i++)
            {
                if (syllables.Length > i + 1)
                {
                    sib1 = syllables[i];
                    sib2 = syllables[i+1];
                    if (syllables.Length > i +2) {
                        sib3 = syllables[i+2];
                    }
          
                    if (Regex.IsMatch(sib2,reg1) && (!string.IsNullOrEmpty(sib3) && Regex.IsMatch(sib3,reg2)) ) {
                        string yadda = "found";
                    }

                }
            }

            //foreach (string s in syllables)
            //{
            //    test.Append(s);
            //    test.Append(" and ");
                

            //}
            
            label1.Text = test.ToString();
               
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                entry = textBox1.Text;
               

            }
            else
            {
                label1.Text = "no entry";
                return;
            }

            entry = entry.Replace("/", "");  // I'm just going to assume that this won't be multiple phrases. Deal with it another time.
            //int l = entry.Length - 1;
            //int m = (int)entry[l];
            // dump a final tsheg.
            // string[] arsyllables = entry.Split(' ');
            // ArrayList syllables = new ArrayList(entry.Split(' '));
            string[] syllables = entry.Split(' ');
            string sib1 = "";
            string sib2 = "";
            string sib3 = "";

            //pa/ba/po/bo/mo med/ldan/bral/bya/can" not followed by pa/ba/po/bo.
          //  string reg1 = "/^(?:pa|ba|po|bo|mo)$/";
            //string reg2 = "/^(?:med|ldan|bral|bya|can)$/";
            //string reg3 = "/^[pb][ao](?:'i|s|'am|'ang|'o|r)?$/";
            string test = "";
            
            Regex  reg1 = new Regex("/^(pa|ba|po|bo|mo)/");
            Regex reg2 = new Regex("/^(med|ldan|bral|bya|can)/");
            Regex reg3 = new Regex("/^[pb][ao](?:'i|s|'am|'ang|'o|r)/");
                //for (int i = 0; i < syllables.Length; i++)
            //{
                if (syllables.Length > 1)
                {
                    sib1 = syllables[0];
                    sib2 = syllables[1];
                    if (syllables.Length > 2)
                    {
                        sib3 = syllables[2];
                    }
                    if (reg1.IsMatch(sib1) ) {
                        test += sib1 + " matches r1 " + reg1;
                    }
                    if (reg2.IsMatch(sib1))
                    {
                        test += sib1 + " matches r2 " + reg2;
                    }
                    if (reg3.IsMatch(sib1))
                    {
                        test += sib1 + " matches r3 " + reg3;
                    }
                    //if (Regex.IsMatch(sib2, reg1) && (!string.IsNullOrEmpty(sib3) && Regex.IsMatch(sib3, reg2)))
                    //{
                    //    string yadda = "found";
                    //}

                }
                label1.Text = test;
            //}

            //foreach (string s in syllables)
            //{
            //    test.Append(s);
            //    test.Append(" and ");


            //}

            label1.Text = test.ToString();
               
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            string regexString;
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                entry = textBox1.Text;
                regexString = textBox2.Text;

            }
            else
            {
                label1.Text = "no entry";
                return;
            }
            string[] syllables = entry.Split(' ');
            string test = "";
            //Regex reg1 = new Regex("/^(pa|ba|po|bo|mo)/");
            //Regex reg2 = new Regex("/^(med|ldan|bral|bya|can)/");
            //Regex reg3 = new Regex("/^[pb][ao](?:'i|s|'am|'ang|'o|r)/");
            Regex reg1 = new Regex(regexString);
            
            foreach (string s in syllables)
            {
                Match match = Regex.Match(s, regexString);
                if (match.Success) { 
                    test += s + " matches r1 " + regexString;
                }
                if (reg1.IsMatch(s))
                {
                    test += s + " matches r1 " + regexString;
                }
            }

            label1.Text = test;
        }
    }
}
