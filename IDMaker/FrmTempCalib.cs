using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IDMaker
{
    public partial class FrmTempCalib : Form
    {
        [DllImport("user32.dll")]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        public byte[] iTemp = new byte[6] { 20, 27, 29, 31, 35, 100 };// 数据库温度方程的标识

        public double[][] a0 = new double[6][], a1 = new double[6][];
        public int[] mLog = new int[6], mSubPt = new int[6];
        public double[] XFirst = new double[6], XLast = new double[6], XMid = new double[6], YFirst = new double[6];

        public int[] mSubSec = new int[6], Prerequiste1 = new int[6], Prerequiste2 = new int[6];
        public double[][] ld_XArray = new double[6][], ld_YArray = new double[6][];
        public int[] li_CountP=new int[6];
        
        int m_iCurrentCurveSelectItem = 0;    // 当前选择的曲线方程序号        
        bool bCancelChange;

        public bool bMdiShown = false;
        public int m_iProjectNumber = 1;// 当前项目数，为判定测试值显示样式
        // 添加多曲线数据操作区域
        private int m_iCurve1TestValue = 0;// 曲线一的测试值
        private int m_iCurve2TestValue = 0;// 测试值 0-11 对应index值
        private int m_iMoreCurveFlag = 0;// 是否多曲线        
        private int m_iCurve1TestMolecularValue = 1;// 多项目的曲线1分子值1-4，对应index+1
        private int m_iCurve1TestDenominatorValue = 1;// 多项目的曲线1分母值1-4，对应index+1
        private int m_iTestMolecularValue = 1;// 多项目的分子值1-4，对应index+1
        private int m_iTestDenominatorValue = 1;// 多项目的分母值1-4，对应index+1
        private double m_dMoreCurveAValue = 0;// 多项目的A值
        private int m_iMoreCurveAFlag = 0;// 与A值比较，0: >, 1: <= 
        private bool m_bFirstResetMoreCurveComboBox = false;// 首次加载控件不重置check引起的设置默认曲线1，其他check操作则吧曲线设置为曲线1        
        public void SetCurve1TestValue(int iTestValue)
        {
            m_iCurve1TestValue = iTestValue;
        }
        public void SetCurve1TestMolecularValue(int iTestValue)
        {
            m_iCurve1TestMolecularValue = iTestValue;
        }
        public void SetCurve1TestDenominatorValue(int iTestValue)
        {
            m_iCurve1TestDenominatorValue = iTestValue;
        }
        public int GetMoreCurveFlag()
        {
            return m_iMoreCurveFlag;
        }
        public void SetMoreCurveFlag(int bFlag)
        {
            m_iMoreCurveFlag = bFlag;
            updataMoreCurveWidget();
        }
        // 测试值  0-9 对应index值
        public int GetTestValue()
        {
            return m_iCurve2TestValue;
        }
        public void  SetTestValue(int iTest)
        {
            m_iCurve2TestValue = iTest;
            if (m_iCurve2TestValue >= 0 && m_iCurve2TestValue <= 11)
            {
                m_pMoreCurveTC.SelectedIndex = m_iCurve2TestValue;
                m_pMoreCurveTC.Update();
            }
            
        }
        // 分子 1-4
        public int GetTestMolecularValue()
        {
            return m_iTestMolecularValue;
        }
        public void  SetTestMolecularValue(int iValue)
        {
            m_iTestMolecularValue = iValue;
            if (m_iTestMolecularValue > 0 && m_iTestMolecularValue <= 4)
            {
                cm_Fz.SelectedIndex = m_iTestMolecularValue - 1;
            }            
        }
        // 分母 1-4
        public int GetTestDenominatorValue()
        {
            return m_iTestDenominatorValue;
        }
        public void  SetTestDenominatorValue(int iValue)
        {
            m_iTestDenominatorValue = iValue;
            if (m_iTestDenominatorValue > 0 && m_iTestDenominatorValue <= 4)
            {
                cm_Fm.SelectedIndex = m_iTestDenominatorValue - 1;
            } 
        }
        // A值
        public double GetMoreCurveAValue()
        {
            return m_dMoreCurveAValue;
        }
        public void SetMoreCurveAValue(double dAValue)
        {
            m_dMoreCurveAValue = dAValue;
            m_pCurveAValueText.Text = m_dMoreCurveAValue.ToString();
        }
        // > <=
        public double GetMoreCurveAFlag()
        {
            return m_iMoreCurveAFlag;
        }
        public void SetMoreCurveAFlag(int iAFlag)
        {
            m_iMoreCurveAFlag = iAFlag;
            m_pCurveAFlagCBox.SelectedIndex = m_iMoreCurveAFlag;
        }
        public FrmTempCalib()
        {
            InitializeComponent();
            for (int i = 0; i < 6; i++)//在定义窗体的时候new,而不是在Load窗体时
            {
                if (ld_XArray[i] == null)
                {
                    ld_XArray[i] = new double[16];
                    ld_YArray[i] = new double[16];
                }
                a0[i] = new double[16];
                a1[i] = new double[16];
            }
        }
        private void FrmTempCalib_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            bMdiShown = false;
            e.Cancel = true;
        }
        private void FrmTempCalib_Load(object sender, EventArgs e)
        {
            lb_ItemNo.Text = "当前为第" + (ClassCS.gi_SelectProIndex == 0 ? 1 : ClassCS.gi_SelectProIndex) + "个项目";
            //初始化温度选择下拉
            if (System.IO.File.Exists(ClassCS.iniPARA))
            {
                for (int i = 0; i < 6; i++)
                {
                    string sTemp = ClassIni.ReadIniData("Calib Item" + ClassCS.gi_SelectProIndex, "Temperature" + i, ClassCS.iniPARA);
                    //cmb_Temp.Items[i] = "曲线" + (i+1) + "——" + sTemp + "℃";
                    if (sTemp.Trim() != "")
                    {
                        try
                        {
                            iTemp[i] = byte.Parse(sTemp);
                        }
                        catch (System.Exception ex)
                        {
                        }
                    }
                }
            }

            initLsv(); //初始化控件
            m_pMoreCurveComboBox.SelectedIndex = 0;

        }
        public bool bRefresh = false;
        private void FrmTempCalib_Activated(object sender, EventArgs e)
        {
            if (bRefresh)
            {
                cmb_Temp_SelectedIndexChanged(m_pMoreCurveComboBox , null);
                bRefresh = false;
            }
        }

        bool bModified = false;
        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {// 多曲线选择
            if (m_pMoreCurveComboBox.SelectedIndex == -1)
            {
                return;
            }
            
            m_iCurrentCurveSelectItem = m_pMoreCurveComboBox.SelectedIndex;
            // 更新测试值
            if (m_iProjectNumber == 0)
            {
                if (m_iCurrentCurveSelectItem == 0)
                {// 曲线1，置灰测试值，更新为曲线1测试值
                    m_pMoreCurveTC.SelectedIndex = m_iCurve1TestValue;
                    m_pMoreCurveTC.Enabled = false;
                    
                }
                else
                {
                    m_pMoreCurveTC.SelectedIndex = m_iCurve2TestValue;
                    m_pMoreCurveTC.Enabled = true;
                    
                }
            }
            else
            {
                if (m_iCurrentCurveSelectItem == 0)
                {// 曲线1，置灰测试值，更新为曲线1测试值
                    cm_Fz.SelectedIndex = m_iCurve1TestMolecularValue - 1;
                    cm_Fm.SelectedIndex = m_iCurve1TestDenominatorValue - 1;
                    cm_Fz.Enabled = false;
                    cm_Fm.Enabled = false;
                }
                else
                {
                    cm_Fz.SelectedIndex = m_iTestMolecularValue - 1;
                    cm_Fm.SelectedIndex = m_iTestDenominatorValue - 1;
                    cm_Fz.Enabled = true;
                    cm_Fm.Enabled = true;
                }
            }
            if (m_pHasMoreCurveCBox.Checked)
            {
                if (m_iCurrentCurveSelectItem == 0)
                {// // A值显示
                    m_pCurveAValueLabel.Visible = true;
                    m_pCurveAValueText.Visible = true;
                    m_pCurveAFlagCBox.Visible = true;
                }
                else
                {
                    // A值隐藏
                    m_pCurveAValueLabel.Visible = false;
                    m_pCurveAValueText.Visible = false;
                    m_pCurveAFlagCBox.Visible = false;
                }
            }
            
            //加载各条件,更新浓度TC数据
            cm_Log.SelectedIndex = mLog[m_iCurrentCurveSelectItem];
            cm_SubCount.SelectedIndex = mSubSec[m_iCurrentCurveSelectItem];//不分段
            tb_Subsection1.Text = mSubPt[m_iCurrentCurveSelectItem].ToString();
            cm_Prerequisite1.SelectedIndex = Prerequiste1[m_iCurrentCurveSelectItem];
            cm_Prerequisite2.SelectedIndex = Prerequiste2[m_iCurrentCurveSelectItem];
            //浓度 TC
            string sXArray = "", sYArray = "";
            for (int i = 0; i < li_CountP[m_iCurrentCurveSelectItem]; i++)
            {
                if (sXArray == "")
                {
                    sXArray = ld_XArray[m_iCurrentCurveSelectItem][i].ToString();
                    sYArray = ld_YArray[m_iCurrentCurveSelectItem][i].ToString();

                }
                else
                {
                    sXArray += "\t" + ld_XArray[m_iCurrentCurveSelectItem][i].ToString();
                    sYArray += "\t" + ld_YArray[m_iCurrentCurveSelectItem][i].ToString();
                }
            }
            bCancelChange = true;
            tb_DensFcPlus.Text = ""; tb_TCFcPlus.Text = "";
            initLsv(); 
            bCancelChange = false;
            if (sXArray.Trim(new char[] { '0', '\t' }) != "")
                tb_DensFcPlus.Text = sXArray + "\r" + sYArray;
            tb_Equation1.Text = ""; tb_Equation2.Text = "";
            if (li_CountP[m_iCurrentCurveSelectItem] > 0)
                showEquation(a0[m_iCurrentCurveSelectItem],a1[m_iCurrentCurveSelectItem]);
        }

        private void cm_SubCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSubSec[m_iCurrentCurveSelectItem] = cm_SubCount.SelectedIndex;
            if (cm_SubCount.SelectedIndex == 0)//不分段
            {
                tb_Subsection1.Text = "0";
                tb_Subsection1.Enabled = false;
                panel_Prerequiste.Visible = false;
            }
            else
            {
                //tb_Subsection1.Enabled = true;
                panel_Prerequiste.Visible = true;
            }
            
        }
        private void cm_Log_SelectedIndexChanged(object sender, EventArgs e)
        {
            mLog[m_iCurrentCurveSelectItem] = cm_Log.SelectedIndex;
        }

        private void tb_Subsection1_TextChanged(object sender, EventArgs e)
        {
            if (tb_Subsection1.Text=="")
            {
                return;
            }
            try
            {
                mSubPt[m_iCurrentCurveSelectItem] = int.Parse(tb_Subsection1.Text);
                if (mSubPt[m_iCurrentCurveSelectItem]!=0 &&(mSubPt[m_iCurrentCurveSelectItem] < 4 || mSubPt[m_iCurrentCurveSelectItem]>10))
                {
                    MessageBox.Show("分段点须在4-10之间!");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("格式不正确!");
            }
        }

        private void cm_Prerequisite1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Prerequiste1[m_iCurrentCurveSelectItem] = cm_Prerequisite1.SelectedIndex;
        }

        private void cm_Prerequisite2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Prerequiste2[m_iCurrentCurveSelectItem] = cm_Prerequisite2.SelectedIndex;
        }

        private void btn_ClearTemp_Click(object sender, EventArgs e)
        {
            li_CountP[m_iCurrentCurveSelectItem] = 0;
            tb_DensFcPlus.Text = "";
            tb_TCFcPlus.Text = "";
            initLsv();
            tb_Equation1.Text = ""; tb_Equation2.Text = "";
//             //清除Param.ini文件的数据
//             ClassIni.WriteIniData("FitData", "mLog", "", ClassCS.iniPARA);
//             ClassIni.WriteIniData("FitData", "mSubFlag", "", ClassCS.iniPARA);
//             ClassIni.WriteIniData("FitData", "mSubPt", "", ClassCS.iniPARA);
//             ClassIni.WriteIniData("FitData", "mPrerequiste1", "", ClassCS.iniPARA);
//             ClassIni.WriteIniData("FitData", "mPrerequiste2", "", ClassCS.iniPARA);
//             ClassIni.WriteIniData("FitData", "mXArray", "", ClassCS.iniPARA);
//             ClassIni.WriteIniData("FitData", "mYArray", "", ClassCS.iniPARA);
//             ClassIni.WriteIniData("FitData", "mEquation1", "", ClassCS.iniPARA);
//             ClassIni.WriteIniData("FitData", "mEquation2", "", ClassCS.iniPARA);
            //Hex相应ini文件历史记录
            ClassIni.WriteIniData("Plus Calib Parameter" + m_iCurrentCurveSelectItem, "mLog", "", ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + m_iCurrentCurveSelectItem, "mSubSec", "", ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + m_iCurrentCurveSelectItem, "mSubPt", "", ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + m_iCurrentCurveSelectItem, "mPrerequiste1", "", ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + m_iCurrentCurveSelectItem, "mPrerequiste2", "", ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + m_iCurrentCurveSelectItem, "mXArray", "", ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + m_iCurrentCurveSelectItem, "mYArray", "", ClassCS.iniTempName);

        }
        private void initLsv()
        {
            lv_Point.Items.Clear();
            for (int i = 0; i < 16; i++)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = "";//(i + 1).ToString();
                li.SubItems.Add("");
                li.SubItems.Add("");
                lv_Point.Items.Add(li);
            }
        }
        private void tb_DensFcPlus_TextChanged(object sender, EventArgs e)
        {
            if (bCancelChange)
            {
                return;
            }
            try
            {
                li_CountP[m_iCurrentCurveSelectItem] = 0;
                ld_XArray[m_iCurrentCurveSelectItem] = new double[16];

                string str = tb_DensFcPlus.Text.Trim();//浓度标准点
                if (str == "")
                    return;
                string[] arrayRow = str.Split('\r'); //回车符 复制了excel多行
                if (arrayRow.Length > 1)//如果有多行
                {
                    tb_DensFcPlus.Text = arrayRow[0];
                    tb_TCFcPlus.Text = arrayRow[1];//把第二行放入Y标准点????
                }
                else
                {
                    string[] array = arrayRow[0].Split('\t'); //\t 制表符 //复制了一行excel多列 
                    if (array.Length > 16)
                    {
                        MessageBox.Show("标准点点数不能超过16个，请重新输入！");
                        return;
                    }
                    initLsv();
                    for (int i = 0; i < array.Length; i++)
                    {
                        lv_Point.Items[i].SubItems[0].Text = (i + 1).ToString();//序号
                        lv_Point.Items[i].SubItems[1].Text = array[i].ToString();//X 浓度点
                        ld_XArray[m_iCurrentCurveSelectItem][i] = Convert.ToDouble(array[i]);
                    }
                    li_CountP[m_iCurrentCurveSelectItem] = array.Length;//标准点个数
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void tb_TCFcPlus_TextChanged(object sender, EventArgs e)
        {
            if (bCancelChange)
            {
                return;
            }
            try
            {
                ld_YArray[m_iCurrentCurveSelectItem] = new double[16];
                string str = tb_TCFcPlus.Text.Trim();
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
                    lv_Point.Items[i].SubItems[0].Text = (i + 1).ToString();//序号
                    lv_Point.Items[i].SubItems[2].Text = arrayRow[i].ToString();//TC值
                    ld_YArray[m_iCurrentCurveSelectItem][i] = Convert.ToDouble(arrayRow[i]);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_ReadEch_Click(object sender, EventArgs e)
        {
            try
            {
                string sLog = "", sSubSec = "", sSubPt = "", sPrerequiste1 = "", sPrerequiste2 = "", sXArray = "", sYArray = "", sEqua1 = "", sEqua2 = "";
                sLog = ClassIni.ReadIniData("FitData", "mLog", ClassCS.iniPARA);
                sSubSec = ClassIni.ReadIniData("FitData", "mSubFlag", ClassCS.iniPARA);
                sSubPt = ClassIni.ReadIniData("FitData", "mSubPt", ClassCS.iniPARA);
                sPrerequiste1 = ClassIni.ReadIniData("FitData", "mPrerequiste1", ClassCS.iniPARA);
                sPrerequiste2 = ClassIni.ReadIniData("FitData", "mPrerequiste2", ClassCS.iniPARA);
                sXArray = ClassIni.ReadIniData("FitData", "mXArray", ClassCS.iniPARA);
                sYArray = ClassIni.ReadIniData("FitData", "mYArray", ClassCS.iniPARA);
                sEqua1 = ClassIni.ReadIniData("FitData", "mEquation1", ClassCS.iniPARA);
                sEqua2 = ClassIni.ReadIniData("FitData", "mEquation2", ClassCS.iniPARA);
                cm_Log.SelectedIndex = int.Parse(sLog);
                cm_SubCount.SelectedIndex = int.Parse(sSubSec);
                if (cm_SubCount.SelectedIndex == 0)//不分段
                    tb_Subsection1.Text = "0";
                else
                    tb_Subsection1.Text = sSubPt;
                cm_Prerequisite1.SelectedIndex = int.Parse(sPrerequiste1);
                cm_Prerequisite2.SelectedIndex = int.Parse(sPrerequiste2);
                tb_DensFcPlus.Text = sXArray.Replace(",", "\t");
                tb_TCFcPlus.Text = sYArray.Replace(",", "\t");
                tb_Equation1.Text = sEqua1.Replace('~', '^');
                tb_Equation2.Text = sEqua2.Replace('~', '^');
                
                SaveTip = false;
                //btn_Save.PerformClick();//如果没保存, 自动保存一次
                btn_Save_Click(btn_Save, null);
                SaveTip = true;

            }
            catch (System.Exception ex)
            {

            }
        }

        //打开拟合软件
        //public static void openEch()
        //{
        //    string pName = "FitCurve";//要启动的进程名称，可以在任务管理器里查看，一般是不带.exe后缀的;
        //    Process[] Procs = Process.GetProcessesByName(pName);//在所有已启动的进程中查找需要的进程；
        //    if (Procs.Length > 0)//如果查找到
        //    {
        //        IntPtr handle = Procs[0].MainWindowHandle;
        //        SwitchToThisWindow(handle, true);    // 激活，显示在最前
        //    }
        //    else
        //    {
        //        Process.Start(pName + ".exe");//否则启动进程
        //    }
        //}

        private void btn_OpenEch_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(Application.StartupPath + "\\FitCurve.exe");
            string pName = "FitCurve";//要启动的进程名称，可以在任务管理器里查看，一般是不带.exe后缀的;
            Process[] Procs = Process.GetProcessesByName(pName);//在所有已启动的进程中查找需要的进程；
            if (Procs.Length > 0)//如果查找到
            {
                IntPtr handle = Procs[0].MainWindowHandle;
                SwitchToThisWindow(handle, true);    // 激活，显示在最前
            }
            else
            {
                Process.Start(pName + ".exe");//否则启动进程
            }
        }
        private void btn_Fit_Click(object sender, EventArgs e)
        {
            if (li_CountP[m_iCurrentCurveSelectItem] == 0)
            {
                MessageBox.Show("请输入标准点, 再进行拟合!");
                return;
            }
            
            //拟合
            bool bSubFlag=mSubSec[m_iCurrentCurveSelectItem]==1;//为1表示分两段,0表示不分段
            bool bLog=mLog[m_iCurrentCurveSelectItem]==1;
            int[] polyNum=new int[2];
            double[] ma0 = new double[16], ma1 = new double[16];
            //a0[m_iCurrentCurveSelectItem] = new double[16]; a1[m_iCurrentCurveSelectItem] = new double[16];//不可少,因为有可能第一次拟合把a1[TId]的内存销毁了

            int iResult = MyDll.FitEqu(ld_XArray[m_iCurrentCurveSelectItem], ld_YArray[m_iCurrentCurveSelectItem], Prerequiste1[m_iCurrentCurveSelectItem], Prerequiste2[m_iCurrentCurveSelectItem], 6,
                                bSubFlag, bLog, li_CountP[m_iCurrentCurveSelectItem], mSubPt[m_iCurrentCurveSelectItem], ma0, ma1, polyNum);
            bool fitOK = false;
            if (bSubFlag)//分段
            {
                if (iResult == 2)
                    fitOK = true;
            }
            else
            {
                if (iResult == 1)
                    fitOK = true;
            }
            if (!fitOK)
            {
                MessageBox.Show("拟合失败, 请重试!");
                return;
            }
            tb_Equation1.Text = ""; tb_Equation2.Text = "";
            
            //显示方程
            showEquation(ma0,ma1);

        }
        // bug 存在，
        private void showEquation(double[] ma0,double[] ma1)
        {
            try
            {
                if (ma0 != null)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (ma0[i] == 0)//|| ma1[i].ToString("F9") == "0.000000000"
                        {
                            //break;     
                            if(i == 0)
                            {
                                tb_Equation1.Text = "y = 0";
                            }
                            continue;
                        }
                        if (tb_Equation1.Text == "")
                        {
                            tb_Equation1.Text = "y = " + ma0[i].ToString();//a0
                        }
//                         else if (ma0[i] < 0)
//                         {
//                             string a=ma0[i].ToString();
//                             tb_Equation1.Text += " " + a + "*x^" + i;
//                         }
                        else
                            tb_Equation1.Text += "+" + ma0[i].ToString() + "*x^" + i;
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
            if (mSubSec[m_iCurrentCurveSelectItem] == 0)//不分段
            {
                return;
            }
            try
            {
                //if (ma1[0] != 0)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (ma1[i] == 0 )//|| ma1[i].ToString("F9")=="0.000000000"
                        {
                            //break;
                            if (i == 0)
                            {
                                tb_Equation2.Text = "y = 0";
                            }
                            continue;
                        }
                        if (tb_Equation2.Text == "")
                        {
                            tb_Equation2.Text = "y = " + ma1[i].ToString();
                        }
//                         else if (ma1[i] < 0)
//                         {
//                             string a = ma1[i].ToString();
//                             tb_Equation2.Text += " " + a + "*x^" + i;
//                         }
                        else
                            tb_Equation2.Text += "+" + ma1[i].ToString() + "*x^" + i;
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        private void btn_setTemp_Click(object sender, EventArgs e)
        {
            FrmTempSetting frmTSetting = new FrmTempSetting();
            DialogResult dr= frmTSetting.ShowDialog();
            if (dr==DialogResult.OK)
            {
                for (int i = 0; i < 6; i++)
                {
                    string sTemp = ClassIni.ReadIniData("Calib Item" + ClassCS.gi_SelectProIndex, "Temperature" + i, ClassCS.iniPARA);
                    //cmb_Temp.Items[i] = "曲线" + (i + 1) + "——" + sTemp + "℃";
                    if (sTemp.Trim() != "")
                    {
                        try
                        {
                            iTemp[i] = byte.Parse(sTemp);
                        }
                        catch (System.Exception ex)
                        {
                        }
                    }
                }
            }
        }

        private void tb_Equation_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text.Replace("\r\n", "");
        }

        private bool GetCoEff()
        {
            if (tb_Equation1.Text.IndexOf("y = ") != 0)
            {
                MessageBox.Show("方程1格式不正确!");
                return false;
            }
            a0[m_iCurrentCurveSelectItem] = new double[16]; a1[m_iCurrentCurveSelectItem] = new double[16];
            string sEqu = tb_Equation1.Text.Replace("y = ", "");
            string[] sCoeff = sEqu.Split(new char[] { '+',' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sCoeff.Length; i++)
            {
                int id = sCoeff[i].IndexOf("*x");
                if (id > 0)
                {
                    sCoeff[i] = sCoeff[i].Substring(0, id);
                }
                try
                {
                    a0[m_iCurrentCurveSelectItem][i] = double.Parse(sCoeff[i]);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("方程1系数" + (i + 1) + "错误!");
                    return false;
                }
            }
            //方程2
            if (mSubSec[m_iCurrentCurveSelectItem] == 0)// && tb_Equation2.Text.Trim()==""
            {
                return true;
            }
            if (tb_Equation2.Text.IndexOf("y = ") != 0)
            {
                MessageBox.Show("方程2格式不正确!");
                return false;
            }
            sEqu = tb_Equation2.Text.Replace("y = ", "");
            sCoeff = sEqu.Split(new char[] { '+',' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sCoeff.Length; i++)
            {
                int id = sCoeff[i].IndexOf("*x");
                if (id > 0)
                {
                    sCoeff[i] = sCoeff[i].Substring(0, id);
                }
                try
                {
                    a1[m_iCurrentCurveSelectItem][i] = double.Parse(sCoeff[i]);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("方程2系数" + (i + 1) + "错误!");
                    return false;
                }
            }
            return true;
        }
        bool SaveTip = true;//需要提示
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (li_CountP[m_iCurrentCurveSelectItem] == 0)
            {
                MessageBox.Show("请输入标准点, 再进行保存!");
                return;
            }
            XFirst[m_iCurrentCurveSelectItem] = ld_XArray[m_iCurrentCurveSelectItem][0]; XLast[m_iCurrentCurveSelectItem] = ld_XArray[m_iCurrentCurveSelectItem][li_CountP[m_iCurrentCurveSelectItem] - 1];
            YFirst[m_iCurrentCurveSelectItem] = ld_YArray[m_iCurrentCurveSelectItem][0];//mSubPt[m_iCurrentCurveSelectItem] - 1, 改成发送第一个点对应TC值2016.8.18 
            if (mSubPt[m_iCurrentCurveSelectItem] != 0)
            {
                XMid[m_iCurrentCurveSelectItem] = ld_XArray[m_iCurrentCurveSelectItem][mSubPt[m_iCurrentCurveSelectItem] - 1]; 
            }
            else
            {
                XMid[m_iCurrentCurveSelectItem] = ld_XArray[m_iCurrentCurveSelectItem][0]; 
            }
            if (GetCoEff())
            {
                if (SaveTip)
                    MessageBox.Show("保存当前曲线定标数据成功!");
            }
            //保存到temp.ini
            int ProNo = ClassCS.gi_SelectProIndex;
            string sLog = "", sSubSec = "", sSubPt = "", sPrerequiste1 = "", sPrerequiste2 = "", sXArray = "", sYArray = "";
            for (int i = 0; i < 6; i++)
            {
                sLog += mLog[i].ToString() + ";";
                sSubSec += mSubSec[i].ToString() + ";";
                sSubPt += mSubPt[i].ToString() + ";";
                sPrerequiste1 += Prerequiste1[i].ToString() + ";";
                sPrerequiste2 += Prerequiste2[i].ToString() + ";";
                string sX = "", sY = "";
                for (int j = 0; j < li_CountP[i]; j++)
                {
                    sX += ld_XArray[i][j] + ","; sY += ld_YArray[i][j] + ",";
                }
                if (sX != "")
                {
                    sX = sX.Substring(0, sX.Length - 1); sY = sY.Substring(0, sY.Length - 1);//去掉最后一个,

                }
                if (sXArray == "")
                {
                    sXArray = sX;
                    sYArray = sY;
                }
                else
                {
                    sXArray += ";" + sX;
                    sYArray += ";" + sY;
                }
            }
            ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mLog", sLog, ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mSubSec", sSubSec, ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mSubPt", sSubPt, ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mPrerequiste1", sPrerequiste1, ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mPrerequiste2", sPrerequiste2, ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mXArray", sXArray, ClassCS.iniTempName);
            ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mYArray", sYArray, ClassCS.iniTempName);

        }


        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)                               //判断系统消息的ID号     
            {
                case 520:
                    this.Owner.WindowState = FormWindowState.Normal;
                    this.WindowState = FormWindowState.Normal;
                    btn_ReadEch.PerformClick();
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
            //Application.DoEvents();
        }
        // 是否多曲线
        private void m_pHasMoreCurveCBox_CheckedChanged(object sender, EventArgs e)
        {
            if(m_pHasMoreCurveCBox.Checked)
            {
                m_iMoreCurveFlag = 1;
                m_pMoreCurveComboBox.Visible = true;
                m_pMoreCurveCalculLabel.Visible = true;
                // 根据项目数显示
                if (m_iProjectNumber > 0)
                {
                    m_pMoreCurveTC.Visible = false;// 测试值
                    cm_Fz.Visible = true;// 分子                    
                    lb_xg.Visible = true;
                    cm_Fm.Visible = true;// 分母                    
                }
                else
                {
                    m_pMoreCurveTC.Visible = true;                    
                    cm_Fz.Visible = false;
                    lb_xg.Visible = false;
                    cm_Fm.Visible = false;
                }                
                //m_pCurveAValueLabel.Visible = true;
                //m_pCurveAValueText.Visible = true;
                //m_pCurveAFlagCBox.Visible = true;
                if (m_iCurrentCurveSelectItem == 0)
                {// // A值显示
                    m_pCurveAValueLabel.Visible = true;
                    m_pCurveAValueText.Visible = true;
                    m_pCurveAFlagCBox.Visible = true;
                }
                else
                {
                    // A值隐藏
                    m_pCurveAValueLabel.Visible = false;
                    m_pCurveAValueText.Visible = false;
                    m_pCurveAFlagCBox.Visible = false;
                }
                
            }
            else
            {
                m_iMoreCurveFlag = 0;
                m_pMoreCurveComboBox.Visible = false;
                m_pMoreCurveCalculLabel.Visible = false;
                m_pMoreCurveTC.Visible = false;
                m_pCurveAValueLabel.Visible = false;
                m_pCurveAValueText.Visible = false;
                m_pCurveAFlagCBox.Visible = false;
                cm_Fz.Visible = false;
                lb_xg.Visible = false;
                cm_Fm.Visible = false;
            }
            //
            if (m_bFirstResetMoreCurveComboBox)
            {
                m_pMoreCurveComboBox.SelectedIndex = 0;// 重置，默认曲线1,首次加载不需要重置，否则会加载不到浓度和方程数据            
            }
            else
            {
                m_bFirstResetMoreCurveComboBox = true;
            }
            //
            m_pMoreCurveTC.SelectedIndex = 0;// 重置
            cm_Fz.SelectedIndex = 0;// 重置
            cm_Fm.SelectedIndex = 0;// 重置
            m_pCurveAValueText.Text = "";
            m_pCurveAFlagCBox.SelectedIndex = 0;
            // 更新测试值
            if (m_iProjectNumber == 0)
            {
                if (m_iCurrentCurveSelectItem == 0)
                {// 曲线1，置灰测试值，更新为曲线1测试值
                    m_pMoreCurveTC.SelectedIndex = m_iCurve1TestValue;
                    m_pMoreCurveTC.Enabled = false;
                }
                else
                {
                    m_pMoreCurveTC.SelectedIndex = m_iCurve2TestValue;
                    m_pMoreCurveTC.Enabled = true;
                }
            }
            else
            {
                if (m_iCurrentCurveSelectItem == 0)
                {// 曲线1，置灰测试值，更新为曲线1测试值
                    cm_Fz.SelectedIndex = m_iCurve1TestMolecularValue - 1;
                    cm_Fm.SelectedIndex = m_iCurve1TestDenominatorValue - 1;
                    cm_Fz.Enabled = false;
                    cm_Fm.Enabled = false;
                }
                else
                {
                    cm_Fz.SelectedIndex = m_iTestMolecularValue - 1;
                    cm_Fm.SelectedIndex = m_iTestDenominatorValue - 1;
                    cm_Fz.Enabled = true;
                    cm_Fm.Enabled = true;
                }
            }          
            
        }
        // 刷新动态的多曲线控件
        public void updataMoreCurveWidget()
        {
            if (Convert.ToBoolean(m_iMoreCurveFlag))
            {
                m_pHasMoreCurveCBox.Checked = true;
                m_pMoreCurveComboBox.Visible = true;                
                m_pMoreCurveCalculLabel.Visible = true;
                // 根据项目数显示
                if (m_iProjectNumber > 0)
                {
                    m_pMoreCurveTC.Visible = false;
                    cm_Fz.Visible = true;
                    //cm_Fz.SelectedIndex = 0;
                    lb_xg.Visible = true;
                    cm_Fm.Visible = true;
                    //cm_Fm.SelectedIndex = 0;
                }
                else
                {
                    m_pMoreCurveTC.Visible = true;
                    //m_pMoreCurveTC.SelectedIndex = 0;
                    cm_Fz.Visible = false;
                    lb_xg.Visible = false;
                    cm_Fm.Visible = false;
                }

                if (m_iCurrentCurveSelectItem == 0)
                {// // A值显示
                    m_pCurveAValueLabel.Visible = true;
                    m_pCurveAValueText.Visible = true;
                    m_pCurveAFlagCBox.Visible = true;
                }
                else
                {
                    // A值隐藏
                    m_pCurveAValueLabel.Visible = false;
                    m_pCurveAValueText.Visible = false;
                    m_pCurveAFlagCBox.Visible = false;
                }

            }
            else
            {
                m_pHasMoreCurveCBox.Checked = false;
                m_pMoreCurveComboBox.Visible = false;
                m_pMoreCurveCalculLabel.Visible = false;
                m_pMoreCurveTC.Visible = false;
                m_pCurveAValueLabel.Visible = false;
                m_pCurveAValueText.Visible = false;
                m_pCurveAFlagCBox.Visible = false;
                cm_Fz.Visible = false;
                lb_xg.Visible = false;
                cm_Fm.Visible = false;
            }
        }
        // 只允许输入浮点型
        private void m_pCurveAValueText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' 
                || e.KeyChar == '.' || e.KeyChar == 0x08)
            {
                e.Handled = false;
                
            }
            else
            {
                e.Handled = true;
            }
        }

        // 分子值改变
        private void cm_Fz_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_pMoreCurveComboBox.SelectedIndex == 1)
            {
                m_iTestMolecularValue = cm_Fz.SelectedIndex + 1;// 1-4 ,index + 1
            }            
        }
        // 分母值改变
        private void cm_Fm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_pMoreCurveComboBox.SelectedIndex == 1)
            {
                m_iTestDenominatorValue = cm_Fm.SelectedIndex + 1;// 1-4 index + 1
            }            
        }
        // 测试值改变0-11
        private void m_pMoreCurveTC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_pMoreCurveComboBox.SelectedIndex == 1)
            {
                m_iCurve2TestValue = m_pMoreCurveTC.SelectedIndex;
            }            
        }
        // A值控件改变
        private void m_pCurveAValueText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_pCurveAValueText.Text != "")
                {
                    m_dMoreCurveAValue = Convert.ToDouble(m_pCurveAValueText.Text);
                }
                else
                {
                    m_dMoreCurveAValue = 0;// 默认0
                }
            }
            catch (System.Exception ex)
            {
                m_pCurveAValueText.Text = "0";// 清空
                MessageBox.Show("请输入正确的A值!");
                
            }
            
        }

        private void m_pCurveAFlagCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_iMoreCurveAFlag = m_pCurveAFlagCBox.SelectedIndex;
        }
    }
}
