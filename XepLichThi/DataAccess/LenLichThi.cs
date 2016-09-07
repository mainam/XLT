using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DataAccess
{
    public class LenLichThi
    {
        private DanhSachMonThi DsMonThi;
        public DanhSachMonThi DsMonThiDaXep = new DanhSachMonThi();
        List<GioThi> DSGioThi;
        public LenLichThi(DanhSachMonThi dsmt, List<GioThi> dst)
        {
            DsMonThi = dsmt;
            DSGioThi = dst;
        }
        public LenLichThi() { }
        Thread TienTrinh;
        frmProcess frm;
        void ShowProcess()
        {
            frm = new frmProcess();
            frm.ShowDialog();
        }
        public bool DuocPhepTo(MonThi mt, int tiet)
        {
            string s = DSGioThi[tiet].Gio.Trim();
            if (mt.MonThucHanh)
            {
                if (s == "9:00" || s == "15:00")
                    return false;
            }
            return true;
        }

        private bool GioDaDung(MonThi mThi, int tiet)
        {
            return mThi.DSTietDaTo.Contains(tiet);
        }
        void UpdateDs(MonThi mt)
        {
            DsMonThi.ThemTietDaTo(mt, DSGioThi.IndexOf(mt.Tiet));
            DsMonThi.Remove(mt);
            DsMonThiDaXep.Add(mt);
        }
        public bool ThucHien()
        {
            if (DsMonThi.SoLuongMau > DSGioThi.Count)
                return BatLoi.ThongBao2("Số lượng giờ thi sử dụng không đủ để xếp lịch. Vui lòng kiểm tra lại");
            try
            {
                TienTrinh = new Thread(ShowProcess);
                TienTrinh.Start();
                int TietXep = 0;
                while (DsMonThi.Length > 0)
                {
                    DsMonThi.SortGiam();
                    if (DuocPhepTo(DsMonThi[0], TietXep))
                    {
                        DsMonThi.SetGio(DsMonThi[0], DSGioThi[TietXep]);
                        UpdateDs(DsMonThi[0]);
                    }
                    for (int i = 0; i < DsMonThi.Length; i++)
                    {
                        MonThi mt2 = DsMonThi[i];
                        if (!GioDaDung(DsMonThi[i], TietXep) && DuocPhepTo(DsMonThi[i], TietXep))
                        {
                            DsMonThi.SetGio(mt2, DSGioThi[TietXep]);
                            UpdateDs(mt2);
                            i--;
                        }
                    }
                    TietXep++;
                }

            }
            catch (Exception)
            {
                TienTrinh.Abort();
                return false;
            }
            TienTrinh.Abort();
            return true;
        }
    }
}
