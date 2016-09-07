using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class DanhSachMonThi
    {
        public List<MonThi> ds = new List<MonThi>();
        public DanhSachMonThi(List<MonThi> mt)
        {
            ds = mt;
        }
        public DanhSachMonThi() { }
        public MonThi this[string MaHP]
        {
            get
            {
                return ds.Find(delegate(MonThi mt)
                {
                    return mt.Mamh == MaHP;
                });
            }
        }
        public MonThi this[int index]
        {
            get
            {
                return ds[index];
            }
        }
        public void SetGio(MonThi mt, GioThi value)
        {
            mt.Tiet = value;
        }
        public int Length
        {
            get
            {
                return ds.Count;
            }
        }
        public void Add(MonThi mt)
        {
            ds.Add(mt);
        }
        public void Remove(MonThi mt)
        {
            ds.Remove(mt);
        }
        public void SortGiam()
        {
            ds.Sort(delegate(MonThi mt1, MonThi mt2)
            {
                return -mt1.DSMonCungNhom.Count.CompareTo(mt2.DSMonCungNhom.Count);
            });
        }
        public int SoLuongMau
        {
            get
            {
                int sl = 0;
                foreach (MonThi mt in ds)
                    if (sl < mt.DSMonCungNhom.Count)
                        sl = mt.DSMonCungNhom.Count;
                return sl;
            }
        }
        public void ThemTietDaTo(MonThi mthi, int Tiet)
        {

            foreach (string mhp in mthi.DSMonCungNhom)
                try
                {
                    this[mhp].ThemMauCam(Tiet);

                }
                catch (Exception)
                {
                }
        }

    }
}
