using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection; // 引用这个才能使用Missing字段 
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
                    Busiclass.MsgError("无法创建Excel对象，可能您的机子未安装Excel");
                    return;
                }
                //xlApp.Visible = true;
                xlApp.Visible = false ;
                workbooks = xlApp.Workbooks;
                workbook = workbooks.Add(true);
                OpenMode = 0;
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                rowRead = worksheet.UsedRange.Cells.Rows.Count; //worksheet.Rows.Count;
                ColumnRead = worksheet.UsedRange.Cells.Columns.Count; //worksheet.Rows.Count;
                //worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, 61]).Columns.AutoFit();
                worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, 1]).ColumnWidth = 6;
                worksheet.get_Range(worksheet.Cells[1, 2], worksheet.Cells[1, 2]).ColumnWidth = 12;
                worksheet.get_Range(worksheet.Cells[1, 13], worksheet.Cells[1, 13]).ColumnWidth = 25;
                worksheet.Cells[1, 1] = "序号";
                worksheet.Cells[1, 2] = "图片名称";
                worksheet.Cells[1, 3] = "圆名称";
                worksheet.Cells[1, 4] = "R";
                worksheet.Cells[1, 5] = "G";
                worksheet.Cells[1, 6] = "B";
                worksheet.Cells[1, 7] = "C";
                worksheet.Cells[1, 8] = "M";
                worksheet.Cells[1, 9] = "Y";
                worksheet.Cells[1, 10] = "K";
                worksheet.Cells[1, 11] = "圆心坐标X";
                worksheet.Cells[1, 12] = "圆心坐标Y";
                worksheet.Cells[1, 13] = "直径";
                worksheet.Cells[1, 14] = "时间";
                /*
                
                int n = 0;
                for (int i = 2; i < 242; i = i + 3)
                {
                    n++;
                    worksheet.Cells[1, i] = (n).ToString() + "列 R";
                    worksheet.Cells[1, i + 1] = (n).ToString() + "列 G";
                    worksheet.Cells[1, i + 2] = (n).ToString() + "列 B";
                }
                for (int i = 0; i < 720; i++)
                    worksheet.Cells[i + 2, 1] = (i + 1).ToString();             
                 */ 
            }
            catch (System.Exception ex)
            {
                CloseExcel();
                Busiclass.MsgError("无法创建Excel对象，可能您的机子未安装Excel");
            }          
        }

        /// <summary>Y 行  X 列
        /// Y 行  X 列
        /// </summary>
        /// <param name="y">行</param>
        /// <param name="x">列</param>
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
                //保存工作簿
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
              
                //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8：Excel97-2003格式
                //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12：Excel2007格式
                //另存为97-2003的兼容模式              
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
                Busiclass.MsgError("保存Excel出错,文件可能正被打开！\n" + ex.Message);
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
                    GC.Collect();//强行销毁
                }
            }
            catch
            { 
            }
             */ 

        }
        #region 导出dataGridView1到Excel  SaveDataGridViewExcel
        /// <summary>
        /// 保存dataGridView1到Excel
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="saveFileDialog"></param>
        public static void SaveDataGridViewExcel(DataGridView dataGridView1)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xlsx files(*.xlsx)|*.xlsx"; //保存为xls格式
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
                //写标题
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dataGridView1.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                //写内容
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
                Busiclass.MsgOK("数据导出完成");
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message.ToString(), "错误提示");
                Busiclass.MsgError(e.Message.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }
        #endregion


        #region  自动listView1数据到 Excel AutotListViewExcel
        /// <summary>
        /// 自动listView1数据到 Excel
        /// </summary>
        /// <param name="listView1"></param>
        /// <param name="FileName"></param>
        public static void AutotListViewExcel(ListView listView1, string saveFileName)
        {
            //toolStripStatusLabel1.Text = "正在保存Excel数据,请稍候...";
            //   DataTable xslTable=(DataTable)this.dgrd_Show.DataSource;//取得dataGrid绑定的DataSet
            //   if(xslTable==null) return;
            // if (listView1.Items.Count == 0) return;
            Excel.Application xlApp = new Excel.Application();
            try
            {
                if (xlApp == null)
                {
                    Busiclass.MsgError("无法创建Excel对象，可能您的机子未安装Excel");
                    return;
                }
                Excel.Workbooks workbooks = xlApp.Workbooks;
                Excel.Workbook workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                Excel.Range range;
                //   string oldCaption=this.listView1.ca.CaptionText;
                long totalCount = listView1.Items.Count;
                long rowRead = 0;
                float percent = 0;

                //写入表头
                for (int i = 0; i < listView1.Columns.Count; i++)
                {
                    Application.DoEvents();
                    worksheet.Cells[1, i + 1] = listView1.Columns[i].Text.ToString();
                }
                //设置表头显示样式
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

                //写入数值
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
                        //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8：Excel97-2003格式
                        //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12：Excel2007格式
                        //另存为97-2003的兼容模式
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
                        Busiclass.MsgError("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                        xlApp.Quit();
                        GC.Collect();//强行销
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
                GC.Collect();//强行销毁
            }

        }
        #endregion


        #region  导出listView1数据到 Excel ExportListViewExcel
        /// <summary>
        /// 导出listView1数据到 Excel
        /// </summary>
        /// <param name="listView1"></param>
        /// <param name="FileName"></param>
        public static void ExportListViewExcel(ListView listView1)
        {
            //toolStripStatusLabel1.Text = "正在保存Excel数据,请稍候...";
            //   DataTable xslTable=(DataTable)this.dgrd_Show.DataSource;//取得dataGrid绑定的DataSet
            //   if(xslTable==null) return;
            // if (listView1.Items.Count == 0) return;
            string saveFileName = "";
            bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = ".xls";
            saveDialog.Filter = "Excel文件|*..xls";
            saveDialog.RestoreDirectory = true;
            //saveDialog.FileName = FileName; //Busiclass.gs_PathName + "\\Data\\" + ls_FileName + ".xls";// "Sheet1";
            //saveDialog.ShowDialog();

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                Excel.Application xlApp = new Excel.Application();
                if (xlApp == null)
                {
                    Busiclass.MsgError("无法创建Excel对象，可能您的机子未安装Excel");
                    return;
                }
                Excel.Workbooks workbooks = xlApp.Workbooks;
                Excel.Workbook workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                Excel.Range range;
                //   string oldCaption=this.listView1.ca.CaptionText;
                long totalCount = listView1.Items.Count;
                long rowRead = 0;
                float percent = 0;

                //写入表头
                for (int i = 0; i < listView1.Columns.Count; i++)
                {
                    Application.DoEvents();
                    worksheet.Cells[1, i + 1] = listView1.Columns[i].Text.ToString();
                }
                //设置表头显示样式 ，设置宽度
                if (listView1.Columns.Count>28)
                {
                    worksheet.get_Range(worksheet.Cells[1, 29], worksheet.Cells[1, 29]).ColumnWidth = 25;
                }
                
                Excel.Range productTitle = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, listView1.Columns.Count]);
                productTitle.Font.ColorIndex = 5;
                productTitle.Font.Bold = true;
                productTitle.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                productTitle.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;

                //写入数值
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
                        //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8：Excel97-2003格式
                        //Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12：Excel2007格式
                        //另存为97-2003的兼容模式
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
                        Busiclass.MsgError("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                        xlApp.Quit();
                        GC.Collect();//强行销
                        return;
                    }
                }
                else
                {
                    fileSaved = false;
                }

                xlApp.Quit();
                GC.Collect();//强行销毁
                Busiclass.MsgOK("列表数据导出到Excel完毕!       ");
                //toolStripStatusLabel1.Text = "Excel保存完毕...";
            }
        }
        #endregion

        #region 导出数据到CSV
        /// <summary>
        /// 导出数据到CSV
        /// </summary>
        /// <param name="className"></param>
        /// <param name="ex"></param>        
        public static void ExportListViewCSV(ListView listView1)
        {
            string saveFileName = "";
            bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "csv";
            saveDialog.Filter = "csv文件|*.csv";
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

                    //写入表头
                    for (int i = 0; i < listView1.Columns.Count; i++)
                    {
                        line += listView1.Columns[i].Text.ToString() + ",";
                    }
                    sw.WriteLine(line);
                    //写入详细数据
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
                    Busiclass.MsgOK("导出数据到CSV格式文件完成!    ");
                }
                catch (Exception ex)
                {
                    sw.Close();
                    fs.Close();
                  Busiclass.MsgError("导出文件时出错 \n" + ex.Message);
                }
            }
        }
        #endregion 
    }
}
