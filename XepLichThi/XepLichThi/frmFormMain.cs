using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DataAccess;
using System.Threading;

namespace XepLichThi
{
    public partial class frmFormMain : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public frmFormMain()
        {
            InitializeComponent();
        }
        string saveFileName = "";

        void HideStatus()
        {
            Thread.Sleep(2000);
            lbStatus.Text = "";
            TienTrinh.Abort();
        }
        Thread TienTrinh;
        void ShowStatus(string Text)
        {
            lbStatus.Text = Text;
            TienTrinh = new Thread(HideStatus);
            TienTrinh.Start();
        }

        private void frmXepLichThi_Load(object sender, EventArgs e)
        {
            labelX1.Location = new Point((this.Width - labelX1.Width) / 2, labelX1.Location.Y);
            labelX6.Location = new Point((this.Width - labelX6.Width) / 2, labelX6.Location.Y);
            rtMain.Select();
            lbStatus.Text = "Hãy mở file dữ liệu và thực hiện sắp xếp lịch thi";
            lblTime.Text = DuLieu.GetDay();
            if (XuLyXml.DaTaoTaiKhoan())
                Active_Form(false);
            else
            {
                DialogResult rs = MessageBox.Show("Ứng dụng chưa đăng kí tài khoản, bạn có muốn tạo tài khoản kô", "Chưa tạo tài khoản", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.OK)
                    TaoTaiKhoan();
                else
                {
                    Active_Form(false);
                    btnDangNhap.Text = "Tạo tài khoản";
                }
            }
        }
        #region RibonControl
        private void rtChucNang_Click(object sender, EventArgs e)
        {
            if (!DangNhap)
            {
                rtMain.Select();
                BatLoi.ThongBao2("Vui lòng đăng nhập để sử dụng chương trình");
            }
        }
        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (ribon1.Expanded)
            {
                ribon1.Expanded = false;
                btnExpand.Image = XepLichThi.Properties.Resources.a;
            }
            else
            {
                btnExpand.Image = XepLichThi.Properties.Resources.col;
                ribon1.Expanded = true;
            }
        }
        private void ribbonTabItem_DClick(object sender, EventArgs e)
        {
            if (ribon1.Expanded)
            {
                btnExpand.Image = XepLichThi.Properties.Resources.col;
            }
            else
            {
                btnExpand.Image = XepLichThi.Properties.Resources.a;
            }
        }
        #endregion
        #region Chuc nang======================
        private void btnDangNhap_TextChanged(object sender, EventArgs e)
        {
            if (btnDangNhap.Text == "Đăng Nhập")
            {
                this.btnDangNhap.Image = global::XepLichThi.Properties.Resources.open_key_icon;
                btnDoiMatKhau.Enabled = false;
            }
            if (btnDangNhap.Text == "Đăng Xuất")
            {
                this.btnDangNhap.Image = global::XepLichThi.Properties.Resources.Button_Log_Off_icon;
                btnDoiMatKhau.Enabled = true;
            }
        }
        void Active_Form(bool active)
        {
            DangNhap = active;
            if (active)
            {
                ShowStatus("Đăng nhập thành công!");
                btnDangNhap.Text = "Đăng Xuất";
                this.btnCapNhat.Enabled = true;
                if (!XuLyXml.DaTaoBacHoc())
                    if (BatLoi.DgResul("Bậc học chưa được cài đặt, bạn có muốn cài đặt không?"))
                    {
                        frmQlBacHoc qlbh = new frmQlBacHoc();
                        qlbh.ShowDialog();
                    }
            }
            else
            {
                this.btnDangNhap.Text = "Đăng Nhập";
                this.btnCapNhat.Enabled = false;
            }
        }
        private void btnThoatCT_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Thoát khỏi chương trình", "Thoát chương trình", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.OK)
                Application.Exit();
        }
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            DialogResult rs = openFileDialog.ShowDialog();
            try
            {
                if (rs == DialogResult.OK)
                {
                    XuLyDataGridView.LoadData(grwDanhSach, openFileDialog.FileName);
                    grwDanhSach.Columns[2].Width=300;
                    btnChonNhom.Enabled = btnXuatExcel.Enabled = btnInKetQua.Enabled = true;
                    btnChonPhong.Enabled = btnXepLichThi.Enabled = false;
                    ShowStatus("Load Dữ Liệu Thành Công");
                }
            }
            catch (Exception)
            {
                BatLoi.ThongBao2("Lỗi! Không thể load dữ liệu từ file đã chọn");
            }
        }
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (grwDanhSach.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu để lưu");
                    return;
                }
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                saveFileName = saveFileDialog.FileName;
                DataColumn c = ((DataTable)grwDanhSach.DataSource).Columns["NHÓM"];
                ((DataTable)grwDanhSach.DataSource).Columns.Remove(c);
                Excel.Export((DataTable)grwDanhSach.DataSource, saveFileName, "KẾT QUẢ XẾP LỊCH THI");
                ((DataTable)grwDanhSach.DataSource).Columns.Add(c);
                ShowStatus("Lưu Kết Quả Thành Công");
            }
            catch (Exception)
            {
                ShowStatus("Đã xảy ra lỗi... Dữ liệu chưa được lưu");
            }
        }
        private void btnInKetQua_Click(object sender, EventArgs e)
        {
            if (grwDanhSach.Rows.Count > 0)
            {
                Printer pr = new Printer();
                DataGridViewColumn c = grwDanhSach.Columns["NHÓM"];
                grwDanhSach.Columns.Remove(c);
                pr.print(grwDanhSach);
                grwDanhSach.Columns.Add(c);
            }
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            Active_Form(false);
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (btnDangNhap.Text == "Đăng Nhập")
            {
                if (XuLyXml.DaTaoTaiKhoan())
                {
                    frmDangNhap frm = new frmDangNhap();
                    Active_Form((frm.ShowDialog() == DialogResult.OK));
                    btnDangNhap.Text = "Đăng Xuất";
                }
                else
                    TaoTaiKhoan();
            }
            else
            {
                Active_Form(false);
                btnDangNhap.Text = "Đăng Nhập";
                btnXepLichThi.Enabled = btnXuatExcel.Enabled = btnInKetQua.Enabled = grwDanhSach.Visible = false;
            }
        }
        private void btnXepLichThi_Click(object sender, EventArgs e)
        {
            LenLichThi llt = new LenLichThi(DsMonThi(), GioThi.GetAll);
            if (llt.ThucHien())
                XuLyDataGridView.ShowData(llt.DsMonThiDaXep, grwDanhSach, "NGÀY", "GIỜ THI");
        }
        private void btnQlNhom_Click(object sender, EventArgs e)
        {
            frmQlBacHoc ql = new frmQlBacHoc();
            ql.ShowDialog();
        }
        private void btnQlPhong_Click(object sender, EventArgs e)
        {
            frmQlPhongHoc ql = new frmQlPhongHoc();
            ql.ShowDialog();
        }
        private void btnQlGioThi_Click(object sender, EventArgs e)
        {
            frmQlTiet qlt = new frmQlTiet();
            qlt.ShowDialog();
        }
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau();
            frm.ShowDialog();
        }
        void TaoTaiKhoan()
        {
            frmDangKi frm = new frmDangKi();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Active_Form(true);
                btnDangNhap.Text = "Đăng nhập";
            }
            else
                btnDangNhap.Text = "Tạo tài khoản";

        }
        bool DangNhap = false;
        #endregion
        #region DataGridView=======================
        private void grwDanhSach_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                grwDanhSach.CurrentCell = grwDanhSach.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            catch (Exception)
            {
            }
        }
        DanhSachMonThi DsMonThi()
        {
            DanhSachMonThi Dsmt = new DanhSachMonThi(new List<MonThi>());
            foreach (DataGridViewRow r in grwDanhSach.Rows)
                if (Convert.ToString(r.Cells["NHÓM"].Value) != "")
                {

                    MonThi mt = new MonThi(Convert.ToString(r.Cells["MÃ HP"].Value));
                    string phong1 = Convert.ToString(((DataGridViewTextBoxCell)r.Cells["PHÒNG"]).Value);
                    string nhom1 = Convert.ToString(r.Cells["NHÓM"].Value);
                    if (phong1 != "") mt.MonThucHanh = true;
                    foreach (DataGridViewRow r2 in grwDanhSach.Rows)
                    {
                        if (r2.Index != r.Index)
                        {
                            string nhom2 = Convert.ToString(r2.Cells["NHÓM"].Value);
                            string phong2 = Convert.ToString(((DataGridViewTextBoxCell)r2.Cells["PHÒNG"]).Value);
                            if (DuLieu.KiemTraTrung(nhom1, nhom2) || DuLieu.KiemTraTrung(phong1, phong2))
                                mt.ThemMonCungNhom(Convert.ToString(r2.Cells[1].Value));
                        }
                    }
                    Dsmt.ds.Add(mt);
                }
            return Dsmt;
        }
        #endregion
        private void btnChonNhom_Click(object sender, EventArgs e)
        {
            DataTable kq = XuLyDataGridView.GetTable(new string[] { "MÃ HP", "TÊN HỌC PHẦN", "LỚP", "NHÓM" }, (DataTable)grwDanhSach.DataSource);
            frmChonNhom fcn = new frmChonNhom(kq);
            if (fcn.ShowDialog() == DialogResult.OK)
            {
                XuLyDataGridView.ShowData(fcn.dgrDanhSach, grwDanhSach, "NHÓM", "NHÓM");
            }
            btnChonPhong.Enabled = btnXepLichThi.Enabled = true;
        }

        void MonThiMay(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (tb.Rows[i]["HT THI"].ToString().ToLower() != "Thi Máy".ToLower())
                {
                    tb.Rows.RemoveAt(i);
                    i--;
                }
            }
        }

        private void btnChonPhong_Click(object sender, EventArgs e)
        {
            DataTable kq = XuLyDataGridView.GetTable(new string[] { "MÃ HP", "TÊN HỌC PHẦN", "SỐ SV", "HT THI", "PHÒNG" }, (DataTable)grwDanhSach.DataSource);
            MonThiMay(kq);
            frmChonPhong fcp = new frmChonPhong(kq);
            if (fcp.ShowDialog() == DialogResult.OK)
            {
                XuLyDataGridView.ShowData(fcp.dgrDanhSach, grwDanhSach, "PHÒNG", "PHÒNG");
            }
        }

        private void grwDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                DateTime columnValue;
                if (e.Value != null && DateTime.TryParse((e.Value.ToString()), out columnValue))
                    e.Value = DateTime.Parse(e.Value.ToString()).ToShortTimeString();
            }
            if (e.ColumnIndex == 7)
            {
                DateTime columnValue;
                if (e.Value != null && DateTime.TryParse((e.Value.ToString()), out columnValue))
                    e.Value = DateTime.Parse(e.Value.ToString()).ToShortDateString();
            }
        }
        public void frmFormMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized && this.WindowState != FormWindowState.Minimized)
                this.WindowState = FormWindowState.Maximized;
        }
    }
}
