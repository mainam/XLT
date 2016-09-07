using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class MonThi
    {        
        string mamh;
        public bool MonThucHanh;
        public string Mamh
        {
            get { return mamh; }
            set { mamh = value; }
        }
        public List<string> DSMonCungNhom = new List<string>();
        public  List<int> DSTietDaTo = new List<int>();
        public GioThi Tiet;
        public string Phong;
        public MonThi(string mamh)
        {
            Mamh = mamh;
        }
        public void ThemMauCam(int mau)
        {
            DSTietDaTo.Add(mau);
        }
        public void SetGio(GioThi gt)
        {
            Tiet = gt;
        }
        public void ThemMonCungNhom(string MaMH)
        {
            DSMonCungNhom.Add(MaMH);
        }
    }

}
