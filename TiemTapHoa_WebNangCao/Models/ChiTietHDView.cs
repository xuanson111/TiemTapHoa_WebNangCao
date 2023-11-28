using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace TiemTapHoa_WebNangCao.Models
{
    public class ChiTietHDView
    {
        public int soThuTu {  get; set; }
        public int maHH { get; set; }
        public int TenHH { get; set; }
        public int soLuong { get; set; }
        public Nullable<double> donGia { get; set; }

    }
}