using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class DuLieu
    {
        public static string ThuTrongTuan(DateTime day)
        {
            switch (day.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Thứ Hai";
                case DayOfWeek.Tuesday:
                    return "Thứ Ba";
                case DayOfWeek.Wednesday:
                    return "Thứ Tư";
                case DayOfWeek.Thursday:
                    return "Thứ Năm";
                case DayOfWeek.Friday:
                    return "Thứ Sáu";
                case DayOfWeek.Saturday:
                    return "Thứ Bảy";
                case DayOfWeek.Sunday:
                    return "Chủ Nhật";
                default:
                    return "";
            }
        }
        public static string GetDay()
        {
            string s = "";
            DateTime day = DateTime.Now;
            s = "Hôm nay " + ThuTrongTuan(day) + ", ngày " + day.Day + " tháng " + day.Month + " năm " + day.Year;
            return s;
        }
        public static bool KiemTraTrung(string a, string b)
        {
            if (a == "" || b == "")
                return false;
            List<string> a1 = new List<string>(a.Split(';'));
            List<string> b2 = new List<string>(b.Split(';'));
            foreach (string st in a1)
                if (b2.Contains(st))
                    return true;
            return false;
        }
    }
}
