using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataAccess;
namespace XepLichThi
{
    public partial class frmChonPhong : Form
    {
        public frmChonPhong()
        {
            InitializeComponent();
        }
        public frmChonPhong(DataTable source)
        {
            InitializeComponent();
            dgrDanhSach.DataSource = source;
            XuLyDataGridView.ReadOnly(dgrDanhSach);
            DataGridViewButtonColumn objbot = XuLyDataGridView.CreateButtonColumn("CHỌN");
            dgrDanhSach.Columns.Insert(6, objbot);
            dgrDanhSach.Columns[0].Width = 40;
            dgrDanhSach.Columns[6].Width = 50;
            dgrDanhSach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgrDanhSach_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if(e.ColumnIndex==0)
                {
                        frmSelectPhong fsp = new frmSelectPhong(Convert.ToString(dgrDanhSach.CurrentRow.Cells[6].Value));
                        fsp.StartPosition = FormStartPosition.CenterParent;
                        if (fsp.ShowDialog() == DialogResult.OK)
                            dgrDanhSach.CurrentRow.Cells[6].Value = string.Join(";", fsp.Result());
                }
            }
            catch (Exception)
            {
            }
        }

        private void frmChonPhong_Load(object sender, EventArgs e)
        {
            dgrDanhSach.Columns[1].ReadOnly
                = dgrDanhSach.Columns[2].ReadOnly
                = dgrDanhSach.Columns[3].ReadOnly
                = dgrDanhSach.Columns[4].ReadOnly = true;
        }


        string SelectPhong(int sl, List<Phong> DsPhong)
        {
            string s = "A";


            return s;
        }
        private void btnTuDongChon_Click(object sender, EventArgs e)
        {
            //List<Phong> DsPhong = XuLyXml.DocDsPhong();
            //foreach (DataGridViewRow r in dgrDanhSach.Rows)
            //{
            //    int sl = Convert.ToInt32(r.Cells["SỐ SV"].Value);
            //    r.Cells["PHÒNG"].Value = SelectPhong(sl, DsPhong);
            //}

        }

    }
}
