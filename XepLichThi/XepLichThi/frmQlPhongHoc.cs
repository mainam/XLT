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
    public partial class frmQlPhongHoc : DevComponents.DotNetBar.Office2007Form
    {
        public frmQlPhongHoc()
        {
            InitializeComponent();
        }

        List<Phong> GetDsPhong()
        {
            List<Phong> kq = new List<Phong>();
            foreach (DataGridViewRow r in dgrDanhSach.Rows)
            {
                string st = Convert.ToString(r.Cells[0].Value);
                if (st.Trim() != "")
                {
                    kq.Add(new Phong(st, Convert.ToInt32(r.Cells[1].Value)));
                }
            }
            return kq;
        }
        bool KiemTra()
        {
            if (BatLoi.TextNull(txtTenPhong.Text, "Vui lòng nhập tên phòng")
                && BatLoi.TextNull(txtSoLuong.Text, "Vui lòng nhập số chỗ ngồi")
                && BatLoi.SoLuong(txtSoLuong.Text, "Số lượng chỗ ngồi sai, vui lòng kiểm tra lại"))
            {
                foreach (DataGridViewRow r in dgrDanhSach.Rows)
                {
                    if (Convert.ToString(r.Cells[0].Value).ToLower() == txtTenPhong.Text.ToLower())
                        return BatLoi.ThongBao2("Bậc này đã có trong danh sách");
                }
                return !BatLoi.ThongBao2("Thêm thành công");
            }
            return false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTra())
                dgrDanhSach.Rows.Add(txtTenPhong.Text, txtSoLuong.Text,"Xóa");
        }

        void LoadData(List<Phong> ds)
        {
            dgrDanhSach.Rows.Clear();
            foreach (Phong ph in ds)
                dgrDanhSach.Rows.Add(ph.TenPhong, ph.SoCho,"Xóa");
        }

        public void frmQlPhongHoc_Load(object sender, EventArgs e)
        {
            LoadData(XuLyXml.DocDsPhong());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgrDanhSach_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                    dgrDanhSach.Rows.Remove(dgrDanhSach.CurrentRow);
            }
            catch (Exception)
            {
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            XuLyXml.LuuPhongHoc(GetDsPhong());
            this.Close();

        }
    }
}