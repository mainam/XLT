using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DataAccess;

namespace XepLichThi
{
    public partial class frmQlBacHoc : DevComponents.DotNetBar.Office2007Form
    {
        public frmQlBacHoc()
        {
            InitializeComponent();
        }


        bool KiemTra(string Text)
        {
            if (Text.Trim() == "")
                return BatLoi.ThongBao2("Vui lòng nhập tên bậc học");
            foreach (DataGridViewRow r in dgrDanhSach.Rows)
            {
                if (Convert.ToString(r.Cells[0].Value).ToLower() == Text.ToLower())
                    return BatLoi.ThongBao2("Bậc này đã có trong danh sách");
            }
            return !BatLoi.ThongBao2("Thêm thành công");
        }

        List<string> GetDsBacHoc()
        {
            List<string> kq = new List<string>();
            foreach (DataGridViewRow r in dgrDanhSach.Rows)
            {
                string st = Convert.ToString(r.Cells[0].Value);
                if (st.Trim() != "")
                    kq.Add(st);
            }
            return kq;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTra(txtText.Text.Trim()))
            {
                dgrDanhSach.Rows.Add(new string[] { txtText.Text, "Xóa" });
                txtText.Text = "";
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

        void LoadData(List<string> ds)
        {
            dgrDanhSach.Rows.Clear();
            foreach (string st in ds)
                dgrDanhSach.Rows.Add(st,"Xóa");
        }

        public void frmQlBacHoc_Load(object sender, EventArgs e)
        {
            LoadData(XuLyXml.DocDsBacHoc());

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            XuLyXml.LuuBacHoc(GetDsBacHoc(), "BacHoc", "BacHocItems");
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
    }

}