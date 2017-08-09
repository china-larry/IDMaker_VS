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
    public partial class frmMultiple : Form
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



        #region  参数个数 li_Count
        /// <summary>
        /// 参数个数
        /// </summary>
        private int gi_ConcentrationCount = 0, li_CountP = 0;
        #endregion

        #region X浓度 ld_XArray
        /// <summary>
        /// X浓度
        /// </summary>
        private double[] ld_XArray = new double[32], ld_XArrayP = new double[32];
        #endregion

        #region Y： TC 荧光值 ld_YArray
        /// <summary>
        /// Y： TC 荧光值
        /// </summary>
        private double[] ld_YArray = new double[32];
        #endregion

        public frmMultiple()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!bSaved)
            {
                DialogResult result;
                result = Busiclass.MsgYesNo("尚未保存,确定退出？");
                if (result == DialogResult.Yes)
                {
                    e.Cancel = false; //退出
                }
                else
                    e.Cancel = true;
            }
        }

        bool bSaved = false;
        private void frmMain_Load(object sender, EventArgs e)
        {

            label98.Text = "当前选择 " + ClassCS.gs_SelectProIndex;

            init();

            #region  初始化结果一
            /*
            StructSP[0].ProName = "CRP";
            StructSP[0].Min = "5";
            StructSP[0].MinShow = "<5";
            StructSP[0].Max = "200";
            StructSP[0].MaxShow = ">200";
            StructSP[0].MidShow = "X";
            StructSP[0].RangeMin = "5";
            StructSP[0].RangeMax = "200";

           
            StructSP[1].ProName = "hsCRP";
            StructSP[1].Min = "0.5";
            StructSP[1].MinShow = "<0.5 mg/L";
            StructSP[1].Max = "5";
            StructSP[1].MaxShow = ">5 mg/L";
            StructSP[1].MidShow = "X";
            StructSP[1].RangeMin = "0.5";
            StructSP[1].RangeMax = "5";
             */

            iniShowPriner(0);
            #endregion

            initLV(listView1);

            SetShow();

           if (ClassCS.isinit[ClassCS.gi_SelectProIndex - 2])  //是否已初始化       
                ReadHex(ClassCS.by, ClassCS.gi_SelectProIndex);
            bSaved = false;
        }

        #region SetShow
        private void SetShow()
        {
            if (ClassCS.gi_QX == 1)
            {
                // groupBox2.Enabled = true;             
                groupBox3.Enabled = true;
                groupBox5.Enabled = true;
                // groupBox6.Enabled = true;
            }
            else
            {
                //  groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox5.Enabled = false;
                //  groupBox6.Enabled = false;
            }
        }
        #endregion

        #region init 初始化界面
        /// <summary>
        /// 初始化界面
        /// </summary>
        private void init()
        {
            panel7.Visible = false;

            #region  初始化cm_Method
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
            #endregion


            #region 初始化数据
            for (int i = 0; i < 16; i++)
            {
                if (i < 6)
                {
                    #region 初始化处理方法的条件
                    cm_Prerequisite0.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite1.Items.Add((i + 1).ToString() + "次方");
                    cm_Prerequisite2.Items.Add((i + 1).ToString() + "次方");
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
                if (i < 5)
                    StructSP[i] = new StructShowPriner();
            }


            #endregion

            #region  初始化cm_Method
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

            #region  初始化cm_Prerequisite
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

            #region  默认选项
            //cbBoxOverflow.SelectedIndex = 2;
            //cbBoxShortage.SelectedIndex = 3;
            
            cm_T.SelectedIndex = 2;
            cm_C.SelectedIndex = 3;
            cm_Temperature.SelectedIndex = 1;

            cm_Log.SelectedIndex = 1;
            cm_SubCount.SelectedIndex = 0;

            #endregion

            ClearShowPriner();

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
                lv.Items.Add(li);
            }
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            gi_ConcentrationCount = 0;
            tb_DensFc2.Text = "";
            tb_TCFc2.Text = "";
            
            initLV(listView1);
        }

        private void tb_Dens_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ld_XArray = new double[16];
                gi_ConcentrationCount = 0;
                string str = tb_DensFc2.Text.Trim();
                if (str == "")
                    return;
                //str = str.Replace("\n", "");
                string[] arrayRow = str.Split('\r'); //回车符 复制了excel多行
                if (arrayRow.Length > 1)
                {
                    tb_TCFc2.Text = arrayRow[1];
                    tb_DensFc2.Text = arrayRow[0];
                }
                else
                {
                    string[] array = arrayRow[0].Split('\t'); //\t 制表符 //复制了一行excel多列   
                    for (int i = 0; i < array.Length; i++)
                    {
                        listView1.Items[i].SubItems[0].Text = (i + 1).ToString();
                        ld_XArray[i] = Convert.ToDouble(array[i]);
                        listView1.Items[i].SubItems[1].Text = array[i].ToString();
                        gi_ConcentrationCount = i + 1;
                    }
                }
                if (gi_ConcentrationCount > 16)
                {
                    Busiclass.MsgError("标准点点数不能超过16个，请重新输入！");
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
                for (int i = 0; i < arrayRow.Length; i++)
                {
                    listView1.Items[i].SubItems[0].Text = (i + 1).ToString();
                    ld_YArray[i] = Convert.ToDouble(arrayRow[i]);
                    listView1.Items[i].SubItems[2].Text = arrayRow[i].ToString();
                }
            }
            catch (System.Exception ex)
            {
                Busiclass.MsgError(ex.Message.ToString());
            }
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

        #region 写double型的数据 WriteDouble
        /// <summary>
        ///  写double型的数据
        /// </summary>
        /// <param name="X">当前值</param>
        /// <param name="By">写入的地方</param>
        /// <param name="n">写入的行 </param>
        /// <param mode="n">写入类型 0:表示保留8位有效数字(0-7)，第15位 位标志位  
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

            else if (mode == 1)
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
                        By[Row, i] = Convert.ToByte(str.Substring(i, 1));//0-7
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
            else if (mode == 2)
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
        #endregion

        private void cm_SubCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bNoSub = cm_SubCount.SelectedIndex == 0;
            label57.Visible = label58.Visible = bNoSub;
            cm_Method0.Visible = cm_Prerequisite0.Visible = bNoSub;
            panel3.Visible = !bNoSub;
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

        private void button1_Click_1(object sender, EventArgs e)//保存按钮
        {
            try
            {
                // 飞测plus不需要标准点，只有飞测2才需要
                //if (gi_ConcentrationCount < 1 && li_CountP < 1)
                //{
                //    Busiclass.MsgError("请输入标准点后,再进行保存！");
                //    return;
                //}
                ClearByte(ClassCS.gi_SelectProIndex);
                WriteByte(ClassCS.gi_SelectProIndex);//
                ClassCS.isinit[ClassCS.gi_SelectProIndex - 2] = true;

                //保存plus的拟合条件到ini文件
                
//                 int ProNo = ClassCS.gi_SelectProIndex;
//                 string sLog = "", sSubSec = "", sSubPt = "", sPrerequiste1 = "", sPrerequiste2 = "", sXArray = "", sYArray = "";
//                 for (int i = 0; i < 6; i++)
//                 {
//                     sLog += frmTCalib.mLog[i].ToString() + ";";
//                     sSubSec += frmTCalib.mSubSec[i].ToString() + ";";
//                     sSubPt += frmTCalib.mSubPt[i].ToString() + ";";
//                     sPrerequiste1 += frmTCalib.Prerequiste1[i].ToString() + ";";
//                     sPrerequiste2 += frmTCalib.Prerequiste2[i].ToString() + ";";
//                     string sX = "", sY = "";
//                     for (int j = 0; j < frmTCalib.li_CountP[i]; j++)
//                     {
//                         sX += frmTCalib.ld_XArray[i][j] + ","; sY += frmTCalib.ld_YArray[i][j] + ",";
//                     }
//                     if (sX != "")
//                     {
//                         sX = sX.Substring(0, sX.Length - 1); sY = sY.Substring(0, sY.Length - 1);//去掉最后一个,
// 
//                     }
//                     if (sXArray == "")
//                     {
//                         sXArray = sX;
//                         sYArray = sY;
//                     }
//                     else
//                     {
//                         sXArray += ";" + sX;
//                         sYArray += ";" + sY;
//                     }
//                 }
//                 ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mLog", sLog, ClassCS.iniTempName);
//                 ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mSubSec", sSubSec, ClassCS.iniTempName);
//                 ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mSubPt", sSubPt, ClassCS.iniTempName);
//                 ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mPrerequiste1", sPrerequiste1, ClassCS.iniTempName);
//                 ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mPrerequiste2", sPrerequiste2, ClassCS.iniTempName);
//                 ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mXArray", sXArray, ClassCS.iniTempName);
//                 ClassIni.WriteIniData("Plus Calib Parameter" + ProNo, "mYArray", sYArray, ClassCS.iniTempName);

                bSaved = true;
            }
            catch (System.Exception ex)
            {
                Busiclass.MsgError(ex.Message.ToString());
            }
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
                            if (!(i <= 5 && j >= 2 && j <= 5) && !(i >= 1 && i <= 2 && j >= 7 && j <= 0xf) && !(i >= 0xC4 && i <= 0xdd))//这些是项目23的
                            {
                                ClassCS.gd_Byte[i, j] = 0;
                            }
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
                    }
                    for (int i = 0; i < 6; i++)//取对数,分段点 
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
//                 case 2:
//                     for (int y = 257; y <= 335; y++)//79行一个项目?
//                     {
//                         for (int x = 0; x <16; x++)
//                         {
//                             ClassCS.gd_Byte[y, x] = 0;
//                         }
//                     }
//                     for (int y = 416; y <= 451; y++)//Plus项目
//                     {
//                         for (int x = 0; x < 16; x++)
//                         {
//                             ClassCS.gd_Byte[y, x] = 0;
//                         }
//                     }
//                     break;
// 
//                 case 3:
//                     for (int y = 336; y <= 414; y++)
//                     {
//                         for (int x = 0; x <16; x++)
//                         {
//                             ClassCS.gd_Byte[y, x] = 0;
//                         }
//                     }
//                     for (int y = 452; y <= 487; y++)//Plus项目
//                     {
//                         for (int x = 0; x < 16; x++)
//                         {
//                             ClassCS.gd_Byte[y, x] = 0;
//                         }
//                     }
//                     break;

            }
        }


        //飞测Plus温度校准定标
        FrmTempCalib frmTCalib = new FrmTempCalib();
        private void btn_TempCalib_Click(object sender, EventArgs e)
        {
            //FrmTempCalib.openEch();
            frmTCalib.m_iProjectNumber = 2;// 多项目进入
            frmTCalib.SetCurve1TestMolecularValue(cm_T.SelectedIndex + 1);
            frmTCalib.SetCurve1TestDenominatorValue(cm_C.SelectedIndex + 1);
            if (!frmTCalib.bMdiShown)
            {
                frmTCalib.Show(this.Owner);
                frmTCalib.bMdiShown = true;                
            }
            else
                frmTCalib.Activate();
            
            frmTCalib.updataMoreCurveWidget();
        }

        #region   将参数生成Byte数组 WriteByte
        /// <summary>
        /// 将参数生成Byte数组
        /// </summary>
        private void WriteByte(int ProNo)//保存当前项目的设置
        {
            #region  获取 结果1,只保存了一个项目的结果
            StructSP[0].ProName = tb_ResultName1.Text;
            StructSP[0].Min = tb_ResultMin1.Text;
            StructSP[0].MinShow = tb_ResultMinShow1.Text;
            StructSP[0].Max = tb_ResultMax1.Text;
            StructSP[0].MaxShow = tb_ResultMaxShow1.Text;
            StructSP[0].MidShow = tb_ResultMid1.Text;
            StructSP[0].RangeMin = tb_ResultRangeBegin1.Text;
            StructSP[0].RangeMax = tb_ResultRangeEnd1.Text;
            #endregion

            //所有的选择  0:启用； 1:不启用 
            #region 临时变量
            byte[] byteArray;
            int Pos = 257, n = 0;
            #endregion

            //ProNo 2 :0x101   3:0x150  4:0x19F  每个项目4F（79）行
            #region 第0x101详细参数
            Pos = 0x101 + (ProNo - 2) * 79;//不是从256开始? 不同项目保存到不同地址,每个地址占用79行?
            n = 0;

            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte((cm_T.SelectedIndex + 1).ToString(), 10);//分子
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte((cm_C.SelectedIndex + 1).ToString(), 10);//分母

            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(gi_ConcentrationCount.ToString(), 10);//标准点总数
            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Log.SelectedIndex.ToString(), 10);//0:处理取对数   1：不取对数  第8个位置
            if (cm_SubCount.Text == "不分段")
                ClassCS.gd_Byte[Pos, n++] = 1; //不分段
            else
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_SubCount.Text, 10);//分段的段数

            #region 温补（上半字节）、区域匹配(下半字节)是否启用
            if (cm_Temperature.Text == "启用")//是否启用温度补偿 启用:0  高位
            {//n=5
                ClassCS.gd_Byte[Pos, n++] = 0x00;
            }
            else //不启用: 1
            {
                ClassCS.gd_Byte[Pos, n++] = 0x10;
            }
            #endregion

            //加样冲顶提示是否启用
            if (cb_Overflow.Checked)// 启用冲顶提示: 低半字节存是否冲顶,高半字节存加样提示
            {//n = 6
                if (cb_Shortage.Checked)
                    ClassCS.gd_Byte[Pos, n++] = 0x00;//0为提示
                else
                    ClassCS.gd_Byte[Pos, n++] = 0x10;
            }
            else //不启用: 1
            {
                if (cb_Shortage.Checked)
                    ClassCS.gd_Byte[Pos, n++] = 0x01;//0为提示
                else
                    ClassCS.gd_Byte[Pos, n++] = 0x11;
            }
            //冲顶加样峰值名称
            ClassCS.gd_Byte[Pos, n++] = (byte)cbBoxOverflow.SelectedIndex;
            ClassCS.gd_Byte[Pos, n++] = (byte)cbBoxShortage.SelectedIndex;

            ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_DecimalsDigit.Text, 10);//小数点的位数
            #endregion

            #region 第102(十六进制)行  1020H-102FH 分段点位置
            Pos = 0x102 + (ProNo - 2) * 79;
            n = 0;
            if (cm_SubCount.Text == "不分段")//不分段写在0位置  分段从1位置开始写
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(gi_ConcentrationCount.ToString(), 10);//不分段写在0位置
            else
            {
                n++;
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(tb_Subsection1.Text, 10);//分段从1位置开始写
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection1.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection2.Text, 10, 16), 16);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Busiclass.ConvertString(tb_Subsection2.Text, 10, 16) + Busiclass.ConvertString(tb_Subsection3.Text, 10, 16), 16);
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
            //冲顶值 1a1DH-1a1FH 三个位置存 T2段判断是否冲顶 0-260000  add by zhou zhang kui 2012-10-09
            string Overflow = Busiclass.ConvertString(tb_Overflow.Text, 10, 16).PadLeft(6, '0');
            ClassCS.gd_Byte[Pos, 13] = Convert.ToByte(Overflow.Substring(0, 2), 16);//高
            ClassCS.gd_Byte[Pos, 14] = Convert.ToByte(Overflow.Substring(2, 2), 16);//中
            ClassCS.gd_Byte[Pos, 15] = Convert.ToByte(Overflow.Substring(4, 2), 16);//低 
            #endregion

            #region 第103(十六进制)行  1030H-103FH 每段对应的拟合方法
            Pos = 0x103 + (ProNo - 2) * 79; ;
            n = 0;
            if (cm_SubCount.Text == "不分段")//不分段写在0位置  分段从1位置开始写
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method0.SelectedIndex.ToString(), 10);//不分段写在0位置
            else
            {
                n++;
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method1.SelectedIndex.ToString(), 10);//分段从1位置开始写
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method2.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method3.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method4.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method5.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method6.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method7.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method8.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method9.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method10.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method11.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method12.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method13.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method14.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Method15.SelectedIndex.ToString(), 10);
            }

            //1a2DH-1a2FH 三个位置存 加样临界值 0-260000  add by zhou zhang kui 2012-10-09
            string Shortage = Busiclass.ConvertString(tb_Shortage.Text, 10, 16).PadLeft(6, '0'); //Convert.ToString(Convert.ToInt32(tb_Shortage.Text), 16).PadLeft(6, '0');
            ClassCS.gd_Byte[Pos, 13] = Convert.ToByte(Shortage.Substring(0, 2), 16);
            ClassCS.gd_Byte[Pos, 14] = Convert.ToByte(Shortage.Substring(2, 2), 16);
            ClassCS.gd_Byte[Pos, 15] = Convert.ToByte(Shortage.Substring(4, 2), 16);
            #endregion

            #region 第104(十六进制)行 每段幂数限制条件
            Pos = 0x104 + (ProNo - 2) * 79;
            n = 0;
            if (cm_SubCount.Text == "不分段")//不分段写在0位置  分段从1位置开始写
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite0.SelectedIndex.ToString(), 10);//不分段写在0位置
            else
            {
                n++;
                string Prerequisite = cm_Prerequisite1.SelectedIndex.ToString();
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Prerequisite, 10);//分段从1位置开始写
                Prerequisite = cm_Prerequisite2.SelectedIndex.ToString();
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Prerequisite, 10);
                Prerequisite = cm_Prerequisite3.SelectedIndex.ToString();
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(Prerequisite, 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite4.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite5.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite6.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite7.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite8.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite9.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite10.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite11.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite12.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite13.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite14.SelectedIndex.ToString(), 10);
                ClassCS.gd_Byte[Pos, n++] = Convert.ToByte(cm_Prerequisite15.SelectedIndex.ToString(), 10);
            }
            #endregion

            #region 第105-114(十六进制)行 X值 浓度值    第115-124行  最多16个参数??
            //0x105 -0x114 X值 浓度值     0x115-0x124 荧光值
            Pos = 0x105 + (ProNo - 2) * 79;
            for (int i = 0; i < gi_ConcentrationCount; i++)
            {
                WriteDouble(ld_XArray[i], ref ClassCS.gd_Byte, i + Pos, 0);//X值 浓度值
                WriteDouble(ld_YArray[i], ref ClassCS.gd_Byte, i + Pos + 16, 0);//Y值 TC测试值(X最多16个了?)
            }
            #endregion

            #region 参数

            #region  0x125  血清 、 血浆子数
            Pos = 0x125 + (ProNo - 2) * 79;
            WriteDouble(Convert.ToDouble(tb_Serum.Text), ref ClassCS.gd_Byte, Pos, 0);
            #endregion

            #region 第 0x126 (十六进制)行 全血
            Pos = 0x126 + (ProNo - 2) * 79;
            WriteDouble(Convert.ToDouble(tb_WholeBlood.Text), ref ClassCS.gd_Byte, Pos, 0);
            #endregion

            #region 第 0x127(十六进制)行 尿液
            Pos = 0x127 + (ProNo - 2) * 79;
            WriteDouble(Convert.ToDouble(tb_UrineValue.Text), ref ClassCS.gd_Byte, Pos, 0);
            #endregion

            #region 第 0x128(十六进制)行 粪便
            Pos = 0x128 + (ProNo - 2) * 79;
            WriteDouble(Convert.ToDouble(tb_Excrement.Text), ref ClassCS.gd_Byte, Pos, 0);
            #endregion

            #region 第 0x129(十六进制)行 质检子数
            Pos = 0x129 + (ProNo - 2) * 79;
            WriteDouble(Convert.ToDouble(tb_Quality.Text), ref ClassCS.gd_Byte, Pos, 0);
            #endregion

            #region 第 0x12A(十六进制)行 a
            Pos = 0x12A + (ProNo - 2) * 79;
            WriteDouble(Convert.ToDouble(tb_a.Text), ref ClassCS.gd_Byte, Pos, 0);
            #endregion

            #region 第0x12B(十六进制)行 b
            Pos = 0x12B + (ProNo - 2) * 79;
            WriteDouble(Convert.ToDouble(tb_b.Text), ref ClassCS.gd_Byte, Pos, 0);
            #endregion

            #region 第0x12C(十六进制)行 a1
            Pos = 0x12C + (ProNo - 2) * 79;
            WriteDouble(Convert.ToDouble(tb_a1.Text), ref ClassCS.gd_Byte, Pos, 0);
            #endregion

            #region 第0x12D(十六进制)行 b1
            Pos = 0x12D + (ProNo - 2) * 79;
            WriteDouble(Convert.ToDouble(tb_b1.Text), ref ClassCS.gd_Byte, Pos, 0);
            #endregion

            #endregion

            //Pos++;//报告单标题

            #region 第0x12F (十六进制)行 单位
            Pos = 0x12F + (ProNo - 2) * 79;
            byteArray = System.Text.Encoding.Default.GetBytes(tb_Unit.Text);
            if (byteArray.Length > 16)
            {
                Busiclass.MsgError("输入的 单位 大于设定的长度(8个汉字),请重新输入！ ");
                return;
            }
            for (int i = 0; i < byteArray.Length; i++)
                ClassCS.gd_Byte[Pos, i] = byteArray[i];
            #endregion


            #region 第0x130-0x134 (十六进制)行 检测范围
            Pos = 0x130 + (ProNo - 2) * 79;
            for (int i = 0; i < 5; i++)
            {
                if (StructSP[i].RangeMin.Trim() != "")
                    WriteDouble(Convert.ToDouble(StructSP[i].RangeMin.Trim()), ref ClassCS.gd_Byte, i + Pos, 1);
                if (StructSP[i].RangeMax.Trim() != "")
                    WriteDouble(Convert.ToDouble(StructSP[i].RangeMax.Trim()), ref ClassCS.gd_Byte, i + Pos, 2);
            }
            //Pos += 4;
            #endregion

            #region 第0x135-0x139 (十六进制)行 检验项目名称
            Pos = 0x135 + (ProNo - 2) * 79;
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
            //Pos += 4;//0x139
            #endregion

            //Pos++;//0x13a

            Pos = 0x14F + (ProNo - 2) * 79;//0x14F,往前存

            #region 14F 显示结果
            //最后一行：0字节存 结果的个数， 1字节开始存
            //单个结果的协议包：最小比值+ "&" +小于最小的显示+ "&" +最大比值+ "&" +大于最大的显示+ "&" +中间值的显示结果 + 间隔符## +.......... +整个结束符%%%%
            string ls_Result = "";
            int li_ResultCount = 0;
            for (int i = 0; i < 5; i++)
            {
                if (StructSP[i].ProName.Trim() != "" && StructSP[i].ProName.Trim() != "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0")
                {
                    li_ResultCount++;
                    ls_Result += StructSP[i].Min + "&" + StructSP[i].MinShow + "&" + StructSP[i].Max + "&" + StructSP[i].MaxShow + "&" + StructSP[i].MidShow + "##";
                }
            }
            //ClassCS.gd_Byte[255, 0] = (byte)li_ResultCount;
            ls_Result = li_ResultCount.ToString() + ls_Result + "%%%%";
            byteArray = System.Text.Encoding.Default.GetBytes(ls_Result);

            n = 0;
            int m = 1;
            for (int i = 0; i < byteArray.Length; i++) //最后一行 存15个字节
            {
                ClassCS.gd_Byte[Pos - m + 1, n] = byteArray[i];
                n++;
                if (i == (m * 16 - 1))
                {
                    n = 0;
                    m++;
                }
            }
            #endregion


      #region 飞测Plus参数
            #region 第0x1a0(十六进制)行开始 6个温度定标曲线的方程系数
            Pos = 0x1a0 + (ProNo - 2) * 42;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (frmTCalib.a0[i] != null)
                    {
                        WriteDoubleNew(frmTCalib.a0[i][j], Pos + 7 * i + j, 0);//曲线1
                        WriteDoubleNew(frmTCalib.a1[i][j], Pos + 7 * i + j, 8);//曲线1
                    }
                }
            }
            #endregion
            #region 16个浓度值(只曲线1) 2016.9.18 
            // 保存项目二和项目三的曲线1
            Pos = 0x0e + (ProNo - 2) * 8;
            for (int i = 0; i < 8; i++)
            {
                WriteDoubleNew(frmTCalib.ld_XArray[0][2 * i], Pos + i, 0);
                WriteDoubleNew(frmTCalib.ld_XArray[0][2 * i + 1], Pos + i, 8);
            }
            // 项目二曲线二
            if (ProNo == 2)
            {
                for (int i = 0; i < 8; i++)
                {
                    WriteDoubleNew(frmTCalib.ld_XArray[1][2 * i], 0x1ee + i, 0);
                    WriteDoubleNew(frmTCalib.ld_XArray[1][2 * i + 1], 0x1ee + i, 8);
                }
            }            
            // 项目三曲线二
            if (ProNo == 3)
            {
                for (int i = 0; i < 8; i++)
                {
                    WriteDoubleNew(frmTCalib.ld_XArray[1][2 * i], 0x1f7 + i, 0);
                    WriteDoubleNew(frmTCalib.ld_XArray[1][2 * i + 1], 0x1f7 + i, 8);
                }
            }            
            #endregion
            //////////////////////////////////////////////////////////////////////////
            // 项目二是否是多曲线，存储多曲线数值
            int iTwoProHasMoreCurvePos = 0;
            bool bHasMoreCurvePos = false;
            if (ProNo == 2)
            {
                iTwoProHasMoreCurvePos = 0x1f6;
                bHasMoreCurvePos = true;
            }
            
            // 项目三是否是多曲线，存储多曲线数值
            if (ProNo == 3)
            {
                iTwoProHasMoreCurvePos = 0x1ff;
                bHasMoreCurvePos = true;
            }        
            //
            if(bHasMoreCurvePos = true)
            {
                bool bMoreCurveFlag = Convert.ToBoolean(frmTCalib.GetMoreCurveFlag());
                if (bMoreCurveFlag)
                {
                    // flag值
                    ClassCS.gd_Byte[iTwoProHasMoreCurvePos, 8] = (byte)1;
                    // A值
                    WriteDoubleNew(frmTCalib.GetMoreCurveAValue(), iTwoProHasMoreCurvePos, 0);
                    // 测试值
                    ClassCS.gd_Byte[iTwoProHasMoreCurvePos, 9] = (byte)frmTCalib.GetTestValue();
                    // 分子
                    ClassCS.gd_Byte[iTwoProHasMoreCurvePos, 10] = (byte)frmTCalib.GetTestMolecularValue();
                    // 分母
                    ClassCS.gd_Byte[iTwoProHasMoreCurvePos, 11] = (byte)frmTCalib.GetTestDenominatorValue();
                    // AFlag
                    ClassCS.gd_Byte[iTwoProHasMoreCurvePos, 12] = (byte)frmTCalib.GetMoreCurveAFlag();
                }
                else
                {
                    // flag值
                    ClassCS.gd_Byte[iTwoProHasMoreCurvePos, 8] = (byte)0;
                }
            }
            
            //////////////////////////////////////////////////////////////////////////
            //首尾浓度,分段点浓度,分段点TC
            Pos = 0xc4 + (ProNo - 2) * 13;
            for (int i = 0; i < 6; i++)
            {
                WriteDoubleNew(frmTCalib.XFirst[i], Pos + 2 * i, 0);
                WriteDoubleNew(frmTCalib.XLast[i], Pos + 2 * i + 1, 0);
                WriteDoubleNew(frmTCalib.XMid[i], Pos + 2 * i, 8);
                WriteDoubleNew(frmTCalib.YFirst[i], Pos + 2 * i + 1, 8);
            }
            //标准点数, 温度值
            Pos = 0xd0 + (ProNo - 2) * 13;
            for (int i = 0; i < 6; i++)
            {
                ClassCS.gd_Byte[Pos, i] = (byte)frmTCalib.li_CountP[i];
                ClassCS.gd_Byte[Pos, 8 + i] = frmTCalib.iTemp[i];
            }

            //是否取对数,分段点位置
            for (int i = 0; i < 6; i++)
            {
                ClassCS.gd_Byte[i, (ProNo - 1) * 2] = (byte)(frmTCalib.mLog[i]);//1为取对数
                ClassCS.gd_Byte[i, (ProNo - 1) * 2 + 1] = (byte)frmTCalib.mSubPt[i];//0为不分段
            }
            //加样冲顶提示是否启用(第二项目第1行,第3项在2行)
            if (cb_OverflowP.Checked)// 启用冲顶提示: 低半字节存是否冲顶,高半字节存加样提示
            {
                if (cb_ShortageP.Checked)
                    ClassCS.gd_Byte[ProNo - 1, 7] = 0x00;//0为提示
                else
                    ClassCS.gd_Byte[ProNo - 1, 7] = 0x10;
            }
            else //不启用: 1
            {
                if (cb_ShortageP.Checked)
                    ClassCS.gd_Byte[ProNo - 1, 7] = 0x01;//0为提示
                else
                    ClassCS.gd_Byte[ProNo - 1, 7] = 0x11;
            }
            //冲顶加样峰值名称
            ClassCS.gd_Byte[ProNo - 1, 8] = (byte)cbBoxOverflowP.SelectedIndex;
            ClassCS.gd_Byte[ProNo - 1, 9] = (byte)cbBoxShortageP.SelectedIndex;
            //冲顶临界值0-260000
            Pos = ProNo-1;//1行2行
            string OverflowP = Busiclass.ConvertString(tb_OverflowP.Text, 10, 16).PadLeft(6, '0');
            ClassCS.gd_Byte[Pos, 0x0a] = Convert.ToByte(OverflowP.Substring(0, 2), 16);
            ClassCS.gd_Byte[Pos, 0x0b] = Convert.ToByte(OverflowP.Substring(2, 2), 16);
            ClassCS.gd_Byte[Pos, 0x0c] = Convert.ToByte(OverflowP.Substring(4, 2), 16);
            //加样临界值 
            string ShortageP = Busiclass.ConvertString(tb_ShortageP.Text, 10, 16).PadLeft(6, '0');
            ClassCS.gd_Byte[Pos, 0x0d] = Convert.ToByte(ShortageP.Substring(0, 2), 16);
            ClassCS.gd_Byte[Pos, 0x0e] = Convert.ToByte(ShortageP.Substring(2, 2), 16);
            ClassCS.gd_Byte[Pos, 0x0f] = Convert.ToByte(ShortageP.Substring(4, 2), 16);

         #endregion

            Busiclass.MsgOK("保存数据成功！");
            // WriteHex();
        }
        #endregion
        private void WriteDoubleNew(double a, int row, int btOffset)//新方式写double型数据 btOffset:偏移的字节数(0或8)
        {
            byte[] btArray = BitConverter.GetBytes(a);
            if (btArray.Length != 8)
            {
                MessageBox.Show("double型转字节错误!");
            }
            for (int i = 0; i < 8; i++)
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

        #region 读取double型的数据 ReadDouble
        /// <summary>
        ///  写double型的数据
        /// </summary>
        /// <param name="X">当前值</param>
        /// <param name="n">写入的行 </param>
        /// <param mode="n">写入类型 0:表示保留8位有效数字(0-7)，第15位 位标志位  
        /// 1: 表示保留7位有效数字(0-6)，第7位 位标志位
        /// 2: 表示保留7位有效数字(8-14)，第15位 位标志位</param>
        private double ReadDouble(int n, int mode)
        {
            double a = 0;
            string Bz = "", str = "";
            int li_Pow = 0, li_Zf = 0;
            if (mode == 0)
            {
                for (int i = 0; i < 8; i++)
                    str += ClassCS.gd_Byte[n, i].ToString();
                a = Convert.ToDouble(str) / 10000000;//8个有效数字
                Bz = Busiclass.ConvertString(Busiclass.ConvertString(ClassCS.gd_Byte[n, 15].ToString(), 10, 16), 16, 2).PadLeft(8, '0');
            }
            else if (mode == 1)
            {
                for (int i = 0; i < 7; i++)
                    str += ClassCS.gd_Byte[n, i].ToString();
                a = Convert.ToDouble(str) / 1000000;//7个有效数字
                Bz = Busiclass.ConvertString(Busiclass.ConvertString(ClassCS.gd_Byte[n, 7].ToString(), 10, 16), 16, 2).PadLeft(8, '0');
            }
            else if (mode == 2)
            {
                for (int i = 8; i < 15; i++)
                    str += ClassCS.gd_Byte[n, i].ToString();
                a = Convert.ToDouble(str) / 1000000;//7个有效数字
                Bz = Busiclass.ConvertString(Busiclass.ConvertString(ClassCS.gd_Byte[n, 15].ToString(), 10, 16), 16, 2).PadLeft(8, '0');
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
            byte[] btArray = new byte[8];
            for (int i = 0; i < 8; i++)
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
            string sCoeff = iZS.ToString() + "." + iXS.ToString().PadLeft(9, '0');

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

        #region  ReadHex 读取Hex文件
        /// <summary>
        /// 读取Hex文件
        /// </summary>
        private void ReadHex(byte[] by, int ProNo)
        {
            string str = "";

            int Pos = 512;
            int n = 0;
            byte byd = 0;
            try
            {
                //ProNo 2 :0x101   3:0x150  4:0x19F  每个项目4F（79）行
                #region  101H 位置的数据
                Pos = 0x101 + (ProNo - 2) * 79;
                n = 0;

                cm_T.SelectedIndex = ClassCS.gd_Byte[Pos, n++] - 1;
                cm_C.SelectedIndex = ClassCS.gd_Byte[Pos, n++] - 1;
                gi_ConcentrationCount = ClassCS.gd_Byte[Pos, n++];

                cm_Log.SelectedIndex = ClassCS.gd_Byte[Pos, n++];
                byd = ClassCS.gd_Byte[Pos, n++];
                if (byd == 1)
                    cm_SubCount.SelectedIndex = 0;
                else
                    cm_SubCount.Text = byd.ToString();
                byd = ClassCS.gd_Byte[Pos, n++];//温补
                cm_Temperature.SelectedIndex = byd >> 4;
                //冲顶加样
                byd = ClassCS.gd_Byte[Pos, n++];
                cb_Overflow.Checked = (byd & 0x0f) == 0;
                cb_Shortage.Checked = (byd & 0xf0) == 0;
                //冲顶加样峰值名称
                cbBoxOverflow.SelectedIndex = ClassCS.gd_Byte[Pos, n++];
                cbBoxShortage.SelectedIndex = ClassCS.gd_Byte[Pos, n++];

                tb_DecimalsDigit.Text = ClassCS.gd_Byte[Pos, n++].ToString();
                #endregion

                #region 分段的起始点 取103H 位置的数据
                Pos = 0x103 + (ProNo - 2) * 79; //1030H
                //if (cm_SubCount.Text == "不分段")
                {
                    cm_Method0.SelectedIndex = ClassCS.gd_Byte[Pos, 0];//0x103处理方式
                    cm_Prerequisite0.SelectedIndex = ClassCS.gd_Byte[Pos + 1, 0];//0x104
                }
                //else
                {
                    #region 0x102 H 分段点起始位置
                    //0x1021 1022 H
                    Pos = 0x102 + (ProNo - 2) * 79;
                    str = Busiclass.ConvertString(ClassCS.gd_Byte[Pos, 1].ToString(), 10, 16).PadLeft(2, '0');
                    tb_Subsection1.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                    str = Busiclass.ConvertString(ClassCS.gd_Byte[Pos, 2].ToString(), 10, 16).PadLeft(2, '0');
                    tb_Subsection2.Text = Convert.ToInt32(str.Substring(1, 1), 16).ToString();
                    //冲顶临界值
                    string date = Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[Pos, 13]), 16).PadLeft(2, '0') +
                              Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[Pos, 14]), 16).PadLeft(2, '0') +
                              Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[Pos, 15]), 16).PadLeft(2, '0');
                    tb_Overflow.Text = Convert.ToInt32(date, 16).ToString();

                    #endregion

                    #region 0x1031 0x1032 H 每段对应的拟合方法
                    Pos = 0x103 + (ProNo - 2) * 79;
                    cm_Method1.SelectedIndex = ClassCS.gd_Byte[Pos, 1];
                    cm_Method2.SelectedIndex = ClassCS.gd_Byte[Pos, 2];
                    //加样临界值
                    date = Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[Pos, 13]), 16).PadLeft(2, '0') +
                                  Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[Pos, 14]), 16).PadLeft(2, '0') +
                                  Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[Pos, 15]), 16).PadLeft(2, '0');
                    tb_Shortage.Text = Convert.ToInt32(date, 16).ToString();

                    #endregion

                    #region 0x104 0x104 H 每段对应的限制条件
                    Pos = 0x104 + (ProNo - 2) * 79;
                    cm_Prerequisite1.SelectedIndex = ClassCS.gd_Byte[Pos, 1];
                    cm_Prerequisite2.SelectedIndex = ClassCS.gd_Byte[Pos, 2];
                    #endregion
                }
                #endregion

                #region 105H- 114H读取X的值 浓度    115H- 124H读取Y的值 对应的测试值
                Pos = 0x105 + (ProNo - 2) * 79;
                for (int i = 0; i < gi_ConcentrationCount; i++)
                {
                    ld_XArray[i] = ReadDouble(Pos + i, 0);
                    ld_YArray[i] = ReadDouble(Pos + i + 16, 0);
                }
                for (int i = 0; i < gi_ConcentrationCount; i++)
                {
                    listView1.Items[i].SubItems[0].Text = (i + 1).ToString();
                    listView1.Items[i].SubItems[1].Text = Convert.ToDouble(ld_XArray[i].ToString("F8")).ToString();
                    listView1.Items[i].SubItems[2].Text = Convert.ToDouble(ld_YArray[i].ToString("F8")).ToString();
                }
                #endregion

                #region 125H 读取血清、血浆子数
                Pos = 0x125 + (ProNo - 2) * 79;

                tb_Serum.Text = Convert.ToDouble(ReadDouble(Pos, 0).ToString("F8")).ToString();
                #endregion

                #region 126H 全血子数
                Pos = 0x126 + (ProNo - 2) * 79;
                tb_WholeBlood.Text = Convert.ToDouble(ReadDouble(Pos, 0).ToString("F8")).ToString();
                #endregion

                #region 127H 尿液子数
                Pos = 0x127 + (ProNo - 2) * 79;
                tb_UrineValue.Text = Convert.ToDouble(ReadDouble(Pos, 0).ToString("F8")).ToString();
                #endregion

                #region 128H 粪便子数
                Pos = 0x128 + (ProNo - 2) * 79;
                tb_Excrement.Text = Convert.ToDouble(ReadDouble(Pos, 0).ToString("F8")).ToString();
                #endregion

                #region 129H 质检子数
                Pos = 0x129 + (ProNo - 2) * 79;
                tb_Quality.Text = Convert.ToDouble(ReadDouble(Pos, 0).ToString("F8")).ToString();
                #endregion

                #region 12AH a
                Pos = 0x12A + (ProNo - 2) * 79;
                tb_a.Text = Convert.ToDouble(ReadDouble(Pos, 0).ToString("F8")).ToString();
                #endregion

                #region 12BH b
                Pos = 0x12B + (ProNo - 2) * 79;
                tb_b.Text = Convert.ToDouble(ReadDouble(Pos, 0).ToString("F8")).ToString();
                #endregion

                #region 12CH a1
                Pos = 0x12C + (ProNo - 2) * 79;
                tb_a1.Text = Convert.ToDouble(ReadDouble(Pos, 0).ToString("F8")).ToString();
                #endregion

                #region 12DH b1
                Pos = 0x12D + (ProNo - 2) * 79;
                tb_b1.Text = Convert.ToDouble(ReadDouble(Pos, 0).ToString("F8")).ToString();
                #endregion


                #region 12EH 报告单标题
                str = "";
                Pos = 0x12E + (ProNo - 2) * 79;
                for (int i = 0; i < 16; i++)
                    str += Busiclass.ConvertString(ClassCS.gd_Byte[Pos, i].ToString(), 10, 16).PadLeft(2, '0');
                //tb_TitleName.Text = ClassCS.UnHex(str, "gb2312");
                #endregion

                #region 12FH 单位
                str = "";
                Pos = 0x12F + (ProNo - 2) * 79;
                for (int i = 0; i < 16; i++)
                    str += Busiclass.ConvertString(ClassCS.gd_Byte[Pos, i].ToString(), 10, 16).PadLeft(2, '0');
                tb_Unit.Text = ClassCS.UnHex(str, "gb2312");
                #endregion

                #region 130H - 134 检测范围
                Pos = 0x130 + (ProNo - 2) * 79;
                StructSP[0].RangeMin = ReadDouble(Pos, 1).ToString("F8");
                StructSP[0].RangeMax = ReadDouble(Pos, 2).ToString("F8");

                Pos = 0x131 + (ProNo - 2) * 79;
                StructSP[1].RangeMin = ReadDouble(Pos, 1).ToString("F8");
                StructSP[1].RangeMax = ReadDouble(Pos, 2).ToString("F8");

                Pos = 0x132 + (ProNo - 2) * 79;
                StructSP[2].RangeMin = ReadDouble(Pos, 1).ToString("F8");
                StructSP[2].RangeMax = ReadDouble(Pos, 2).ToString("F8");

                Pos = 0x133 + (ProNo - 2) * 79;
                StructSP[3].RangeMin = ReadDouble(Pos, 1).ToString("F8");
                StructSP[3].RangeMax = ReadDouble(Pos, 2).ToString("F8");

                Pos = 0x134 + (ProNo - 2) * 79;
                StructSP[4].RangeMin = ReadDouble(Pos, 1).ToString("F8");
                StructSP[4].RangeMax = ReadDouble(Pos, 2).ToString("F8");
                #endregion

                #region 860H - 8A0 检测项目名称
                str = "";
                Pos = 0x135 + (ProNo - 2) * 79;
                for (int i = 0; i < 16; i++)
                    str += Busiclass.ConvertString(ClassCS.gd_Byte[Pos, i].ToString(), 10, 16).PadLeft(2, '0');
                tb_ResultName1.Text = ClassCS.UnHex(str, "gb2312");
                StructSP[0].ProName = tb_ResultName1.Text;

                Pos = 0x136 + (ProNo - 2) * 79;
                str = "";
                for (int i = 0; i < 16; i++)
                    str += Busiclass.ConvertString(ClassCS.gd_Byte[Pos, i].ToString(), 10, 16).PadLeft(2, '0');
                StructSP[1].ProName = ClassCS.UnHex(str, "gb2312");

                Pos = 0x137 + (ProNo - 2) * 79;
                str = "";
                for (int i = 0; i < 16; i++)
                    str += Busiclass.ConvertString(ClassCS.gd_Byte[Pos, i].ToString(), 10, 16).PadLeft(2, '0');
                StructSP[2].ProName = ClassCS.UnHex(str, "gb2312");

                Pos = 0x138 + (ProNo - 2) * 79;
                str = "";
                for (int i = 0; i < 16; i++)
                    str += Busiclass.ConvertString(ClassCS.gd_Byte[Pos, i].ToString(), 10, 16).PadLeft(2, '0');
                StructSP[3].ProName = ClassCS.UnHex(str, "gb2312");

                Pos = 0x139 + (ProNo - 2) * 79;
                str = "";
                for (int i = 0; i < 16; i++)
                    str += Busiclass.ConvertString(ClassCS.gd_Byte[Pos, i].ToString(), 10, 16).PadLeft(2, '0');
                StructSP[4].ProName = ClassCS.UnHex(str, "gb2312");
                #endregion


                Pos = 0x14F + (ProNo - 2) * 79;
                #region  获取 结果1 最后一行开始 打印内容
                //最后一行：0字节存 结果的个数， 1字节开始存
                //单个结果的协议包：最小比值+ "$" +小于最小的显示+ "$" +最大比值+ "$" +大于最大的显示+ "$" +中间值的显示结果 + 间隔符## +.......... +整个结束符%%%%
                //Pos = 4080; //0xFF0 H
                string ls_Result = "";
                while (true)
                {
                    str = "";
                    for (int i = 0; i < 16; i++)
                        str += Busiclass.ConvertString(ClassCS.gd_Byte[Pos, i].ToString(), 10, 16).PadLeft(2, '0');
 
                    ls_Result += ClassCS.UnHex(str, "gb2312");
                    Pos--;
                    int li_Pos = ls_Result.IndexOf("%%%%");
                    if (li_Pos > 0)//存在3个%
                    {
                        int li_ResultCount = Convert.ToInt32(ls_Result.Substring(0, 1));
                        if (li_ResultCount == 0 || li_Pos<=3)//CYQ2016.8.1
                            break;
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


            #region 飞测Plus相关
                //读取加样冲顶相关
                byd = ClassCS.gd_Byte[ProNo - 1, 7];//加样和冲顶提示(第一行和第二行)
                if (byd >> 4 == 0)
                    cb_ShortageP.Checked = true;
                else
                    cb_ShortageP.Checked = false;
                if ((byd & 0x0F) == 0)
                    cb_OverflowP.Checked = true;
                else
                    cb_OverflowP.Checked = false;
                //峰值名称
                if (ClassCS.gd_Byte[ProNo - 1, 8] == 255)//冲顶
                    cbBoxOverflowP.Text = "T2";
                else
                    cbBoxOverflowP.SelectedIndex = ClassCS.gd_Byte[ProNo - 1, 8];
                if (ClassCS.gd_Byte[ProNo - 1, 9] == 255)//加样
                    cbBoxShortageP.Text = "C";
                else
                    cbBoxShortageP.SelectedIndex = ClassCS.gd_Byte[ProNo - 1, 9];
                //临界值
                string dateP = Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[ProNo - 1, 0x0a]), 16).PadLeft(2, '0') +
                              Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[ProNo - 1, 0x0b]), 16).PadLeft(2, '0') +
                              Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[ProNo - 1, 0x0c]), 16).PadLeft(2, '0');
                tb_OverflowP.Text = Convert.ToInt32(dateP, 16).ToString();
                dateP = Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[ProNo - 1, 0x0d]), 16).PadLeft(2, '0') +
                              Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[ProNo - 1, 0x0e]), 16).PadLeft(2, '0') +
                              Convert.ToString(Convert.ToInt32(ClassCS.gd_Byte[ProNo - 1, 0x0f]), 16).PadLeft(2, '0');
                tb_ShortageP.Text = Convert.ToInt32(dateP, 16).ToString();
                
                //读取方程系数
                Pos = 0x1a0 + (ProNo - 2) * 42;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        frmTCalib.a0[i][j] = ReadDoubleNew(Pos + 7 * i + j, 0);//曲线1
                        frmTCalib.a1[i][j] = ReadDoubleNew(Pos + 7 * i + j, 8);//曲线2
                    }
                    
                }
                //读曲线1所有浓度值，指项目二和项目三的曲线
                for (int i = 0; i < 6; i++)
                {
                    if (frmTCalib.ld_XArray[i] == null)
                    {
                        frmTCalib.ld_XArray[i] = new double[16];
                        frmTCalib.ld_YArray[i] = new double[16];
                    }
                }
                Pos = 0x0e + (ProNo - 2) * 8;
                for (int i = 0; i < 8; i++)
                {
                    frmTCalib.ld_XArray[0][2 * i] = ReadDoubleNew(Pos + i, 0);
                    frmTCalib.ld_XArray[0][2 * i + 1] = ReadDoubleNew(Pos + i, 8);
                }
                // 项目二的曲线二
                if (ProNo == 2)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        frmTCalib.ld_XArray[1][2 * i] = ReadDoubleNew(0x1ee + i, 0);
                        frmTCalib.ld_XArray[1][2 * i + 1] = ReadDoubleNew(0x1ee + i, 8);
                    }
                }
                // 项目三的曲线二,
                if (ProNo == 3)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        frmTCalib.ld_XArray[1][2 * i] = ReadDoubleNew(0x1f7 + i, 0);
                        frmTCalib.ld_XArray[1][2 * i + 1] = ReadDoubleNew(0x1f7 + i, 8);
                    }
                }
                
                //////////////////////////////////////////////////////////////////////////
                // 读取多曲线属性
                int iHasMoreCurvePos = 0;
                bool bHasMoreCurvePos = false;
                if (ProNo == 2)
                {
                   iHasMoreCurvePos = 0x1f6;// flag值  
                   bHasMoreCurvePos = true;
                }
                if (ProNo == 3)
                {
                    iHasMoreCurvePos = 0x1ff;// flag值   
                    bHasMoreCurvePos = true;
                }
                if (bHasMoreCurvePos)
                {
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
                }
                //////////////////////////////////////////////////////////////////////////
                //读首位浓度值
                Pos = 0xc4 + (ProNo - 2) * 13;
                for (int i = 0; i < 6; i++)
                {
                    frmTCalib.li_CountP[i] = ClassCS.gd_Byte[0xD0 + (ProNo - 2) * 13, i];
                    frmTCalib.XFirst[i] = ReadDoubleNew(Pos + 2 * i, 0);//首浓度
                    frmTCalib.XLast[i] = ReadDoubleNew(Pos + 1 + 2 * i, 0);//尾浓度
                    frmTCalib.XMid[i] = ReadDoubleNew(Pos + 2 * i, 8);//分段点浓度
                    frmTCalib.YFirst[i] = ReadDoubleNew(Pos + 1 + 2 * i, 8);//分段点TC
                }
                if (ClassCS.bHaveIni)//有ini才去读取tempini文件
                {
                    //从ini读取三联卡定标数据
                    string sLog = "", sSubSec = "", sSubPt = "", sPrerequiste1 = "", sPrerequiste2 = "", sXArray = "", sYArray = "";
                    sLog = ClassIni.ReadIniData("Plus Calib Parameter" + ProNo, "mLog", ClassCS.iniTempName);//项目2,3是从temp.ini读取,保存
                    sSubSec = ClassIni.ReadIniData("Plus Calib Parameter" + ProNo, "mSubSec", ClassCS.iniTempName);
                    sSubPt = ClassIni.ReadIniData("Plus Calib Parameter" + ProNo, "mSubPt", ClassCS.iniTempName);
                    sPrerequiste1 = ClassIni.ReadIniData("Plus Calib Parameter" + ProNo, "mPrerequiste1", ClassCS.iniTempName);
                    sPrerequiste2 = ClassIni.ReadIniData("Plus Calib Parameter" + ProNo, "mPrerequiste2", ClassCS.iniTempName);
                    sXArray = ClassIni.ReadIniData("Plus Calib Parameter" + ProNo, "mXArray", ClassCS.iniTempName);
                    sYArray = ClassIni.ReadIniData("Plus Calib Parameter" + ProNo, "mYArray", ClassCS.iniTempName);
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

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        //读取加载
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] by = new byte[256 * 16];
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.DefaultExt = "Hex";
                openDialog.Filter = "Hex文件|*.Hex";
                openDialog.RestoreDirectory = true;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    init();
                    //ClearShowPriner();
                    ld_XArray = new double[32];
                    ld_YArray = new double[32];

                    System.IO.FileStream fs = new System.IO.FileStream(openDialog.FileName, System.IO.FileMode.Open);
                    bt_ClearTemp.PerformClick();
                    //ClassCS.gd_Byte = new byte[256 * 16];
                    fs.Read(by, 0, by.Length);
                    fs.Close();

                    //ClassCS.gd_Byte = new byte[256 * 16];

                    //ClassCS.gd_Byte = new byte[256 * 2, 16];
//                     for (int i = 0; i < 32; i++)//0x20之前的是老得版本的数据
//                     {
//                         for (int j = 0; j < 16; j++)
//                         {
//                             ClassCS.gd_Byte[i, j] = by[16 * i + j]; ;
//                         }
//                     }
                    ReadHex(by, ClassCS.gi_SelectProIndex);
                }
            }
            catch (System.Exception ex)
            {
                Busiclass.MsgError("读取Hex文件出错，请检查hex文件是否正确？" + ex.Message.ToString());
            }

        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            ClassCS.gi_AreaName = 0;
            frmRegister frm = new frmRegister();
            frm.ShowDialog();
            frm.Dispose();

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



        private void 退出ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
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

        private void tb_Overflow_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tb_Shortage_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Brush b = Brushes.Blue;
            Pen p = Pens.Red;
            groupBox_Paint(sender, e, b, p);
        }

        private void cbBoxOverflow_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbBoxOverflowP.SelectedIndex = cbBoxOverflow.SelectedIndex;
        }
        private void cbBoxShortage_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbBoxShortageP.SelectedIndex = cbBoxShortage.SelectedIndex;
        }
        // 清除浓度TC
        private void btn_ClearTemp_Click(object sender, EventArgs e)
        {
            gi_ConcentrationCount = 0;
            li_CountP = 0;
            tb_DensFc2.Text = "";
            tb_TCFc2.Text = "";

            initLV(listView1);
        }
    }
}