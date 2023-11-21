using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiemTapHoa.Models;
using TiemTapHoa_WebNangCao.Models;

namespace TiemTapHoa.Controllers
{
    public class ChucVuController : Controller
    {
        DBTiemTapHoaEntities db = new DBTiemTapHoaEntities();
        // GET: ChucVu
        public ActionResult Index(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var nccLst = db.ChucVus.Where(ncc => ncc.TenCV.ToLower().Contains(searchString.ToLower())).ToList();
                return View(nccLst);
            }
            Session["page"] = "ChucVu";
            return View(db.ChucVus.ToList());
        }

        public ActionResult Create()
        {
            ChucVu cv = new ChucVu();
            return View(cv);
        }
        [HttpPost]
        public ActionResult Create(ChucVu cv)
        {
            db.ChucVus.Add(cv);
            db.SaveChanges();
            return RedirectToAction("Index", "ChucVu");
        }

        public ActionResult Edit(int id)
        {
            ChucVu editCV = db.ChucVus.Find(id);
            return View(editCV);
        }

        [HttpPost]
        public ActionResult Edit(ChucVu chucvu)
        {
            ChucVu editCV = db.ChucVus.Find(chucvu.MaCV);
            editCV.TenCV = chucvu.TenCV;
            editCV.LuongCV = chucvu.LuongCV;
            db.SaveChanges();
            return RedirectToAction("Index", "ChucVu");
        }

        public ActionResult Delete(int id)
        {
            ChucVu chucvu = db.ChucVus.Find(id);
            db.ChucVus.Remove(chucvu);
            db.SaveChanges();
            return RedirectToAction("Index", "ChucVu");
        }
    }
}