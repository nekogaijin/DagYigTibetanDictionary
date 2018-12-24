using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DagYig
{
    public partial class frmPronounce : Form
    {
        public frmPronounce()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text)) { return; }
            string entry = textBox1.Text;
            String[] syl = entry.Split(' ');
            //1. use an exceptions list
            // that's a todo/
            // next


        }
    }
}
