using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataAccess;
using System.Security.Cryptography;

namespace XepLichThi
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = XuLyXml.GetTenDangNhap();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtMatKhauCu.ResetText();
            this.txtMauKhauMoi.ResetText();
            this.txtConfirmPass.ResetText();
            txtMatKhauCu.Focus();
        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {
            if(!XuLyXml.DocTaiKhoan(txtTenDangNhap.Text,MaHoaMatKhau.MaHoa(txtMatKhauCu.Text)))
            {
                MessageBox.Show("Mật khẩu cũ không đúng vui lòng nhập lại", "Sai mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReset.PerformClick();
                return;
            }
            if (txtMauKhauMoi.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Mật khẩu không giống nhau, vui lòng nhập lại","Sai mật khẩu",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnReset.PerformClick();
                return;
            }
            try
            {
                XuLyXml.LuuTaiKhoan(txtTenDangNhap.Text.ToLower(), MaHoaMatKhau.MaHoa(txtMauKhauMoi.Text));
                MessageBox.Show("Đổi thành công thông tin tài khoản","Đổi mật khẩu",MessageBoxButtons.OK,MessageBoxIcon.Information);
                btnThoat.PerformClick();
            }
            catch
            { MessageBox.Show("Không thể đổi mật khẩu","Lỗi đổi mật khẩu",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }
    }
}
