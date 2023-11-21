using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiemTapHoa.Models;
using TiemTapHoa_WebNangCao.Models;

namespace TiemTapHoa.Controllers
{
    public class NhaCungCapController : Controller
    {
        DBTiemTapHoaEntities db = new DBTiemTapHoaEntities();

        // GET: NhaCungCap
        public ActionResult Index(string searchString, string filter)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var nccLst = db.NhaCungCaps.Where(ncc => ncc.TenNCC.ToLower().Contains(searchString.ToLower())).ToList();
                return View(nccLst);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.filterNCC = filter;
                if (filter == "Tất cả")
                {
                    return View(db.NhaCungCaps.ToList());
                }
                var nccLst = db.NhaCungCaps.Where(ncc => ncc.DiaChi.ToLower().Contains(filter.ToLower())).ToList();
                return View(nccLst);
            }
            Session["page"] = "NhaCungCap";
            return View(db.NhaCungCaps.ToList());
        }

        public ActionResult Create()
        {
            NhaCungCap ncc = new NhaCungCap();
            return View(ncc);
        }

        [HttpPost]
        public ActionResult Create(NhaCungCap ncc)
        {
            db.NhaCungCaps.Add(ncc);
            int rowsAffected = db.SaveChanges();
            if (rowsAffected > 0)
            {
                ViewBag.successAddNCC = "Thêm thành công";
            }
            else
            {
                ViewBag.errorAddNCC = "Không thể thêm mới nhà cung cấp, vui lòng kiểm tra lại thông tin vừa nhập!";
            }
            return View(ncc);
        }

        public ActionResult Edit(int id)
        {
            NhaCungCap ncc = db.NhaCungCaps.Find(id);
            return View(ncc);
        }

        [HttpPost]
        public ActionResult Edit(NhaCungCap ncc)
        {
            NhaCungCap editNCC = db.NhaCungCaps.Find(ncc.MaNCC);
            editNCC.MaNCC = ncc.MaNCC;
            editNCC.TenNCC = ncc.TenNCC;
            editNCC.SDT = ncc.SDT;
            editNCC.SoTaiKhoan = ncc.SoTaiKhoan;
            editNCC.Email = ncc.Email;
            editNCC.DiaChi = ncc.DiaChi;
            int rowsAffected = db.SaveChanges();
            if (rowsAffected > 0)
            {
                ViewBag.successEditNCC = "Sửa thành công";
            }
            else
            {
                ViewBag.errorEditNCC = "Sửa thất bại, vui lòng kiểm tra lại thông tin vừa nhập!";
            }
            return View(ncc);
        }

        public ActionResult Delete(int id)
        {
            NhaCungCap ncc = db.NhaCungCaps.Find(id);
            db.NhaCungCaps.Remove(ncc);
            db.SaveChanges();
            return RedirectToAction("Index", "NhaCungCap");
        }
    }
}