using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Collections;

namespace IDMaker
{
    #region 业务类
    public class Busiclass
    {
        public static string gs_ExeName;

        #region 接口

        public static int PrintMode;
        public static int sign = 0;
        public const int SPI_SETWORKAREA = 47;
        public const int SPI_GETWORKAREA = 48;

        [DllImport("coredll.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpWindowName, string lpClassName);

        [DllImport("coredll.dll", EntryPoint = "ShowWindow")]
        public static extern bool ShowWindow(int hWnd, int nCmdShow);

        [DllImport("coredll.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
                int uAction,
                int uParam,
                ref Rectangle lpvParam,
                int fuWinIni
                );

        [DllImport("coredll.dll")]
        public static extern bool UnregisterFunc(
        uint fsModifiers, //组合键的键值
        uint vk //热键键值
        );

        [DllImport("coredll.dll")]
        public static extern bool RegisterHotKey(
         IntPtr hWnd, //要注册的窗体的句柄 
         int id, // 热键的键值
         uint fsModifiers, //组合键的键值 
         uint vk // virtual-key code （虚拟键的编码，这里和第二个参数一样）
        );

        [DllImport("coredll.dll")]
        public static extern bool UnregisterHotKey(
         IntPtr hWnd, //要注册的窗体的句柄
         int id //热键的键值
        );

        #endregion

        #region SQL全局查询语句
        /// <summary>
        /// SQL全局查询语句
        /// </summary>
        public static string ls_SQL;
        public string _ls_SQL
        {
            set
            {
                value = ls_SQL;
            }
            get
            {
                return ls_SQL;
            }
        }
        #endregion

        #region 当前的窗体
        /// <summary>
        /// 当前的窗体
        /// </summary>
        public static string ls_Hand;
        public string _ls_Hand
        {
            set
            {
                value = ls_Hand;
            }
            get
            {
                return ls_Hand;
            }
        }
        #endregion

        #region 文件路径
        /// <summary>
        /// 文件路径
        /// </summary>
        public static string PathName;
        public string _PathName
        {
            set
            {
                value = PathName;
            }
            get
            {
                return PathName;
            }
        }
        #endregion

        #region 用户ID
        /// <summary>
        /// 用户ID
        /// </summary>
        public static string UserID;
        public string _UserID
        {
            set
            {
                value = UserID;
            }
            get
            {
                return UserID;
            }
        }
        #endregion

        #region 标本类型
        /// <summary>
        /// 标本类型
        /// </summary>
        public static string ExemplarStyle;
        public string _ExemplarStyle
        {
            set
            {
                value = ExemplarStyle;
            }
            get
            {
                return ExemplarStyle;
            }
        }
        #endregion

        #region 用户名称
        /// <summary>
        /// 用户名称
        /// </summary>
        public static string UserName;
        public string _UserName
        {
            set
            {
                value = UserName;
            }
            get
            {
                return UserName;
            }
        }
        #endregion

        #region 字符串转16进制字节数组
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        #endregion

        #region 字节数组转16进制字符串
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    //string.Format("{0:X2}", buf[i]);
                    //returnStr += bytes[i].ToString("X2");
                    returnStr += string.Format("{0:X2}", bytes[i]);
                    returnStr += " ";
                }
            }
            return returnStr;
        }
        #endregion

        #region 字符转换为十六进制
        /// <summary>
        /// 字符转换为十六进制
        /// </summary>
        /// <param name="Str">周:D6DC or 1:31</param>
        /// <returns></returns>
        public static string StrToHex(string Str)
        {
            string ls_str = "";
            byte[] GetByte = Encoding.Default.GetBytes(Str);
            for (int i = 0; i < GetByte.Length; i++)
            {
                ls_str += GetByte[i].ToString("X2");
            }
            return ls_str;
        }
        #endregion

        #region 十六进制转换为字符
        /// <summary>
        /// 十六进制转换为字符
        /// </summary>
        /// <param name="HexStr"></param>
        /// <returns></returns>
        public static string HexToStr(string HexStr)
        {
            if (HexStr == null)
            {
                return "";
            }
            HexStr = HexStr.Replace(",", "");
            HexStr = HexStr.Replace("\n", "");
            HexStr = HexStr.Replace("\\", "");
            HexStr = HexStr.Replace(" ", "");
            HexStr = HexStr.Replace("00", "");
            if (HexStr.Length % 2 != 0)
            {
                HexStr += "20";
            }
            byte[] bytes = new byte[HexStr.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(HexStr.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("gb2312");
            return chs.GetString(bytes, 0, bytes.Length);
        }
        #endregion

        #region 整型转16进制字节数组
        /// <summary>
        /// 整型转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>十六进制数字
        /// <param name="Hexlen"></param>长度几个BYTE
        /// <returns></returns>
        public static byte[] IntToHexByte(string hexString, int Hexlen)
        {
            string fdsg = IntToHex(hexString).PadLeft(Hexlen * 2, '0');
            byte[] returnBytes = new byte[fdsg.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(fdsg.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        #endregion

        #region 十六进制字节数组转整型
        /// <summary>
        /// 十六进制字节数组转整型
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static string HexByteToInt(byte[] Bytes)
        {
            string returnStr = "";
            if (Bytes != null)
            {
                for (int i = 0; i < Bytes.Length; i++)
                {
                    returnStr += HexToInt(IntToHex(Bytes[i].ToString()));
                }
            }
            return returnStr;
        }
        #endregion

        #region 日期转十六进制
        /// <summary>
        /// 日期转十六进制
        /// </summary>
        /// <param name="StrDate"></param>
        /// <param name="IntLen"></param>
        /// <returns></returns>
        public static byte[] DateToHexByte(string StrDate, int IntLen)
        {
            string ls_Date = StrDate.Trim();
            ls_Date = ls_Date.Replace("-", "");
            ls_Date = ls_Date.Replace(".", "");
            ls_Date = ls_Date.Replace("/", "");
            ls_Date = ls_Date.PadLeft(IntLen * 2, '0');
            byte[] IntHex = new byte[ls_Date.Length / 2];
            for (int i = 0; i < IntHex.Length; i++)
            {
                IntHex[i] = Convert.ToByte(IntToHex(ls_Date.Substring(i * 2, 2)), 16);
            }
            return IntHex;
        }
        #endregion

        #region 十六制转日期格式
        /// <summary>
        /// 十六制转日期格式
        /// </summary>
        /// <param name="HexStr"></param>
        /// <param name="IntStart"></param>
        /// <param name="IntLen"></param>
        /// <returns></returns>
        public static string HexToDate(string HexStr, int IntStart, int IntLen)
        {
            string ls_HexStr = "", ls_HexStr1 = "", ls_GetDate = "";
            ls_HexStr = HexStr.Replace(" ", "");
            if (IntStart > HexStr.Length)
            {
                return "";
            }
            if (IntLen > HexStr.Length)
            {
                return "";
            }
            if (IntStart > IntLen)
            {
                return "";
            }
            try
            {
                for (int n = IntStart; n <= IntLen; n++)
                {
                    ls_HexStr1 += Busiclass.HexToInt(ls_HexStr.Substring(n * 2, 2)) + "-";
                }
                ls_GetDate = ls_HexStr1.Substring(0, ls_HexStr1.Length - 1);
            }
            catch
            {
                return "";
            }
            return ls_GetDate;
        }
        #endregion

        #region 时间转十六进制字符
        /// <summary>
        /// 时间转十六进制字符
        /// </summary>
        /// <param name="StrMinSec"></param>
        /// <returns></returns>
        public static byte[] DateMinSecToHex(string StrMinSec)
        {
            string[] Str = StrMinSec.Trim().Split(':');
            byte[] HexByte = new byte[Str.Length];
            try
            {
                for (int i = 0; i < Str.Length; i++)
                {
                    HexByte[i] = Convert.ToByte(IntToHex(Str[i].ToString()), 16);
                }
            }
            catch
            {
                return null;
            }
            return HexByte;
        }
        #endregion

        #region 金额转十六进制字节数组
        /// <summary>
        /// 金额转十六进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] IntMoneyToHexByte(string hexString, int len)
        {
            ArrayList lst = new ArrayList();
            string ls_Money = "", ls_Money1 = "", ls_HexStr = "", ls_HexStr1 = "";
            ls_Money = hexString.Substring(0, hexString.IndexOf("."));
            ls_Money1 = hexString.Substring(hexString.IndexOf(".") + 1, 2);
            ls_HexStr = IntToHex(ls_Money).PadLeft((len - 1) * 2, '0');
            ls_HexStr1 = IntToHex(ls_Money1).PadLeft(2, '0');
            byte[] HexByte = new byte[ls_HexStr.Length / 2];
            byte[] HexByte1 = new byte[1] { Convert.ToByte(HexToInt(ls_HexStr1)) };
            for (int i = 0; i < HexByte.Length; i++)
            {
                HexByte[i] = Convert.ToByte(HexToInt(ls_HexStr.Substring(i * 2, 2)));
            }
            lst.AddRange(HexByte);
            lst.AddRange(HexByte1);
            byte[] GetHexByte = (byte[])lst.ToArray(typeof(byte));
            return GetHexByte;
        }
        #endregion

        #region 数字转十六进制字符(整型最大值65535)
        /// <summary>
        /// 数字转十六进制字符(整型最大值65535)
        /// </summary>
        /// <param name="IntHex"></param>
        /// <returns></returns>
        public static string IntToHex(string IntHex)
        {
            return ConvertString(IntHex, 10, 16).PadLeft(2, '0').ToUpper();
        }
        #endregion

        #region 十六进制字符转数字
        /// <summary>
        /// 十六进制字符转数字
        /// </summary>
        /// <param name="HexInt"></param>
        /// <returns></returns>
        public static string HexToInt(string HexInt)
        {
            return ConvertString(HexInt, 16, 10).PadLeft(2, '0');
        }
        #endregion

        #region 返回指定长度的十六进制转字符串
        /// <summary>
        /// 返回指定长度的十六进制转字符串
        /// </summary>
        /// <param name="HexStr"></param>
        /// <param name="IntStart"></param>
        /// <param name="IntLen"></param>
        /// <returns></returns>
        public static string GetHexToStr(string HexStr, int IntStart, int IntLen)
        {
            string ls_HexStr = "", ls_HexStr1 = "";
            ls_HexStr = HexStr.Replace(" ", "");
            if (IntStart > HexStr.Length)
            {
                return "";
            }
            if (IntLen > HexStr.Length)
            {
                return "";
            }
            if (IntStart > IntLen)
            {
                return "";
            }
            try
            {
                for (int n = IntStart; n <= IntLen; n++)
                {
                    ls_HexStr1 += ls_HexStr.Substring(n * 2, 2);
                }
            }
            catch
            {
                return "";
            }
            return Busiclass.HexToStr(ls_HexStr1);
        }
        #endregion

        #region 返回指长度的单个十六进制转数字
        /// <summary>
        /// 返回指长度的单个十六进制转数字
        /// </summary>
        /// <param name="HexStr"></param>
        /// <param name="IntStart"></param>
        /// <param name="IntLen"></param>
        /// <returns></returns>
        public static string GetHexToInt(string HexStr, int IntStart, int IntLen)
        {
            string ls_HexStr = "", ls_HexStr1 = "";
            ls_HexStr = HexStr.Replace(" ", "");
            if (IntStart > HexStr.Length)
            {
                return "";
            }
            if (IntLen > HexStr.Length)
            {
                return "";
            }
            if (IntStart > IntLen)
            {
                return "";
            }
            try
            {
                for (int n = IntStart; n <= IntLen; n++)
                {
                    ls_HexStr1 += Busiclass.HexToInt(ls_HexStr.Substring(n * 2, 2));
                }
            }
            catch
            {
                return "";
            }
            return ls_HexStr1;
        }
        #endregion

        #region 返回指长度的多个十六进制转数字
        /// <summary>
        /// 返回指长度的多个十六进制转数字
        /// </summary>
        /// <param name="HexStr"></param>
        /// <param name="IntStart"></param>
        /// <param name="IntLen"></param>
        /// <returns></returns>
        public static string GetMuHexToInt(string HexStr, int IntStart, int IntLen)
        {
            string ls_HexStr = "", ls_HexStr1 = "";
            ls_HexStr = HexStr.Replace(" ", "");
            if (IntStart > HexStr.Length)
            {
                return "";
            }
            if (IntLen > HexStr.Length)
            {
                return "";
            }
            if (IntStart > IntLen)
            {
                return "";
            }
            try
            {
                for (int n = IntStart; n <= IntLen; n++)
                {
                    ls_HexStr1 += ls_HexStr.Substring(n * 2, 2);
                }
            }
            catch
            {
                return "";
            }
            return Busiclass.HexToInt(ls_HexStr1);
        }
        #endregion

        #region 返回指定长度的字符串
        /// <summary>
        /// 返回指定长度的字符串
        /// </summary>
        /// <param name="HexStr"></param>
        /// <param name="IntStart"></param>
        /// <param name="IntLen"></param>
        /// <returns></returns>
        public static string GetlenStr(string HexStr, int IntStart, int IntLen)
        {
            string ls_HexStr = "", ls_HexStr1 = "";
            ls_HexStr = HexStr.Replace(" ", "");
            if (IntStart > HexStr.Length)
            {
                return "";
            }
            if (IntLen > HexStr.Length)
            {
                return "";
            }
            if (IntStart > IntLen)
            {
                return "";
            }
            try
            {
                for (int n = IntStart; n <= IntLen; n++)
                {
                    ls_HexStr1 += ls_HexStr.Substring(n * 2, 2);
                }
            }
            catch
            {
                return "";
            }
            return ls_HexStr1;
        }
        #endregion

        #region 两个日期比较大小(格式090618)
        /// <summary>
        /// 两个日期比较大小(格式090618)
        /// </summary>
        /// <param name="DateOne"></param>
        /// <param name="DateTwo"></param>
        /// <returns></returns>
        public static int CompDate1(string DateOne, string DateTwo)
        {
            //返回1：大于，0：相等，－1：小于
            DateTime dt = new DateTime();
            DateTime dt2 = new DateTime();
            string ls_DateOne = "", ls_DateTwo = "", ls_GetDateOne = "", ls_GetDateTwo = "";
            for (int i = 0; i < DateOne.Length / 2; i++)
            {
                ls_DateOne += DateOne.Substring(i * 2, 2).PadLeft(2, '0') + "-";
            }

            for (int j = 0; j < DateTwo.Length / 2; j++)
            {
                ls_DateTwo += DateTwo.Substring(j * 2, 2).PadLeft(2, '0') + "-";
            }
            ls_GetDateOne = ls_DateOne.Substring(0, ls_DateOne.Length - 1);
            ls_GetDateTwo = ls_DateTwo.Substring(0, ls_DateTwo.Length - 1);
            try
            {
                dt = Convert.ToDateTime(ls_GetDateOne);
                dt2 = Convert.ToDateTime(ls_GetDateTwo);
                int fd = System.DateTime.Compare(dt, dt2);
            }
            catch
            {
                Busiclass.MsgError("日期格式不正确");
            }
            if (System.DateTime.Compare(dt, dt2) > 0)
                return 1;
            else if (System.DateTime.Compare(dt, dt2) == 0)
                return 0;
            else
                return -1;
        }
        #endregion

        #region 两个日期比较大小(格式2009-06-28/09-06-28)
        /// <summary>
        /// 两个日期比较大小(格式2009-06-28/09-06-28)
        /// </summary>
        /// <param name="Date1"></param>
        /// <param name="Date2"></param>
        /// <returns></returns>
        public static int CompDate2(string Date1, string Date2)
        {
            DateTime dt = new DateTime();
            DateTime dt2 = new DateTime();
            try
            {
                dt = Convert.ToDateTime(Date1);
                dt2 = Convert.ToDateTime(Date2);
                int fd = System.DateTime.Compare(dt, dt2);
            }
            catch
            {
                Busiclass.MsgError("日期格式不正确");
            }
            if (System.DateTime.Compare(dt, dt2) > 0)
                return 1;
            else if (System.DateTime.Compare(dt, dt2) == 0)
                return 0;
            else
                return -1;
        }
        #endregion

        #region 两个日期比较大小
        /// <summary>
        /// 两个日期比较大小
        /// </summary>
        /// <param name="Date1"></param>第一日期
        /// <param name="Date2"></param>第二日期
        /// <param name="DateFormat"></param>日期模式(1:090618,2:09-06-18)
        /// <returns></returns>
        public static int CompDate(string Date1, string Date2, int DateFormat)
        {
            int RunNum = -10000;
            if (DateFormat == 1)
            {
                RunNum = CompDate1(Date1, Date2);
            }
            if (DateFormat == 2)
            {
                RunNum = CompDate2(Date1, Date2);
            }
            return RunNum;
        }
        #endregion

        #region 2进制、8进制、10进制、16进制
        /// <summary>
        /// 2进制、8进制、10进制、16进制 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>原来的格式
        /// <param name="toBase"></param>将要转换成的格式
        /// <returns></returns>
        /// ConvertString(0010, 2,16)
        public static string ConvertString(string value, int fromBase, int toBase)
        {
            if (value != "")
            {
                try
                {
                    long intValue = Convert.ToInt64(value, fromBase);
                    return Convert.ToString(intValue, toBase);
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 16进制数组转为2进制、8进制、10进制
        /// <summary>
        /// 16进制数组转为2进制、8进制、10进制
        /// </summary>
        /// <param name="HexStrng"></param>
        /// <param name="toBase"></param>
        /// <returns></returns>
        public static string ConvertHexToString(string HexString, int toBase)
        {
            string HexToString = "";
            if (HexString != "")
            {
                HexString = HexString.Replace(",", "");
                HexString = HexString.Replace("\n", "");
                HexString = HexString.Replace("\\", "");
                HexString = HexString.Replace(" ", "");
                HexToString = ConvertString(HexString, 16, toBase);
            }
            return HexToString;
        }
        #endregion

        #region 保留两2小数
        /// <summary>
        /// 保留2位小数
        /// </summary>
        /// <param name="StrNumber"></param>
        /// <returns></returns>
        public string DecPoint2(double StrNumber)
        {
            string StrDec;
            StrDec = StrNumber.ToString("f2");
            return StrDec;
        }
        #endregion

        #region 显示百分比
        /// <summary>
        /// 显示百分比
        /// </summary>
        /// <param name="StrNumber"></param>
        /// <returns></returns>
        public string Numberbt(double StrNumber)
        {
            string StrDecbt;
            StrDecbt = StrNumber.ToString() + "%";
            return StrDecbt;
        }
        #endregion

        #region 比较两个图片是否相同
        /// <summary>
        /// 比较两个图片是否相同
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <returns></returns>
        ///*if (Same(pictureBox1.Image, pictureBox2.Image))        
        public bool CompPicBox(Image image1, Image image2)
        {
            MemoryStream ms1 = new MemoryStream();
            MemoryStream ms2 = new MemoryStream();
            image1.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);
            image2.Save(ms2, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] im1 = ms1.GetBuffer();
            byte[] im2 = ms2.GetBuffer();
            if (im1.Length != im2.Length)
                return false;
            else
            {
                for (int i = 0; i < im1.Length; i++)
                    if (im1[i] != im2[i])
                        return false;
            }
            return true;
        }
        #endregion

        #region 消息框提示MsgOK
        /// <summary>
        /// 消息框提示MsgOK
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static DialogResult MsgOK(string Text)
        {
            return MessageBox.Show(Text, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }
        #endregion

        #region 消息框提示MsgError
        /// <summary>
        /// 消息框提示MsgError
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static DialogResult MsgError(string Text)
        {
            return MessageBox.Show(Text, "万孚提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
        #endregion

        #region 消息框提示MsgYesNo
        /// <summary>
        /// 消息框提示MsgYesNo
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static DialogResult MsgYesNo(string Text)
        {
            return MessageBox.Show(Text, "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
        }
        #endregion

        #region 产生数字和字符混合的随机字符串
        /// <summary>
        /// 产生数字和字符混合的随机字符串
        /// </summary>
        /// <param name="codeCount"></param>指定长度
        /// <returns></returns>
        public static string CreateRandomCode(int codeCount)
        {
            string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";//0,Oo,I都被去掉，个数和下面对应起来
            int maxc = Convert.ToInt32((allChar.Length) / 2);
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(maxc);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        #endregion

        #region 将字符串反置
        /// <summary>
        /// 将字符串反置
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string BackString(string str)
        {
            str = str.Replace(" ", "");
            str = str.Replace("\\", "");
            str = str.Replace(",", "");
            str = str.Replace("@", "");
            int lenInt = str.Length;
            string[] z = new string[str.Length];
            for (int i = 0; i < lenInt; i++)
            {
                z[i] = str.Substring(i, 1);
            }
            str = "";
            for (int i = lenInt - 1; i >= 0; i--)
            {
                str = str + z[i];
            }
            return str;
        }
        #endregion

        #region 定期Copy新的文件到其他目录下
        /// <summary>
        /// 定期Copy新的文件到其他目录下
        /// </summary>
        public void CopyToDir()
        {
            foreach (string vFile in Directory.GetFiles(@"c:\file")) // 遍历目录  
            {
                if (File.GetLastWriteTime(vFile) >= DateTime.Now.AddHours(-1)) // 如果文件是在一个小时内修改的 
                    File.Move(vFile, @"C:\" + Path.GetFileName(vFile)); // 复制用Copy(),这里用Move()更恰当 
            }
        }
        #endregion

        #region 将控件设置为透明
        /// <summary>
        /// 将控件设置为透明
        /// </summary>
        /// <param name="ControlName"></param>当前控件
        /// <param name="ParentControl"></param>父控件
        public static void Trans(Control ControlName, Control ParentControl)
        {
            ControlName.BackColor = Color.Transparent;
            ControlName.Parent = ParentControl;
        }
        #endregion

        #region 记录错误信息
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="className"></param>
        /// <param name="ex"></param>        
        public void ErrorRecord(string error, Exception ex)
        {
            try
            {
                FileStream fs = null;
                string ls_Path = Busiclass.PathName + "\\error.txt";

                if (!File.Exists(ls_Path))
                {
                    fs = new FileStream(ls_Path, FileMode.Create);
                }
                else
                {
                    fs = new FileStream(ls_Path, FileMode.Append);
                }
                /*
                if (!File.Exists("\\DiskOnChip\\MoTick\\error.txt"))
                {
                    fs = new FileStream("\\DiskOnChip\\MoTick\\error.txt", FileMode.Create);
                }
                else
                {
                    fs = new FileStream("\\DiskOnChip\\MoTick\\error.txt", FileMode.Append);
                }
                 */
                /*
                 if (!File.Exists("\\DiskOnChip\\MoTick\\error.txt"))
                 {
                     fs = new FileStream("\\DiskOnChip\\MoTick\\error.txt", FileMode.Create);
                 }
                 else
                 {
                     fs = new FileStream("\\DiskOnChip\\MoTick\\error.txt", FileMode.Append);
                 }
                 */
                //StackTrace st = new StackTrace(true);
                //string transferMethod = st.GetFrame(1).GetMethod().Name.ToString();
                StreamWriter sw = new StreamWriter(fs);
                //sw.WriteLine("类 名：" + className);
                //sw.WriteLine("调用错误记录的方法：" + transferMethod);
                //sw.WriteLine("引发错误的方法：" + ex.StackTrace.ToString());
                sw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + error);
                sw.WriteLine("错误消息:" + ex.Message.ToString());
                sw.Close();
            }
            catch
            {

            }
        }
        #endregion

        #region 十六进制字符转换为数值
        /// <summary>
        /// 十六进制字符转换为数值
        /// </summary>
        /// <param name="szVal"></param>
        /// <returns></returns>
        public static int Hex2ToInt(char[] szVal)
        {
            int nRet;
            int[] nNum = new int[4];

            for (int i = 0; i < 2; i++)
            {
                if (szVal[i] >= '0' && szVal[i] <= '9')
                    nNum[i] = szVal[i] - '0';
                else if (szVal[i] >= 'A' && szVal[i] <= 'F')
                    nNum[i] = szVal[i] - 'A' + 10;
                else
                    nNum[i] = 0;
            }

            nRet = (nNum[0] << 4) + nNum[1];
            return nRet;
        }
        #endregion


    }
    #endregion

    #region UDP接收与发送服务
    public class NetServer
    {
        /*[DllImport("Iphlpapi.dll", EntryPoint = "SendARP", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern int SendARP(Int32 LocalIP, Int32 RemoteIP, ref Int64 mac, ref Int32 length);

        [DllImport("Ws2_32.dll",EntryPoint = "inet_addr", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern Int32 inet_addr(string ip); */

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct ICMP_OPTIONS
        {
            public Byte Ttl;
            public Byte Tos;
            public Byte Flags;
            public Byte OptionsSize;
            public IntPtr OptionsData;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct ICMP_ECHO_REPLY
        {
            public int Address;
            public int Status;
            public int RoundTripTime;
            public Int16 DataSize;
            public Int16 Reserved;
            public IntPtr DataPtr;
            public ICMP_OPTIONS Options;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 250)]
            public String Data;
        }

        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern IntPtr IcmpCreateFile();

        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern bool IcmpCloseHandle(IntPtr handle);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern Int32 IcmpSendEcho(IntPtr icmpHandle, Int32 destinationAddress,
                                                 String requestData, Int32 requestSize,
                                                 ref ICMP_OPTIONS requestOptions, ref ICMP_ECHO_REPLY replyBuffer,
                                                 Int32 replySize, Int32 timeout);

        /// <summary>
        /// 当前IP
        /// </summary>
        public static string LocalIP;
        public string _LocalIP
        {
            set
            {
                value = LocalIP;
            }
            get
            {
                return LocalIP;
            }
        }

        /// <summary>
        /// 当前端口
        /// </summary>
        public static int LocalProt;
        public int _LocalProt
        {
            set
            {
                value = LocalProt;
            }
            get
            {
                return LocalProt;
            }
        }

        /// <summary>
        /// 远程IP
        /// </summary>
        public static string RemotIP;
        public string _RemotIP
        {
            set
            {
                value = RemotIP;
            }
            get
            {
                return RemotIP;
            }
        }

        /// <summary>
        /// 远程端口
        /// </summary>
        public static int RemotProt;
        public int _RemotProt
        {
            set
            {
                value = RemotProt;
            }
            get
            {
                return RemotProt;
            }
        }

        /// <summary>
        /// 网络状态
        /// </summary>
        public static string GetNetStatus;

        /// <summary>
        /// 接收数据组
        /// </summary>
        public string[] CheckData;

        /// <summary>
        /// 接收数据
        /// </summary>
        public string RetStr;

        /// <summary>
        /// 通过委托显示多线程接收信息
        /// </summary>
        public delegate void Checkdelegate();

        #region 启动服务接收数据
        /// <summary>
        /// 启动服务接收数据
        /// </summary>
        public void Recevie()
        {
            IPEndPoint RevIP = new IPEndPoint(IPAddress.Any, LocalProt);
            UdpClient UdpReceive = new UdpClient(RevIP);
            IPEndPoint IpEnd = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    byte[] ReceiveByte = UdpReceive.Receive(ref IpEnd);
                    RetStr = Encoding.Default.GetString(ReceiveByte, 0, ReceiveByte.Length);
                    //CheckData = RetStr.Split('*');
                    if (RetStr != "")
                    {
                        //this.Invoke(new Checkdelegate(CheckInfo));
                        /*for (int i = 0; i < ReceiveByte.Length; i++)
                        {
                            RetStr += String.Format("{0:X2} ", ReceiveByte[i]);
                        }*/
                    }
                    RetStr = "";
                }
                catch (Exception er)
                {
                    MessageBox.Show("接收数据失败:" + er.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
        }

        #endregion

        #region 发送数据到远程服务
        /// <summary>
        /// 发送数据到远程服务
        /// </summary>
        /// <param name="Strbyte"></param>发送信息
        /// <param name="RemoteIP"></param>远程服务IP
        /// <param name="RemotePort"></param>远程服务端口
        public void SendToRemote(string Strbyte, string RemoteIP, int RemotePort)
        {
            UdpClient UdpSend = new UdpClient();
            try
            {
                byte[] GetByte = System.Text.Encoding.Default.GetBytes(Strbyte);
                int SendCount = 0;
                while (true)
                {
                    try
                    {
                        UdpSend.Send(GetByte, GetByte.Length, RemoteIP, RemotePort);
                        break;
                    }
                    catch
                    {
                        if (SendCount < 3)
                        {
                            SendCount++;
                            continue;
                        }
                        else
                        {
                            MessageBox.Show("发送数据失败", "信息提示");
                            break;
                        }
                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show("网络通讯失败:" + er.Message, "信息提示");
            }
            UdpSend.Close();
        }
        #endregion

        #region 检测目标主机网络是否连通
        /// <summary>
        /// 检测目标主机网络是否连通
        /// </summary>
        /// <param name="ls_IP"></param>
        /// <returns></returns>
        public string CheckNet(string ls_IP)
        {
            if (CheckPing(ls_IP))
            {
                GetNetStatus = "连网状态";
            }
            else
            {
                GetNetStatus = "断网状态";
            }
            return GetNetStatus;
        }
        #endregion

        #region 检测网络状态
        /// <summary>
        /// 检测网络状态
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public bool CheckPing(string IP)
        {
            IntPtr ICMPHandle;
            String sData;
            ICMP_OPTIONS oICMPOptions = new ICMP_OPTIONS();
            ICMP_ECHO_REPLY ICMPReply = new ICMP_ECHO_REPLY();
            Int32 iReplies;
            ICMPHandle = IcmpCreateFile();
            Int32 iIP = BitConverter.ToInt32(IPAddress.Parse(IP).GetAddressBytes(), 0);
            sData = "x";
            oICMPOptions.Ttl = 255;

            iReplies = IcmpSendEcho(ICMPHandle, iIP,
                sData, sData.Length, ref oICMPOptions, ref ICMPReply,
                Marshal.SizeOf(ICMPReply), 30);
            IcmpCloseHandle(ICMPHandle);
            return iReplies == 1;
        }
        #endregion

        #region 发送数据
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="s"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SendData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;
            while (total < size)
            {
                sent = s.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }
            return total;
        }
        #endregion

        #region 接受数据
        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="s"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] ReceiveData(Socket s, int size)
        {
            int total = 0;
            int dataleft = size;
            byte[] data = new byte[size];
            int recv;
            while (total < size)
            {
                recv = s.Receive(data, total, dataleft, SocketFlags.None);
                if (recv == 0)
                {
                    data = null;
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }
        #endregion

        #region 发送文件
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SendFileData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;
            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = s.Send(datasize);
            while (total < size)
            {
                sent = s.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }
            return total;
        }
        #endregion

        #region 接收文件
        /// <summary>
        /// 接收文件
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ReceiveFileData(Socket s)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            recv = s.Receive(datasize, 0, 4, SocketFlags.None);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];
            while (total < size)
            {
                recv = s.Receive(data, total, dataleft, SocketFlags.None);
                if (recv == 0)
                {
                    data = null;
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }
        #endregion

    }
    #endregion

    #region XML操作类
    public class XmlClass
    {
        private XmlDocument myDc = new XmlDocument();

        #region 装载配置文件
        /// <summary>
        /// 装载配置文件
        /// </summary>
        /// <param name="Path">配置文件的路径</param>        
        public string ls_xmlPath;
        public void xmlPath(string ls_Path)
        {
            try
            {
                ls_xmlPath = ls_Path;
                myDc.Load(ls_xmlPath);
            }
            catch
            {

            }
        }
        #endregion

        #region 将配置文件中的值读出
        /// <summary>
        /// 将配置文件中的值读出
        /// </summary>
        public string GetXmlReader(string ls_ParNode, string ls_StrName)
        {
            return myDc.SelectSingleNode(ls_ParNode).SelectSingleNode(ls_StrName).InnerText;
        }
        #endregion

        #region 写入配置文件
        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <returns></returns>
        public void SaveXmlFile(string ls_ParNode, string ls_StrName, string ls_SetValues)
        {
            myDc.SelectSingleNode(ls_ParNode).SelectSingleNode(ls_StrName).InnerText = ls_SetValues;
            myDc.Save(ls_xmlPath);
        }
        #endregion
    }
    #endregion
}
