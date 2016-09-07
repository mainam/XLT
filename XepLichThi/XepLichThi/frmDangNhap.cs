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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        int dem = 0;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtTenDangNhap.ResetText();
            this.txtMatKhau.ResetText();
            this.txtTenDangNhap.Focus();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = XuLyXml.GetTenDangNhap();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (XuLyXml.DocTaiKhoan(txtTenDangNhap.Text.ToLower(), MaHoaMatKhau.MaHoa(txtMatKhau.Text)))
                this.Close();
            else
            {
                dem++;
                if (dem > 5)
                {
                    MessageBox.Show("Đăng nhập sai quá 5 lần, không thể tiếp tục chương trình", "Đăng nhập không thành công");
                    Application.Exit();
                }
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai, vui lòng nhập lại", "Đăng nhập không thành công");
                btnReset.PerformClick();
            }
        }
        private void frmDangNhap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(null, null);
        }
    }
}
