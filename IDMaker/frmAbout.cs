using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IDMaker
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void label6_ParentChanged(object sender, EventArgs e)
        {

        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            label1.Text = ClassCS.ProName;
            label7.Text = ClassCS.sBuildId;
            label3.Text = ClassCS.gs_company_info;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}