using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace DataAccess
{
    public class Excel
    {
        public static DataTable Import(string fileName)
        {
            OleDbConnection cnn = Helper.getConnection(fileName);
            OleDbCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT * FROM [Sheet1$]";
            //3. Lay du lieu
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public static void Export(DataTable dt, string fileName, string title)
        {
            ///////----- Thao tac voi file co san
            //1. Chạy phần mềm Excel
            //Microsoft.Office.Interop.Excel.Application t = new Microsoft.Office.Interop.Excel.Application();
            //t.Visible = true;
            ////2. Mở một file excel đã có, và cho biến x trỏ đến file excel này
            //Microsoft.Office.Interop.Excel.Workbook x;
            //x = t.Workbooks.Open(fileName, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            ////4. Cho một biến s trỏ đến một sheet của file excel x (đã tạo ở Lưu ý 2. hoặc 3.)
            //Microsoft.Office.Interop.Excel.Worksheet s;
            //s = (Microsoft.Office.Interop.Excel.Worksheet)x.Worksheets[1];

            ////-------Tạo một file excel mới
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook 
            oExcel.Visible =true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = "KetQua";



            // Tạo phần đầu nếu muốn
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "L1");
            head.MergeCells = true;
            head.RowHeight = 30;
            head.Value2 = title;
            head.Font.Bold = true;
            head.Font.Name = "Tahoma";
            head.Font.Size = "18";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 
            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A2", "A2");
            cl1.Value2 = "STT";
            cl1.ColumnWidth = 8;

            Microsoft.Office.Interop.Excel.Range cl2 = cl1.Next;
            cl2.Value2 = "MÃ HP";
            cl2.ColumnWidth = 10;

            Microsoft.Office.Interop.Excel.Range cl3 = cl2.Next;
            cl3.Value2 = "TÊN HỌC PHẦN";
            cl3.ColumnWidth = 35;


            Microsoft.Office.Interop.Excel.Range cl5 = cl3.Next;
            cl5.Value2 = "SỐ TC";
            cl5.ColumnWidth = 8;

            Microsoft.Office.Interop.Excel.Range cl6 = cl5.Next;
            cl6.Value2 = "SỐ SV";
            cl6.ColumnWidth = 8;

            Microsoft.Office.Interop.Excel.Range cl7 = cl6.Next;
            cl7.Value2 = "HT THI";
            cl7.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range cl8 = cl7.Next;
            cl8.Value2 = "THỜI LƯỢNG";
            cl8.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range cl9 = cl8.Next;
            cl9.Value2 = "NGÀY";
            cl9.ColumnWidth = 15;
            cl9.EntireColumn.NumberFormat = "dd-MM-yyyy";

            Microsoft.Office.Interop.Excel.Range cl10 = cl9.Next;
            cl10.Value2 = "GIỜ";
            cl10.ColumnWidth = 10;
            cl10.EntireColumn.NumberFormat = "hh:mm";

            Microsoft.Office.Interop.Excel.Range cl11 = cl10.Next;
            cl11.Value2 = "PHÒNG";
            cl11.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range cl12 = cl11.Next;
            cl12.Value2 = "LỚP";
            cl12.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl13 = cl12.Next;
            cl13.Value2 = "GIẢNG VIÊN";
            cl13.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A2", "L2");
            rowHead.Font.Bold = true;
            // Kẻ viền
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            // Thiết lập màu nền
            rowHead.Interior.ColorIndex = 15;
            rowHead.Font.Size = 13;
            rowHead.RowHeight = 20;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


            // Tạo mảng đối tượng để lưu dữ toàn bộ dữ liệu trong DataTable,
            // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.
            object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];

            //Chuyển dữ liệu từ DataTable vào mảng đối tượng
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                DataRow dr = dt.Rows[r];
                for (int c = 0; c < dt.Columns.Count-1; c++)
                {
                        arr[r, c] = dr[c];
                }
            }
            //Thiết lập vùng điền dữ liệu
            int rowStart = 3;
            int columnStart = 1;

            int rowEnd = rowStart + dt.Rows.Count - 1;
            int columnEnd = dt.Columns.Count;

            // Ô bắt đầu điền dữ liệu
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            // Ô kết thúc điền dữ liệu
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            // Lấy về vùng điền dữ liệu
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);


            //Điền dữ liệu vào vùng đã thiết lập
            range.Value2 = arr;

            // Kẻ viền
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            // Định dạng
            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            oSheet.get_Range(c1, c2).Font.Size = 12;
            //oBook.SaveAs(fileName,Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet, false,false,false, false,Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,false, false, false, false, false);
            oBook.SaveAs(fileName);
            oBook.Close();
            oExcel.Quit();
            DialogResult rs = MessageBox.Show("Lưu thành công, bạn có muốn mở file không","Lưu file",MessageBoxButtons.OKCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.OK)
                Process.Start(fileName);
        }
        
    }
}
