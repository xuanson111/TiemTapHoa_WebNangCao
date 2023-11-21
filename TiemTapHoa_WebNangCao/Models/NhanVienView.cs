using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiemTapHoa.Models
{
    public class NhanVienView
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string SoTaiKhoan { get; set; }
        public Nullable<int> ChucVu { get; set; }
        public string TenChucVu { get; set; }
        public Nullable<double> LuongCV { get; set; }
        public string GioiTinh { get; set; }
        public string UrlHinhAnh { get; set; }
        public HttpPostedFileBase FileHinhAnh { get; set; }
    }
}