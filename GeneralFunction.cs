using ClosedXML.Excel;
using System.Data;

namespace APACExportTrackX
{
    public class GeneralFunction
    {
        public static DataTable GetDataFromExcel(string path)
        {
            DataTable dt = new DataTable();
            try
            {
                using (XLWorkbook workBook = new XLWorkbook(path))
                {

                    IXLWorksheet workSheet = workBook.Worksheet(1);
                    bool firstRow = true;
                    int skiprows = 1;
                    int skip = 1;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        //skiprows = skiprows - 1;
                        if (skiprows< skip)
                        {
                            //Use the first row to add columns to DataTable.
                            if (firstRow)
                            {
                                int j = 0;
                                foreach (IXLCell cell in row.Cells())
                                {
                                    //if (!string.IsNullOrEmpty(cell.Value.ToString()))
                                    //{
                                    //    dt.Columns.Add(cell.Value.ToString());
                                    //}
                                    //else
                                    //{    //string A = "A" + j;
                                    //     //dt.Columns.Add(A.ToString());
                                    //}
                                    dt.Columns.Add(cell.Value.ToString().Trim());

                                    j++;
                                }
                                firstRow = false;
                            }
                            else
                            {
                                if (!row.IsEmpty())
                                {
                                    try
                                    {
                                        int i = 0;
                                        DataRow toInsert = dt.NewRow();
                                        foreach (IXLCell cell in row.Cells(1, dt.Columns.Count))
                                        {
                                            try
                                            {
                                                if (cell.Value.ToString().Trim() == "")
                                                {
                                                    toInsert[i] = "";
                                                }
                                                else
                                                {
                                                    toInsert[i] = cell.Value.ToString();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                            }

                                            i++;
                                        }
                                        dt.Rows.Add(toInsert);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                                //}
                            }


                            
                        }
                        skip = skip + 1;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        
    }
}
