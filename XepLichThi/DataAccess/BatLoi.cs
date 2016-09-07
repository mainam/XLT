using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataAccess
{
    public class BatLoi
    {
        public static bool ThongBao2(string Text)
        {
            MessageBox.Show(Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        public static bool DgResul(string Text)
        {
            return MessageBox.Show(Text, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)==DialogResult.OK;
        }


        public static bool TextNull(string Text, string Mes)
        {
            if (Text.Trim() == "")
                return ThongBao2(Mes);
            return true;
        }
        public static bool SoLuong(string Text, string Mes)
        {
            try
            {
                int i = int.Parse(Text);
            }
            catch (Exception)
            {
                return ThongBao2(Mes);
            }
            return true;
        }
    }
}
