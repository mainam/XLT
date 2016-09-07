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
    public partial class frmChonNhom : Form
    {
        public frmChonNhom()
        {
            InitializeComponent();
        }
        DataGridView source;
        public frmChonNhom(DataGridView sou)
        {
            InitializeComponent();
            dgrDanhSach.DataSource = source = sou;
            DataGridViewButtonColumn objbot = XuLyDataGridView.CreateButtonColumn("CHỌN");

            dgrDanhSach.Columns.Insert(5, objbot);
            dgrDanhSach.Columns[0].Width = 40;
            dgrDanhSach.Columns[6].Width = 50;
            dgrDanhSach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public frmChonNhom(DataTable source)
        {
            InitializeComponent();
            dgrDanhSach.DataSource = source;
            XuLyDataGridView.ReadOnly(dgrDanhSach);
            DataGridViewButtonColumn objbot = XuLyDataGridView.CreateButtonColumn("CHỌN");
            dgrDanhSach.Columns.Insert(5, objbot);
            dgrDanhSach.Columns[0].Width = 40;
            dgrDanhSach.Columns[5].Width = 50;
            dgrDanhSach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void frmChonNhom_Load(object sender, EventArgs e)
        {

        }

        private void dgrDanhSach_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    frmSelectNhom fsn = new frmSelectNhom(Convert.ToString(dgrDanhSach.CurrentRow.Cells[5].Value));
                    fsn.StartPosition = FormStartPosition.CenterParent;
                    if (fsn.ShowDialog() == DialogResult.OK)
                        dgrDanhSach.CurrentRow.Cells[5].Value = string.Join(";", fsn.Result());
                }
            }
            catch (Exception)
            {
            }

        }

        private void btnTuTongChon_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgrDanhSach.Rows)
            {
                try
                {
                    if (r.Cells["MÃ HP"].Value.ToString().Substring(2, 1) != "0")
                        r.Cells["NHÓM"].Value = "Đại Học";
                    else
                        r.Cells["NHÓM"].Value = "Cao Đẳng";
                }
                catch (Exception)
                {

                }
            }
        }

    }
}
