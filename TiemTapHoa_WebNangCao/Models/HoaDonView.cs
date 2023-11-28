using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiemTapHoa_WebNangCao.Models
{
    public class HoaDonView
    {
        public int MaHD {  get; set; }
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public Nullable<double> TongTien { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
    }
}