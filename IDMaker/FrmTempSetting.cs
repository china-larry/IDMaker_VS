using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IDMaker
{
    public partial class FrmTempSetting : Form
    {
        public FrmTempSetting()
        {
            InitializeComponent();
        }
        string[] st = new string[7] {"0", "20", "27", "29", "31", "35", "100" };
        private void FrmTempSetting_Load(object sender, EventArgs e)
        {
            dgvTempSet.Rows.Add(6);
            for (int i = 0; i < 6;i++ )
            {
                string strTemp = ClassIni.ReadIniData("Calib Item" + ClassCS.gi_SelectProIndex, "Temperature" + i, ClassCS.iniPARA);
                if (strTemp.Trim()!="")
                {
                    st[i + 1] = strTemp;
                }
                dgvTempSet[1, i].Value = st[i]; dgvTempSet[2, i].Value = st[i+1];
            }
            dgvTempSet[0, 0].Value = "曲线1--20℃"; dgvTempSet[0, 1].Value = "曲线2--25℃"; dgvTempSet[0, 2].Value = "曲线3--27℃";
            dgvTempSet[0, 3].Value = "曲线4--29℃"; dgvTempSet[0, 4].Value = "曲线5--31℃"; dgvTempSet[0, 5].Value = "曲线6--35℃";
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                string st = dgvTempSet[2, i].Value.ToString();
                ClassIni.WriteIniData("Calib Item" + ClassCS.gi_SelectProIndex, "Temperature" + i,st, ClassCS.iniPARA);
            }
            //MessageBox.Show("已保存到PARA.ini,请勿删除该文件!");
           
        }

        private void dgvTempSet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex!=2 ||e.RowIndex<0)
            {
                return;
            }
            int rid = e.RowIndex;
            if (rid < 5)
            {
                dgvTempSet[1, rid + 1].Value = dgvTempSet[2, rid].Value;
            }
        }
    }
}
