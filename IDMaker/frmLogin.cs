using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IDMaker
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*  �߼��û�
                ��ͨ�û�
                SYSTEM
             */
            if (comboBox1.Text == "�߼��û�")
            {
                if (textBox2.Text.Trim() == "0688")
                {
                    ClassCS.gi_QX = 1;
                    Close();
                }
                else
                {
                    Busiclass.MsgError("�������,���������룡");
                    textBox2.Focus();
                }
            }
            else if (comboBox1.Text == "��ͨ�û�")
            {
                ClassCS.gi_QX = 0;
                Close();
            }
            else if (comboBox1.Text == "SYSTEM")
            {
                if (textBox2.Text.Trim() == "wanfu")
                {
                    ClassCS.gi_QX = 1;
                    Close();
                }
                else
                {
                    Busiclass.MsgError("�������,���������룡");
                    textBox2.Focus();
                }
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar== 13 )
            {
                button1.Focus();
            }
        }
    }
}