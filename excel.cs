
public void Run() 
{ 
 //add your codes here:
 List<VTask> tasks = new List<VTask>();
 tasks.Add(new VTask(){Task="task1",Price="100",Url=@"http://www.baidu.com"});
 tasks.Add(new VTask(){Task="task2",Price="200",Url=@"http://www.qq.com"});
 
 //ExportExcel(tasks);
}

private void ExportExcel(List<VTask> docList)
{
            Cursor.Current = Cursors.WaitCursor;
			string fileName = @"..\caiji" + DateTime.Now.ToString("yyyyMMddhhmm");

            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Excel.Application ea = new Excel.Application();
            try
            {               
                int rowIndex = 1;

                ea.Application.Workbooks.Add(true);

                ea.Cells[1, 1] = "Task";
                ea.Cells[1, 2] = "Price";
                ea.Cells[1, 3] = "Url";

                int totalCount = docList.Count;

                for (int i = 0; i < totalCount; i++)
                {
                    if (docList[i] is VTask)
                    {
                        VTask doc = docList[i];
                        rowIndex++;
                        ea.Cells[rowIndex, 1] = doc.Task;
                        ea.Cells[rowIndex, 2] = doc.Price;
                        ea.Cells[rowIndex, 3] = doc.Url;
                    }
                }

                ea.get_Range("A1:Z1").Font.Bold = true;
                ea.Columns.AutoFit();
                ea.Rows.AutoFit();
                ea.Visible = false;
                ea.ActiveWorkbook.SaveAs(fileName, Excel.XlFileFormat.xlExcel8, null, null, false, false, Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null);
                
                ea.Visible = true;
            }
            catch (Exception ex)
            {
                Logger.log(ExceptionHelper.ToString(ex));
            }
            finally
            {
                //ea.Quit();
                ea = null;
            }
        }
        
[Serializable]
public class VTask
{
	public string Task{get;set;}
	public string Price{get;set;}
	public string Url{get;set;}
}
