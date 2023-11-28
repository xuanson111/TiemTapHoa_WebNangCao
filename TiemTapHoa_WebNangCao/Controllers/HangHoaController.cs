using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiemTapHoa.Models;
using TiemTapHoa_WebNangCao.Models;
using WebGrease.Css.Ast.Selectors;

namespace TiemTapHoa.Controllers
{
    public class HangHoaController : Controller
    {
        DBTiemTapHoaEntities db = new DBTiemTapHoaEntities();
        // GET: HangHoa
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
            Session["page"] = "HangHoa";
            return View(db.HangHoas.ToList());
        }

        public ActionResult Create()
        {
            HangHoa hh = new HangHoa();
            return View(hh);
        }
        [HttpPost]
        public ActionResult Create(HangHoa hangHoa, HttpPostedFileBase FileHinhAnh)
        {
            HandleImg(hangHoa, FileHinhAnh);
            db.HangHoas.Add(hangHoa);
            int rowAffected = db.SaveChanges();
            if (rowAffected > 0)
            {
                ViewBag.addSuccessHH = "Thêm thành công";
            }
            else
            {
                ViewBag.addErrorHH = "Thêm thất bại, vui lòng kiểm tra lại thông tin";
            }
            return View(hangHoa);
        }

        public ActionResult Edit(int id)
        {
            HangHoa hh = db.HangHoas.Find(id);
            return View(hh);
        }
        [HttpPost]
        public ActionResult Edit(HangHoa hh, HttpPostedFileBase FileHinhAnh)
        {
            HangHoa editHH = db.HangHoas.Find(hh.MaHH);
            editHH.TenHH = hh.TenHH;
            editHH.DonGia = hh.DonGia;
            editHH.SoLuong = hh.SoLuong;
            editHH.LoaiHangHoa = hh.LoaiHangHoa;
            editHH.DonVi = hh.DonVi;
            // kiểm tra xem người dùng có muốn chỉnh sửa ảnh không
            if (FileHinhAnh != null && FileHinhAnh.ContentLength > 0)
            {
                // Kiểm tra xem đường dẫn hình ảnh cũ có hợp lệ không
                if (!string.IsNullOrEmpty(editHH.HinhAnh) && editHH.HinhAnh.StartsWith("/assets/img/"))
                {
                    // Xóa hình ảnh cũ nếu nó tồn tại
                    string duongDanTepTin = Server.MapPath("~" + editHH.HinhAnh);
                    if (System.IO.File.Exists(duongDanTepTin))
                    {
                        System.IO.File.Delete(duongDanTepTin);
                    }
                }
                // thực hiện lưu hình ảnh mới vào file /assets/Img
                HandleImg(editHH, FileHinhAnh);
            }
            int rowAffected = db.SaveChanges();
            if (rowAffected > 0)
            {
                ViewBag.editSuccessHH = "Sửa thành công";
            }
            else
            {
                ViewBag.editErrorHH = "Sửa thất bại, vui lòng kiểm tra lại thông tin";
            }
            return View(editHH);
        }
        
        public ActionResult Delete(int id)
        {
            HangHoa HH = db.HangHoas.Find(id);
            if (HH.HinhAnh != null)
            {
                string duongDanTepTin = Path.Combine(Server.MapPath("~/assets/img/"), HH.HinhAnh.Substring(12));
                if (System.IO.File.Exists(duongDanTepTin))
                {
                    // Thực hiện xóa tệp tin
                    System.IO.File.Delete(duongDanTepTin);
                }

            }
            db.HangHoas.Remove(HH);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void HandleImg(HangHoa hh, HttpPostedFileBase FileHinhAnh)
        {
            // Xử lý hình ảnh
            if (FileHinhAnh != null && FileHinhAnh.ContentLength > 0)
            {
                // Xử lý tệp tin được tải lên, ví dụ lưu vào thư mục và cập nhật đường dẫn trong model
                string folderPath = Server.MapPath("~/assets/Img");
                // Lấy tên tập tin (không bao gồm .jpg  .png)
                string fileName = Path.GetFileNameWithoutExtension(FileHinhAnh.FileName);
                // lấy đuôi của tập tin (vd: .jpg)
                string extension = Path.GetExtension(FileHinhAnh.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string fullPath = Path.Combine(folderPath, fileName);
                FileHinhAnh.SaveAs(fullPath);

                // Lưu đường dẫn vào model, ví dụ:
                hh.HinhAnh = "/assets/img/" + fileName;
            }
        }
        
    }
}