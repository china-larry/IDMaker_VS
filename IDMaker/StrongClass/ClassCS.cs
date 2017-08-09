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
    //  Author   :  ���ſ�                                                              
    //  DateTime :  2011-12-18                                                                             
    //  Version  :  1.0                                                                                   
    //  Modify   :  unknown                                                                                  
    //  tel      :  18688483860  15974748  gejiemei520@163.com                                             
    //  Function :  �ۺ���                                                                         
    // (C)Copyright 2011.12, ��������Ȩ��
    //                                               
    ///////////////////////////////////////////////////////////////////////////////
    class ClassCS
    {
        #region  ������ڵ���Ϣ
        //public static string gs_company_info = "zhouzhangkui ��������Ȩ�档";
        public static string gs_company_info = "�����������＼���ɷ����޹�˾";
        public static string gs_rights = "��������Ȩ�� Copyright(C)2009-2017";
        public static string sBuildId = "All Rights Reserved(Build6.2.3.1.2017.08.02)";
        public static string gs_Author = "�����: ��Զǿ";
        public static string gs_Tel = "�绰:         800-999-4268  020-32299999";
        public static string gs_Support = "";
        public static string ProName = "����ӫ����Hex����ϵͳV6.2.3.1";
        #endregion

        #region  ϵͳ����
        public static byte[] by = new byte[256 * 2 * 16];
        public static int gi_QX = 0, gi_AreaName = 0, gi_SelectProIndex = 0, gi_Read = 0, gi_cbBoxProCount = 0;
        public static string AreaName = "";

        public static string gs_SelectProIndex = "";

        public static bool[] isinit = new bool[3];

        public static string iniFileName = "",iniTempName = Application.StartupPath + "\\Temp.ini", iniPARA = Application.StartupPath + "\\PARA.ini";
        public static bool bHaveIni = true;
        #endregion

        //ǰ4K�ֽ�
        #region  �������� ��256*2�У�ÿ��16���ֽ� gd_Byte
        /// <summary>
        /// �������� ��256�У�ÿ��16���ֽ�
        /// </summary>
        public static byte[,] gd_Byte = new byte[256 * 4, 16];
        #endregion

        #region ArrayAreaName ʡ����
        public static string[] ArrayAreaName = new string[]{
           "������","�����","�ӱ�ʡ",
"ɽ��ʡ","���ɹ�������","����ʡ","����ʡ","������ʡ","�Ϻ���","����ʡ","�㽭ʡ","����ʡ","����ʡ","����ʡ","ɽ��ʡ","����ʡ",
"����ʡ","����ʡ","�㶫ʡ","����׳��������","����ʡ","������","�Ĵ�ʡ","����ʡ","����ʡ","����������","����ʡ","����ʡ","�ຣʡ",
"���Ļ���������","�½�ά���������","̨��","���","����","����"};
        #endregion

        #region gi_ShowError �Ƿ���д�����ʾ �������ʾ ��ʾ ���ԣ������� gi_ShowError
        /// <summary>
        /// �Ƿ���д�����ʾ �������ʾ ��ʾ ���ԣ������� gi_ShowError
        /// </summary>
        public static int gi_ShowError = 0;
        #endregion

        #region  CreateForm ����MDI����
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
        //�ж��Ӵ����ظ���
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

        #region  IsNullEmpty �ж��ַ����Ƿ�Ϊ��
        /// <summary>
        /// �ж��ַ����Ƿ�Ϊ��
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

        #region  ReadIni ��ȡini�ļ�
        /// <summary>
        /// ��ȡini�ļ�
        /// </summary>
        public static void ReadIni()
        {
            Busiclass.gs_ExeName = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;      
            //ClassCS.gi_ShowError = Convert.ToInt32(ClassIni.ReadIniData("Communication", "ShowError", "0", Busiclass.gs_ExeName + "config.ini").Trim());
        }
        #endregion

        #region MoveFile �ƶ��ļ�
        /// <summary>
        /// �ƶ��ļ�
        /// </summary>
        /// <param name="OrignFile">ԭ�ļ�</param>
        /// <param name="NewFile">���ļ�</param>
        /// <returns> 0:�쳣  1:�ļ�������  2:�ɹ�</returns>
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

        #region ChnToHex ���� �ַ� ת���루gb2312�� ChnToHex   0:30  ��ã� C4E3 BAC3
        /// <summary>
        /// ���� �ַ� ת���루gb2312�� ChnToHex
        /// </summary>
        /// <param name="CHNStr"></param>
        /// <returns></returns>
        public static string ChnToHex(string CHNStr)
        {
            //string ss = CHNStr;
            String Hex = "";
            string st = string.Empty;
            //��ȡANSI��...
            for (int n = 0; n < CHNStr.Length; n++)
            {
                //MessageBox.Show(Convert.ToString(ss[n]));
                byte[] array = System.Text.Encoding.Default.GetBytes(CHNStr[n].ToString());

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] >= 161 && array[i] <= 247)
                    {
                        // st =st+System.Text.Encoding.Default.GetString(array, i, 2);
                        st = st + string.Format(" ���ֽ�:{0},���ֽ�:{1}" + Environment.NewLine, array[i], array[i + 1]);
                        //��ȡ�ֽ�(ʮ����)��ʮ������������ת���ɴ�д...
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

        #region UnHex ���루gb2312��ת ����\�ַ� UnHex  31: 1:  C4E3 BAC3:���
        /// <summary>
        ///  ���루gb2312��ת ����\�ַ� UnHex  31: 1:  C4E3 BAC3:���
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
