using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Reporting.WinForms;
using DataAccess;

namespace XepLichThi
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }
        //public DataTable dt;
        private void frmReport_Load(object sender, EventArgs e)
        {

            //DataSet dsReport = new dsLichThi();

            //dsReport.Tables.Add(Excel.Import(@"D:\Book1.xls"));
            ////provide local report information to viewer
            //reportViewer.LocalReport.ReportEmbeddedResource =
            //"XepLichThi.rptLichThi.rdlc";

            ////prepare report data source
            //ReportDataSource rds = new ReportDataSource();
            //rds.Name = "dsLichThi_dtLichThi";
            //rds.Value = dsReport.Tables[0];
            //reportViewer.LocalReport.DataSources.Add(rds);
            //dataGridView1.DataSource = dsReport.Tables[0];

            ////load report viewer
            //reportViewer.RefreshReport();
            
            //declare Connection, command and other related objects


            OleDbConnection conReport = Helper.getConnection(@"D:\Book1.xlsx");
            OleDbCommand cmdReport = new OleDbCommand();
            OleDbDataAdapter daReport;
            DataSet dsReport = new dsLichThi();

            try
            {
                //open connection
                conReport.Open();

                //prepare connection object to get the data through reader and populate into dataset
                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = "Select top 20 * from [Sheet1$]";

                //read data from command object
                daReport = new OleDbDataAdapter(cmdReport);

                //new cool thing with ADO.NET... load data directly from reader to dataset
                //dsReport.Tables[0].Load(drReport);
                daReport.Fill(dsReport);

                //close reader and connection
                //daReport.Close();
                conReport.Close();

                //provide local report information to viewer
                reportViewer.LocalReport.ReportEmbeddedResource =
                "XepLichThi.rptLichThi.rdlc";

                //prepare report data source
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsLichThi_dtLichThi";
                rds.Value = dsReport.Tables[0];
                reportViewer.LocalReport.DataSources.Add(rds);

                //load report viewer
                reportViewer.RefreshReport();
                //dataGridView1.DataSource = dsReport.Tables[0];
            }
            catch (Exception ex)
            {
                //display generic error message back to user
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //check if connection is still open then attempt to close it
                if (conReport.State == ConnectionState.Open)
                {
                    conReport.Close();
                }
            }

            //this.reportViewer.RefreshReport();
        }

        private void dtLichThiBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
