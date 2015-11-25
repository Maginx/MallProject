using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//add by designer
using System.Data;
using System.IO;
using System.Text;
//using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Data.OleDb;

/// <summary>
/// Excel导出
/// </summary>
internal sealed class ExportExcel
{
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="excelTable">DataTable数据集</param>
    /// <param name="filePath">文件保存路径</param>
    /// <returns>导出结果</returns>
    public static bool SaveDataTableToExcel(System.Data.DataTable excelTable, string filePath)
    {
        //Microsoft.Office.Interop.Excel.Application app =
        //    new Microsoft.Office.Interop.Excel.ApplicationClass();
        //try
        //{
        //    app.Visible = false;
        //    Workbook wBook = app.Workbooks.Add(true);
        //    Worksheet wSheet = wBook.Worksheets[1] as Worksheet;

        //    if (excelTable.Rows.Count > 0)
        //    {
        //        int row = 0;
        //        row = excelTable.Rows.Count;
        //        int col = excelTable.Columns.Count;
        //        for (int i = 0; i < row; i++)
        //        {
        //            for (int j = 0; j < col; j++)
        //            {
        //                string str = excelTable.Rows[i][j].ToString();
        //                wSheet.Cells[i + 2, j + 1] = str;
        //            }
        //        }
        //    }

        //    int size = excelTable.Columns.Count;
        //    for (int i = 0; i < size; i++)
        //    {
        //        wSheet.Cells[1, 1 + i] = excelTable.Columns[i].ColumnName;
        //    }
        //    //设置禁止弹出保存和覆盖的询问提示框 
        //    app.DisplayAlerts = false;
        //    app.AlertBeforeOverwriting = false;
        //    //保存工作簿 
        //    wBook.Save();
        //    //保存excel文件 
        //    app.Save(filePath);
        //    app.SaveWorkspace(filePath);
        //    app.Quit();
        //    app = null;
        //    return true;
        //}
        //catch (Exception err)
        //{
        //    MessageBox.Show("导出Excel出错！错误原因：" + err.Message, "提示信息",
        //        MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    return false;
        //}
        //finally
        //{
        //}
        return false;
    }
    ///<summary>
    /// 导出Excel
    ///</summary>
    public static void SaveExcel(System.Data.DataTable dt, string Filter, string FileName, string SheetName)
    {
        try
        {

            string ConnStr;
            ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" + FileName + "\";Extended Properties=\"Excel 8.0;HDR=YES\"";
            OleDbConnection conn_excel = new OleDbConnection();
            conn_excel.ConnectionString = ConnStr;
            OleDbCommand cmd_excel = new OleDbCommand();
            string sql;
            sql = SqlCreate(dt, SheetName);
            conn_excel.Open();
            cmd_excel.Connection = conn_excel;
            cmd_excel.CommandText = sql;
            cmd_excel.ExecuteNonQuery();
            conn_excel.Close();
            OleDbDataAdapter da_excel = new OleDbDataAdapter("Select * From [" + SheetName + "$]", conn_excel);
            System.Data.DataTable dt_excel = new System.Data.DataTable();
            da_excel.Fill(dt_excel);
            da_excel.InsertCommand = SqlInsert(SheetName, dt, conn_excel);
            DataRow dr_excel;
            string ColumnName;
            foreach (DataRow dr in dt.Select(Filter))
            {
                dr_excel = dt_excel.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    ColumnName = dc.ColumnName;
                    dr_excel[ColumnName] = dr[ColumnName];
                }
                dt_excel.Rows.Add(dr_excel);
            }
            da_excel.Update(dt_excel);
            conn_excel.Close();
        }
        catch
        {
        }
    }


    private static string GetDataType(Type i)
    {
        string s;
        switch (i.Name)
        {
            case "String":
                s = "Char";
                break;
            case "Int32":
                s = "Int";
                break;
            case "Int64":
                s = "Int";
                break;
            case "Int16":
                s = "Int";
                break;
            case "Double":
                s = "Double";
                break;
            case "Decimal":
                s = "Double";
                break;
            default:
                s = "Char";
                break;
        }
        return s;
    }
    private static OleDbType StringToOleDbType(Type i)
    {
        OleDbType s;
        switch (i.Name)
        {
            case "String":
                s = OleDbType.Char;
                break;
            case "Int32":
                s = OleDbType.Integer;
                break;
            case "Int64":
                s = OleDbType.Integer;
                break;
            case "Int16":
                s = OleDbType.Integer;
                break;
            case "Double":
                s = OleDbType.Double;
                break;
            case "Decimal":
                s = OleDbType.Decimal;
                break;
            default:
                s = OleDbType.Char;
                break;
        }
        return s;
    }
    private static string SqlCreate(System.Data.DataTable dt, string SheetName)
    {
        string sql;
        sql = "CREATE TABLE " + SheetName + " (";
        foreach (DataColumn dc in dt.Columns)
        {
            sql += "[" + dc.ColumnName + "] " + GetDataType(dc.DataType) + " ,";
        }
        //sql = "CREATE TABLE [" + SheetName + "] (";
        //foreach (C1.Win.C1TrueDBGrid.C1DataColumn dc in grid.Columns)
        //{
        //    sql += "[" + dc.Caption + "] " + GetDataType(dc.DataType) + ",";
        //}
        //sql = sql.Substring(0, sql.Length - 1);
        //sql += ")";
        sql = sql.Substring(0, sql.Length - 1);
        sql += ")";
        return sql;

    }
    // 生成 InsertCommand 并设置参数
    private static OleDbCommand SqlInsert(string SheetName, System.Data.DataTable dt, OleDbConnection conn_excel)
    {
        OleDbCommand i;
        string sql;
        sql = "INSERT INTO [" + SheetName + "$] (";
        foreach (DataColumn dc in dt.Columns)
        {
            sql += "[" + dc.ColumnName + "] ";
            sql += ",";
        }
        sql = sql.Substring(0, sql.Length - 1);
        sql += ") VALUES (";
        foreach (DataColumn dc in dt.Columns)
        {
            sql += "?,";
        }
        sql = sql.Substring(0, sql.Length - 1);
        sql += ")";
        i = new OleDbCommand(sql, conn_excel);
        foreach (DataColumn dc in dt.Columns)
        {
            i.Parameters.Add("@" + dc.Caption, StringToOleDbType(dc.DataType), 0, dc.Caption);
        }
        return i;
    }

}

