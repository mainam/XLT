using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace DataAccess
{
    public class Helper
    {
        //public static OleDbConnection getConnection(string FileName)
        //{
        //    //string connString = @"provider=Microsoft.Jet.OLEDB.4.0;data source="+FileName+"; Extended Properties=Excel 8.0;";
        //    //            string connectionstring = String.Empty;
        //    string connString = "";
        //    string[] splitdot = FileName.Split(new char[1] { '.' });
        //    string dot = splitdot[splitdot.Length - 1].ToLower();
        //    if (dot == "xls")
        //    {
        //        //tao chuoi ket noi voi Excel 2003
        //        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
        //    }
        //    else if (dot == "xlsx")
        //    {
        //        //tao chuoi ket noi voi Excel 2007
        //        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
        //    }


        //    OleDbConnection cnn = new OleDbConnection(connString);

        //    return cnn;
        //}
        public static OleDbConnection getConnection(string FileName)
        {
            string connString = "";
            string provider = "Microsoft.Jet.OLEDB.4.0";
            string extended = FileName.Substring(FileName.LastIndexOf('.') + 1).ToLower();
            if (extended.CompareTo("xlsx") == 0)
                provider = "Microsoft.ACE.OLEDB.12.0";
            connString = "Provider=" + provider + ";Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            OleDbConnection cnn = new OleDbConnection(connString);
            return cnn;
        }
    }
    public class Xuly
    {
        //public static 
    }

}
