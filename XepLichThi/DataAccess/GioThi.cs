using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class GioThi
    {
        public GioThi()
        { }
        public string Ngay, Gio;
        public GioThi(string ngay, string gio)
        {
            Ngay = ngay;
            Gio = gio;
        }
        public static List<GioThi> GetAll
        {
            get
            {
                return XuLyXml.DocDsGioThi();
            }
        }

    }
}
