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
    public partial class frmSelectNhom : Form
    {
        public frmSelectNhom()
        {
            InitializeComponent();
        }
        string nhom;
        public  frmSelectNhom(string text)
        {
            InitializeComponent();
            nhom = text;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmQlBacHoc fbh = new frmQlBacHoc();
            fbh.ShowDialog();

        }

        public string[] Result()
        {
            string[] kq = new string[clbDsNhom.CheckedItems.Count];
            for (int i = 0; i < clbDsNhom.CheckedItems.Count; i++)
            {
                string s = clbDsNhom.CheckedItems[i].ToString();
                kq[i] = s;
            }
            return kq;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void SetData(string Text)
        {
            List<string> s = new List<string>(Text.Split(';'));
            for (int i = 0; i < clbDsNhom.Items.Count; i++)
                if (s.Contains(clbDsNhom.Items[i].ToString()))
                    clbDsNhom.SetItemChecked(i, true);
        }


        private void frmSelectNhom_Load(object sender, EventArgs e)
        {
            clbDsNhom.Items.Clear();
            List<string> DsNhom = XuLyXml.DocDsBacHoc();
            foreach (string st in DsNhom)
            {
                clbDsNhom.Items.Add(st);
            }
            SetData(nhom);
        }

    }
}
