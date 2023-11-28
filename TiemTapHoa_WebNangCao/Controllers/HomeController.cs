using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiemTapHoa.Models;
using TiemTapHoa_WebNangCao.Models;

namespace TiemTapHoa_WebNangCao.Controllers
{
    public class HomeController : Controller
    {
        DBTiemTapHoaEntities db = new DBTiemTapHoaEntities();
        public ActionResult Index()
        {
            var recentBills = (from hd in db.HoaDons
                               join nv in db.NhanViens
                               on hd.MaNV equals nv.MaNV
                               orderby hd.NgayTao descending
                               select new HoaDonView
                               {
                                   MaHD = hd.MaHD,
                                   MaNV = nv.MaNV,
                                   TenNV = nv.TenNV,
                                   TongTien = hd.TongTien,
                                   NgayTao = hd.NgayTao,
                               }).Take(5).ToList();
            Session["page"] = "Home";
            return View(recentBills);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}