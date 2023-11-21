using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiemTapHoa.Models;
using TiemTapHoa_WebNangCao.Models;

namespace TiemTapHoa.Controllers
{
    public class HangHoaController : Controller
    {
        DBTiemTapHoaEntities db = new DBTiemTapHoaEntities();
        // GET: HangHoa
        public ActionResult Index()
        {
            Session["page"] = "HangHoa";
            return View(db.HangHoas.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HangHoa hangHoa)
        {
            db.HangHoas.Add(hangHoa);
            db.SaveChanges();
            return View();
        }

        public ActionResult Edit(int id)
        {
            HangHoa hh = db.HangHoas.Find(id);
            return View(hh);
        }
        [HttpPost]
        public ActionResult Edit(HangHoa hh)
        {
            HangHoa editHH = db.HangHoas.Find(hh.MaHH);
            editHH.TenHH = hh.TenHH;
            editHH.DonGia = hh.DonGia;
            editHH.SoLuong = hh.SoLuong;
            db.SaveChanges();
            return View();
        }


        public ActionResult Delete(int? id)
        {
            HangHoa hh = db.HangHoas.Find(id);
            return View(hh);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            HangHoa HH = db.HangHoas.Find(id);
            db.HangHoas.Remove(HH);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}