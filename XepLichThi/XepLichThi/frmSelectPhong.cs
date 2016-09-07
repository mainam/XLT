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
    public partial class frmSelectPhong : Form
    {
        public frmSelectPhong(string Text)
        {
            InitializeComponent();
            phong = Text;
        }
        public frmSelectPhong()
        {
            InitializeComponent();
        }
        string phong = "";

        bool Contain(string s, List<string> ar)
        {
            return ar.Contains(s.Substring(0, s.IndexOf("(")));
        }
        void SetData(string Text)
        {
            List<string> s = new List<string>(Text.Split(';'));
            for (int i = 0; i < clbDsPhong.Items.Count; i++)
                if (Contain(clbDsPhong.Items[i].ToString(), s))
                    clbDsPhong.SetItemChecked(i, true);

        }

        private void frmSelectPhong_Load(object sender, EventArgs e)
        {
            clbDsPhong.Items.Clear();
            List<Phong> Dsphong = XuLyXml.DocDsPhong();
            foreach (Phong ph in Dsphong)
            {
                string st = ph.TenPhong + "(" + ph.SoCho + ")";
                clbDsPhong.Items.Add(st);
            }
            SetData(phong);
        }
        public string[] Result()
        {
            string[] kq = new string[clbDsPhong.CheckedItems.Count];
            for (int i = 0; i < clbDsPhong.CheckedItems.Count; i++)
            {
                string s = clbDsPhong.CheckedItems[i].ToString();
                kq[i] = s.Substring(0, s.IndexOf('('));
            }
            return kq;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            frmQlPhongHoc fph = new frmQlPhongHoc();
            fph.ShowDialog();
        }
    }
}
