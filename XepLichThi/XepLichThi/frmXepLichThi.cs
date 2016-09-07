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
    public partial class frmXepLichThi : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public frmXepLichThi()
        {
            InitializeComponent();
        }
        string saveFileName = "";

        private void frmXepLichThi_Load(object sender, EventArgs e)
        {
            rtMain.Select();
            cbbStyle.SelectedIndex = 0;
            cbbNgonNgu.SelectedIndex = 0;
            lblTime.Text = DuLieu.GetDay();
            if (XuLyXml.DaTaoTaiKhoan())
                Active_Form(true);
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
        private void cbbStyle_ComboBoxTextChanged(object sender, EventArgs e)
        {
            switch (cbbStyle.SelectedItem.ToString())
            {
                case "Office2007Blue":
                    styleManager1.ManagerStyle = eStyle.Office2007Blue;
                    break;
                case "Office2007Silver":
                    styleManager1.ManagerStyle = eStyle.Office2007Silver;
                    break;
                case "Office2007Black":
                    styleManager1.ManagerStyle = eStyle.Office2007Black;
                    break;
                case "Office2007VistaGlass":
                    styleManager1.ManagerStyle = eStyle.Office2007VistaGlass;
                    break;
                case "Office2010Silver":
                    styleManager1.ManagerStyle = eStyle.Office2010Silver;
                    break;
                case "Office2010Blue":
                    styleManager1.ManagerStyle = eStyle.Office2010Blue;
                    break;
                case "Office2010Black":
                    styleManager1.ManagerStyle = eStyle.Office2010Black;
                    break;
                case "Windows7Blue":
                    styleManager1.ManagerStyle = eStyle.Windows7Blue;
                    break;
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
                    grwDanhSach.Columns.Clear();
                    DataTable dt = Excel.Import(openFileDialog.FileName);
                    grwDanhSach.DataSource = dt;
                    grwDanhSach.Visible = true;
                    DataGridViewComboBoxColumn objCol = XuLyDataGridView.CreateComboboxColumn("BẬC HỌC", XuLyXml.DocDsBacHoc());
                    grwDanhSach.Columns.Insert(3, objCol);
                    DataGridViewCheckBoxColumn objChe = XuLyDataGridView.CreateCheckBoxColumn("THI PHÒNG MÁY");
                    grwDanhSach.Columns.Insert(4, objChe);
                    grwDanhSach.Columns.RemoveAt(11);
                    DataGridViewTextBoxColumn objtex = XuLyDataGridView.CreateTextboxColumn("PHÒNG THI");
                    grwDanhSach.Columns.Insert(11, objtex);
                    DataGridViewButtonColumn objbot = XuLyDataGridView.CreateButtonColumn("THÊM");
                    grwDanhSach.Columns.Insert(12, objbot);
                    FormatColumn();
                    btnXepLichThi.Enabled = btnXuatExcel.Enabled = btnInKetQua.Enabled = true;
                }
            }
            catch (Exception)
            {

            }
        }
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            DataTable tb = GetTable();
            if (!grwDanhSach.Visible)
            {
                MessageBox.Show("Chưa có dữ liệu để lưu");
                return;
            }
            DialogResult rs = saveFileDialog.ShowDialog();
            if (rs != DialogResult.OK)
                return;
            saveFileName = saveFileDialog.FileName;
            DataTable dt = (DataTable)grwDanhSach.DataSource;
            Excel.Export(dt, saveFileName, "KẾT QUẢ XẾP LỊCH THI");
        }
        private void btnInKetQua_Click(object sender, EventArgs e)
        {
            if (grwDanhSach.Rows.Count > 0)
            {
                Printer pr = new Printer();
                pr.print(grwDanhSach);
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
            LenLichThi llt = new LenLichThi(DsMonThi(), GioThi.GetAll, Phong.GetAll.Count);
            if (llt.ThucHien())
                ShowData(llt.DsMonThi);
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
        void FormatColumn()
        {
            grwDanhSach.Columns[0].Width = 40;
            grwDanhSach.Columns[1].Width = 60;
            grwDanhSach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grwDanhSach.Columns[3].Width = 100;
            grwDanhSach.Columns[4].Width = 100;
            grwDanhSach.Columns[5].Width = 70;
            grwDanhSach.Columns[6].Width = 70;
            grwDanhSach.Columns[7].Width = 70;
            grwDanhSach.Columns[8].Width = 80;
            grwDanhSach.Columns[9].Width = 100;
            grwDanhSach.Columns[10].Width = 90;
            grwDanhSach.Columns[11].Width = 100;
            grwDanhSach.Columns[12].Width = 50;
        }
        private void dgwDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 9)
                {
                    DateTime columnValue;
                    if (e.Value != null && DateTime.TryParse((e.Value.ToString()), out columnValue))
                        e.Value = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                }
                if (e.ColumnIndex == 10)
                {
                    DateTime columnValue;
                    if (e.Value != null && DateTime.TryParse((e.Value.ToString()), out columnValue))
                        e.Value = DateTime.Parse(e.Value.ToString()).ToShortTimeString();
                }
            }
            catch (Exception)
            {
            }
        }
        DataTable GetTable()
        {
            DataTable kq = new DataTable();
            kq.Columns.Add("1");
            kq.Columns.Add("2");
            kq.Columns.Add("3");
            kq.Columns.Add("4");
            kq.Columns.Add("5");
            kq.Columns.Add("6");
            kq.Columns.Add("7");
            kq.Columns.Add("8");
            kq.Columns.Add("9");
            kq.Columns.Add("10");
            kq.Columns.Add("11");
            kq.Columns.Add("12");
            kq.Columns.Add("13");
            kq.Columns.Add("14");
            foreach (DataGridViewRow r in grwDanhSach.Rows)
            {
                kq.Rows.Add(r.Cells[0].Value, r.Cells[1].Value, r.Cells[2].Value, r.Cells[3].Value, r.Cells[4].Value);
            }
            return kq;
        }
        DataGridViewRow GetRow(string MaHP)
        {
            foreach (DataGridViewRow r in grwDanhSach.Rows)
                if (Convert.ToString(r.Cells[1].Value) == MaHP)
                    return r;
            return null;
        }
        void ShowData(DanhSachMonThi dsmt)
        {
            foreach (MonThi mt in dsmt.ds)
            {
                GetRow(mt.Mamh).Cells[9].Value = mt.Tiet.Ngay;
                GetRow(mt.Mamh).Cells[10].Value = mt.Tiet.Gio;
            }
        }
        private void grwDanhSach_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                grwDanhSach.CurrentCell = grwDanhSach.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.ColumnIndex == 11)
                {
                    DataGridViewComboBoxCell c2 = (DataGridViewComboBoxCell)grwDanhSach.Rows[e.RowIndex].Cells[11];
                    if (c2.ReadOnly)
                        grwDanhSach.CurrentCell.ToolTipText = "Không thể chọn";
                    else
                        grwDanhSach.CurrentCell.ToolTipText = "Nhấn để chọn phòng thi";
                }
            }
            catch (Exception)
            {
            }
        }
        private void grwDanhSach_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                bool c = (bool)grwDanhSach.CurrentCell.Value;
                DataGridViewComboBoxCell c2 = (DataGridViewComboBoxCell)grwDanhSach.Rows[e.RowIndex].Cells[12];
                grwDanhSach.Rows[e.RowIndex].Cells[11].Value = "";
                if (c)
                    c2.ReadOnly = false;
                else
                    c2.ReadOnly = true;
            }
        }
        private void grwDanhSach_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                frmSelectPhong fsp = new frmSelectPhong();
                fsp.StartPosition = FormStartPosition.CenterParent;
                if (fsp.ShowDialog() == DialogResult.OK)
                {
                    grwDanhSach.CurrentRow.Cells[11].Value = string.Join(";", fsp.Result());
                }

            }
        }
        DanhSachMonThi DsMonThi()
        {
            DanhSachMonThi Dsmt = new DanhSachMonThi(new List<MonThi>());
            foreach (DataGridViewRow r in grwDanhSach.Rows)
                if (Convert.ToString(r.Cells[3].Value) != "")
                {
                    MonThi mt = new MonThi(Convert.ToString(r.Cells[1].Value));
                    foreach (DataGridViewRow r2 in grwDanhSach.Rows)
                    {
                        if (r2.Index != r.Index)
                        {
                            string bac1 = Convert.ToString(r.Cells[3].Value);
                            string bac2 = Convert.ToString(r2.Cells[3].Value);
                            bool thimay1 = Convert.ToBoolean(((DataGridViewCheckBoxCell)r.Cells[4]).Value);
                            bool thimay2 = Convert.ToBoolean(((DataGridViewCheckBoxCell)r2.Cells[4]).Value);
                            string phong1 = Convert.ToString(((DataGridViewComboBoxCell)r.Cells[11]).Value);
                            string phong2 = Convert.ToString(((DataGridViewComboBoxCell)r2.Cells[11]).Value);
                            if (bac2 != "")
                            {
                                if (bac1 == bac2 || (thimay1 && thimay2 &&
                                Phong.TrungPhong(phong1, phong2)))
                                    mt.ThemMonCungNhom(Convert.ToString(r2.Cells[1].Value));
                            }
                        }
                    }
                    Dsmt.ds.Add(mt);
                }
            return Dsmt;
        }


        #endregion

        private void lblTime_Click(object sender, EventArgs e)
        {

        }



    }
}
