using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Drawing;

namespace IDMaker
{
    ///////////////////////////////////////////////////////////////////////////////
    //
    //  Author   :  周张奎                                                              
    //  DateTime :  2011-12-18                                                                             
    //  Version  :  1.0                                                                                   
    //  Modify   :  unknown                                                                                  
    //  tel      :  18688483860  15974748  gejiemei520@163.com                                             
    //  Function :  综合类                                                                         
    // (C)Copyright 2011.12, 保留所有权益
    //                                               
    ///////////////////////////////////////////////////////////////////////////////
    class ClassCS
    {
        #region  定义关于的信息
        //public static string gs_company_info = "zhouzhangkui 保留所有权益。";
        public static string gs_company_info = "广州万孚生物技术股份有限公司";
        public static string gs_rights = "保留所有权益 Copyright(C)2009-2017";
        public static string sBuildId = "All Rights Reserved(Build6.2.3.1.2017.08.02)";
        public static string gs_Author = "设计者: 陈远强";
        public static string gs_Tel = "电话:         800-999-4268  020-32299999";
        public static string gs_Support = "";
        public static string ProName = "万孚荧光仪Hex生成系统V6.2.3.1";
        #endregion

        #region  系统参数
        public static byte[] by = new byte[256 * 2 * 16];
        public static int gi_QX = 0, gi_AreaName = 0, gi_SelectProIndex = 0, gi_Read = 0, gi_cbBoxProCount = 0;
        public static string AreaName = "";

        public static string gs_SelectProIndex = "";

        public static bool[] isinit = new bool[3];

        public static string iniFileName = "",iniTempName = Application.StartupPath + "\\Temp.ini", iniPARA = Application.StartupPath + "\\PARA.ini";
        public static bool bHaveIni = true;
        #endregion

        //前4K字节
        #region  数据数组 共256*2行，每行16个字节 gd_Byte
        /// <summary>
        /// 数据数组 共256行，每行16个字节
        /// </summary>
        public static byte[,] gd_Byte = new byte[256 * 4, 16];
        #endregion

        #region ArrayAreaName 省名称
        public static string[] ArrayAreaName = new string[]{
           "北京市","天津市","河北省",
"山西省","内蒙古自治区","辽宁省","吉林省","黑龙江省","上海市","江苏省","浙江省","安徽省","福建省","江西省","山东省","河南省",
"湖北省","湖南省","广东省","广西壮族自治区","海南省","重庆市","四川省","贵州省","云南省","西藏自治区","陕西省","甘肃省","青海省",
"宁夏回族自治区","新疆维吾尔自治区","台湾","香港","澳门","国外"};
        #endregion

        #region gi_ShowError 是否进行错误提示 如果不提示 表示 忽略（继续） gi_ShowError
        /// <summary>
        /// 是否进行错误提示 如果不提示 表示 忽略（继续） gi_ShowError
        /// </summary>
        public static int gi_ShowError = 0;
        #endregion

        #region  CreateForm 创建MDI窗体
        public static void CreateForm(Form frm, Form thisFrom)
        {
            //frmParameter frm = new frmParameter();
            if (ChildShow(frm, thisFrom) == true)
            {
                frm.MdiParent = thisFrom;
                frm.Icon = thisFrom.Icon;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }
        //判断子窗体重复打开
        private static bool ChildShow(Form f, Form thisFrom)
        {
            foreach (Form frm in thisFrom.MdiChildren)
            {
                if (frm.Name == f.Name)
                {
                    frm.Visible = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Maximized;
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region  IsNullEmpty 判断字符串是否为空
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        public static bool IsNullEmpty(string str)
        {
            bool rtValue = false;
            //str = str.Trim();
            if (str == "" || str == null || str == string.Empty)
            {
                rtValue = true;
            }
            return rtValue;
        }
        #endregion

        #region  ReadIni 读取ini文件
        /// <summary>
        /// 读取ini文件
        /// </summary>
        public static void ReadIni()
        {
            Busiclass.gs_ExeName = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;      
            //ClassCS.gi_ShowError = Convert.ToInt32(ClassIni.ReadIniData("Communication", "ShowError", "0", Busiclass.gs_ExeName + "config.ini").Trim());
        }
        #endregion

        #region MoveFile 移动文件
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="OrignFile">原文件</param>
        /// <param name="NewFile">新文件</param>
        /// <returns> 0:异常  1:文件不存在  2:成功</returns>
        public static int MoveFile(string OrignFile, string NewFile)
        {
            int i = 0;
            try
            {
                if (File.Exists(OrignFile))
                {
                    File.Move(OrignFile, NewFile);
                    i = 2;
                }
                else
                    i = 1;
            }
            catch (System.Exception ex)
            {
                i = 0;
                Busiclass.MsgError(ex.Message.ToString());
            }
            return i;
        }
        #endregion

        #region ChnToHex 汉字 字符 转内码（gb2312） ChnToHex   0:30  你好： C4E3 BAC3
        /// <summary>
        /// 汉字 字符 转内码（gb2312） ChnToHex
        /// </summary>
        /// <param name="CHNStr"></param>
        /// <returns></returns>
        public static string ChnToHex(string CHNStr)
        {
            //string ss = CHNStr;
            String Hex = "";
            string st = string.Empty;
            //获取ANSI码...
            for (int n = 0; n < CHNStr.Length; n++)
            {
                //MessageBox.Show(Convert.ToString(ss[n]));
                byte[] array = System.Text.Encoding.Default.GetBytes(CHNStr[n].ToString());

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] >= 161 && array[i] <= 247)
                    {
                        // st =st+System.Text.Encoding.Default.GetString(array, i, 2);
                        st = st + string.Format(" 高字节:{0},低字节:{1}" + Environment.NewLine, array[i], array[i + 1]);
                        //获取字节(十进制)的十六进制数，并转换成大写...
                        string a = Convert.ToString(array[i], 16).ToUpperInvariant();
                        string b = Convert.ToString(array[i + 1], 16).ToUpperInvariant();
                        Hex += a + b;
                        i++;

                    }
                    else
                    {
                        // st =st+ System.Text.Encoding.Default.GetString(array, i,1);
                        st = st + string.Format(" ASCII:{0}" + Environment.NewLine, array[i]);
                        st = Convert.ToString(array[i], 16);
                        Hex += st;
                    }
                }
            }
            return Hex;
        }
        #endregion

        #region UnHex 内码（gb2312）转 汉字\字符 UnHex  31: 1:  C4E3 BAC3:你好
        /// <summary>
        ///  内码（gb2312）转 汉字\字符 UnHex  31: 1:  C4E3 BAC3:你好
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="charset">"gb2312" </param>
        /// <returns></returns>
        public static string UnHex(string hex, string charset)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";
            }
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                   System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    //throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            return chs.GetString(bytes).Replace("\0", "");
        }
        #endregion
    }
}
