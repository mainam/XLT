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
    public partial class frmQlTiet : Form
    {
        public frmQlTiet()
        {
            InitializeComponent();
        }


        List<GioThi> GetDsGioThi()
        {
            List<GioThi> kq = new List<GioThi>();
            foreach (DataGridViewRow r in dgrDanhSach.Rows)
            {
                GioThi gt = new GioThi(r.Cells[0].Value.ToString(), r.Cells[1].Value.ToString());
                kq.Add(gt);
            }
            return kq;
        }



        private void btnGetGio_Click(object sender, EventArgs e)
        {

            try
            {
                if (DateTime.Parse(dtpBatDau.Value.ToShortDateString()) >
                    DateTime.Parse(dtpKetThuc.Value.ToShortDateString()))
                {
                    BatLoi.ThongBao2("Vui lòng kiểm tra lại ngày");
                    return;
                }
            }
            catch (Exception)
            {
                BatLoi.ThongBao2("Vui lòng kiểm tra lại ngày");
                return;
            }
            dgrDanhSach.Rows.Clear();
            double ttday = (dtpKetThuc.Value - dtpBatDau.Value).TotalDays;
            for (int i = 0; i <= ttday; i++)
            {
                string st = dtpBatDau.Value.AddDays(i).ToString("dd/MM/yyyy");
                dgrDanhSach.Rows.Add(st, " 7:00", "Xóa");
                dgrDanhSach.Rows.Add(st, " 9:00", "Xóa");
                dgrDanhSach.Rows.Add(st, " 13:00", "Xóa");
                dgrDanhSach.Rows.Add(st, " 15:00", "Xóa");
            }
        }

        private void dgrDanhSach_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                    dgrDanhSach.Rows.Remove(dgrDanhSach.CurrentRow);
            }
            catch (Exception)
            {
            }
        }

        void LoadData(List<GioThi> ds)
        {
            dgrDanhSach.Rows.Clear();
            foreach (GioThi gt in ds)
                dgrDanhSach.Rows.Add(new string[] { gt.Ngay, gt.Gio, "Xóa" });
        }


        public void frmQlBacHoc_Load(object sender, EventArgs e)
        {

            LoadData(XuLyXml.DocDsGioThi());
            dtpBatDau.Value = DateTime.Now;
            dtpKetThuc.Value = dtpBatDau.Value.AddDays(1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dgrDanhSach_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgrDanhSach.CurrentCell = dgrDanhSach.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            catch (Exception)
            {
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            XuLyXml.LuuGioThi(GetDsGioThi());
            this.Close();
        }

        private void dgrDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
                dgrDanhSach.Rows.RemoveAt(e.RowIndex);
        }

    }
}
