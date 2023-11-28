using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiemTapHoa_WebNangCao.Models;

namespace TiemTapHoa_WebNangCao.Controllers
{
    public class HoaDonController : Controller
    {
        DBTiemTapHoaEntities db = new DBTiemTapHoaEntities();
        List<ChiTietHDView> lstChiTietHD = null;
        // GET: HoaDon
        public ActionResult Index(string searchString, string filter)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var hangHoaLst = db.HangHoas.Where(nv => nv.TenHH.ToLower().Contains(searchString.ToLower()));
                return View(hangHoaLst);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.loaiHH = filter;
                if (filter == "0") return View(db.HangHoas.ToList());
                var hangHoaLst = db.HangHoas.Where(nv => nv.LoaiHangHoa.ToString().ToLower().Contains(filter.ToLower()));
                return View(hangHoaLst);
            }
            Session["page"] = "HoaDon";
            return View(db.HangHoas.ToList());
        }

        public void addOrderItem(int id)
        {

            
        }
    }
}