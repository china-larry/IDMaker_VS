using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IDMaker
{
    class ClassIni
    {
        #region API函数声明
        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        #endregion

        //static string FilePath = Application.StartupPath + "\\"; //INI文件路径
        #region 读Ini文件
        public static string ReadIniData(string Section, string Key,string iniFileName)
        {
            //string iniFilePath = FilePath + iniFileName;
            if (File.Exists(iniFileName))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, "", temp, 1024, iniFileName);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion

        #region 写Ini文件
        public static bool WriteIniData(string Section, string Key, string Value, string iniFileName)
        {
            //string iniFilePath = FilePath + iniFileName;
            if (!File.Exists(iniFileName))
            {
                FileStream fs = new FileStream(iniFileName, FileMode.OpenOrCreate);
                fs.Close();
            }
            long OpStation = WritePrivateProfileString(Section, Key, Value, iniFileName);
            if (OpStation == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
