using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace DataAccess
{
    public class MaHoaMatKhau
    {
        public static string MaHoa(string mk)
        {
            string mk2MD5 = GetMD5(mk), mk_MaHoa = "";
            mk_MaHoa += mk2MD5.Substring(8, 16) + mk2MD5.Substring(0, 8) + mk2MD5.Substring(24, 8);
            return mk_MaHoa;
        }
        public static string GetMD5(string chuoi)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(chuoi);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                str_md5 += b.ToString("X2");
            }

            return str_md5;
        }
    }
}
