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
    public partial class frmDangKi : Form
    {
        public frmDangKi()
        {
            InitializeComponent();
        }
        public bool active = false;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtConfirmPass.ResetText();
            this.txtMatKhau.ResetText();
            this.txtTenDangNhap.ResetText();
            this.txtTenDangNhap.Focus();
        }

        private void btnTaoTK_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text.Length == 0 || txtMatKhau.Text.Length == 0)
            {
                MessageBox.Show("Chưa nhập tên đăng nhập hoặc mật khẩu","Lỗi tạo tài khoản",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnReset.PerformClick();
                return;
            }
            if (txtMatKhau.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Mật khẩu không giống nhau, vui lòng nhập lại","Lỗi tạo tài khoản",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnReset.PerformClick();
                return;
            }
            try
            {
                XuLyXml.LuuTaiKhoan(txtTenDangNhap.Text.ToLower(),MaHoaMatKhau.MaHoa(txtMatKhau.Text));
                MessageBox.Show("Đã tạo thành công tài khoản","Tạo tài khoản",MessageBoxButtons.OK,MessageBoxIcon.Information);
                active = true;
                this.Close();
            }
            catch
            {
            }
        }
    }
}
