using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace DataAccess
{

    public class XuLyXml
    {
        static string FileName = "Data\\Data.xml";
        public static void ThemNode(XmlDocument doc, XmlNode Items, string Name, string value)
        {
            XmlNode Node = doc.CreateNode(XmlNodeType.Element, Name, "");
            Node.InnerText = value;
            Items.AppendChild(Node);
        }
        public static void XoaNode(XmlDocument doc, string NodeName, bool save)
        {
            try
            {
                XmlNodeList nodes = doc.SelectNodes("Data/" + NodeName);
                foreach (XmlNode xno in nodes)
                    xno.ParentNode.RemoveChild(xno);
            }
            catch (Exception)
            {
            }
            if (save)
                LuuFile(doc);
        }
        public static void TaoFile(string FileName)
        {
            XmlTextWriter writer = new XmlTextWriter(FileName, System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("Data");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
        public static XmlDocument MoFile()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                if (!Directory.Exists("data"))
                    Directory.CreateDirectory("Data");
                if (!File.Exists(FileName))
                    TaoFile(FileName);
                doc.Load(FileName);
            }
            catch (Exception)
            {
            }
            return doc;
        }
        public static XmlDocument MoFile(string FileName)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(FileName);
            }
            catch (Exception)
            {
            }
            return doc;
        }
        public static bool LuuFile(XmlDocument doc)
        {
            try
            {
                if (!File.Exists(FileName))
                    TaoFile(FileName);
                doc.Save(FileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<Phong> DocDsPhong()
        {
            List<Phong> kq = new List<Phong>();
            try
            {
                XmlDocument doc = MoFile();
                XmlNodeList Nodes = doc.SelectNodes("Data/PhongHoc/PhongItems");
                foreach (XmlNode xno in Nodes)
                    kq.Add(new Phong(xno.ChildNodes[0].InnerText, Convert.ToInt32(xno.ChildNodes[1].InnerText)));
                return kq;
            }
            catch (Exception)
            {
                return new List<Phong>();
            }
        }
        public static bool DaTaoBacHoc()
        {
            try
            {
                XmlDocument doc = MoFile();
                XmlNodeList Nodes = doc.SelectNodes("Data/BacHoc/BacHocItems");
                if (Nodes.Count > 0)
                    return true;
            }
            catch { }
            return false;
        }




        public static List<GioThi> DocDsGioThi()
        {
            List<GioThi> kq = new List<GioThi>();
            try
            {
                XmlDocument doc = MoFile();
                XmlNodeList Nodes = doc.SelectNodes("Data/GioThi/GioThiItems");
                foreach (XmlNode xno in Nodes)
                    kq.Add(new GioThi(xno.ChildNodes[0].InnerText, xno.ChildNodes[1].InnerText));
                return kq;
            }
            catch (Exception)
            {
                return new List<GioThi>();
            }
        }

        public static List<string> DocDsBacHoc()
        {
            List<string> kq = new List<string>();
            try
            {
                XmlDocument doc = MoFile();
                XmlNodeList Nodes = doc.SelectNodes("Data/BacHoc/BacHocItems");
                foreach (XmlNode xno in Nodes)
                    kq.Add(xno.InnerText);
                return kq;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        public static bool LuuBacHoc(List<string> DanhSachMonHoc, string NodeName, string NodeItems)
        {
            try
            {
                XmlDocument doc = MoFile();
                XoaNode(doc, NodeName, true);
                XmlNode Items = doc.CreateNode(XmlNodeType.Element, NodeName, "");
                foreach (string st in DanhSachMonHoc)
                    ThemNode(doc, Items, NodeItems, st);
                doc.DocumentElement.AppendChild(Items);
                LuuFile(doc);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public static bool LuuPhongHoc(List<Phong> DanhSachPhongHoc)
        {
            try
            {
                XmlDocument doc = MoFile();
                XoaNode(doc, "PhongHoc", true);
                XmlNode Items = doc.CreateNode(XmlNodeType.Element, "PhongHoc", "");
                foreach (Phong ph in DanhSachPhongHoc)
                {
                    XmlNode Items2 = doc.CreateNode(XmlNodeType.Element, "PhongItems", "");
                    ThemNode(doc, Items2, "TenPhong", ph.TenPhong);
                    ThemNode(doc, Items2, "SoCho", ph.SoCho.ToString());
                    Items.AppendChild(Items2);
                }
                doc.DocumentElement.AppendChild(Items);
                LuuFile(doc);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public static bool LuuGioThi(List<GioThi> DanhSachGioThi)
        {
            try
            {
                XmlDocument doc = MoFile();
                XoaNode(doc, "GioThi", true);
                XmlNode Items = doc.CreateNode(XmlNodeType.Element, "GioThi", "");
                foreach (GioThi gt in DanhSachGioThi)
                {
                    XmlNode Items2 = doc.CreateNode(XmlNodeType.Element, "GioThiItems", "");
                    ThemNode(doc, Items2, "Ngay", gt.Ngay);
                    ThemNode(doc, Items2, "Gio", gt.Gio);
                    Items.AppendChild(Items2);
                }
                doc.DocumentElement.AppendChild(Items);
                LuuFile(doc);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static bool DocTaiKhoan(string user, string pass)
        {
            try
            {
                XmlDocument doc = MoFile();
                XmlNode Node = doc.SelectNodes("Data/TaiKhoan")[0];
                if (Node.ChildNodes[0].InnerText == user && Node.ChildNodes[1].InnerText == pass)
                    return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        public static string GetTenDangNhap()
        {
            try
            {
                XmlDocument doc = MoFile();
                XmlNode Node = doc.SelectNodes("Data/TaiKhoan")[0];
                return Node.ChildNodes[0].InnerText;
            }
            catch (Exception)
            {
            }
            return "";
        }
        public static bool DaTaoTaiKhoan()
        {
            try
            {
                XmlDocument doc = MoFile();
                if (doc.SelectNodes("Data/TaiKhoan").Count > 0)
                    return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        public static bool LuuTaiKhoan(string user, string pass)
        {
            try
            {
                XmlDocument doc = MoFile();
                XoaNode(doc, "TaiKhoan", true);
                XmlNode Items = doc.CreateNode(XmlNodeType.Element, "TaiKhoan", "");
                ThemNode(doc, Items, "User", user.ToLower());
                ThemNode(doc, Items, "Pass", pass);
                doc.DocumentElement.AppendChild(Items);
                LuuFile(doc);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
