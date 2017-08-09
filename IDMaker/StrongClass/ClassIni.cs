using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IDMaker
{
    class ClassIni
    {
        #region API��������
        [DllImport("kernel32")]//����0��ʾʧ�ܣ���0Ϊ�ɹ�
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//����ȡ���ַ����������ĳ���
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        #endregion

        //static string FilePath = Application.StartupPath + "\\"; //INI�ļ�·��
        #region ��Ini�ļ�
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

        #region дIni�ļ�
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
