using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace IDMaker
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            listBox1.Items.Clear();
            for (int i = 0; i < ClassCS.ArrayAreaName.Length;i++)
            {
                listBox1.Items.Add((i + 1).ToString() + " :" + ClassCS.ArrayAreaName[i]);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = listBox1.Items[listBox1.SelectedIndex].ToString();
            int pos = str.IndexOf(":");
            if (pos > 0)
                textBox1.Text = str.Substring(pos + 1, str.Length - pos - 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim()=="")
            {
                Busiclass.MsgError("注册区域不能为空！");
                return;
            }
            ClassCS.gi_AreaName = 1;
            ClassCS.AreaName = textBox1.Text;
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClassCS.gi_AreaName = 0;
            Close();
        }
    }
}