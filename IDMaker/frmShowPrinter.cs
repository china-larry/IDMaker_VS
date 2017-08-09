using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IDMaker
{
    public partial class frmShowPrinter : Form
    {
        public frmShowPrinter()
        {
            InitializeComponent();
        }
        #region 绘制groupBox边框
        /// <summary>
        /// 绘制groupBox边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="b">标题字体颜色</param>
        /// <param name="p">边框</param>
        private void groupBox_Paint(object sender, PaintEventArgs e, Brush b, Pen p)
        {
            e.Graphics.Clear(((GroupBox)sender).BackColor);
            e.Graphics.DrawString(((GroupBox)sender).Text, ((GroupBox)sender).Font, b, 10, 1); // Brushes.Blue
            e.Graphics.DrawLine(p, 1, 7, 8, 7); //Pens.Red
            e.Graphics.DrawLine(p, e.Graphics.MeasureString(((GroupBox)sender).Text, ((GroupBox)sender).Font).Width + 8, 7, ((GroupBox)sender).Width - 2, 7);
            e.Graphics.DrawLine(p, 1, 7, 1, ((GroupBox)sender).Height - 2);
            e.Graphics.DrawLine(p, 1, ((GroupBox)sender).Height - 2, ((GroupBox)sender).Width - 2, ((GroupBox)sender).Height - 2);
            e.Graphics.DrawLine(p, ((GroupBox)sender).Width - 2, 7, ((GroupBox)sender).Width - 2, ((GroupBox)sender).Height - 2);


            /*
              e.Graphics.Clear(groupBox4.BackColor);
              e.Graphics.DrawString(groupBox4.Text, groupBox4.Font, Brushes.Blue, 10, 1);
              e.Graphics.DrawLine(Pens.Red, 1, 7, 8, 7);
              e.Graphics.DrawLine(Pens.Red, e.Graphics.MeasureString(groupBox4.Text, groupBox4.Font).Width + 8, 7, groupBox4.Width - 2, 7);
              e.Graphics.DrawLine(Pens.Red, 1, 7, 1, groupBox4.Height - 2);
              e.Graphics.DrawLine(Pens.Red, 1, groupBox4.Height - 2, groupBox4.Width - 2, groupBox4.Height - 2);
              e.Graphics.DrawLine(Pens.Red, groupBox4.Width - 2, 7, groupBox4.Width - 2, groupBox4.Height - 2);  
             */
        }
        #endregion

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Red;
            groupBox_Paint(sender, e, b, p);
        }

        private void frmShowPrinter_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result;
                result = Busiclass.MsgYesNo("确定退出？");
                if (result == DialogResult.Yes)
                    Close();
            }
            catch (System.Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearData(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearData(2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClearData(3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClearData(4);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClearData(5);
        }

        /// <summary>
        /// 只能输入正实数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnlyEnterPlusNumber(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0) e.Handled = true;
        }


        #region OnlyEnterNumber 只能输入数字（含负号小数点）
        /// 只能输入数字（含负号小数点）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnlyEnterNumber(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0) e.Handled = true;
        }
        #endregion

        private void SetShow()
        {
            if (ClassCS.gi_QX==1)
            {
                button1.Enabled = true;
                button3.Enabled = true;
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button3.Enabled = false;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
            }
        }

        private void frmShowPrinter_Load(object sender, EventArgs e)
        {
            SetShow();

            #region  初始化 结果1
            tb_ResultName1.Text = frmMain.StructSP[0].ProName;
            tb_ResultMin1.Text = frmMain.StructSP[0].Min;
            tb_ResultMinShow1.Text = frmMain.StructSP[0].MinShow;
            tb_ResultMax1.Text = frmMain.StructSP[0].Max;
            tb_ResultMaxShow1.Text = frmMain.StructSP[0].MaxShow;
            tb_ResultMid1.Text = frmMain.StructSP[0].MidShow;
            if (frmMain.StructSP[0].RangeMin.Trim() != "")
                tb_ResultRangeBegin1.Text = Convert.ToDouble(frmMain.StructSP[0].RangeMin).ToString();
            else
                tb_ResultRangeBegin1.Text = "";
            if (frmMain.StructSP[0].RangeMax.Trim() != "")
                tb_ResultRangeEnd1.Text = Convert.ToDouble(frmMain.StructSP[0].RangeMax).ToString();
            else
                tb_ResultRangeEnd1.Text = "";
            // 定性
            if(tb_ResultMinShow1.Text == "阴")
            {
                tb_ResultMinShow1.Enabled = false;
                tb_ResultMaxShow1.Enabled = false;
                tb_ResultMid1.Enabled = false;
                tb_ResultRangeBegin1.Enabled = false;
                tb_ResultRangeEnd1.Enabled = false;
                
            }
            else
            {
                tb_ResultMinShow1.Enabled = true;
                tb_ResultMaxShow1.Enabled = true;
                tb_ResultMid1.Enabled = true;
                tb_ResultRangeBegin1.Enabled = true;
                tb_ResultRangeEnd1.Enabled = true;
               
            }
            #endregion

            #region  初始化 结果2
            tb_ResultName2.Text = frmMain.StructSP[1].ProName;
            tb_ResultMin2.Text = frmMain.StructSP[1].Min;
            tb_ResultMinShow2.Text = frmMain.StructSP[1].MinShow;
            tb_ResultMax2.Text = frmMain.StructSP[1].Max;
            tb_ResultMaxShow2.Text = frmMain.StructSP[1].MaxShow;
            tb_ResultMid2.Text = frmMain.StructSP[1].MidShow;

            if (frmMain.StructSP[1].RangeMin.Trim() != "")
                tb_ResultRangeBegin2.Text = Convert.ToDouble(frmMain.StructSP[1].RangeMin).ToString();
            else
                tb_ResultRangeBegin2.Text = "";
            if (frmMain.StructSP[1].RangeMax.Trim() != "")
                tb_ResultRangeEnd2.Text = Convert.ToDouble(frmMain.StructSP[1].RangeMax).ToString();
            else
                tb_ResultRangeEnd2.Text = "";
            // 定性
            if (tb_ResultMinShow2.Text == "阴")
            {
                tb_ResultMinShow2.Enabled = false;
                tb_ResultMaxShow2.Enabled = false;
                tb_ResultMid2.Enabled = false;
                tb_ResultRangeBegin2.Enabled = false;
                tb_ResultRangeEnd2.Enabled = false;
        
            }
            else
            {
                tb_ResultMinShow2.Enabled = true;
                tb_ResultMaxShow2.Enabled = true;
                tb_ResultMid2.Enabled = true;
                tb_ResultRangeBegin2.Enabled = true;
                tb_ResultRangeEnd2.Enabled = true;
                
            }
            #endregion

            #region  初始化 结果3
            tb_ResultName3.Text = frmMain.StructSP[2].ProName;
            tb_ResultMin3.Text = frmMain.StructSP[2].Min;
            tb_ResultMinShow3.Text = frmMain.StructSP[2].MinShow;
            tb_ResultMax3.Text = frmMain.StructSP[2].Max;
            tb_ResultMaxShow3.Text = frmMain.StructSP[2].MaxShow;
            tb_ResultMid3.Text = frmMain.StructSP[2].MidShow;
            if (frmMain.StructSP[2].RangeMin.Trim() != "")
                tb_ResultRangeBegin3.Text = Convert.ToDouble(frmMain.StructSP[2].RangeMin).ToString();
            else
                tb_ResultRangeBegin3.Text = "";
            if (frmMain.StructSP[2].RangeMax.Trim() != "")
                tb_ResultRangeEnd3.Text = Convert.ToDouble(frmMain.StructSP[2].RangeMax).ToString();
            else
                tb_ResultRangeEnd3.Text = "";
            #endregion

            #region  初始化 结果4
            tb_ResultName4.Text = frmMain.StructSP[3].ProName;
            tb_ResultMin4.Text = frmMain.StructSP[3].Min;
            tb_ResultMinShow4.Text = frmMain.StructSP[3].MinShow;
            tb_ResultMax4.Text = frmMain.StructSP[3].Max;
            tb_ResultMaxShow4.Text = frmMain.StructSP[3].MaxShow;
            tb_ResultMid4.Text = frmMain.StructSP[3].MidShow;
            if (frmMain.StructSP[3].RangeMin.Trim() != "")
                tb_ResultRangeBegin4.Text = Convert.ToDouble(frmMain.StructSP[3].RangeMin).ToString();
            else
                tb_ResultRangeBegin4.Text = "";
            if (frmMain.StructSP[3].RangeMax.Trim() != "")
                tb_ResultRangeEnd4.Text = Convert.ToDouble(frmMain.StructSP[3].RangeMax).ToString();
            else
                tb_ResultRangeEnd4.Text = "";
            #endregion

            #region  初始化 结果5
            tb_ResultName5.Text = frmMain.StructSP[4].ProName;
            tb_ResultMin5.Text = frmMain.StructSP[4].Min;
            tb_ResultMinShow5.Text = frmMain.StructSP[4].MinShow;
            tb_ResultMax5.Text = frmMain.StructSP[4].Max;
            tb_ResultMaxShow5.Text = frmMain.StructSP[4].MaxShow;
            tb_ResultMid5.Text = frmMain.StructSP[4].MidShow;
            if (frmMain.StructSP[4].RangeMin.Trim() != "")
                tb_ResultRangeBegin5.Text = Convert.ToDouble(frmMain.StructSP[4].RangeMin).ToString();
            else
                tb_ResultRangeBegin5.Text = "";
            if (frmMain.StructSP[4].RangeMax.Trim() != "")
                tb_ResultRangeEnd5.Text = Convert.ToDouble(frmMain.StructSP[4].RangeMax).ToString();
            else
                tb_ResultRangeEnd5.Text = "";
            #endregion
        }

        #region ClearData 清空数据
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="mode"></param>
        private void ClearData(int mode)
        {
            switch (mode)
            {
                case 1:
                    tb_ResultName1.Text = "";
                    tb_ResultMin1.Text = "";
                    tb_ResultMinShow1.Text = "";
                    tb_ResultMax1.Text = "";
                    tb_ResultMaxShow1.Text = "";
                    tb_ResultMid1.Text = "";
                    tb_ResultRangeBegin1.Text = "";
                    tb_ResultRangeEnd1.Text = "";
                    break;
                case 2:
                    tb_ResultName2.Text = "";
                    tb_ResultMin2.Text = "";
                    tb_ResultMinShow2.Text = "";
                    tb_ResultMax2.Text = "";
                    tb_ResultMaxShow2.Text = "";
                    tb_ResultMid2.Text = "";
                    tb_ResultRangeBegin2.Text = "";
                    tb_ResultRangeEnd2.Text = "";
                    break;
                case 3:
                    tb_ResultName3.Text = "";
                    tb_ResultMin3.Text = "";
                    tb_ResultMinShow3.Text = "";
                    tb_ResultMax3.Text = "";
                    tb_ResultMaxShow3.Text = "";
                    tb_ResultMid3.Text = "";
                    tb_ResultRangeBegin3.Text = "";
                    tb_ResultRangeEnd3.Text = "";
                    break;
                case 4:
                    tb_ResultName4.Text = "";
                    tb_ResultMin4.Text = "";
                    tb_ResultMinShow4.Text = "";
                    tb_ResultMax4.Text = "";
                    tb_ResultMaxShow4.Text = "";
                    tb_ResultMid4.Text = "";
                    tb_ResultRangeBegin4.Text = "";
                    tb_ResultRangeEnd4.Text = "";
                    break;
                case 5:
                    tb_ResultName5.Text = "";
                    tb_ResultMin5.Text = "";
                    tb_ResultMinShow5.Text = "";
                    tb_ResultMax5.Text = "";
                    tb_ResultMaxShow5.Text = "";
                    tb_ResultMid5.Text = "";
                    tb_ResultRangeBegin5.Text = "";
                    tb_ResultRangeEnd5.Text = "";
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 6;i++)
            {
                ClearData(i);
            }
        }
        // 读取hex
        private void button3_Click(object sender, EventArgs e)
        {
            #region  获取 结果1
            frmMain.StructSP[0].ProName = tb_ResultName1.Text;
            frmMain.StructSP[0].Min = tb_ResultMin1.Text;
            frmMain.StructSP[0].MinShow = tb_ResultMinShow1.Text;
            frmMain.StructSP[0].Max = tb_ResultMax1.Text;
            frmMain.StructSP[0].MaxShow = tb_ResultMaxShow1.Text;
            frmMain.StructSP[0].MidShow = tb_ResultMid1.Text;
            frmMain.StructSP[0].RangeMin = tb_ResultRangeBegin1.Text;
            frmMain.StructSP[0].RangeMax = tb_ResultRangeEnd1.Text;
            #endregion

            #region  获取 结果2
            frmMain.StructSP[1].ProName = tb_ResultName2.Text;
            frmMain.StructSP[1].Min = tb_ResultMin2.Text;
            frmMain.StructSP[1].MinShow = tb_ResultMinShow2.Text;
            frmMain.StructSP[1].Max = tb_ResultMax2.Text;
            frmMain.StructSP[1].MaxShow = tb_ResultMaxShow2.Text;
            frmMain.StructSP[1].MidShow = tb_ResultMid2.Text;
            frmMain.StructSP[1].RangeMin = tb_ResultRangeBegin2.Text;
            frmMain.StructSP[1].RangeMax = tb_ResultRangeEnd2.Text;
            #endregion

            #region  获取 结果3
            frmMain.StructSP[2].ProName = tb_ResultName3.Text;
            frmMain.StructSP[2].Min = tb_ResultMin3.Text;
            frmMain.StructSP[2].MinShow = tb_ResultMinShow3.Text;
            frmMain.StructSP[2].Max = tb_ResultMax3.Text;
            frmMain.StructSP[2].MaxShow = tb_ResultMaxShow3.Text;
            frmMain.StructSP[2].MidShow = tb_ResultMid3.Text;
            frmMain.StructSP[2].RangeMin = tb_ResultRangeBegin3.Text;
            frmMain.StructSP[2].RangeMax = tb_ResultRangeEnd3.Text;
            #endregion

            #region  获取 结果4
            frmMain.StructSP[3].ProName = tb_ResultName4.Text;
            frmMain.StructSP[3].Min = tb_ResultMin4.Text;
            frmMain.StructSP[3].MinShow = tb_ResultMinShow4.Text;
            frmMain.StructSP[3].Max = tb_ResultMax4.Text;
            frmMain.StructSP[3].MaxShow = tb_ResultMaxShow4.Text;
            frmMain.StructSP[3].MidShow = tb_ResultMid4.Text;
            frmMain.StructSP[3].RangeMin = tb_ResultRangeBegin4.Text;
            frmMain.StructSP[3].RangeMax = tb_ResultRangeEnd4.Text;
            #endregion

            #region  获取 结果5
            frmMain.StructSP[4].ProName = tb_ResultName5.Text;
            frmMain.StructSP[4].Min = tb_ResultMin5.Text;
            frmMain.StructSP[4].MinShow = tb_ResultMinShow5.Text;
            frmMain.StructSP[4].Max = tb_ResultMax5.Text;
            frmMain.StructSP[4].MaxShow = tb_ResultMaxShow5.Text;
            frmMain.StructSP[4].MidShow = tb_ResultMid5.Text;
            frmMain.StructSP[4].RangeMin = tb_ResultRangeBegin5.Text;
            frmMain.StructSP[4].RangeMax = tb_ResultRangeEnd5.Text;
            #endregion

            Close();
        }

        private void tb_ResultMin1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusNumber(sender, e);
        }

        private void tb_ResultMax1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusNumber(sender, e);
        }

        private void tb_ResultRangeBegin1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusNumber(sender, e);
        }
    }
}