using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection; // �����������ʹ��Missing�ֶ� 
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;

namespace IDMaker
{
    class ClassExcel
    {
        private int ColumnRead = 0;
        public int rowRead = 0;
        public Excel.Worksheet worksheet = null;
        public Excel.Application xlApp = null;
        public Excel.Workbook workbook = null;
        public Excel.Workbooks workbooks = null;
        public Excel.Range range = null;

        public void OpenExcel(string FileName)
        {
            try
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                int OpenMode = 0; ;
                xlApp = new Excel.Application();
                if (xlApp == null)
                {
                    Busiclass.MsgError("�޷�����Excel���󣬿������Ļ���δ��װExcel");
                    return;
                }
                //xlApp.Visible = true;
                xlApp.Visible = false ;
                workbooks = xlApp.Workbooks;
                workbook = workbooks.Add(true);
                OpenMode = 0;
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];//ȡ��sheet1
                rowRead = worksheet.UsedRange.Cells.Rows.Count; //worksheet.Rows.Count;
                ColumnRead = worksheet.UsedRange.Cells.Columns.Count; //worksheet.Rows.Count;
                //worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, 61]).Columns.AutoFit();
                worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, 1]).ColumnWidth = 6;
                worksheet.get_Range(worksheet.Cells[1, 2], worksheet.Cells[1, 2]).ColumnWidth = 12;
                worksheet.get_Range(worksheet.Cells[1, 13], worksheet.Cells[1, 13]).ColumnWidth = 25;
                worksheet.Cells[1, 1] = "���";
                worksheet.Cells[1, 2] = "ͼƬ����";
                worksheet.Cells[1, 3] = "Բ����";
                worksheet.Cells[1, 4] = "R";
                worksheet.Cells[1, 5] = "G";
                worksheet.Cells[1, 6] = "B";
                worksheet.Cells[1, 7] = "C";
                worksheet.Cells[1, 8] = "M";
                worksheet.Cells[1, 9] = "Y";
                worksheet.Cells[1, 10] = "K";
                worksheet.Cells[1, 11] = "Բ������X";
                worksheet.Cells[1, 12] = "Բ������Y";
                worksheet.Cells[1, 13] = "ֱ��";
                worksheet.Cells[1, 14] = "ʱ��";
                /*
                
                int n = 0;
                for (int i = 2; i < 242; i = i + 3)
                {
                    n++;
                    worksheet.Cells[1, i] = (n).ToString() + "�� R";
                    worksheet.Cells[1, i + 1] = (n).ToString() + "�� G";
                    worksheet.Cells[1, i + 2] = (n).ToString() + "�� B";
                }
                for (int i = 0; i < 720; i++)
                    worksheet.Cells[i + 2, 1] = (i + 1).ToString();             
                 */ 
            }
            catch (System.Exception ex)
            {
                CloseExcel();
                Busiclass.MsgError("�޷�����Excel���󣬿������Ļ���δ��װExcel");
            }          
        }

        /// <summary>Y ��  X ��
        /// Y ��  X ��
        /// </summary>
        /// <param name="y">��</param>
        /// <param name="x">��</param>
        /// <param name="date"></param>
        public void WriteExcel(int y, int x, string date)
        {
            try
            {
                worksheet.Cells[y, x ] = date;
            }
            catch (System.Exception ex)
            {
            	
            }
            
            // Application.DoEvents();
        }
        public void SaveExcel(string FileName)
        {
            try
            {
                //if (File.Exists(FileName))
                //   File.Delete(FileName);
               
                xlApp.DisplayAlerts = false;
                //xlApp.AlertBeforeOverwriting = false;
                //���湤����
                //workbook.Save();
                //xlApp.Save(FileName);
                
                workbook.SaveAs(FileName,
                       Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                       Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
                       Missing.Value, Missing.Value);
                //CloseExcel();

                /* 
                 workbook.SaveAs(FileName,
                        56, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value);
                  */
              
                //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8��Excel97-2003��ʽ
                //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12��Excel2007��ʽ
                //���Ϊ97-2003�ļ���ģʽ              
                //workbook.Saved = true;
                //workbook.SaveCopyAs(FileName);
                //workbook.SaveAs(FileName, Missing.Value, "", Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //'Microsoft.Office.Interop.Excel,   Version=12.0.0.0  2007
                //Microsoft.Office.Interop.Excel,   Version=12.0.0.0  2003
                /*
                workbook.SaveAs(saveFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing,
                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                 */
            }
            catch (Exception ex)
            {
                Busiclass.MsgError("����Excel����,�ļ����������򿪣�\n" + ex.Message);
            }
        }

        public void CloseExcel()
        {
            /*
            try
            {
                if (xlApp != null)
                {
                    worksheet = null;
                    workbook = null;
                    workbooks = null;
                    xlApp.Quit();
                    //xlApp = null;
                    GC.Collect();//ǿ������
                }
            }
            catch
            { 
            }
             */ 

        }
        #region ����dataGridView1��Excel  SaveDataGridViewExcel
        /// <summary>
        /// ����dataGridView1��Excel
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="saveFileDialog"></param>
        public static void SaveDataGridViewExcel(DataGridView dataGridView1)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xlsx files(*.xlsx)|*.xlsx"; //����Ϊxls��ʽ
            saveFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File To";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
            string str = "";
            try
            {
                //д����
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dataGridView1.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                //д����
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < dataGridView1.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        tempStr += dataGridView1.Rows[j].Cells[k].Value.ToString();
                    }

                    sw.WriteLine(tempStr);
                }
                sw.Close();
                myStream.Close();
                Busiclass.MsgOK("���ݵ������");
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message.ToString(), "������ʾ");
                Busiclass.MsgError(e.Message.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }
        #endregion


        #region  �Զ�listView1���ݵ� Excel AutotListViewExcel
        /// <summary>
        /// �Զ�listView1���ݵ� Excel
        /// </summary>
        /// <param name="listView1"></param>
        /// <param name="FileName"></param>
        public static void AutotListViewExcel(ListView listView1, string saveFileName)
        {
            //toolStripStatusLabel1.Text = "���ڱ���Excel����,���Ժ�...";
            //   DataTable xslTable=(DataTable)this.dgrd_Show.DataSource;//ȡ��dataGrid�󶨵�DataSet
            //   if(xslTable==null) return;
            // if (listView1.Items.Count == 0) return;
            Excel.Application xlApp = new Excel.Application();
            try
            {
                if (xlApp == null)
                {
                    Busiclass.MsgError("�޷�����Excel���󣬿������Ļ���δ��װExcel");
                    return;
                }
                Excel.Workbooks workbooks = xlApp.Workbooks;
                Excel.Workbook workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//ȡ��sheet1
                Excel.Range range;
                //   string oldCaption=this.listView1.ca.CaptionText;
                long totalCount = listView1.Items.Count;
                long rowRead = 0;
                float percent = 0;

                //д���ͷ
                for (int i = 0; i < listView1.Columns.Count; i++)
                {
                    Application.DoEvents();
                    worksheet.Cells[1, i + 1] = listView1.Columns[i].Text.ToString();
                }
                //���ñ�ͷ��ʾ��ʽ
                if (listView1.Columns.Count > 12)
                {
                    worksheet.get_Range(worksheet.Cells[1, 13], worksheet.Cells[1, 13]).ColumnWidth = 25;
                }

                Excel.Range productTitle = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, listView1.Columns.Count]);
                productTitle.Font.ColorIndex = 5;
                productTitle.Font.Bold = true;
                productTitle.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                productTitle.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, 1]).ColumnWidth = 6;
                worksheet.get_Range(worksheet.Cells[1, 2], worksheet.Cells[1, 2]).ColumnWidth = 12;

                worksheet.get_Range(worksheet.Cells[1, 11], worksheet.Cells[1, 11]).ColumnWidth = 10;
                worksheet.get_Range(worksheet.Cells[1, 12], worksheet.Cells[1, 12]).ColumnWidth = 10;

                worksheet.get_Range(worksheet.Cells[1, 13], worksheet.Cells[1, 13]).ColumnWidth = 10;
                worksheet.get_Range(worksheet.Cells[1, 14], worksheet.Cells[1, 14]).ColumnWidth = 20;

                //д����ֵ
                //this.lblpro.Visible = true;
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    Application.DoEvents();
                    for (int i = 0; i < listView1.Columns.Count; i++)
                    {
                        worksheet.Cells[j + 2, i + 1] = listView1.Items[j].SubItems[i].Text;
                    }
                }
                if (saveFileName != "")
                {
                    try
                    {
                        //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8��Excel97-2003��ʽ
                        //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12��Excel2007��ʽ
                        //���Ϊ97-2003�ļ���ģʽ
                        workbook.Saved = true;
                        workbook.SaveCopyAs(saveFileName);

                        //'Microsoft.Office.Interop.Excel,   Version=12.0.0.0  2007
                        //Microsoft.Office.Interop.Excel,   Version=12.0.0.0  2003
                        /*
                        workbook.SaveAs(saveFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing,
                            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                         */
                    }
                    catch (Exception ex)
                    {
                        Busiclass.MsgError("�����ļ�ʱ����,�ļ����������򿪣�\n" + ex.Message);
                        xlApp.Quit();
                        GC.Collect();//ǿ����
                        return;
                    }
                }
            }
            catch
            { 
            }
            finally
            {
                xlApp.Quit();
                GC.Collect();//ǿ������
            }

        }
        #endregion


        #region  ����listView1���ݵ� Excel ExportListViewExcel
        /// <summary>
        /// ����listView1���ݵ� Excel
        /// </summary>
        /// <param name="listView1"></param>
        /// <param name="FileName"></param>
        public static void ExportListViewExcel(ListView listView1)
        {
            //toolStripStatusLabel1.Text = "���ڱ���Excel����,���Ժ�...";
            //   DataTable xslTable=(DataTable)this.dgrd_Show.DataSource;//ȡ��dataGrid�󶨵�DataSet
            //   if(xslTable==null) return;
            // if (listView1.Items.Count == 0) return;
            string saveFileName = "";
            bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = ".xls";
            saveDialog.Filter = "Excel�ļ�|*..xls";
            saveDialog.RestoreDirectory = true;
            //saveDialog.FileName = FileName; //Busiclass.gs_PathName + "\\Data\\" + ls_FileName + ".xls";// "Sheet1";
            //saveDialog.ShowDialog();

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                Excel.Application xlApp = new Excel.Application();
                if (xlApp == null)
                {
                    Busiclass.MsgError("�޷�����Excel���󣬿������Ļ���δ��װExcel");
                    return;
                }
                Excel.Workbooks workbooks = xlApp.Workbooks;
                Excel.Workbook workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//ȡ��sheet1
                Excel.Range range;
                //   string oldCaption=this.listView1.ca.CaptionText;
                long totalCount = listView1.Items.Count;
                long rowRead = 0;
                float percent = 0;

                //д���ͷ
                for (int i = 0; i < listView1.Columns.Count; i++)
                {
                    Application.DoEvents();
                    worksheet.Cells[1, i + 1] = listView1.Columns[i].Text.ToString();
                }
                //���ñ�ͷ��ʾ��ʽ �����ÿ��
                if (listView1.Columns.Count>28)
                {
                    worksheet.get_Range(worksheet.Cells[1, 29], worksheet.Cells[1, 29]).ColumnWidth = 25;
                }
                
                Excel.Range productTitle = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, listView1.Columns.Count]);
                productTitle.Font.ColorIndex = 5;
                productTitle.Font.Bold = true;
                productTitle.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                productTitle.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;

                //д����ֵ
                //this.lblpro.Visible = true;
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    Application.DoEvents();
                    for (int i = 0; i < listView1.Columns.Count; i++)
                    {
                        worksheet.Cells[j + 2, i + 1] = listView1.Items[j].SubItems[i].Text;
                    }
                }
                if (saveFileName != "")
                {
                    try
                    {
                        //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8��Excel97-2003��ʽ
                        //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12��Excel2007��ʽ
                        //���Ϊ97-2003�ļ���ģʽ
                        workbook.Saved = true;
                        workbook.SaveCopyAs(saveFileName);

                        //'Microsoft.Office.Interop.Excel,   Version=12.0.0.0  2007
                        //Microsoft.Office.Interop.Excel,   Version=12.0.0.0  2003
                        /*
                        workbook.SaveAs(saveFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing,
                            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                         */
                        fileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        fileSaved = false;
                        Busiclass.MsgError("�����ļ�ʱ����,�ļ����������򿪣�\n" + ex.Message);
                        xlApp.Quit();
                        GC.Collect();//ǿ����
                        return;
                    }
                }
                else
                {
                    fileSaved = false;
                }

                xlApp.Quit();
                GC.Collect();//ǿ������
                Busiclass.MsgOK("�б����ݵ�����Excel���!       ");
                //toolStripStatusLabel1.Text = "Excel�������...";
            }
        }
        #endregion

        #region �������ݵ�CSV
        /// <summary>
        /// �������ݵ�CSV
        /// </summary>
        /// <param name="className"></param>
        /// <param name="ex"></param>        
        public static void ExportListViewCSV(ListView listView1)
        {
            string saveFileName = "";
            bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "csv";
            saveDialog.Filter = "csv�ļ�|*.csv";
            saveDialog.RestoreDirectory = true;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                FileStream fs = new FileStream(saveFileName, FileMode.Create);
                //string ls_Path = Busiclass.gs_ExeName + "error.txt";
                /*
                if (!File.Exists(ls_Path))
                {
                    fs = new FileStream(saveFileName, FileMode.Create);
                }
                else
                {
                    fs = new FileStream(saveFileName, FileMode.Append);
                }
                */
                string line = "";
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                try
                {

                    //д���ͷ
                    for (int i = 0; i < listView1.Columns.Count; i++)
                    {
                        line += listView1.Columns[i].Text.ToString() + ",";
                    }
                    sw.WriteLine(line);
                    //д����ϸ����
                    for (int j = 0; j < listView1.Items.Count; j++)
                    {
                        Application.DoEvents();
                        line = "";
                        for (int i = 0; i < listView1.Columns.Count; i++)
                        {
                            line += listView1.Items[j].SubItems[i].Text + ",";
                        }
                        sw.WriteLine(line);
                    }

                    sw.Close();
                    fs.Close();
                    Busiclass.MsgOK("�������ݵ�CSV��ʽ�ļ����!    ");
                }
                catch (Exception ex)
                {
                    sw.Close();
                    fs.Close();
                  Busiclass.MsgError("�����ļ�ʱ���� \n" + ex.Message);
                }
            }
        }
        #endregion 
    }
}
