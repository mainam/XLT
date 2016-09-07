using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class Phong
    {
        public string TenPhong;
        public int SoCho;
        public Phong(string ten, int sl)
        {
            TenPhong = ten;
            SoCho = sl;
        }
        public static List<Phong> GetAll
        {
            get
            {
                return XuLyXml.DocDsPhong();
            }
        }


    }
}
