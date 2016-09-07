using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XepLichThi
{
    public partial class frmLoadData : Form
    {
        public frmLoadData()
        {
            InitializeComponent();
        }

        private void frmLoadData_Load(object sender, EventArgs e)
        {
            this.Hide();
            frmFormMain frmmain = new frmFormMain();
            frmmain.ShowDialog();
            Application.Exit();
        }
    }
}
