using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TiemTapHoa.Models;
using TiemTapHoa_WebNangCao.Models;

namespace TiemTapHoa_WebNangCao.Controllers
{
    public class PhieuNhapController : Controller
    {
        // GET: PhieuNhap

        DBTiemTapHoaEntities db = new DBTiemTapHoaEntities();
        public ActionResult Index(string filter)
        {
            Session["page"] = "PhieuNhap";
            var phieuNhap = from NCC in db.NhaCungCaps
                            join pn in db.PhieuNhaps
                            on NCC.MaNCC equals pn.MaNCC
                            select new PhieuNhapView
                            {
                                MaPhieu = pn.MaPhieu,
                                NhaCungCap = NCC.MaNCC,
                                TenNCC = NCC.TenNCC,
                                NgayTao = pn.NgayTao,
                                ThanhTien = pn.ThanhTien,
                            };
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.ncc = filter;
                if (filter == "0") { return View(phieuNhap); }
                var pnLst = phieuNhap.Where(pn => pn.NhaCungCap.ToString().ToLower().Contains(filter.ToLower()));
                return View(pnLst);
            }
            return View(phieuNhap);
        }

        public ActionResult Details(int id)
        {
            var ctphieuNhap = from ctpn in db.ChiTietPhieuNhaps
                              join pn in db.PhieuNhaps
                              on ctpn.MaPhieu equals pn.MaPhieu
                              join hh in db.HangHoas
                              on ctpn.MaHH equals hh.MaHH
                              join ncc in db.NhaCungCaps
                              on pn.MaNCC equals ncc.MaNCC
                              select new ChiTietPhieuNhapView
                              {
                                  MaPhieu = pn.MaPhieu,
                                  HangHoa = hh.MaHH,
                                  TenHH = hh.TenHH,
                                  NhaCungCap = ncc.MaNCC,
                                  TenNCC = ncc.TenNCC,
                                  NgayTao = pn.NgayTao,
                                  SoLuong = ctpn.SoLuong,
                                  DonGia = ctpn.DonGia,
                                  ThanhTien = pn.ThanhTien,
                              };
            ChiTietPhieuNhapView ctpn1 = ctphieuNhap.FirstOrDefault(m => m.MaPhieu.Equals(id));
            //var result = ctphieuNhap.ToList();
            return View(ctpn1);
        }

        public ActionResult Create()
        {
            ChiTietPhieuNhapView ctpn = new ChiTietPhieuNhapView();
            return View(ctpn);
        }

        [HttpPost]
        public ActionResult Create(ChiTietPhieuNhapView ctpn, PhieuNhapView pn)
        {

            ChiTietPhieuNhap addCTPN = new ChiTietPhieuNhap(ctpn.MaPhieu, ctpn.HangHoa, ctpn.SoLuong, ctpn.DonGia);
            db.ChiTietPhieuNhaps.Add(addCTPN);
            pn.ThanhTien = ctpn.SoLuong * ctpn.DonGia;
            PhieuNhap addPN = new PhieuNhap(pn.MaPhieu, pn.NhaCungCap, pn.NgayTao, pn.ThanhTien);
            db.PhieuNhaps.Add(addPN);
            int rowAffected = db.SaveChanges();
            if (rowAffected > 0)
            {
                ViewBag.addSuccessNV = "Thêm thành công";
            }
            else
            {
                ViewBag.addErrorNV = "Thêm thất bại, vui lòng kiểm tra lại thông tin";
            }
            return View(ctpn);
        }
    }
}