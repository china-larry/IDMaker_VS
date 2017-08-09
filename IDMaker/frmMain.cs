using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace IDMaker
{
    public partial class frmMain : Form
    {
        #region StructShowPriner 显示和打印结果的参数 结构体
        /// <summary>
        /// 显示和打印结果的参数 结构体
        /// </summary>
        public struct StructShowPriner
        {
            public string ProName;//项目名
            public string Min;//比对的最小值
            public string MinShow;//比对最小值后显示的结果
            public string Max; //比对的最大值
            public string MaxShow;//比对最大值后显示的结果
            public string MidShow;//比对后 中间值的结果
            public string RangeMin;//检测范围的小值
            public string RangeMax;//检测范围的大值
        }
        #endregion

        #region StructSP 显示和打印结果的参数 结构体 数组
        /// <summary>
        /// 
        /// </summary>
        public static StructShowPriner[] StructSP = new StructShowPriner[5];
        #endregion

        #region  数据数组 共256行，每行16个字节 ClassCS.gd_Byte
        /// <summary>
        /// 数据数组 共256行，每行16个字节
        /// </summary>
        
        #endregion

        #region  参数个数 li_Count
        /// <summary>
        /// 参数个数
        /// </summary>
        private int gi_ConcentrationCount = 0,li_CountP = 0;// 飞测2浓度数量
        #endregion

        #region X浓度 ld_XArray
        /// <summary>
        /// X浓度
        /// </summary>
        private double[] ld_XArray = new double[32], ld_XArrayP = new double[32];
        #endregion

        #region Y： TC 值 ld_YArray
        /// <summary>
        /// Y： TC 荧光值
        /// </summary>
        private double[] ld_YArray = new double[32], ld_YArrayP = new double[32];//plus的TC值
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DialogResult result;
                result = Busiclass.MsgYesNo("确定退出？");
                if (result == DialogResult.Yes)
                {
                    this.Dispose();
                    Application.Exit();
                    e.Cancel = false; //退出
                }
                else
                    e.Cancel = true;
            }
            catch (System.Exception ex)
            {

            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            ClassCS.gi_cbBoxProCount = 0;//项目数
            this.Text = ClassCS.ProName;//项目名称
            ClassCS.gi_QX = 0;
            toolStripStatusLabel1.Text = ClassCS.gs_company_info + "   " + ClassCS.gs_rights + "   " + ClassCS.sBuildId;
            toolStripStatusLabel2.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            init();

            #region  初始化结果一 CRP\ 结果二 hsCRP 的数据
//             StructSP[0].ProName = "cTnI";
//             StructSP[0].Min = "0.03";
//             StructSP[0].MinShow = "<0.03 ng/ml";
//             StructSP[0].Max = "50";
//             StructSP[0].MaxShow = ">50 ng/ml";
//             StructSP[0].MidShow = "X";
//             StructSP[0].RangeMin = "0.03";
//             StructSP[0].RangeMax = "50";

            StructSP[0].ProName = "hsCRP";
            StructSP[0].Min = "0.5";
            StructSP[0].MinShow = "<0.5 mg/L";
            StructSP[0].Max = "5";
            StructSP[0].MaxShow = ">5 mg/L";
            StructSP[0].MidShow = "X";
            StructSP[0].RangeMin = "0.5";
            StructSP[0].RangeMax = "5";

            StructSP[1].ProName = "CRP";
            StructSP[1].Min = "5";
            StructSP[1].MinShow = "<5 mg/L";
            StructSP[1].Max = "200";
            StructSP[1].MaxShow = ">200 mg/L";
            StructSP[1].MidShow = "X";
            StructSP[1].RangeMin = "5";
            StructSP[1].RangeMax = "200";

            iniShowPriner(0);//将CRP的显示方式表现在控件中
            #endregion
            initLV(listView1);
            SetShow();
        }

        private void SetShow()
        {
            if (ClassCS.gi_QX == 1)//系统用户
            {
                //groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                //groupBox3.Enabled = true;
                groupBox5.Enabled = true;
                //groupBox6.Enabled = true;
            }
            else //普通用户
            {
                //groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                //groupBox3.Enabled = false;
                groupBox5.Enabled = false;
                //groupBox6.Enabled = false;
            }
        }
        #region init 初始化界面
        /// <summary>
        /// 初始化界面
        /// </summary>
        private void init()
        {
            tb_PreBatch.Text = "";
            cbBoxProCount.SelectedIndex = 0;//项目总数默认为1
            panel7.Visible = false;

            

            #region  初始化 仪器类型
            cm_Type.Items.Clear();
            cm_Type.Items.Add("飞测");
            cm_Type.Items.Add("安测");
            cm_Type.SelectedIndex = 0;
            #endregion

            #region 初始化数据
            cm_Produce.Items.Clear();//产品代码
            for (int i = 0; i < 256; i++)
                cm_Produce.Items.Add(i.ToString());

            cm_Year.Items.Clear();

            cm_Month.Items.Clear();
            cm_SerialNumber.Items.Clear();//条码

            #region  初始化限制条件cm_Prerequisite
            cm_Prerequisite0.Items.Clear();
            cm_Prerequisite1.Items.Clear();
            cm_Prerequisite2.Items.Clear();

            cm_Prerequisite3.Items.Clear();
            cm_Prerequisite4.Items.Clear();
            cm_Prerequisite5.Items.Clear();
            cm_Prerequisite6.Items.Clear();
            cm_Prerequisite7.Items.Clear();
            cm_Prerequisite8.Items.Clear();
            cm_Prerequisite9.Items.Clear();
            cm_Prerequisite10.Items.Clear();
            cm_Prerequisite11.Items.Clear();
            cm_Prerequisite12.Items.Clear();
            cm_Prerequisite13.Items.Clear();
            cm_Prerequisite14.Items.Clear();
            cm_Prerequisite15.Items.Clear();

            cm_Prerequisite0.Items.Add("自动选择最佳");
            cm_Prerequisite1.Items.Add("自动选择最佳");
            cm_Prerequisite2.Items.Add("自动选择最佳");

            cm_Prerequisite3.Items.Add("自动选择最佳");
            cm_Prerequisite4.Items.Add("自动选择最佳");
            cm_Prerequisite5.Items.Add("自动选择最佳");
            cm_Prerequisite6.Items.Add("自动选择最佳");
            cm_Prerequisite7.Items.Add("自动选择最佳");
            cm_Prerequisite8.Items.Add("自动选择最佳");
            cm_Prerequisite9.Items.Add("自动选择最佳");
            cm_Prerequisite10.Items.Add("自动选择最佳");
            cm_Prerequisite11.Items.Add("自动选择最佳");
            cm_Prerequisite12.Items.Add("自动选择最佳");
            cm_Prerequisite13.Items.Add("自动选择最佳");
            cm_Prerequisite14.Items.Add("自动选择最佳");
            cm_Prerequisite15.Items.Add("自动选择最佳");

            cm_Prerequisite0.SelectedIndex = 0;
            cm_Prerequisite1.SelectedIndex = 0;
            cm_Prerequisite2.SelectedIndex = 0;

            cm_Prerequisite3.SelectedIndex = 0;
            cm_Prerequisite4.SelectedIndex = 0;
            cm_Prerequisite5.SelectedIndex = 0;
            cm_Prerequisite6.SelectedIndex = 0;
            cm_Prerequisite7.SelectedIndex = 0;
            cm_Prerequisite8.SelectedIndex = 0;
            cm_Prerequisite9.SelectedIndex = 0;
            cm_Prerequisite10.SelectedIndex = 0;
            cm_Prerequisite11.SelectedIndex = 0;
            cm_Prerequisite12.SelectedIndex = 0;
            cm_Prerequisite13.SelectedIndex = 0;
            cm_Prerequisite14.SelectedIndex = 0;
            cm_Prerequisite15.SelectedIndex = 0;
            #endregion

            for (int i = 0; i < 16; i++)
            {
                cm_SerialNumber.Items.Add((i + 1).ToString());
            }
            for (int i = 0; i < 16; i++)
            {
                cm_Year.Items.Add((i + 2012).ToString());
                //cm_Year.Items.Add((i + 2012).ToString());
                if (i < 6)
                {
                    #region 初始化处理方法的条件
                    cm_Prerequisite0.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite1.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite2.Items.Add((i + 1).ToString() + "次方");

                    cm_Prerequisite3.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite4.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite5.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite6.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite7.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite8.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite9.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite10.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite11.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite12.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite13.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite14.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite15.Items.Add((i + 1).ToString() + "次方");
                    #endregion
                }
                if (i < 12)
                    cm_Month.Items.Add(string.Format("{0:D2}", i + 1));
                if (i < 5)
                    StructSP[i] = new StructShowPriner();
            }

            cm_Year.SelectedIndex = cm_Year.FindString(DateTime.Now.ToString("yyyy"));//DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            cm_Month.SelectedIndex = cm_Month.FindString(DateTime.Now.ToString("MM"));
            #endregion

            #region  初始化处理方法cm_Method,
            cm_Method0.Items.Clear();
            cm_Method1.Items.Clear();
            cm_Method2.Items.Clear();
            cm_Method3.Items.Clear();
            cm_Method4.Items.Clear();
            cm_Method5.Items.Clear();
            cm_Method6.Items.Clear();
            cm_Method7.Items.Clear();
            cm_Method8.Items.Clear();
            cm_Method9.Items.Clear();
            cm_Method10.Items.Clear();
            cm_Method11.Items.Clear();
            cm_Method12.Items.Clear();
            cm_Method13.Items.Clear();
            cm_Method14.Items.Clear();
            cm_Method15.Items.Clear();

            cm_Method0.Items.Add("多项式");
            cm_Method1.Items.Add("多项式");
            cm_Method2.Items.Add("多项式");
            cm_Method3.Items.Add("多项式");
            cm_Method4.Items.Add("多项式");
            cm_Method5.Items.Add("多项式");
            cm_Method6.Items.Add("多项式");
            cm_Method7.Items.Add("多项式");
            cm_Method8.Items.Add("多项式");
            cm_Method9.Items.Add("多项式");
            cm_Method10.Items.Add("多项式");
            cm_Method11.Items.Add("多项式");
            cm_Method12.Items.Add("多项式");
            cm_Method13.Items.Add("多项式");
            cm_Method14.Items.Add("多项式");
            cm_Method15.Items.Add("多项式");
            cm_Method0.SelectedIndex = 0;
            cm_Method1.SelectedIndex = 0;
            cm_Method2.SelectedIndex = 0;
            cm_Method3.SelectedIndex = 0;
            cm_Method4.SelectedIndex = 0;
            cm_Method5.SelectedIndex = 0;
            cm_Method6.SelectedIndex = 0;
            cm_Method7.SelectedIndex = 0;
            cm_Method8.SelectedIndex = 0;
            cm_Method9.SelectedIndex = 0;
            cm_Method10.SelectedIndex = 0;
            cm_Method11.SelectedIndex = 0;
            cm_Method12.SelectedIndex = 0;
            cm_Method13.SelectedIndex = 0;
            cm_Method14.SelectedIndex = 0;
            cm_Method15.SelectedIndex = 0;

            #endregion

            

            #region  默认选项
            cm_Produce.SelectedIndex = 0;
            cm_SerialNumber.SelectedIndex = 0;

            cm_Temperature.SelectedIndex = 1;//启用温补
            cm_AreaUse.SelectedIndex = 1;   //启用区域  
            cm_Log.SelectedIndex = 1;//取对数
            cm_SubCount.SelectedIndex = 0;//不分段
            cm_Fz.SelectedIndex = 0;
            cm_Fm.SelectedIndex = 3;

            #endregion

            ClearShowPriner();//清空显示结构体

            #region  初始化结果选项
            cm_ResultSelect.Items.Clear();
            cm_ResultSelect.Items.Add("结果一");
            cm_ResultSelect.Items.Add("结果二");
            cm_ResultSelect.Items.Add("结果三");
            cm_ResultSelect.Items.Add("结果四");
            cm_ResultSelect.Items.Add("结果五");
            cm_ResultSelect.SelectedIndex = 0;
            #endregion




        }
        #endregion

        #region  清空显示选择的结果项 ClearShowPriner
        /// <summary>
        ///  清空显示选择的结果项 ClearShowPriner
        /// </summary>
        private void ClearShowPriner()
        {
            for (int i = 0; i < 5; i++)
            {
                StructSP[i].ProName = "";
                StructSP[i].Min = "";
                StructSP[i].MinShow = "";
                StructSP[i].Max = "";
                StructSP[i].MaxShow = "";
                StructSP[i].MidShow = "";
                StructSP[i].RangeMin = "";
                StructSP[i].RangeMax = "";
            }
        }
        #endregion

        #region  显示选择的结果项 iniShowPriner
        /// <summary>
        /// 显示选择的结果项 iniShowPriner
        /// </summary>
        /// <param name="mode"></param>
        private void iniShowPriner(int mode)
        {
            tb_ResultName1.Text = StructSP[mode].ProName;
            tb_ResultMin1.Text = StructSP[mode].Min;
            tb_ResultMinShow1.Text = StructSP[mode].MinShow;
            tb_ResultMaxShow1.Text = StructSP[mode].MaxShow;
            tb_ResultMax1.Text = StructSP[mode].Max;
            tb_ResultMid1.Text = StructSP[mode].MidShow;
            if (StructSP[mode].RangeMax.Trim() != "")
                tb_ResultRangeEnd1.Text = Convert.ToDouble(StructSP[mode].RangeMax).ToString();
            else
                tb_ResultRangeEnd1.Text = "";
            if (StructSP[mode].RangeMin.Trim() != "")
                tb_ResultRangeBegin1.Text = Convert.ToDouble(StructSP[mode].RangeMin).ToString();
            else
                tb_ResultRangeBegin1.Text = "";
        }
        #endregion

        #region initLV 初始化 ListView控件
        /// <summary>
        /// 初始化 ListView控件
        /// </summary>
        /// <param name="lv"></param>
        private void initLV(ListView lv)
        {
            lv.Items.Clear();
            for (int i = 0; i < 33; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = "";//(i + 1).ToString();
                li.SubItems.Add("");
                li.SubItems.Add("");
//                 li.SubItems.Add("");//cyq2016.7.6添加Plus单独浓度值
//                 li.SubItems.Add("");
                lv.Items.Add(li);
            }
        }
        #endregion

        private void btn_ClearTemp_Click(object sender, EventArgs e)
        {
            gi_ConcentrationCount = 0;
            li_CountP = 0;
            tb_DensFc2.Text = "";
            tb_TCFc2.Text = "";
            
            initLV(listView1);
        }

        private void tb_Dens_TextChanged(object sender, EventArgs e)//浓度框
        {
            try
            {
                ld_XArray = new double[32];
                gi_ConcentrationCount = 0;
                string str = tb_DensFc2.Text.Trim();//浓度标准点
                if (str == "")
                    return;
                //str = str.Replace("\n", "");
                string[] arrayRow = str.Split('\r'); //回车符 复制了excel多行
                if (arrayRow.Length > 1)//如果有多行
                {
                    tb_DensFc2.Text = arrayRow[0];
                    tb_TCFc2.Text = arrayRow[1];//把第二行放入Y标准点????
                }
                else
                {
                    string[] array = arrayRow[0].Split('\t'); //\t 制表符 //复制了一行excel多列 
                    if (array.Length >16)
                    {
                        Busiclass.MsgError("标准点点数不能超过16个，请重新输入！");
                        return;
                    }
                    for (int i = 0; i < array.Length; i++)
                    {
                        listView1.Items[i].SubItems[0].Text = (i + 1).ToString();//序号
                        listView1.Items[i].SubItems[1].Text = array[i].ToString();//X 浓度点
                        ld_XArray[i] = Convert.ToDouble(array[i]);
                    }
                    gi_ConcentrationCount = array.Length;//标准点个数
                }

            }
            catch (System.Exception ex)
            {
                Busiclass.MsgError(ex.Message.ToString());
            }
        }
        private void tb_TCFc2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ld_YArray = new double[32];
                string str = tb_TCFc2.Text.Trim();
                if (str == "")
                    return;
                string[] arrayRow = str.Split('\t'); //\t 制表符 //复制了一行excel多列  
                if (arrayRow.Length > 16)
                {
                    MessageBox.Show("标准点点数不能超过16个，请重新输入！");
                    return;
                }
                for (int i = 0; i < arrayRow.Length; i++)
                {
                    listView1.Items[i].SubItems[0].Text = (i + 1).ToString();//序号
                    ld_YArray[i] = Convert.ToDouble(arrayRow[i]);
                    listView1.Items[i].SubItems[2].Text = arrayRow[i].ToString();//TC值
                }
            }
            catch (System.Exception ex)
            {
                Busiclass.MsgError(ex.Message.ToString());
            }
        }
        


        #region  WriteHex 写Hex文件
        /// <summary>
        /// 写Hex文件
        /// </summary>
        /// <param name="by"></param>
        private void WriteHex()
        {
            /*
            string str = "AABBCCDD";
            byte[] by = new byte[str.Length / 2];
            for (int i = 0; i < by.Length; i++)
            {
                string s = str.Substring(i * 2, 2);
                by[i] = (byte)Convert.ToInt32(s, 16);
            }
             */
            byte[] by = new byte[256 * 16];
            if (cbBoxProCount.SelectedIndex == 1 || cbBoxProCount.SelectedIndex == 2)//多项目
                by = new byte[256 * 2 * 16];
            int n = 0;
            for (int i = 0; i < 256 ; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    try
                    {
                        by[n] = ClassCS.gd_Byte[i, j];
                        n++;
                    }
                    catch (System.Exception ex)
                    {
                    }
                    
                }
            }
            if (cbBoxProCount.SelectedIndex == 1 || cbBoxProCount.SelectedIndex == 2)
            {
                //第二组(包括第2项第3项的参数,79行一个)
                for (int i = 256; i < 256 * 2; i++)
                {
                    //Application.DoEvents();
                    for (int j = 0; j < 16; j++)
                    {
                        by[n] = ClassCS.gd_Byte[i, j];
                        n++;
                    }
                }
            }
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "Hex";
            saveDialog.Filter = "Hex文件|*.Hex";
            //saveDialog.InitialDirectory = Application.StartupPath;
            saveDialog.RestoreDirectory = true;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = new System.IO.FileStream(saveDialog.FileName, System.IO.FileMode.Create);
                fs.Write(by, 0, by.Length);
                fs.Close();
                Busiclass.MsgOK("生成Hex成功！");
            }
            else
            {
                return;
            }
            //保存plus的拟合条件到iniTemp
//             string sLog = "", sSubSec = "", sSubPt = "", sPrerequiste1 = "", sPrerequiste2 = "", sXArray = "", sYArray = "";
//             for (int i = 0; i < 6; i++)
//             {
//                 sLog += frmTCalib.mLog[i].ToString() + ";";
//                 sSubSec += frmTCalib.mSubSec[i].ToString() + ";";
//                 sSubPt += frmTCalib.mSubPt[i].ToString() + ";";
//                 sPrerequiste1 += frmTCalib.Prerequiste1[i].ToString() + ";";
//                 sPrerequiste2 += frmTCalib.Prerequiste2[i].ToString() + ";";
//                 string sX = "", sY = "";
//                 for (int j = 0; j < frmTCalib.li_CountP[i]; j++)
//                 {
//                     sX += frmTCalib.ld_XArray[i][j] + ","; sY += frmTCalib.ld_YArray[i][j] + ",";
//                 }
//                 if (sX != "")
//                 {
//                     sX = sX.Substring(0, sX.Length - 1); sY = sY.Substring(0, sY.Length - 1);//去掉最后一个,
// 
//                 }
//                 if (sXArray == "")
//                 {
//                     sXArray = sX;
//                     sYArray = sY;
//                 }
//                 else
//                 {
//                     sXArray += ";" + sX;
//                     sYArray += ";" + sY;
//                 }
//             }
//             ClassIni.WriteIniData("Plus Calib Parameter1", "mLog", sLog, ClassCS.iniTempName);
//             ClassIni.WriteIniData("Plus Calib Parameter1", "mSubSec", sSubSec, ClassCS.iniTempName);
//             ClassIni.WriteIniData("Plus Calib Parameter1", "mSubPt", sSubPt, ClassCS.iniTempName);
//             ClassIni.WriteIniData("Plus Calib Parameter1", "mPrerequiste1", sPrerequiste1, ClassCS.iniTempName);
//             ClassIni.WriteIniData("Plus Calib Parameter1", "mPrerequiste2", sPrerequiste2, ClassCS.iniTempName);
//             ClassIni.WriteIniData("Plus Calib Parameter1", "mXArray", sXArray, ClassCS.iniTempName);
//             ClassIni.WriteIniData("Plus Calib Parameter1", "mYArray", sYArray, ClassCS.iniTempName);
            //ini文件
            ClassCS.iniFileName = saveDialog.FileName.Replace(".Hex",".ini");
            System.IO.File.Copy(ClassCS.iniTempName, ClassCS.iniFileName, true);//从临时文件拷贝到最终ini文件
            
        }
        #endregion
       
        private void button2_Click(object sender, EventArgs e)
        {
            //WriteByteOld();
        }

        #region 写XY的数据 WriteXY
        /// <summary>
        /// 写XY的数据
        /// </summary>
        /// <param name="X">当前X值</param>
        /// <param name="Y">当前Y值</param>
        /// <param name="By">写入的地方</param>
        /// <param name="n">当前写的第几个数</param>
        private void WriteXY(double X, double Y, ref byte[,] By, int n)
        {
            //X值
            int row = n + 2;
            int Row = n;
            for (int i = 0; i < 16; i++)
                By[row, i] = 0;
            if (X < 1 && X > 0)
            {
                By[row, 0] = Convert.ToByte("F" + ((int)(X * 10 + 0.5)).ToString(), 16);  //Convert.ToByte(((int)((X - (int)X) * 100)).ToString(), 16);//x小数部分 十进制显示
            }
            else
            {
                By[row, 0] = (byte)(int)X;//Convert.ToByte(((int)X).ToString(),10);

                int a = (int)((X - (int)X) * 10); //Busiclass.ConvertString(((int)((X - (int)X) * 10)).ToString(),10,16);
                By[row, 9] = (byte)(a); // Convert.ToByte(((int)((X - (int)X) * 10)).ToString(), 16);//x小数部分 十进制显示
            }
            /*
            By[row, 0] = Convert.ToByte(((int)((X - (int)X) * 100)).ToString(), 16);//x小数部分 十进制显示
            string str = Busiclass.ConvertString(((int)X).ToString(), 10, 16).PadLeft(6, '0');//x整数部分 十六进制显示
            By[row, 9] = Convert.ToByte(str.Substring(0, 2), 16);//高
            By[row, 10] = Convert.ToByte(str.Substring(2, 2), 16);//中
            By[row, 11] = Convert.ToByte(str.Substring(4, 2), 16);//低
            */


            //y值 0.085298699-> 08 05 02 09 08 07 ->8.52987 *10~2 =   0.085298699
            //double x = Convert.ToDouble(textBox6.Text); //0.082;
            double j = 1;//幂次
            int PlusminusSign = -1;//幂的正负
            if (Y >= 1 && Y < 10)
            {
                j = 0;
            }
            else if (Y < 1)
            {
                PlusminusSign = 1; //负
                while (true)
                {
                    double data = Y * Math.Pow(10, j); //j * 10;
                    if (data >= 1 && data <= 10)
                        break;
                    j++;
                }
            }
            else if (Y >= 10)
            {
                PlusminusSign = 0;//正
                while (true)
                {
                    double data = Y / Math.Pow(10, j);
                    if (data >= 1 && data < 10)
                        break;
                    j++;
                }
            }


            string str = Y.ToString().Replace(".", "").TrimStart('0');
            for (int i = 0; i < str.Length; i++)
            {
                if (i > 7)//保留8位 有效数
                {
                    if (Convert.ToInt32(str.Substring(i, 1)) > 4)
                        By[row, i] = (byte)(Convert.ToInt32(str.Substring(i - 1, 1)) + 1); //前面的最后一位 四舍五入
                    break;
                }
                else
                    By[row, i + 1] = Convert.ToByte(str.Substring(i, 1));//0-7
            }
            //转换成二进制，做标示位
            string Pow = Busiclass.ConvertString(j.ToString(), 10, 2).PadLeft(7, '0');
            if (Pow.Length > 7)
                Pow = Pow.Substring(Pow.Length - 7, 7);//只取后7位 

            if (PlusminusSign == 0)  //正 10的幂（该字节最高位是符号位） 0 正，1 负
                By[row, 15] = Convert.ToByte(Busiclass.ConvertString("0" + Pow, 2, 10));
            else if (PlusminusSign == 1)//负
                By[row, 15] = Convert.ToByte(Busiclass.ConvertString("1" + Pow, 2, 10));
            else
                By[row, 15] = 0;
            /*
            //y值 0.085298699->00 00 08 05 02 09 08 07 ->0.0852987
            str = Y.ToString("F9");//防止后面取8个长度（整数+小数<8）的时候出错
            int li_Pos = str.IndexOf(".");//寻找小数点位置，判断幂的次数(这位置从左开始，即不是真正的幂次)
            str = str.Replace(".", "");
            if (Convert.ToInt32(str.Substring(8, 1)) > 4)//四舍五入
                str = (Convert.ToInt32(str.Substring(0, 8)) + 1).ToString().PadLeft(8, '0');
            else
                str = str.Substring(0, 8);
            for (int i = 0; i < str.Length; i++)
                By[row, i + 1] = Convert.ToByte(str.Substring(i, 1), 10);
            By[row, 15] = Convert.ToByte(li_Pos.ToString(), 10);//小数点位置
             */
        }
        #endregion

        

        private void cm_SubCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bNoSub = cm_SubCount.SelectedIndex == 0;
            label57.Visible = label58.Visible = bNoSub;
            cm_Method0.Visible = cm_Prerequisite0.Visible = bNoSub;
            panel3.Visible = !bNoSub;
        }
      

        private void cbBoxOverflow_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbBoxOverflowP.SelectedIndex = cbBoxOverflow.SelectedIndex;
        }
        private void cbBoxShortage_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbBoxShortageP.SelectedIndex = cbBoxShortage.SelectedIndex;
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

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Green;
            groupBox_Paint(sender, e, b, p);
        }


        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Red;
            groupBox_Paint(sender, e, b, p);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Red;
            groupBox_Paint(sender, e, b, p);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Red;
            groupBox_Paint(sender, e, b, p);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 获得当期结果一数值
            StructSP[0].ProName = tb_ResultName1.Text;//项目名称
            StructSP[0].Min = tb_ResultMin1.Text;
            StructSP[0].MinShow = tb_ResultMinShow1.Text;//小于的显示
            StructSP[0].Max = tb_ResultMax1.Text;
            StructSP[0].MaxShow = tb_ResultMaxShow1.Text;//大于的显示
            StructSP[0].MidShow = tb_ResultMid1.Text;//区间内的显示
            StructSP[0].RangeMin = tb_ResultRangeBegin1.Text;//检测范围
            StructSP[0].RangeMax = tb_ResultRangeEnd1.Text;
            //
            frmShowPrinter frmsp = new frmShowPrinter();
            frmsp.Icon = this.Icon;
            frmsp.ShowDialog();
            frmsp.Dispose();

            tb_ResultName1.Text = StructSP[0].ProName;
            tb_ResultMin1.Text = StructSP[0].Min;
            tb_ResultMinShow1.Text = StructSP[0].MinShow;
            tb_ResultMax1.Text = StructSP[0].Max;
            tb_ResultMaxShow1.Text = StructSP[0].MaxShow;
            tb_ResultMid1.Text = StructSP[0].MidShow;
            tb_ResultRangeBegin1.Text = StructSP[0].RangeMin;
            tb_ResultRangeEnd1.Text = StructSP[0].RangeMax;
        }

        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region GetBarCode读取产品批号
        /// <summary>
        ///  产品代码 年月
        /// </summary>
        private string GetBatch()
        {
            try
            {
                string str = "";
                string one = Busiclass.ConvertString(cm_Year.Text.Substring(2, 2), 10, 16);
                string two = Busiclass.ConvertString(cm_Month.Text, 10, 16);
                string three = Busiclass.ConvertString(cm_SerialNumber.Text, 10, 16).PadLeft(2, '0');
                if (tb_PreBatch.Text.Trim() == "")
                {
                    return "";
                }
                str = tb_PreBatch.Text.Trim() + one + two + three;
                /*
                string pruBin = string.Format("{0:D5}", Convert.ToInt32(Busiclass.ConvertString(cm_Produce.Text, 10, 2)));
                string YearBin = string.Format("{0:D4}", Convert.ToInt32(Busiclass.ConvertString(cm_Year.SelectedIndex.ToString(), 10, 2)));
                string MonthBin = string.Format("{0:D4}", Convert.ToInt32(Busiclass.ConvertString((cm_Month.SelectedIndex + 1).ToString(), 10, 2)));
                string NoBin = string.Format("{0:D4}", Convert.ToInt32(Busiclass.ConvertString(cm_SerialNumber.Text, 10, 2)));
                byte a = Convert.ToByte(pruBin + YearBin.Substring(0, 3), 2);
                byte b = Convert.ToByte(YearBin.Substring(3, 1) + MonthBin + NoBin.Substring(0, 3), 2);
                byte c = Convert.ToByte(NoBin.Substring(3, 1) + "0000000", 2);
                string str = Busiclass.ConvertString(a.ToString(), 10, 16).PadLeft(2, '0') + Busiclass.ConvertString(b.ToString(), 10, 16).PadLeft(2, '0') + Busiclass.ConvertString(c.ToString(), 10, 16).PadLeft(2, '0').Substring(0, 1);
                 */
                return str.ToUpper();
            }
            catch (System.Exception ex)
            {
                return "";
            }

            // return Busiclass.c a.ToString("2X2X2X", a,b,c);

        }
        #endregion

        #region GetBarCode读取条码号
        /// <summary>
        ///  产品代码 年月
        /// </summary>
        private string GetBarCode()
        {
            try
            {
                string pruBin = string.Format("{0:D5}", Convert.ToInt32(Busiclass.ConvertString(cm_Produce.Text, 10, 2)));//
                string YearBin = string.Format("{0:D4}", Convert.ToInt32(Busiclass.ConvertString((cm_Year.SelectedIndex).ToString(), 10, 2)));
                string MonthBin = string.Format("{0:D4}", Convert.ToInt32(Busiclass.ConvertString((cm_Month.SelectedIndex).ToString(), 10, 2)));
                string NoBin = string.Format("{0:D4}", Convert.ToInt32(Busiclass.ConvertString(cm_SerialNumber.SelectedIndex.ToString(), 10, 2)));
                byte a = (byte)(int)Convert.ToInt32(pruBin + YearBin.Substring(0, 3), 2); //Convert.ToByte(pruBin + YearBin.Substring(0, 3), 2);
                byte b = (byte)(int)Convert.ToInt32(YearBin.Substring(3, 1) + MonthBin + NoBin.Substring(0, 3), 2); //Convert.ToByte(YearBin.Substring(3, 1) + MonthBin + NoBin.Substring(0, 3), 2);
                byte c = (byte)(int)Convert.ToInt32(NoBin.Substring(3, 1) + "0000000", 2); //Convert.ToByte(NoBin.Substring(3, 1) + "0000000", 2);
                string str = Busiclass.ConvertString(a.ToString(), 10, 16).PadLeft(2, '0') + Busiclass.ConvertString(b.ToString(), 10, 16).PadLeft(2, '0') + Busiclass.ConvertString(c.ToString(), 10, 16).PadLeft(2, '0').Substring(0, 1);
                return str.ToUpper();
            }
            catch (System.Exception ex)
            {
                //Busiclass.MsgError(ex.Message.ToString());
                return "";
            }

            // return Busiclass.c a.ToString("2X2X2X", a,b,c);

        }
        private string GetBarCodeExt(ref string CodeExt)
        {
            try
            {
                string pruBin = Convert.ToString(cm_Produce.SelectedIndex, 2).PadLeft(8, '0');// string.Format("{0:D5}", Convert.ToInt32(Busiclass.ConvertString(cm_Produce.Text, 10, 2)));//
                string YearBin = Convert.ToString(cm_Year.SelectedIndex, 2).PadLeft(4, '0');
                string MonthBin = Convert.ToString(cm_Month.SelectedIndex, 2).PadLeft(4, '0');
                string NoBin = Convert.ToString(cm_SerialNumber.SelectedIndex, 2).PadLeft(5, '0');
                byte a = Convert.ToByte(pruBin.Substring(0, 8), 2);
                byte b = Convert.ToByte((YearBin + MonthBin), 2);
                byte c = Convert.ToByte(NoBin + "000", 2);//流水号3位
                //byte d= Convert.ToByte(MonthBin.Substring(3, 1) + NoBin.Substring(0, 6)+"0", 2);

                string str = Convert.ToString(a, 16).PadLeft(2, '0') + Convert.ToString(b, 16).PadLeft(2, '0') + Convert.ToString(c, 16).PadLeft(2, '0');//
                //CodeExt = Convert.ToString(d, 16).PadLeft(2, '0');
                return str.ToUpper();
            }
            catch (System.Exception ex)
            {
                //Busiclass.MsgError(ex.Message.ToString());
                return "";
            }
        }
        #endregion

        #region 温度定标
        FrmTempCalib frmTCalib = new FrmTempCalib();

        private void btn_TempCalib_Click(object sender, EventArgs e)
        {
            //FrmTempCalib.openEch();// 不主动打开拟合软件

            ClassCS.gi_SelectProIndex = 1;
            frmTCalib.m_iProjectNumber = cbBoxProCount.SelectedIndex;// 项目进入单项目为0
            if (frmTCalib.m_iProjectNumber == 0)
            {
                frmTCalib.SetCurve1TestValue(cm_TC.SelectedIndex);
            }
            else
            {
                frmTCalib.SetCurve1TestMolecularValue(cm_Fz.SelectedIndex+1);
                frmTCalib.SetCurve1TestDenominatorValue(cm_Fm.SelectedIndex+1);
            }
            
            if (!frmTCalib.bMdiShown)
            {
                frmTCalib.Show(this);
                frmTCalib.bMdiShown = true;
                //frmTCalib.m_iProjectNumber = cbBoxProCount.SelectedIndex;// 项目进入单项目为0
            }
            else
            {
                frmTCalib.Activate();
                
                //frmTCalib.m_iProjectNumber = cbBoxProCount.SelectedIndex;// 项目进入单项目为0
            }
            frmTCalib.updataMoreCurveWidget();
            
        }
        #endregion


        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (CheckHexcheckBox.Checked == true)
                {
                    if (CheckHexFormat() == false) return;
                }
                ClearByte(1);
                WriteByte();
            }
            catch (System.Exception ex)
            {
                Busiclass.MsgError(ex.Message.ToString());
            }

        }

        private bool CheckHexFormat()
        {
            if (cm_AreaUse.Text.Trim() == "启用")
            {
                if (tb_Area.Text.Trim() == "")
                {
                    Busiclass.MsgError("注册区域不能为空！");
                    return false;
                }
            }
            // 只有飞测2项目才需要检查该数据
            //if (cbBoxProCount.SelectedIndex <3)// 非定性项目
            //{
            //    if (gi_ConcentrationCount < 1 && li_CountP < 1)
            //    {
            //        Busiclass.MsgError("请输入标准点后,再进行保存！");
            //        return false;
            //    }
            //}
            
            if (tb_PreBatch.Text.Trim() == "")
            {
                Busiclass.MsgError("请输入 产品批号前缀 ,再进行保存！");
                return false;
            }
            int a = Convert.ToInt32(tb_CountDownTime.Text);
            if (a == 0 || a > 60)
            {
                Busiclass.MsgError("请输入 倒计时时间 不能大于60 ,请重新输入！");
                return false;
            }

            a = Convert.ToInt32(tb_TestTime.Text);
            if (a == 0 || a > 60)
            {
                Busiclass.MsgError("请输入 测试时间 不能大于60 ，请重新输入！");
                return false;
            }
            if (Convert.ToInt32(tb_T1End.Text) - Convert.ToInt32(tb_T1Begin.Text) < Convert.ToInt32(tb_T1Count.Text))
            {
                Busiclass.MsgError("第一峰参数设置有误，请检查！");
                return false;
            }
            if (Convert.ToInt32(tb_T2End.Text) - Convert.ToInt32(tb_T2Begin.Text) < Convert.ToInt32(tb_T2Count.Text))
            {
                Busiclass.MsgError("第二峰参数设置有误，请检查！");
                return false;
            }
            if (Convert.ToInt32(tb_CEnd.Text) - Convert.ToInt32(tb_CBegin.Text) < Convert.ToInt32(tb_CCount.Text))
            {
                Busiclass.MsgError("第三峰参数设置有误，请检查！");
                return false;
            }
            if (Convert.ToInt32(tb_Overflow.Text) < 1 || Convert.ToInt32(tb_Overflow.Text) > 260000)
            {
                Busiclass.MsgError("T2段判断冲顶值超过 1-260000 的预设范围，请重新输入！");
                return false;
            }
            if (Convert.ToInt32(tb_Shortage.Text) < 1 || Convert.ToInt32(tb_Shortage.Text) > 260000)
            {
                Busiclass.MsgError("C段判断加样值超过 1-260000 的预设范围，请重新输入！");
                return false;
            }
            return true;
        }

        private void ClearByte(int ProNo)
        {
            switch (ProNo)
            {
                case 1:
                    for (int i = 0; i < 256; i++)
                    {
                        for (int j = 0; j < 16; j++)
                        {
                            if ((i <= 5 && j >= 2 && j <= 5) || (i >= 1 && i <= 2 && j >= 7 && j <= 0xf) || (i >= 0xC4 && i <= 0xdd) || (i >= 0x0e && i <= 0x1d))//这些是项目2,3的
                            {

                            }
                            else
                                ClassCS.gd_Byte[i, j] = 0;
                        }
                    }
                    break;

                case 2:
                    for (int j = 0; j < 16; j++)//列
                    {
                        for (int i = 256; i < 0x150; i++)//行飞测2
                        {
                            ClassCS.gd_Byte[i, j] = 0;
                        }
                        for (int i = 0x1a0; i < 0x1ca; i++)//系数
                        {
                            ClassCS.gd_Byte[i, j] = 0;
                        }
                        for (int i = 0xc4; i < 0xd1; i++)//首位浓度值
                        {
                            ClassCS.gd_Byte[i, j] = 0;
                        }
                        for (int i = 0x0e; i <= 0x15; i++)////项目2浓度值
                        {
                            ClassCS.gd_Byte[i, j] = 0;
                        }
                    }
                    for (int i = 0; i < 6;i++ )//取对数,分段点 
                    {
                        ClassCS.gd_Byte[i, 2] = 0;
                        ClassCS.gd_Byte[i, 3] = 0;
                    }
                    for (int j = 7; j < 0xf; j++)//峰值临界值
                    {
                        ClassCS.gd_Byte[1, j] = 0;
                    }
                    break;

                case 3:
                    for (int j = 0; j < 16; j++)//列
                    {
                        for (int i = 0x150; i < 0x1a0; i++)
                        {
                            ClassCS.gd_Byte[i, j] = 0;
                        }
                        for (int i = 0x1ca; i < 0x1f4; i++)
                        {

                            ClassCS.gd_Byte[i, j] = 0;
                        }
                        for (int i = 0xd1; i < 0xde; i++)
                        {
                            ClassCS.gd_Byte[i, j] = 0;
                        }
                        for (int i = 0x16; i <= 0x1d; i++)//项目3浓度值
                        {
                            ClassCS.gd_Byte[i, j] = 0;
                        }
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        ClassCS.gd_Byte[i, 4] = 0;
                        ClassCS.gd_Byte[i, 5] = 0;
                    }
                    for (int j = 7; j < 0xf; j++)
                    {
                        ClassCS.gd_Byte[2, j] = 0;
                    }
                    break;

                
            }
        }


        #region   将参数生成Byte数组 WriteByte
        /// <summary>
        /// 将参数生成Byte数组
        /// </summary>
        private void WriteByte()
        {
            #region  获取 结果1
            StructSP[0].ProName = tb_ResultName1.Text;//项目名称
            StructSP[0].Min = tb_ResultMin1.Text;
            StructSP[0].MinShow = tb_ResultMinShow1.Text;//小于的显示
            StructSP[0].Max = tb_ResultMax1.Text;
            StructSP[0].MaxShow = tb_ResultMaxShow1.Text;//大于的显示
            StructSP[0].MidShow = tb_ResultMid1.Text;//区间内的显示
            StructSP[0].RangeMin = tb_ResultRangeBegin1.Text;//检测范围
            StructSP[0].RangeMax = tb_ResultRangeEnd1.Text;
            #endregion
            //所有的选择  0:启用； 1:不启用 
            #region 临时变量
            byte[] byteArray;
            int Pos = 32, n = 0;
            #endregion

            #region 生成随机数(被注释了)
            /*
            //ClassCS.gd_Byte = new byte[256, 16];
            Random randObj = new Random();
            for (int i = 0; i < 256; i++)
            {
                Application.DoEvents();
                for (int j = 0; j < 16; j++)  //0x20  0x8A
                {
                    ClassCS.gd_Byte[i, j] = 0;

                    //if (i >= 32 && i <= 138)
                    //    ClassCS.gd_Byte[i, j] = 0;
                    //else
                    //    ClassCS.gd_Byte[i, j] = (byte)randObj.Next(0, 255);
                    
                }
            }
            */
            #endregion

            #region 第0x20(十六进制)行  200H-20FH 基础数据
            Pos = 32;
            n = 0;

            #region 产品代码 年月
//             string pruBin = string.Format("{0:D5}", Convert.ToInt32(Busiclass.ConvertString(cm_Produce.Text, 10, 2)));  //自动填充成5位
//             string YearBin = string.Format("{0:D4}", Convert.ToInt32(Busiclass.ConvertString(cm_Year.SelectedIndex.ToString(), 10, 2)));
//             string MonthBin = string.Format("{0:D4}", Convert.ToInt32(Busiclass.ConvertString((cm_Month.SelectedIndex).ToString(), 10, 2)));
//             string NoBin = string.Format("{0:D4}", Convert.ToInt32(Busiclass.ConvertString((cm_SerialNumber.SelectedIndex).ToString(), 10, 2)));
            string sBarCode = tb_BarCodeNumber.Text;//统一填充成完整的6位条码
            //保存6位条码标识
            if (sBarCode.Length == 6)
            {
                ClassCS.gd_Byte[0x1e, 0] = 0x11;//存放6位条码标志
            }
            else//5位条码
                sBarCode = sBarCode.PadRight(6, '0');
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(sBarCode.Substring(0, 2), 16);
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(sBarCode.Substring(2, 2), 16);
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(sBarCode.Substring(4, 2), 16);
            #endregion

            #region 峰信号量位置(单项目的TC公式)
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte((cm_T1.SelectedIndex + 1).ToString(), 10); //n++;
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte((cm_T2.SelectedIndex + 1).ToString(), 10); //n++;
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte((cm_C.SelectedIndex + 1).ToString(), 10);  //n++;
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_TC.SelectedIndex.ToString(), 10);       //n++;
            #endregion

            #region 详细参数(标准点数,对数与否,分段与否)
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(gi_ConcentrationCount);//标准点总数
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Log.SelectedIndex);//0:处理取对数   1：不取对数  第8个位置
            if (cm_SubCount.Text == "不分段")
                ClassCS.gd_Byte[Pos, n++] = 1; //不分段
            else
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_SubCount.Text, 10);//分段的段数

            #region 温补（上半字节）、区域匹配(下半字节)是否启用
            if (cm_Temperature.Text == "启用")//是否启用温度补偿 启用:0  高位
            {
                if (cm_AreaUse.Text == "启用")//是否启用区域限制标志 低位
                    ClassCS.gd_Byte[Pos, n++] = 0x00;
                else
                    ClassCS.gd_Byte[Pos, n++] = 0x01;
            }
            else //不启用: 1
            {
                if (cm_AreaUse.Text == "启用")//是否启用区域限制标志 低位
                    ClassCS.gd_Byte[Pos, n++] = 0x10;
                else
                    ClassCS.gd_Byte[Pos, n++] = 0x11;
            }
            #endregion

            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_CountDownTime.Text, 10);//倒计时时间

            #region 第三个峰值小于4000（上半字节）、第二个字节大于260000(下半字节)加样冲顶提示是否启用
            if (cb_Overflow.Checked)// 启用:0  高位
            {
                if (cb_Shortage.Checked)
                    ClassCS.gd_Byte[Pos, n++] = 0x00;
                else
                    ClassCS.gd_Byte[Pos, n++] = 0x10;
            }
            else //不启用: 1
            {
                if (cb_Shortage.Checked)
                    ClassCS.gd_Byte[Pos, n++] = 0x01;
                else
                    ClassCS.gd_Byte[Pos, n++] = 0x11;
            }
            
            #endregion

            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_TestTime.Text, 10);//开始测试时间
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_DecimalsDigit.Text, 10);//小数点的位数
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Type.SelectedIndex.ToString(), 10);//仪器类型
            #endregion
            #endregion

            #region 第21(十六进制)行  210H-21FH 分段点位置
            Pos = 33;
            n = 0;
            if (cm_SubCount.Text == "不分段")//不分段写在0位置  分段从1位置开始写
                ClassCS.gd_Byte[Pos, n++] = 1; //不分段
                //ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(gi_ConcentrationCount.ToString(), 10);//不分段,写在0位置,写标准点个数???不是写0吗
            else
            {
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_SubCount.Text, 10);//分段的段数
                //n++;
                //分段从1位置开始写
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_Subsection1.Text, 10);//分2段
                                                   //高半字节+低半字节?
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection1.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection2.Text, 10, 16), 16);//分2段?
                
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection2.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection3.Text, 10, 16), 16);//分3段...
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection3.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection4.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection4.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection5.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection5.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection6.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection6.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection7.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection7.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection8.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection8.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection9.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection9.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection10.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection10.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection11.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection11.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection12.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection12.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection13.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection13.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection14.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection14.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection15.Text, 10, 16), 16);
            }
            #endregion

            #region 第21(十六进制)行  21DH-21FH 三个位置存 T2段判断是否冲顶 0-260000  add by zhou zhang kui 2012-10-09
            string Overflow = Busiclass.ConvertString(tb_Overflow.Text, 10, 16).PadLeft(6, '0'); //Convert.ToString( Convert.ToInt32(tb_Overflow.Text), 16).PadLeft(6,'0');
            ClassCS.gd_Byte[Pos, 13] = Convert.ToByte(Overflow.Substring(0, 2), 16);
            ClassCS.gd_Byte[Pos, 14] = Convert.ToByte(Overflow.Substring(2, 2), 16);
            ClassCS.gd_Byte[Pos, 15] = Convert.ToByte(Overflow.Substring(4, 2), 16);
            #endregion

            #region 第22(十六进制)行  220H-22CH 每段对应的拟合方法"多项式"
            Pos = 34;
            n = 0;
            if (cm_SubCount.Text == "不分段")//不分段写在0位置  分段从1位置开始写
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method0.SelectedIndex);//不分段写在0位置
            else
            {
                n++;
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method1.SelectedIndex);//现在只有"多项式"这一种方法
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method2.SelectedIndex);

                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method3.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method4.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method5.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method6.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method7.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method8.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method9.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method10.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method11.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method12.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method13.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method14.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method15.SelectedIndex);
            }
            #endregion

            #region 第22(十六进制)行  22DH-22FH 三个位置存 C段判断是否加样 0-260000  add by zhou zhang kui 2012-10-09
            string Shortage = Busiclass.ConvertString(tb_Shortage.Text, 10, 16).PadLeft(6, '0'); //Convert.ToString(Convert.ToInt32(tb_Shortage.Text), 16).PadLeft(6, '0');
            ClassCS.gd_Byte[Pos, 13] = Convert.ToByte(Shortage.Substring(0, 2), 16);
            ClassCS.gd_Byte[Pos, 14] = Convert.ToByte(Shortage.Substring(2, 2), 16);
            ClassCS.gd_Byte[Pos, 15] = Convert.ToByte(Shortage.Substring(4, 2), 16);
            #endregion

            #region 第23(十六进制)行  230H-23FH 每段对应的限制条件(几次幂)
            Pos = 35;
            n = 0;
            if (cm_SubCount.Text == "不分段")//不分段写在0位置  分段从1位置开始写
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite0.SelectedIndex.ToString(), 10);//不分段写在0位置
            else
            {
                n++;
                //分段从1位置开始写
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite1.SelectedIndex);//
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite2.SelectedIndex);

                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite3.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite4.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite5.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite6.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite7.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite8.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite9.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite10.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite11.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite12.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite13.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite14.SelectedIndex);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite15.SelectedIndex);
            }
            #endregion

            #region 第24-43(十六进制)行  240H-24FH...430H-43FH.. X值 浓度值    第44-63行 Y TC值  最多20行? 每行32个参数
            for (int i = 0; i < gi_ConcentrationCount; i++)
            {
                WriteDouble(ld_XArray[i], ref ClassCS.gd_Byte, i + 0x24, 0);//X值 浓度值
                WriteDouble(ld_YArray[i], ref ClassCS.gd_Byte, i + 0x44, 0);//Y值 TC测试值
            }
            #endregion

            #region 第74(十六进制)行 血清/血浆系数
            WriteDouble(Convert.ToDouble(tb_Serum.Text), ref ClassCS.gd_Byte, 116, 0);
            #endregion

            #region 第75(十六进制)行 全血
            WriteDouble(Convert.ToDouble(tb_WholeBlood.Text), ref ClassCS.gd_Byte, 117, 0);
            #endregion

            #region 第76(十六进制)行 尿液
            WriteDouble(Convert.ToDouble(tb_UrineValue.Text), ref ClassCS.gd_Byte, 118, 0);
            #endregion

            #region 第77(十六进制)行 粪便
            WriteDouble(Convert.ToDouble(tb_Excrement.Text), ref ClassCS.gd_Byte, 119, 0);
            #endregion

            #region 第78(十六进制)行 质检子数
            WriteDouble(Convert.ToDouble(tb_Quality.Text), ref ClassCS.gd_Byte, 120, 0);
            #endregion

            #region 第79(十六进制)行 a
            WriteDouble(Convert.ToDouble(tb_a.Text), ref ClassCS.gd_Byte, 121, 0);
            #endregion

            #region 第7A(十六进制)行 b
            WriteDouble(Convert.ToDouble(tb_b.Text), ref ClassCS.gd_Byte, 122, 0);
            #endregion

            #region 第7B(十六进制)行 a1
            WriteDouble(Convert.ToDouble(tb_a1.Text), ref ClassCS.gd_Byte, 123, 0);
            #endregion

            #region 第7C(十六进制)行 b1
            WriteDouble(Convert.ToDouble(tb_b1.Text), ref ClassCS.gd_Byte, 124, 0);
            #endregion


            #region 第7D(十六进制)行 表示区域
            byteArray = System.Text.Encoding.Default.GetBytes(tb_Area.Text);
            if (byteArray.Length > 16)
            {
                Busiclass.MsgError("输入的 区域 大于设定的长度(8个汉字),请重新输入！ ");
                return;
            }
            for (int i = 0; i < byteArray.Length; i++)
                ClassCS.gd_Byte[125, i] = byteArray[i];
            #endregion

            #region 第7E (十六进制)行 报告单标题
            byteArray = System.Text.Encoding.Default.GetBytes(tb_TitleName.Text);
            if (byteArray.Length > 16)
            {
                Busiclass.MsgError("输入的 报告单标题 大于设定的长度(8个汉字),请重新输入！ ");
                return;
            }
            for (int i = 0; i < byteArray.Length; i++)
                ClassCS.gd_Byte[126, i] = byteArray[i];
            #endregion

            #region 第7F (十六进制)行 单位
            byteArray = System.Text.Encoding.Default.GetBytes(tb_Unit.Text);
            if (byteArray.Length > 16)
            {
                Busiclass.MsgError("输入的 单位 大于设定的长度(8个汉字),请重新输入！ ");
                return;
            }
            for (int i = 0; i < byteArray.Length; i++)
                ClassCS.gd_Byte[127, i] = byteArray[i];
            #endregion

            #region 第80(十六进制)行 峰的参数
            Pos = 128;
            n = 0;
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_T1Begin.Text, 10);
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_T1End.Text, 10);
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_T2Begin.Text, 10);
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_T2End.Text, 10);
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_CBegin.Text, 10);
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_CEnd.Text, 10);
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_T1Count.Text, 10);
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_T2Count.Text, 10);
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_CCount.Text, 10);
            #endregion

            #region 第81-85(十六进制)行 检测范围
            Pos = 129;
            for (int i = 0; i < 5; i++)
            {
                if (StructSP[i].RangeMin.Trim() != "")
                    WriteDouble(Convert.ToDouble(StructSP[i].RangeMin.Trim()), ref ClassCS.gd_Byte, i + Pos, 1);
                if (StructSP[i].RangeMax.Trim() != "")
                    WriteDouble(Convert.ToDouble(StructSP[i].RangeMax.Trim()), ref ClassCS.gd_Byte, i + Pos, 2);
            }
            #endregion

            #region 第86-8A(十六进制)行 检验项目名称
            Pos = 134;
            for (int i = 0; i < 5; i++)
            {
                if (StructSP[i].ProName.Trim() != "")
                {
                    byteArray = System.Text.Encoding.Default.GetBytes(StructSP[i].ProName.Trim());
                    if (byteArray.Length > 16)
                    {
                        string str = string.Format("输入的 结果{0} 的项目名称大于设定的长度(8个汉字)，请重新输入！", i + 1);
                        Busiclass.MsgError(str);
                        return;
                        //break;
                    }
                    for (int j = 0; j < byteArray.Length; j++)
                        ClassCS.gd_Byte[i + Pos, j] = byteArray[j];
                }
            }
            #endregion

            #region 第8B(十六进制)行 表示产品批号前缀
            byteArray = System.Text.Encoding.Default.GetBytes(tb_PreBatch.Text);
            if (byteArray.Length > 16)
            {
                Busiclass.MsgError("输入的 产品批号前缀 大于设定的长度(8个汉字),请重新输入！ ");
                return;
            }
            for (int i = 0; i < byteArray.Length; i++)
                ClassCS.gd_Byte[139, i] = byteArray[i];
            #endregion


            #region 第8DH-B6H行开始,飞测plus的定标数据           
            #region 第8DH-B6H(十六进制)行 6个温度定标曲线的方程系数, 项目1
            for (int i = 0; i < 6;i++ )
            {
                for (int j = 0; j < 7; j++)
                {
                    if (frmTCalib.a0[i]!=null)
                    {
                        WriteDoubleNew(frmTCalib.a0[i][j], 0x8d + 7 * i + j, 0);//曲线1
                        WriteDoubleNew(frmTCalib.a1[i][j], 0x8d + 7 * i + j, 8);//曲线1
                    }
                    
                }
            }
            #endregion
            #region 16个浓度值(只曲线1)
            for (int i = 0; i < 8; i++)
            {
                WriteDoubleNew(frmTCalib.ld_XArray[0][2 * i], 0x06 + i, 0);
                WriteDoubleNew(frmTCalib.ld_XArray[0][2 * i + 1], 0x06 + i, 8);
            }
            // 新增曲线2浓度值
            for (int i = 0; i < 8; i++)
            {
                WriteDoubleNew(frmTCalib.ld_XArray[1][2 * i], 0xde + i, 0);
                WriteDoubleNew(frmTCalib.ld_XArray[1][2 * i + 1], 0xde + i, 8);
            }
            #endregion
            // 是否是多曲线，存储多曲线数值
            bool bMoreCurveFlag = Convert.ToBoolean(frmTCalib.GetMoreCurveFlag());
            int iHasMoreCurvePos = 0xe6;
            // flag值
            ClassCS.gd_Byte[iHasMoreCurvePos, 8] = bMoreCurveFlag ? (byte)1 : (byte)0;
            if (bMoreCurveFlag)
            {
                // A值
                WriteDoubleNew(frmTCalib.GetMoreCurveAValue(), iHasMoreCurvePos, 0);                
                // 测试值
                ClassCS.gd_Byte[iHasMoreCurvePos, 9] = (byte)frmTCalib.GetTestValue();
                // 分子
                ClassCS.gd_Byte[iHasMoreCurvePos, 10] = (byte)frmTCalib.GetTestMolecularValue();
                // 分母
                ClassCS.gd_Byte[iHasMoreCurvePos, 11] = (byte)frmTCalib.GetTestDenominatorValue();
                // AFlag
                ClassCS.gd_Byte[iHasMoreCurvePos, 12] = (byte)frmTCalib.GetMoreCurveAFlag();
            }
            // 是否定性，1为定性，0为非定性
            if (m_pNatureProjectCBox.Checked)
            {
                ClassCS.gd_Byte[iHasMoreCurvePos, 13] = 1;
            }
            else
            {
                ClassCS.gd_Byte[iHasMoreCurvePos, 13] = 0;
            }

            //首位浓度,分段点浓度,分段点TC
            for (int i = 0; i < 6;i++ )
            {
                WriteDoubleNew(frmTCalib.XFirst[i], 0xb7 + 2 * i, 0);
                WriteDoubleNew(frmTCalib.XLast[i], 0xb7 + 2 * i + 1, 0);
                WriteDoubleNew(frmTCalib.XMid[i], 0xb7 + 2 * i, 8);
                WriteDoubleNew(frmTCalib.YFirst[i], 0xb7 + 2 * i + 1, 8);// 第一个TC值
            }
            //标准点数, 温度值
            for (int i = 0; i < 6; i++)
            {
                ClassCS.gd_Byte[0xc3, i] = (byte)frmTCalib.li_CountP[i];
                ClassCS.gd_Byte[0xc3, 8 + i] = frmTCalib.iTemp[i];
            }
            //是否去对数,分段点位置
            for (int i = 0; i < 6;i++ )
            {
                ClassCS.gd_Byte[i, 0] = (byte)frmTCalib.mLog[i];//1为取对数
                ClassCS.gd_Byte[i, 1] = (byte)frmTCalib.mSubPt[i];//0为不分段
            }
            //加样冲顶提示是否启用
            if (cb_OverflowP.Checked)// 启用冲顶提示: 低半字节存是否冲顶,高半字节存加样提示
            {
                if (cb_ShortageP.Checked)
                    ClassCS.gd_Byte[0, 7] = 0x00;//0为提示
                else
                    ClassCS.gd_Byte[0, 7] = 0x10;
            }
            else //不启用: 1
            {
                if (cb_ShortageP.Checked)
                    ClassCS.gd_Byte[0, 7] = 0x01;//0为提示
                else
                    ClassCS.gd_Byte[0, 7] = 0x11;
            }
            //冲顶加样峰值名称
            ClassCS.gd_Byte[0, 8] = (byte)(cbBoxOverflowP.SelectedIndex + 1);
            ClassCS.gd_Byte[0, 9] = (byte)(cbBoxShortageP.SelectedIndex + 1);
            //冲顶临界值
            Pos = 0;
            string OverflowP = Busiclass.ConvertString(tb_OverflowP.Text, 10, 16).PadLeft(6, '0');
            ClassCS.gd_Byte[Pos, 0x0a] = Convert.ToByte(OverflowP.Substring(0, 2), 16);
            ClassCS.gd_Byte[Pos, 0x0b] = Convert.ToByte(OverflowP.Substring(2, 2), 16);
            ClassCS.gd_Byte[Pos, 0x0c] = Convert.ToByte(OverflowP.Substring(4, 2), 16);
            //加样临界值 0-260000
            string ShortageP = Busiclass.ConvertString(tb_ShortageP.Text, 10, 16).PadLeft(6, '0');
            ClassCS.gd_Byte[Pos, 0x0d] = Convert.ToByte(ShortageP.Substring(0, 2), 16);
            ClassCS.gd_Byte[Pos, 0x0e] = Convert.ToByte(ShortageP.Substring(2, 2), 16);
            ClassCS.gd_Byte[Pos, 0x0f] = Convert.ToByte(ShortageP.Substring(4, 2), 16);

       #endregion

            #region 第1F行 0H-1F FH 三联卡控制命令
            if (cbBoxProCount.SelectedIndex == 1 || cbBoxProCount.SelectedIndex == 2)//多项目卡才写CYQ 2016.5.12
            {
                Pos = 31; //496
                n = 0;
                ClassCS.gd_Byte[Pos, n++] = ClassCS.gd_Byte[32, 0];//条码号的产品代码，弃用，存储为条码号
                ClassCS.gd_Byte[Pos, n++] = ClassCS.gd_Byte[32, 1];//条码号的年月，弃用，存储为条码号
                ClassCS.gd_Byte[Pos, n++] = ClassCS.gd_Byte[32, 2];//条码号的流水号，弃用，存储为条码号
                ClassCS.gi_cbBoxProCount = Convert.ToByte(cbBoxProCount.Text, 10);

                ClassCS.gd_Byte[Pos, n++] = (byte)ClassCS.gi_cbBoxProCount;//项目数
                //第1个项目的分子/分母
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte((cm_Fz.SelectedIndex + 1).ToString(), 10);//存放分子选择的序号
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte((cm_Fm.SelectedIndex + 1).ToString(), 10);//存放分母选择的序号

                //第4 峰值区间,取点数
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_X4Begin.Text, 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_X4End.Text, 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_X4Count.Text, 10);
            }
            else
            {// 单项目
                ClassCS.gi_cbBoxProCount = 1;
                ClassCS.gd_Byte[31, 3] = (byte)ClassCS.gi_cbBoxProCount;//项目数
            }
            
            //飞测2 冲顶加样取峰的位置
            ClassCS.gd_Byte[31, 9] = (byte)(cbBoxOverflow.SelectedIndex + 1);
            ClassCS.gd_Byte[31, 10] = (byte)(cbBoxShortage.SelectedIndex + 1);
            #endregion
           
            #region 扩展三联卡 第FF行 扩展 多项目的名称,显示结果
            //最后一行：0字节存 结果的个数， 1字节开始存
            //单个结果的协议包：最小比值+ "&" +小于最小的显示+ "&" +最大比值+ "&" +大于最大的显示+ "&" +中间值的显示结果 + 间隔符## +.......... +整个结束符%%%%
            string ls_Result = "";
            int li_ResultCount = 0;
            for (int i = 0; i < 5; i++)//最多5个项目??
            {
                if (StructSP[i].ProName.Trim() != "" && StructSP[i].ProName.Trim() != "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0")
                {
                    li_ResultCount++;
                    ls_Result += StructSP[i].Min + "&" + StructSP[i].MinShow + "&" + StructSP[i].Max + "&" + StructSP[i].MaxShow + "&" + StructSP[i].MidShow + "##";
                }
            }
            //ClassCS.gd_Byte[255, 0] = (byte)li_ResultCount;
            ls_Result = li_ResultCount.ToString() + ls_Result + "%%%%"; //结果的个数+项目参数(检测范围)+结束符
            byteArray = System.Text.Encoding.Default.GetBytes(ls_Result);

            n = 0;
            int m = 1;
            for (int i = 0; i < byteArray.Length; i++) //最后一行 存15个字节
            {
                ClassCS.gd_Byte[255 - m + 1, n] = byteArray[i];
                n++;
                if (i == (m * 16 - 1))
                {
                    n = 0;
                    m++;
                }
            }
            /*
            n = 0;
            int m = 1;
            for (int i = 0; i < byteArray.Length; i++) //最后一行 存15个字节
            {
                ClassCS.gd_Byte[255 - m + 1, n] = byteArray[i];
                n++;
                if (i == (m * 16 - 1))
                {
                    n = 0;
                    m++;
                }
            }
             */
            #endregion

            WriteHex();
        }
        #endregion

        #region 写double型的数据 WriteDouble
        /// <summary>
        ///  写double型的数据
        /// </summary>
        /// <param name="X">当前值</param>
        /// <param name="By">写入的地方</param>
        /// <param name="n">写入的行 </param>
        /// <param mode="n">写入类型 0:表示保留8位有效数字(0-7)，第15位 位标志位
        /// 0-7位保存数据的去掉小数点和小数点前的0后的10进制,位标志位保存的是幂数转化成的有符号16进制(即是负幂,则最高一位置1)
        /// 1: 表示保留7位有效数字(0-6)，第7位 位标志位
        /// 2: 表示保留7位有效数字(8-14)，第15位 位标志位</param>
        private void WriteDouble(double x, ref byte[,] By, int n, int mode)
        {
            if (x == 0)
                return;

            int Row = n;
            //y值 0.085298699-> 08 05 02 09 08 07 ->8.52987 *10~2 =   0.085298699
            //double x = Convert.ToDouble(textBox6.Text); //0.082;
            double j = 1;//幂次
            int PlusminusSign = -1;//幂的正负
            if (x >= 1 && x < 10)
            {
                j = 0;
            }
            else if (x < 1)
            {
                PlusminusSign = 1; //负
                while (true)
                {
                    double data = x * Math.Pow(10, j); //j * 10;
                    if (data >= 1 && data <= 10)
                        break;
                    j++;
                }
            }
            else if (x >= 10)
            {
                PlusminusSign = 0;//正
                while (true)
                {
                    double data = x / Math.Pow(10, j);
                    if (data >= 1 && data < 10)
                        break;
                    j++;
                }
            }
            if (mode == 0)
            {
                for (int i = 0; i < 16; i++)
                    By[Row, i] = 0;
                string str = x.ToString().Replace(".", "").TrimStart('0');
                for (int i = 0; i < str.Length; i++)
                {
                    if (i > 7)//保留8位 有效数
                    {
                        /*
                        if (Convert.ToInt32(str.Substring(i, 1)) > 4)
                            By[Row, i-1] =(byte)(Convert.ToInt32(str.Substring(i - 1, 1)) + 1); //前面的最后一位 四舍五入
                        break;
                         */
                        //modi by zhou zhang kui 2012-10-08 
                        //如果原来的第最后以为有效数字（第8个位）为9，如果第9个大于4的时候，
                        //第8个位加1后就变成了10 -> 0A 
                        //读取的时候就变成了10，则多了一个数，原本的是8位 变成了9位，导致小数点位置出错
                        //1.1011519946828 写入->1101151A 读取->110115110

                        if (By[Row, i - 1] < 9)
                        {
                            if (Convert.ToInt32(str.Substring(i, 1)) > 4)
                                By[Row, i - 1] = (byte)(Convert.ToInt32(str.Substring(i - 1, 1)) + 1); //前面的最后一位 四舍五入
                        }
                        break;
                    }
                    else
                        By[Row, i] = Convert.ToByte(str.Substring(i, 1));//0-7
                }
                //转换成二进制，做标示位
                string Pow = Busiclass.ConvertString(j.ToString(), 10, 2).PadLeft(7, '0');
                if (Pow.Length > 7)
                    Pow = Pow.Substring(Pow.Length - 7, 7);//只取后7位 

                if (PlusminusSign == 0)  //正 10的幂（该字节最高位是符号位） 0 正，1 负
                    By[Row, 15] = Convert.ToByte(Busiclass.ConvertString("0" + Pow, 2, 10));
                else if (PlusminusSign == 1)//负
                    By[Row, 15] = Convert.ToByte(Busiclass.ConvertString("1" + Pow, 2, 10));
                else
                    By[Row, 15] = 0;
            }

            else if (mode == 1)//0-6位存检测范围的低范围(7位保存幂数)
            {
                for (int i = 0; i < 8; i++)
                    By[Row, i] = 0;
                string str = x.ToString().Replace(".", "").TrimStart('0');
                for (int i = 0; i < str.Length; i++)
                {
                    if (i > 6)//保留7位 有效数
                    {
                        if (Convert.ToInt32(str.Substring(i, 1)) > 4)
                            By[Row, i - 1] = (byte)(Convert.ToInt32(str.Substring(i - 1, 1)) + 1);  //前面的最后一位 四舍五入
                        break;
                    }
                    else
                        By[Row, i] = Convert.ToByte(str.Substring(i, 1));//0-6
                    /*
                    if (i > 6)//保留7位 有效数
                        break;
                    By[Row, i] = Convert.ToByte(str.Substring(i, 1));
                     */
                }
                //转换成二进制，做标示位
                string Pow = Busiclass.ConvertString(j.ToString(), 10, 2).PadLeft(7, '0');
                if (Pow.Length > 7)
                    Pow = Pow.Substring(Pow.Length - 7, 7);//只取后7位 
                if (PlusminusSign == 0)  //正 10的幂（该字节最高位是符号位） 0 正，1 负
                    By[Row, 7] = Convert.ToByte(Busiclass.ConvertString("0" + Pow, 2, 10));
                else if (PlusminusSign == 1)//负
                    By[Row, 7] = Convert.ToByte(Busiclass.ConvertString("1" + Pow, 2, 10));
                else
                    By[Row, 7] = 0;
            }
            else if (mode == 2)//8-14位存检测范围的低范围(15位保存幂数)
            {
                for (int i = 8; i < 16; i++)
                    By[Row, i] = 0;
                string str = x.ToString().Replace(".", "").TrimStart('0');
                for (int i = 0; i < str.Length; i++)
                {
                    if (i > 6)//保留7位 有效数
                    {
                        if (Convert.ToInt32(str.Substring(i, 1)) > 4)
                            By[Row, i + 8 - 1] = (byte)(Convert.ToInt32(str.Substring(i - 1, 1)) + 1); //前面的最后一位 四舍五入
                        break;
                    }
                    else
                        By[Row, i + 8] = Convert.ToByte(str.Substring(i, 1));//8-14
                    /*
                    if (i > 6)//保留7位 有效数
                        break;
                    By[Row, i + 8] = Convert.ToByte(str.Substring(i, 1));
                     */
                }
                //转换成二进制，做标示位
                string Pow = Busiclass.ConvertString(j.ToString(), 10, 2).PadLeft(7, '0');
                if (Pow.Length > 7)
                    Pow = Pow.Substring(Pow.Length - 7, 7);//只取后7位 
                if (PlusminusSign == 0)  //正 10的幂（该字节最高位是符号位） 0 正，1 负
                    By[Row, 15] = Convert.ToByte(Busiclass.ConvertString("0" + Pow, 2, 10));
                else if (PlusminusSign == 1)//负
                    By[Row, 15] = Convert.ToByte(Busiclass.ConvertString("1" + Pow, 2, 10));
                else
                    By[Row, 15] = 0;
            }
        }
        private void WriteDoubleNew(double a, int row, int btOffset)//新方式写double型数据 btOffset:偏移的字节数(0或8)
        {
            byte[] btArray = BitConverter.GetBytes(a);
            if (btArray.Length!=8)
            {
                MessageBox.Show("double型转字节错误!");
            }
            for (int i = 0; i < 8;i++ )
            {
                ClassCS.gd_Byte[row, btOffset + i] = btArray[i];
            }
        }
//         private void WriteDoubleNew(double a, int row, int btOffset)//新方式写double型数据 btOffset:偏移的字节数(0或8)
//         {
//             if (a < 0)
//             {
//                 ClassCS.gd_Byte[row, btOffset] = 1;//负数写1
//             }
//             string sCoeff = a.ToString("F9");//最多保存4字节
//             int pId = sCoeff.IndexOf(".");
//             int iZS = 0, iXS = 0;
//             if (pId != -1)
//             {
//                 iZS = int.Parse(sCoeff.Substring(0, pId));
//                 iXS = int.Parse(sCoeff.Substring(pId + 1));
//             }
//             ClassCS.gd_Byte[row, btOffset + 1] = (byte)(iZS >> 16);
//             ClassCS.gd_Byte[row, btOffset + 2] = (byte)(iZS >> 8);
//             ClassCS.gd_Byte[row, btOffset + 3] = (byte)(iZS & 0xff);
//             ClassCS.gd_Byte[row, btOffset + 4] = (byte)(iXS >> 24);//小数
//             ClassCS.gd_Byte[row, btOffset + 5] = (byte)(iXS >> 16);
//             ClassCS.gd_Byte[row, btOffset + 6] = (byte)(iXS >> 8);
//             ClassCS.gd_Byte[row, btOffset + 7] = (byte)(iXS);
//         }
        #endregion
        

        //读取hex文件
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.DefaultExt = "Hex";
                openDialog.Filter = "Hex文件|*.Hex";
                //openDialog.InitialDirectory = Application.StartupPath;
                openDialog.RestoreDirectory = true;//打开对话框默认到上一次路径下
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    init();
                    ld_XArray = new double[32];
                    ld_YArray = new double[32];
                    System.IO.FileStream fs = new System.IO.FileStream(openDialog.FileName, System.IO.FileMode.Open);
                    bt_ClearTemp.PerformClick();
                    ClassCS.by = new byte[256 * 2 * 16];
                    fs.Read(ClassCS.by, 0, ClassCS.by.Length);//ClassCS.by是hex的所有数据
                    fs.Close();

                    ClassCS.gd_Byte = new byte[256 * 2, 16];//CYQ2016.8.9改,单项多项统一用这个变量

                    for (int i = 0; i < 256 * 2; i++)//0x20之前的是老得版本的数据
                    {
                        for (int j = 0; j < 16; j++)
                        {
                            ClassCS.gd_Byte[i, j] = ClassCS.by[16 * i + j];//转换成行列二维数组
                        }
                    }
//                     for (int i = 256; i < 256 * 2; i++)//后256行
//                     {
//                         for (int j = 0; j < 16; j++)
//                         {
//                             ClassCS.gd_Byte[i, j] = ClassCS.by[16 * i + j];                           
//                         }
//                     }
                    ClassCS.iniFileName = openDialog.FileName.Replace(".Hex", ".ini");
                    if (!System.IO.File.Exists(ClassCS.iniFileName))
                    {
                        ClassCS.bHaveIni = false;
                        MessageBox.Show("没有对应改hex的ini文件,将读取不了温控拟合原始参数!");
                    }
                    else
                    {
                        ClassCS.bHaveIni = true;
                        System.IO.File.Copy(ClassCS.iniFileName, ClassCS.iniTempName, true);//从hex对应ini文件拷贝到temp.ini
                    }
                    ReadHex(ClassCS.by);
                    ClassCS.gi_Read = 1;//已读标志
                }
            }
            catch (System.Exception ex)
            {
                ClassCS.gi_Read = 0;
                Busiclass.MsgError("读取Hex文件出错，请检查hex文件是否正确？" + ex.Message.ToString());
            }

        }

        #region  ReadHex 读取Hex文件
        /// <summary>
        /// 读取Hex文件
        /// </summary>
        private void ReadHex(byte[] by)
        {
            string str = "";
            int Pos = 512;
            int n = 0;
            byte byd = 0;
            // 读取多曲线属性
            int iNatureProjectPos = 0xe6;// flag值

            int iNatureProject = ClassCS.gd_Byte[iNatureProjectPos, 13];
            if (iNatureProject == 1)
            {
                m_pNatureProjectCBox.Checked = true;
            }
            else
            {
                m_pNatureProjectCBox.Checked = false;
            }

            #region 1F 0H-1F FH 三联卡控制命令
            //项目数
            ClassCS.gi_cbBoxProCount = ClassCS.gd_Byte[31, 3];
            bLoadHex = true;
            if (ClassCS.gi_cbBoxProCount == 0)
            {
                cbBoxProCount.SelectedIndex = 0;
            }
            else
            {
                cbBoxProCount.SelectedIndex = ClassCS.gi_cbBoxProCount - 1;
            }
            bLoadHex = false;
            ClassCS.isinit[0] = false;
            ClassCS.isinit[1] = false;
            ClassCS.isinit[2] = false;
            switch (ClassCS.gi_cbBoxProCount)
            {
                case 2:
                    ClassCS.isinit[0] = true;
                    break;
                case 3:
                    ClassCS.isinit[0] = true;
                    ClassCS.isinit[1] = true;
                    break;
                default:  
                    break;
            }

            //第1个项目的分子/分母
            cm_Fz.SelectedIndex = ClassCS.gd_Byte[31, 4] - 1;
            cm_Fm.SelectedIndex = ClassCS.gd_Byte[31, 5] - 1;

            tb_X4Begin.Text = ClassCS.gd_Byte[31, 6].ToString();
            tb_X4End.Text = ClassCS.gd_Byte[31, 7].ToString();
            tb_X4Count.Text = ClassCS.gd_Byte[31, 8].ToString();
            //冲顶加样峰值名称
            if (ClassCS.gd_Byte[31, 9] == 255 || ClassCS.gd_Byte[31, 9] == 0)
                cbBoxOverflow.Text = "T2";
            else //if()
                cbBoxOverflow.SelectedIndex = ClassCS.gd_Byte[31, 9] - 1;
            if (ClassCS.gd_Byte[31, 10] == 255 || ClassCS.gd_Byte[31, 10] == 0)
                cbBoxShortage.Text = "C";
            else
                cbBoxShortage.SelectedIndex = ClassCS.gd_Byte[31, 10] - 1;
            #endregion

            #region 产品代码行 取200H 位置的数据  20H行的数据
            Pos = 512;
            string ls_One = Busiclass.ConvertString(by[Pos++].ToString(), 10, 2).PadLeft(8, '0');
            string ls_Tow = Busiclass.ConvertString(by[Pos++].ToString(), 10, 2).PadLeft(8, '0');
            string ls_Three = Busiclass.ConvertString(by[Pos++].ToString(), 10, 2).PadLeft(8, '0');
            byte ExtCodeFlag = by[0x1e0];//6位条码标志字节
            if (ExtCodeFlag == 0x11)//按新的规则去反求产品代码,年月,流水号
            {
                cm_Produce.SelectedIndex = Convert.ToInt32(Busiclass.ConvertString(ls_One, 2, 10));
                cm_Year.SelectedIndex = Convert.ToInt32(Busiclass.ConvertString(ls_Tow.Substring(0, 4), 2, 10));
                cm_Month.SelectedIndex = Convert.ToInt32(Busiclass.ConvertString(ls_Tow.Substring(4, 4), 2, 10));
                cm_SerialNumber.SelectedIndex = Convert.ToInt32(Busiclass.ConvertString(ls_Three.Substring(0, 5), 2, 10));
            }
            else
            {
                cm_Produce.SelectedIndex = Convert.ToInt32(Busiclass.ConvertString(ls_One.Substring(0, 5), 2, 10));
                cm_Year.SelectedIndex = Convert.ToInt32(Busiclass.ConvertString(ls_One.Substring(5, 3) + ls_Tow.Substring(0, 1), 2, 10));
                cm_Month.SelectedIndex = Convert.ToInt32(Busiclass.ConvertString(ls_Tow.Substring(1, 4), 2, 10));
                cm_SerialNumber.SelectedIndex = Convert.ToInt32(Busiclass.ConvertString(ls_Tow.Substring(5, 3) + ls_Three.Substring(0, 1), 2, 10));
            }
            cm_T1.SelectedIndex = by[Pos++] - 1;
            cm_T2.SelectedIndex = by[Pos++] - 1;
            cm_C.SelectedIndex = by[Pos++] - 1;
            cm_TC.SelectedIndex = by[Pos++];

            gi_ConcentrationCount = by[Pos++];

            cm_Log.SelectedIndex = by[Pos++];
            byd = by[Pos++];
            if (byd == 1)
                cm_SubCount.SelectedIndex = 0;
            else
                cm_SubCount.Text = byd.ToString();
            byd = by[Pos++];
            cm_Temperature.SelectedIndex = byd >> 4;
            cm_AreaUse.SelectedIndex = byd & 0x0F;

            tb_CountDownTime.Text = by[Pos++].ToString();
            byd = by[Pos++];
            if (byd >> 4 == 0)
                cb_Shortage.Checked = true;
            else
                cb_Shortage.Checked = false;
            if ((byd & 0x0F) == 0)
                cb_Overflow.Checked = true;
            else
                cb_Overflow.Checked = false;

            tb_TestTime.Text = by[Pos++].ToString();
            tb_DecimalsDigit.Text = by[Pos++].ToString();
            cm_Type.SelectedIndex = by[Pos++];
            #endregion

            #region 分段的起始点 取210H 位置的数据  21H行的数据
            if (cm_SubCount.Text == "不分段")
            {
                cm_Method0.SelectedIndex = by[544];//0x220
                cm_Prerequisite0.SelectedIndex = by[560];//0x230
            }
            else
            {
                #region 0x211 H 分段点起始位置
                Pos = 529;//0x211 H
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection1.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection2.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                /*
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection3.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection4.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection5.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection6.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection7.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection8.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection9.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection10.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection11.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection12.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection13.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection14.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                str = Busiclass.ConvertString(by[Pos++].ToString(), 10, 16).PadLeft(2, '0');
                tb_Subsection15.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                 */
                #endregion

                #region 0x221 H 每段对应的拟合方法
                Pos = 545;//0x221 H
                cm_Method1.SelectedIndex = by[Pos++];
                cm_Method2.SelectedIndex = by[Pos++];
                /*
                cm_Method3.SelectedIndex = by[Pos++];
                cm_Method4.SelectedIndex = by[Pos++];
                cm_Method5.SelectedIndex = by[Pos++];
                cm_Method6.SelectedIndex = by[Pos++];
                cm_Method7.SelectedIndex = by[Pos++];
                cm_Method8.SelectedIndex = by[Pos++];
                cm_Method9.SelectedIndex = by[Pos++];
                cm_Method10.SelectedIndex = by[Pos++];
                cm_Method11.SelectedIndex = by[Pos++];
                cm_Method12.SelectedIndex = by[Pos++];
                cm_Method13.SelectedIndex = by[Pos++];
                cm_Method14.SelectedIndex = by[Pos++];
                cm_Method15.SelectedIndex = by[Pos++];
                 */
                #endregion

                #region 0x231 H 每段对应的限制条件
                Pos = 561;//0x231 H
                cm_Prerequisite1.SelectedIndex = by[Pos++];
                cm_Prerequisite2.SelectedIndex = by[Pos++];
                /*
                cm_Prerequisite3.SelectedIndex = by[Pos++];
                cm_Prerequisite4.SelectedIndex = by[Pos++];
                cm_Prerequisite5.SelectedIndex = by[Pos++];
                cm_Prerequisite6.SelectedIndex = by[Pos++];
                cm_Prerequisite7.SelectedIndex = by[Pos++];
                cm_Prerequisite8.SelectedIndex = by[Pos++];
                cm_Prerequisite9.SelectedIndex = by[Pos++];
                cm_Prerequisite10.SelectedIndex = by[Pos++];
                cm_Prerequisite11.SelectedIndex = by[Pos++];
                cm_Prerequisite12.SelectedIndex = by[Pos++];
                cm_Prerequisite13.SelectedIndex = by[Pos++];
                cm_Prerequisite14.SelectedIndex = by[Pos++];
                cm_Prerequisite15.SelectedIndex = by[Pos++];
                 */
                #endregion
            }
            #endregion


            #region 读取分段的起始点 取21DH- 21FH位置的数据 T2段判断是否冲顶 add by zhou zhang kui 2012-10-09
            string date = Convert.ToString(Convert.ToInt32(by[541]), 16).PadLeft(2, '0') +
                          Convert.ToString(Convert.ToInt32(by[542]), 16).PadLeft(2, '0') +
                          Convert.ToString(Convert.ToInt32(by[543]), 16).PadLeft(2, '0');
            tb_Overflow.Text = Convert.ToInt32(date, 16).ToString();
            #endregion

            #region 读取分段的起始点 取22DH- 22FH位置的数据 C段判断是否加样 add by zhou zhang kui 2012-10-09
            date = Convert.ToString(Convert.ToInt32(by[557]), 16).PadLeft(2, '0') +
                          Convert.ToString(Convert.ToInt32(by[558]), 16).PadLeft(2, '0') +
                          Convert.ToString(Convert.ToInt32(by[559]), 16).PadLeft(2, '0');
            tb_Shortage.Text = Convert.ToInt32(date, 16).ToString();
            #endregion

            #region 240H- 430H读取X的值 浓度    440H- 630H读取Y的值 对应的测试值
            for (int i = 0; i < gi_ConcentrationCount; i++)
            {
                ld_XArray[i] = ReadDouble(by, 576 + i * 16, 0);
                ld_YArray[i] = ReadDouble(by, 1088 + i * 16, 0);
            }
            for (int i = 0; i < gi_ConcentrationCount; i++)
            {
                listView1.Items[i].SubItems[0].Text = (i + 1).ToString();
                listView1.Items[i].SubItems[1].Text = Convert.ToDouble(ld_XArray[i].ToString("F8")).ToString();
                listView1.Items[i].SubItems[2].Text = Convert.ToDouble(ld_YArray[i].ToString("F8")).ToString();
            }
            #endregion


            #region 740H 读取血清、血浆子数
            Pos = 1856;
            tb_Serum.Text = Convert.ToDouble(ReadDouble(by, 1856, 0).ToString("F8")).ToString();
            #endregion

            #region 750H 全血子数
            Pos += 16;
            tb_WholeBlood.Text = Convert.ToDouble(ReadDouble(by, Pos, 0).ToString("F8")).ToString();
            #endregion

            #region 760H 尿液子数
            Pos += 16;
            tb_UrineValue.Text = Convert.ToDouble(ReadDouble(by, Pos, 0).ToString("F8")).ToString();
            #endregion

            #region 770H 粪便子数
            Pos += 16;
            tb_Excrement.Text = Convert.ToDouble(ReadDouble(by, Pos, 0).ToString("F8")).ToString();
            #endregion

            #region 780H 质检子数
            Pos += 16;
            tb_Quality.Text = Convert.ToDouble(ReadDouble(by, Pos, 0).ToString("F8")).ToString();
            #endregion

            #region 790H a
            Pos += 16;
            tb_a.Text = Convert.ToDouble(ReadDouble(by, Pos, 0).ToString("F8")).ToString();
            #endregion

            #region 7A0H b
            Pos += 16;
            tb_b.Text = Convert.ToDouble(ReadDouble(by, Pos, 0).ToString("F8")).ToString();
            #endregion

            #region 7B0H a1
            Pos += 16;
            tb_a1.Text = Convert.ToDouble(ReadDouble(by, Pos, 0).ToString("F8")).ToString();
            #endregion

            #region 7C0H b1
            Pos += 16;
            tb_b1.Text = Convert.ToDouble(ReadDouble(by, Pos, 0).ToString("F8")).ToString();
            #endregion

            #region 7D0H 区域
            str = "";
            Pos += 16; //2000
            for (int i = 0; i < 16; i++)
                str += Busiclass.ConvertString(by[Pos + i].ToString(), 10, 16).PadLeft(2, '0');
            tb_Area.Text = ClassCS.UnHex(str, "gb2312");
            #endregion

            #region 7E0H 报告单标题
            str = "";
            Pos += 16;
            for (int i = 0; i < 16; i++)
                str += Busiclass.ConvertString(by[Pos + i].ToString(), 10, 16).PadLeft(2, '0');
            tb_TitleName.Text = ClassCS.UnHex(str, "gb2312");
            #endregion

            #region 7F0H 单位
            str = "";
            Pos += 16;
            for (int i = 0; i < 16; i++)
                str += Busiclass.ConvertString(by[Pos + i].ToString(), 10, 16).PadLeft(2, '0');
            tb_Unit.Text = ClassCS.UnHex(str, "gb2312");
            #endregion

            #region 800H 1-3 峰值位置
            str = "";
            Pos += 16;
            int mm = 0;
            tb_T1Begin.Text = by[Pos + mm].ToString();
            mm++;
            tb_T1End.Text = by[Pos + mm].ToString();
            mm++;
            tb_T2Begin.Text = by[Pos + mm].ToString();
            mm++;
            tb_T2End.Text = by[Pos + mm].ToString();
            mm++;
            tb_CBegin.Text = by[Pos + mm].ToString();
            mm++;
            tb_CEnd.Text = by[Pos + mm].ToString();
            mm++;
            tb_T1Count.Text = by[Pos + mm].ToString();
            mm++;
            tb_T2Count.Text = by[Pos + mm].ToString();
            mm++;
            tb_CCount.Text = by[Pos + mm].ToString();
            #endregion

            #region 810H - 850 检测范围
            Pos += 16;//1050
            StructSP[0].RangeMin = ReadDouble(by, Pos, 1).ToString("F8");
            StructSP[0].RangeMax = ReadDouble(by, Pos, 2).ToString("F8");

            Pos += 16;
            StructSP[1].RangeMin = ReadDouble(by, Pos, 1).ToString("F8");
            StructSP[1].RangeMax = ReadDouble(by, Pos, 2).ToString("F8");

            Pos += 16;
            StructSP[2].RangeMin = ReadDouble(by, Pos, 1).ToString("F8");
            StructSP[2].RangeMax = ReadDouble(by, Pos, 2).ToString("F8");

            Pos += 16;
            StructSP[3].RangeMin = ReadDouble(by, Pos, 1).ToString("F8");
            StructSP[3].RangeMax = ReadDouble(by, Pos, 2).ToString("F8");

            Pos += 16;
            StructSP[4].RangeMin = ReadDouble(by, Pos, 1).ToString("F8");
            StructSP[4].RangeMax = ReadDouble(by, Pos, 2).ToString("F8");
            #endregion

            #region 860H - 8A0 检测项目名称
            str = "";
            Pos += 16;//2144
            for (int i = 0; i < 16; i++)
                str += Busiclass.ConvertString(by[Pos + i].ToString(), 10, 16).PadLeft(2, '0');
            tb_ResultName1.Text = ClassCS.UnHex(str, "gb2312");
            StructSP[0].ProName = tb_ResultName1.Text;

            Pos += 16;
            str = "";
            for (int i = 0; i < 16; i++)
                str += Busiclass.ConvertString(by[Pos + i].ToString(), 10, 16).PadLeft(2, '0');
            StructSP[1].ProName = ClassCS.UnHex(str, "gb2312");

            Pos += 16;
            str = "";
            for (int i = 0; i < 16; i++)
                str += Busiclass.ConvertString(by[Pos + i].ToString(), 10, 16).PadLeft(2, '0');
            StructSP[2].ProName = ClassCS.UnHex(str, "gb2312");

            Pos += 16;
            str = "";
            for (int i = 0; i < 16; i++)
                str += Busiclass.ConvertString(by[Pos + i].ToString(), 10, 16).PadLeft(2, '0');
            StructSP[3].ProName = ClassCS.UnHex(str, "gb2312");

            Pos += 16;
            str = "";
            for (int i = 0; i < 16; i++)
                str += Busiclass.ConvertString(by[Pos + i].ToString(), 10, 16).PadLeft(2, '0');
            StructSP[4].ProName = ClassCS.UnHex(str, "gb2312");
            #endregion

            #region  8B0- 8BFH 检测项目名称
            str = "";
            Pos += 16;
            for (int i = 0; i < 16; i++)
                str += Busiclass.ConvertString(by[Pos + i].ToString(), 10, 16).PadLeft(2, '0');
            tb_PreBatch.Text = ClassCS.UnHex(str, "gb2312");
            #endregion


            #region 飞测plus参数
            //读取加样冲顶相关
            byd = by[0x07];//加样和冲顶提示
            if (byd >> 4 == 0)
                cb_ShortageP.Checked = true;
            else
                cb_ShortageP.Checked = false;
            if ((byd & 0x0F) == 0)
                cb_OverflowP.Checked = true;
            else
                cb_OverflowP.Checked = false;
            //冲顶加样峰值名称
            if (by[0x08] == 255 || by[0x08] == 0)
                cbBoxOverflowP.Text = "T2";
            else //if()
                cbBoxOverflowP.SelectedIndex = by[0x08]-1;
            if (by[0x09] == 255 || by[0x09] == 0)
                cbBoxShortageP.Text = "C";
            else
                cbBoxShortageP.SelectedIndex = by[0x09]-1;
            //临界值
            string dateP = Convert.ToString(Convert.ToInt32(by[0x0a]), 16).PadLeft(2, '0') +
                          Convert.ToString(Convert.ToInt32(by[0x0b]), 16).PadLeft(2, '0') +
                          Convert.ToString(Convert.ToInt32(by[0x0c]), 16).PadLeft(2, '0');
            tb_OverflowP.Text = Convert.ToInt32(dateP, 16).ToString();
                  dateP = Convert.ToString(Convert.ToInt32(by[0x0d]), 16).PadLeft(2, '0') +
                          Convert.ToString(Convert.ToInt32(by[0x0e]), 16).PadLeft(2, '0') +
                          Convert.ToString(Convert.ToInt32(by[0x0f]), 16).PadLeft(2, '0');
            tb_ShortageP.Text = Convert.ToInt32(dateP, 16).ToString();
            //读取方程系数
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7;j++ )
                {
                    frmTCalib.a0[i][j] = ReadDoubleNew(0x8d + 7 * i + j, 0);//曲线1
                    frmTCalib.a1[i][j] = ReadDoubleNew(0x8d + 7 * i + j, 8);//曲线2
                }
                
            }
            //读曲线1所有浓度值
            for (int i = 0; i < 6; i++)
            {
                if (frmTCalib.ld_XArray[i] == null)
                {
                    frmTCalib.ld_XArray[i] = new double[16];
                    frmTCalib.ld_YArray[i] = new double[16];
                }
            }
            for (int i = 0; i < 8; i++)
            {
                frmTCalib.ld_XArray[0][2 * i]=ReadDoubleNew(0x06 + i, 0);
                frmTCalib.ld_XArray[0][2 * i + 1] = ReadDoubleNew(0x06 + i, 8);
            }
            //////////////////////////////////////////////////////////////////////////
            // 读项目一曲线2的所有浓度值
            for (int i = 0; i < 8; i++)
            {
                frmTCalib.ld_XArray[1][2 * i] = ReadDoubleNew(0xde + i, 0);
                frmTCalib.ld_XArray[1][2 * i + 1] = ReadDoubleNew(0xde + i, 8);
            }
            // 读取多曲线属性
            int iHasMoreCurvePos = 0xe6;// flag值
            int iMoreCurveFlag = ClassCS.gd_Byte[iHasMoreCurvePos, 8];
            frmTCalib.SetMoreCurveFlag(iMoreCurveFlag);

            if (Convert.ToBoolean(iMoreCurveFlag))
            {
                // A值
                frmTCalib.SetMoreCurveAValue(ReadDoubleNew(iHasMoreCurvePos, 0));
                // 测试值
                frmTCalib.SetTestValue(ClassCS.gd_Byte[iHasMoreCurvePos, 9]);
                // 分子
                frmTCalib.SetTestMolecularValue(ClassCS.gd_Byte[iHasMoreCurvePos, 10]);
                // 分母
                frmTCalib.SetTestDenominatorValue(ClassCS.gd_Byte[iHasMoreCurvePos, 11]);
                // AFlag
                frmTCalib.SetMoreCurveAFlag(ClassCS.gd_Byte[iHasMoreCurvePos, 12]);
            }
        
            
            //////////////////////////////////////////////////////////////////////////
            //读首尾浓度值
            for (int i = 0; i < 6; i++)
            {
                frmTCalib.li_CountP[i] = by[0xC30 + i];  //第一个项目标准点个数

                frmTCalib.XFirst[i] = ReadDoubleNew(0xb7 + 2 * i, 0);//首浓度
                frmTCalib.XLast[i] = ReadDoubleNew(0xb8 + 2 * i, 0);//尾浓度
                frmTCalib.XMid[i] = ReadDoubleNew(0xb7 + 2 * i, 8);//分段点浓度
                frmTCalib.YFirst[i] = ReadDoubleNew(0xb8 + 2 * i, 8);//分段点TC
                
            }
            //从ini读取定标数据
            if (ClassCS.bHaveIni)
            {
                string sLog = "", sSubSec = "", sSubPt = "", sPrerequiste1 = "", sPrerequiste2 = "", sXArray = "", sYArray = "";
                sLog = ClassIni.ReadIniData("Plus Calib Parameter1", "mLog", ClassCS.iniTempName);
                sSubSec = ClassIni.ReadIniData("Plus Calib Parameter1", "mSubSec", ClassCS.iniTempName);
                sSubPt = ClassIni.ReadIniData("Plus Calib Parameter1", "mSubPt", ClassCS.iniTempName);
                sPrerequiste1 = ClassIni.ReadIniData("Plus Calib Parameter1", "mPrerequiste1", ClassCS.iniTempName);
                sPrerequiste2 = ClassIni.ReadIniData("Plus Calib Parameter1", "mPrerequiste2", ClassCS.iniTempName);
                sXArray = ClassIni.ReadIniData("Plus Calib Parameter1", "mXArray", ClassCS.iniTempName);
                sYArray = ClassIni.ReadIniData("Plus Calib Parameter1", "mYArray", ClassCS.iniTempName);
                string[] arrLog, arrSubSec, arrSubPt, arrPrerequiste1, arrPrerequiste2, arrX, arrY;
                arrLog = sLog.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                arrSubSec = sSubSec.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                arrSubPt = sSubPt.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                arrPrerequiste1 = sPrerequiste1.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                arrPrerequiste2 = sPrerequiste2.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                arrX = sXArray.Split(new char[] { ';' });
                arrY = sYArray.Split(new char[] { ';' });
                if (arrLog.Length == 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        try
                        {
                            frmTCalib.mLog[i] = int.Parse(arrLog[i]);
                        }
                        catch (System.Exception ex)
                        {
                        }
                    }
                }
                if (arrSubSec.Length == 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        try
                        {
                            frmTCalib.mSubSec[i] = int.Parse(arrSubSec[i]);
                        }
                        catch (System.Exception ex)
                        {
                        }
                    }
                }
                if (arrSubPt.Length == 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        try
                        {
                            frmTCalib.mSubPt[i] = int.Parse(arrSubPt[i]);
                        }
                        catch (System.Exception ex)
                        {
                        }
                    }
                }
                if (arrPrerequiste1.Length == 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        try
                        {
                            frmTCalib.Prerequiste1[i] = int.Parse(arrPrerequiste1[i]);
                        }
                        catch (System.Exception ex)
                        {
                        }
                    }
                }
                if (arrPrerequiste2.Length == 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        try
                        {
                            frmTCalib.Prerequiste2[i] = int.Parse(arrPrerequiste2[i]);
                        }
                        catch (System.Exception ex)
                        {
                        }
                    }
                }
                
                if (arrX.Length == 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        try
                        {
                            string[] sX = arrX[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            frmTCalib.li_CountP[i] = sX.Length;
                            for (int j = 0; j < sX.Length; j++)
                            {
                                frmTCalib.ld_XArray[i][j] = double.Parse(sX[j]);
                            }
                        }
                        catch (System.Exception ex)
                        {
                        }
                    }
                }
                if (arrY.Length == 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        try
                        {
                            string[] sY = arrY[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < sY.Length; j++)
                            {
                                frmTCalib.ld_YArray[i][j] = double.Parse(sY[j]);
                            }
                        }
                        catch (System.Exception ex)
                        {
                        }
                    }
                }
            }
            frmTCalib.bRefresh = true;
            
            #endregion

            #region FF 获取 结果1 最后一行开始 打印内容
            //最后一行：0字节存 结果的个数， 1字节开始存
            //单个结果的协议包：最小比值+ "$" +小于最小的显示+ "$" +最大比值+ "$" +大于最大的显示+ "$" +中间值的显示结果 + 间隔符## +.......... +整个结束符%%%%
            Pos = 4080; //0xFF0 H
            string ls_Result = "";
            while (true)
            {
                str = "";
                for (int i = 0; i < 16; i++)
                    str += Busiclass.ConvertString(by[Pos + i].ToString(), 10, 16).PadLeft(2, '0');
                ls_Result += ClassCS.UnHex(str, "gb2312");
                Pos -= 16;
                int li_Pos = ls_Result.IndexOf("%%%%");
                if (li_Pos > -1)
                {
                    int li_ResultCount = Convert.ToInt32(ls_Result.Substring(0, 1));
                    ls_Result = ls_Result.Substring(1, li_Pos - 3);
                    string[] sArray = Regex.Split(ls_Result, "##", RegexOptions.IgnoreCase);//结果的组数
                    for (int i = 0; i < li_ResultCount; i++)
                    {
                        string[] sArrayResult = Regex.Split(sArray[i], "&", RegexOptions.IgnoreCase);//结果的组数
                        if (sArrayResult.Length == 5)
                        {
                            StructSP[i].Min = sArrayResult[0];
                            StructSP[i].MinShow = sArrayResult[1];
                            StructSP[i].Max = sArrayResult[2];
                            StructSP[i].MaxShow = sArrayResult[3];
                            StructSP[i].MidShow = sArrayResult[4];
                        }
                    }
                    iniShowPriner(0);
                    break;
                }
            }
            #endregion

        }
        #endregion


        #region 读取double型的数据 ReadDouble
        /// <summary>
        ///  写double型的数据
        /// </summary>
        /// <param name="X">当前值</param>
        /// <param name="n">写入的行 </param>
        /// <param mode="n">写入类型 0:表示保留8位有效数字(0-7)，第15位 位标志位  
        /// 1: 表示保留7位有效数字(0-6)，第7位 位标志位
        /// 2: 表示保留7位有效数字(8-14)，第15位 位标志位</param>
        private double ReadDouble(byte[] by, int n, int mode)
        {
            double a = 0;
            string Bz = "", str = "";
            int li_Pow = 0, li_Zf = 0;
            if (mode == 0)
            {
                for (int i = 0; i < 8; i++)
                    str += by[n + i].ToString();
                a = Convert.ToDouble(str) / 10000000;//8个有效数字
                Bz = Busiclass.ConvertString(Busiclass.ConvertString(by[n + 15].ToString(), 10, 16), 16, 2).PadLeft(8, '0');
            }
            else if (mode == 1)
            {
                for (int i = 0; i < 7; i++)
                    str += by[n + i].ToString();
                a = Convert.ToDouble(str) / 1000000;//7个有效数字
                Bz = Busiclass.ConvertString(Busiclass.ConvertString(by[n + 7].ToString(), 10, 16), 16, 2).PadLeft(8, '0');
            }
            else if (mode == 2)
            {
                for (int i = 8; i < 15; i++)
                    str += by[n + i].ToString();
                a = Convert.ToDouble(str) / 1000000;//7个有效数字
                Bz = Busiclass.ConvertString(Busiclass.ConvertString(by[n + 15].ToString(), 10, 16), 16, 2).PadLeft(8, '0');
            }
            if (Bz != "00000000")
            {
                li_Zf = Convert.ToInt32(Bz.Substring(0, 1));
                li_Pow = Convert.ToInt32(Busiclass.ConvertString(Bz.Substring(1, 7), 2, 10));
                if (li_Zf == 0)//正次方
                    a = a * Math.Pow(10, li_Pow);
                else
                    a = a * Math.Pow(10, -li_Pow);
            }
            return a;
        }
        private double ReadDoubleNew(int row, int btOffset)//新方式写double型数据 btOffset:偏移的字节数(0或8)
        {
            double a = 0;
            byte[] btArray = new byte[8];//{ 0xCF, 0xF7, 0x39, 0x81, 0xFA, 0x04, 0xCD, 0x3F };
            for (int i = 0; i < 8;i++ )
            {
                btArray[i] = ClassCS.gd_Byte[row, btOffset + i];
            }
            a = BitConverter.ToDouble(btArray, 0);
            return a;
        }
        private double ReadDoubleNew1(int row, int btOffset)//新方式写double型数据 btOffset:偏移的字节数(0或8)
        {
            double a = 0;
            int iZS = 0, iXS = 0;
            iZS = (ClassCS.gd_Byte[row, btOffset + 1] << 16) + (ClassCS.gd_Byte[row, btOffset + 2] << 8) + ClassCS.gd_Byte[row, btOffset + 3];
            iXS = (ClassCS.gd_Byte[row, btOffset + 4] << 24) + (ClassCS.gd_Byte[row, btOffset + 5] << 16) + (ClassCS.gd_Byte[row, btOffset + 6] << 8) + ClassCS.gd_Byte[row, btOffset + 7];
            string sCoeff = iZS.ToString() + "." + iXS.ToString().PadLeft(9,'0');

            if (ClassCS.gd_Byte[row, btOffset] == 1)//负数
            {
                sCoeff = "-" + sCoeff;
            }
            try
            {
                a = double.Parse(sCoeff);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("读取double数据错误!");
            }
            return a;
        }
        #endregion
        
        private void button5_Click_1(object sender, EventArgs e)
        {
            ClassCS.gi_AreaName = 0;
            frmRegister frm = new frmRegister();
            frm.ShowDialog();
            frm.Dispose();
            if (ClassCS.gi_AreaName == 1)
                tb_Area.Text = ClassCS.AreaName;
        }

        private void tb_Serum_TextChanged(object sender, EventArgs e)
        {
            InitTextChanged(sender, e);
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

        #region  InitTextChanged 内容改变事件
        /// <summary>
        /// 内容改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitTextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).GetType().ToString() == "System.Windows.Forms.TextBox")
            {

                if (((TextBox)sender).Text.Trim() == "")
                    ((TextBox)sender).Text = "0";
            }
        }
        #endregion

        /// <summary>
        /// 只能输入正整数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnlyEnterPlusInt(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void tb_Serum_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusNumber(sender, e);
        }

        private void cm_ResultSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = cm_ResultSelect.SelectedIndex;
            tb_ResultName1.Text = "";
            tb_ResultMin1.Text = "";
            tb_ResultMinShow1.Text = "";
            tb_ResultMaxShow1.Text = "";
            tb_ResultMax1.Text = "";
            tb_ResultMid1.Text = "";
            tb_ResultRangeEnd1.Text = "";
            tb_ResultRangeBegin1.Text = "";
            switch (n)
            {
                case 0:
                    groupBox4.Text = "结果一   计算得出的浓度值：X";
                    break;
                case 1:
                    groupBox4.Text = "结果二   计算得出的浓度值：X";
                    break;
                case 2:
                    groupBox4.Text = "结果三   计算得出的浓度值：X";
                    break;
                case 3:
                    groupBox4.Text = "结果四   计算得出的浓度值：X";
                    break;
                case 4:
                    groupBox4.Text = "结果五   计算得出的浓度值：X";
                    break;
            }
            if (n > -1 && n < 5)
                iniShowPriner(n);
        }

        private void bt_ClearResult_Click(object sender, EventArgs e)
        {
            tb_ResultName1.Text = "";
            tb_ResultMin1.Text = "";
            tb_ResultMinShow1.Text = "";
            tb_ResultMax1.Text = "";
            tb_ResultMaxShow1.Text = "";
            tb_ResultMid1.Text = "";
            tb_ResultRangeBegin1.Text = "";
            tb_ResultRangeEnd1.Text = "";

            StructSP[0].ProName = tb_ResultName1.Text;
            StructSP[0].Min = tb_ResultMin1.Text;
            StructSP[0].MinShow = tb_ResultMinShow1.Text;
            StructSP[0].Max = tb_ResultMax1.Text;
            StructSP[0].MaxShow = tb_ResultMaxShow1.Text;
            StructSP[0].MidShow = tb_ResultMid1.Text;
            StructSP[0].RangeMin = tb_ResultRangeBegin1.Text;
            StructSP[0].RangeMax = tb_ResultRangeEnd1.Text;
        }

        private void tb_ResultRangeBegin1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusNumber(sender, e);
        }

        private void tb_ResultRangeEnd1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusNumber(sender, e);
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

        private void tb_ResultMin1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusNumber(sender, e);
        }

        private void tb_DecimalsDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_CountDownTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_TestTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_T1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_T2_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_T1Count_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_T2Count_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_CCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void cm_Produce_SelectedIndexChanged(object sender, EventArgs e)
        {
            //选择的产品代码<32的, 流水号只能选1-16(id=0-15)
            int idSN = cm_SerialNumber.SelectedIndex;
            if (cm_Produce.SelectedIndex >= 32)
            {
                cm_SerialNumber.Items.Clear();
                for (int i = 0; i < 32; i++)
                {
                    cm_SerialNumber.Items.Add((i + 1).ToString());
                }
                cm_SerialNumber.SelectedIndex = idSN;
            }
            else
            {
                cm_SerialNumber.Items.Clear();
                for (int i = 0; i < 16; i++)
                {
                    cm_SerialNumber.Items.Add((i + 1).ToString());
                }
                cm_SerialNumber.SelectedIndex = idSN >= 16 ? 0 : idSN;
            }
            string sCodeExt = "";
            if (cm_Produce.SelectedIndex < 32 && cm_SerialNumber.SelectedIndex < 16)
                tb_BarCodeNumber.Text = GetBarCode();
            else
                tb_BarCodeNumber.Text = GetBarCodeExt(ref sCodeExt);
            tb_BatchNumber.Text = GetBatch();
        }
        private void cm_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sCodeExt = "";
            if (cm_Produce.SelectedIndex < 32 && cm_SerialNumber.SelectedIndex < 16)
                tb_BarCodeNumber.Text = GetBarCode();
            else
                tb_BarCodeNumber.Text = GetBarCodeExt(ref sCodeExt);
            tb_BatchNumber.Text = GetBatch();
        }

        private void cm_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sCodeExt = "";
            if (cm_Produce.SelectedIndex < 32 && cm_SerialNumber.SelectedIndex < 16)
                tb_BarCodeNumber.Text = GetBarCode();
            else
                tb_BarCodeNumber.Text = GetBarCodeExt(ref sCodeExt);
            tb_BatchNumber.Text = GetBatch();
        }

        private void cm_SerialNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sCodeExt = "";
            if (cm_Produce.SelectedIndex < 32 && cm_Year.SelectedIndex < 16 && cm_SerialNumber.SelectedIndex < 16)
                tb_BarCodeNumber.Text = GetBarCode();
            else
                tb_BarCodeNumber.Text = GetBarCodeExt(ref sCodeExt);
            tb_BatchNumber.Text = GetBatch();
        }

        private void tb_PreBatch_TextChanged(object sender, EventArgs e)
        {
            tb_BatchNumber.Text = GetBatch();
        }

        private void 退出ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void 读取ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.PerformClick();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void groupBox5_Paint_1(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Red;
            groupBox_Paint(sender, e, b, p);
        }

        private void tb_CBegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_T1End_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_T2End_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_CEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void 高级用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 高级用户ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
            frm.Dispose();
            SetShow();
        }

        private void 普通用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassCS.gi_QX = 0;
            SetShow();
        }

        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void tb_Overflow_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void tb_Shortage_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyEnterPlusInt(sender, e);
        }

        private void label93_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Red;
            groupBox_Paint(sender, e, b, p);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ClassCS.gs_SelectProIndex = cbBoxProIndex.Text;
            ClassCS.gi_SelectProIndex = cbBoxProIndex.SelectedIndex + 2;
            frmMultiple frm = new frmMultiple();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void groupBox7_Paint(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Red;
            groupBox_Paint(sender, e, b, p);
        }

        bool bLoadHex = false;
        // 项目选择
        private void cbBoxProCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbBoxProCount.SelectedIndex;
            //SetProIndex(index+1);

            if (index == 1 || index == 2)//选择项目2/3
            {
                SetProIndex(index + 1);
                cbBoxProIndex.Visible = btn_Setting.Visible = true;
                lb_SelItem.Visible = lb_CurrItem.Visible = true;
                cm_Fz.Visible = cm_Fm.Visible = lb_xg.Visible = true;
                cm_TC.Visible = false;
                cm_T1.Visible = cm_T2.Visible = cm_C.Visible = false;
                lb_X4.Visible = tb_X4Begin.Visible = tb_X4End.Visible = tb_X4Count.Visible = label90.Visible = true;
                cbBoxOverflow.Items.Clear(); cbBoxShortage.Items.Clear(); cbBoxOverflowP.Items.Clear(); cbBoxShortageP.Items.Clear();
                //重置峰值下拉选项
                string[] sItems = new string[] { "X1", "X2", "X3", "X4" };
                cbBoxOverflow.Items.AddRange(sItems); cbBoxShortage.Items.AddRange(sItems);
                cbBoxOverflowP.Items.AddRange(sItems); cbBoxShortageP.Items.AddRange(sItems);
                if (!bLoadHex)
                {
                    tb_T1End.Text = "40"; tb_T2Begin.Text = "41"; tb_T2End.Text = "80"; tb_CBegin.Text = "81"; tb_CEnd.Text = "120";
                    cbBoxOverflow.SelectedIndex = 2; //
                    cbBoxShortage.SelectedIndex = 3;
                    cbBoxOverflowP.SelectedIndex = 2;
                    cbBoxShortageP.SelectedIndex = 3;
                }
                cbBoxOverflow.Enabled = true;
                cbBoxShortage.Enabled = true;
                cbBoxOverflowP.Enabled = true;
                cbBoxShortageP.Enabled = true;
            }
            else//选择项目1
            {
                cbBoxProIndex.Visible = btn_Setting.Visible = false;
                lb_SelItem.Visible = lb_CurrItem.Visible = false;
                cm_Fz.Visible = cm_Fm.Visible = lb_xg.Visible = false;
                cm_TC.Visible = true; cm_TC.SelectedIndex = 0;
                cm_T1.Visible = cm_T2.Visible = cm_C.Visible = true;
                cm_T1.SelectedIndex = 0; cm_T2.SelectedIndex = 1; cm_C.SelectedIndex = 2;
                lb_X4.Visible = tb_X4Begin.Visible = tb_X4End.Visible = tb_X4Count.Visible = label90.Visible = false;
                //重置峰值下拉选项
                cbBoxOverflow.Items.Clear(); cbBoxShortage.Items.Clear(); cbBoxOverflowP.Items.Clear(); cbBoxShortageP.Items.Clear();
                string[] sItems = new string[] { "T1", "T2", "C" };
                cbBoxOverflow.Items.AddRange(sItems); cbBoxShortage.Items.AddRange(sItems);
                cbBoxOverflowP.Items.AddRange(sItems); cbBoxShortageP.Items.AddRange(sItems);
                if (!bLoadHex)
                {
                    tb_T1End.Text = "60"; tb_T2Begin.Text = "61"; tb_T2End.Text = "120"; tb_CBegin.Text = "121"; tb_CEnd.Text = "174";
                    cbBoxOverflow.SelectedIndex = 1; cbBoxOverflowP.SelectedIndex = 1;  //T2
                    cbBoxShortage.SelectedIndex = 2; cbBoxShortageP.SelectedIndex = 2;  //C

                }

            }
                

               
        }

        private void SetProIndex(int index)
        {
            cbBoxProIndex.Items.Clear();
            for (int i = 1; i < index; i++)
            {
                cbBoxProIndex.Items.Add("第" + (i + 1).ToString() + "个项目");
            }
            cbBoxProIndex.SelectedIndex = 0;
            //第2个项目
            //第3个项目
            //第4个项目
        }
        private void cbBoxProIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Setting.Text = "设置" + cbBoxProIndex.Text;
        }

        private void groupBox6_Paint_1(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Red;
            groupBox_Paint(sender, e, b, p);
        }

        private void groupBox2_Paint_1(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Red;
            groupBox_Paint(sender, e, b, p);
        }

        private void tb_BarCodeNumber_Leaved(object sender, EventArgs e)
        {
            AnalyseBarCode(tb_BarCodeNumber.Text);
        }
        private void AnalyseBarCode(string sBarCode)
        {
            if (sBarCode.Length == 5)
            {
                //以下是根据条码号生成5位的原始批号,再加上批号前缀--->最终6/7位批号
                string ls_One = Busiclass.ConvertString(sBarCode.Substring(0, 2), 16, 2).PadLeft(8, '0');//前两位,填充才8位
                string ls_Tow = Busiclass.ConvertString(sBarCode.Substring(2, 2), 16, 2).PadLeft(8, '0');//3,4位
                string ls_Three = Busiclass.ConvertString(sBarCode.Substring(4, 1) + "0", 16, 2).PadLeft(8, '0');//第5位+0,转2进制
                int gi_mItem = Convert.ToInt32(ls_One.Substring(0, 5), 2);//二进制转整型
                /*
                string ls_Year = Busiclass.ConvertString(ls_One.Substring(5, 3) + ls_Tow.Substring(0, 1), 2, 10);
                string ls_Month = Busiclass.ConvertString(ls_Tow.Substring(1, 4), 2, 10);
                string SerialNumber = Busiclass.ConvertString(ls_Tow.Substring(5, 3) + ls_Three.Substring(0, 1), 2, 10);
                 */
                string ls_Year = Busiclass.ConvertString(ls_One.Substring(5, 3) + ls_Tow.Substring(0, 1), 2, 10);
                string ls_Month = (Convert.ToInt32(Busiclass.ConvertString(ls_Tow.Substring(1, 4), 2, 10)) + 1).ToString();//读回来的是月的下拉ID,真实值,要+1
                string SerialNumber = (Convert.ToInt32(Busiclass.ConvertString(ls_Tow.Substring(5, 3) + ls_Three.Substring(0, 1), 2, 10)) + 1).ToString();
                try
                {
                    cm_Produce.SelectedIndex = gi_mItem;
                    cm_Year.SelectedIndex = int.Parse(ls_Year);
                    cm_Month.SelectedIndex = int.Parse(ls_Month) - 1;
                    cm_SerialNumber.Text = SerialNumber;
                }
                catch (System.Exception ex)
                {
                    
                }
            }
            else if (sBarCode.Length == 6)
            {
                try
                {
                    string ls_One = sBarCode.Substring(0, 2);//前两位,填充才8位
                    string ls_Tow = sBarCode.Substring(2, 1);//3位
                    string ls_Three = sBarCode.Substring(3, 1);
                    string ls_Four = sBarCode.Substring(4, 2);
                    cm_Produce.SelectedIndex = Convert.ToInt32(ls_One, 16);
                    cm_Year.SelectedIndex = Convert.ToInt32(ls_Tow, 16);
                    cm_Month.SelectedIndex = Convert.ToInt32(ls_Three, 16);
                    cm_SerialNumber.SelectedIndex = (Convert.ToInt32(ls_Four, 16)) / 8;
                }
                catch (System.Exception ex)
                {

                }
            }
        }
        // 同步min和max数值
        private void tb_ResultMin1_TextChanged(object sender, EventArgs e)
        {
            //if (cbBoxProCount.SelectedIndex == 3)// 目前只修改定性此数值
            {
                tb_ResultRangeBegin1.Text = tb_ResultMin1.Text;
            }
        }

        private void tb_ResultMax1_TextChanged(object sender, EventArgs e)
        {
            //if (cbBoxProCount.SelectedIndex == 3)// 目前只修改定性此数值
            {
                tb_ResultRangeEnd1.Text = tb_ResultMax1.Text;
            }
            
        }

        private void tb_ResultRangeBegin1_TextChanged(object sender, EventArgs e)
        {
            tb_ResultMin1.Text = tb_ResultRangeBegin1.Text;
        }

        private void tb_ResultRangeEnd1_TextChanged(object sender, EventArgs e)
        {
            tb_ResultMax1.Text = tb_ResultRangeEnd1.Text;
        }
        // 是否为定性项目
        private void m_pNatureProjectCBox_CheckedChanged(object sender, EventArgs e)
        {
            // 定性
            if (m_pNatureProjectCBox.Checked)
            {
                // 项目
                cbBoxProCount.SelectedIndex = 0;
                cbBoxProCount.Enabled = false;
                // 控件置灰
                cbBoxProIndex.Visible = btn_Setting.Visible = false;
                lb_SelItem.Visible = lb_CurrItem.Visible = false;
                cm_Fz.Visible = cm_Fm.Visible = lb_xg.Visible = false;
                cm_TC.Visible = true; cm_TC.SelectedIndex = 0;
                cm_T1.Visible = cm_T2.Visible = cm_C.Visible = true;
                cm_T1.SelectedIndex = 0; cm_T2.SelectedIndex = 1; cm_C.SelectedIndex = 2;
                lb_X4.Visible = tb_X4Begin.Visible = tb_X4End.Visible = tb_X4Count.Visible = label90.Visible = false;
                //重置峰值下拉选项
                cbBoxOverflow.Items.Clear(); cbBoxShortage.Items.Clear(); cbBoxOverflowP.Items.Clear(); cbBoxShortageP.Items.Clear();
                string[] sItems = new string[] { "T1", "T2", "C" };
                cbBoxOverflow.Items.AddRange(sItems); cbBoxShortage.Items.AddRange(sItems);
                cbBoxOverflowP.Items.AddRange(sItems); cbBoxShortageP.Items.AddRange(sItems);
                if (!bLoadHex)
                {
                    tb_T1End.Text = "60"; tb_T2Begin.Text = "61"; tb_T2End.Text = "120"; tb_CBegin.Text = "121"; tb_CEnd.Text = "174";
                    cbBoxOverflow.SelectedIndex = 1; cbBoxOverflowP.SelectedIndex = 1;  //T2
                    cbBoxShortage.SelectedIndex = 2; cbBoxShortageP.SelectedIndex = 2;  //C
                }
                // 定性禁用
                tb_Unit.Enabled = false;
                tb_DecimalsDigit.Enabled = false;
                cm_Log.Enabled = false;
                cm_Method0.Enabled = false;
                cm_SubCount.Enabled = false;
                cm_Prerequisite0.Enabled = false;
                //cbBoxOverflowP.Enabled = false;
                //cb_OverflowP.Enabled = false;
                //tb_OverflowP.Enabled = false;
                //cbBoxShortageP.Enabled = false;
                //cb_ShortageP.Enabled = false;
                //tb_ShortageP.Enabled = false;
                //btn_TempCalib.Enabled = false;
                //tb_DensFc2.Enabled = false;
                //listView1.Enabled = false;
                bt_ClearTemp.Enabled = false;
                tb_ResultMid1.Enabled = false;
                tb_ResultRangeBegin1.Enabled = false;
                tb_ResultRangeEnd1.Enabled = false;
                bt_ClearResult.Enabled = false;
                // 阴阳标识
                tb_ResultMinShow1.Enabled = false;
                tb_ResultMinShow1.Text = "阴";
                tb_ResultMaxShow1.Enabled = false;
                tb_ResultMaxShow1.Text = "阳";


                StructSP[0].MinShow = "阴";
                StructSP[0].MaxShow = "阳";
                
                StructSP[0].ProName = "hsCRP";
                StructSP[0].Min = "0.5";

                StructSP[0].Max = "5";

                StructSP[0].MidShow = "X";
                StructSP[0].RangeMin = "0.5";
                StructSP[0].RangeMax = "5";

                
                if (StructSP[1].ProName != "")
                {
                    StructSP[1].MinShow = "阴";
                    StructSP[1].MaxShow = "阳";
                    StructSP[1].ProName = "CRP";
                    StructSP[1].Min = "5";

                    StructSP[1].Max = "200";

                    StructSP[1].MidShow = "X";
                    StructSP[1].RangeMin = "5";
                    StructSP[1].RangeMax = "200";
                }
            }
            else
            {
                cbBoxProCount.Enabled = true;
                // 非定性还原 
                tb_Unit.Enabled = true;
                tb_DecimalsDigit.Enabled = true;
                cm_Log.Enabled = true;
                cm_Method0.Enabled = true;
                cm_SubCount.Enabled = true;
                cm_Prerequisite0.Enabled = true;
                cbBoxOverflowP.Enabled = true;
                //cb_OverflowP.Enabled = true;
                //tb_OverflowP.Enabled = true;
                //cbBoxShortageP.Enabled = true;
                //cb_ShortageP.Enabled = true;
                //tb_ShortageP.Enabled = true;
                //btn_TempCalib.Enabled = true;
                //tb_DensFc2.Enabled = true;
                //listView1.Enabled = true;
                bt_ClearTemp.Enabled = true;
                tb_ResultMid1.Enabled = true;
                tb_ResultRangeBegin1.Enabled = true;
                tb_ResultRangeEnd1.Enabled = true;
                bt_ClearResult.Enabled = true;
                //
                tb_ResultMinShow1.Enabled = true;
                tb_ResultMaxShow1.Enabled = true;
                tb_ResultMinShow1.Text = "<0.5 mg/L";
                tb_ResultMaxShow1.Text = ">5 mg/L";
                //
                StructSP[0].ProName = "hsCRP";
                StructSP[0].Min = "0.5";
                StructSP[0].MinShow = "<0.5 mg/L";
                StructSP[0].Max = "5";
                StructSP[0].MaxShow = ">5 mg/L";
                StructSP[0].MidShow = "X";
                StructSP[0].RangeMin = "0.5";
                StructSP[0].RangeMax = "5";

                if (StructSP[1].ProName != "")
                {
                    StructSP[1].ProName = "CRP";
                    StructSP[1].Min = "5";
                    StructSP[1].MinShow = "<5 mg/L";
                    StructSP[1].Max = "200";
                    StructSP[1].MaxShow = ">200 mg/L";
                    StructSP[1].MidShow = "X";
                    StructSP[1].RangeMin = "5";
                    StructSP[1].RangeMax = "200";
                }
                
            }
        }
    }
}