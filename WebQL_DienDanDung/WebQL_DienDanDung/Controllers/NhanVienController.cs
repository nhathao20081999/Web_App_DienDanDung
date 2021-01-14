using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebQL_DienDanDung.Models;

namespace WebQL_DienDanDung.Controllers
{
    public class NhanVienController : Controller
    {
        DienDanDungDBContextDataContext ddd = new DienDanDungDBContextDataContext();
        // GET: NhanVien
        public ActionResult NhanVien()
        {
            var nv = ddd.tbl_Users.ToList();
            return View(nv);
        }

        public ActionResult ThemNV()
        {
            return RedirectToAction("NhanVien", "NhanVien");
        }

        [HttpPost]
        public ActionResult ThemNV(FormCollection collection, tbl_User nv)
        {
            var tenNV = collection["TenNV"];
            var ngaySinhNV = collection["NgaySinhNV"];
            var gioiTinhNV = Request.Form["GioiTinhNV"];
            var sdtNV = collection["SdtNV"];
            var diachiNV = collection["DiaChiNV"];
            var tinhTrang = Request.Form["TinhTrang"];

            //if (string.IsNullOrEmpty(tenNV))
            //{
            //    ViewData["Them1"] = "Họ Tên Không Được Để Trống!";
            //}
            //else if (string.IsNullOrEmpty(ngaySinhNV))
            //{
            //    ViewData["Them2"] = "Ngày Sinh Không Được Để Trống!";
            //}
            //else if (string.IsNullOrEmpty(sdtNV))
            //{
            //    ViewData["Them3"] = "Số Điện Thoại Không Được Để Trống!";
            //}
            //else if (string.IsNullOrEmpty(diachiNV))
            //{
            //    ViewData["Them4"] = "Địa Chỉ Không Được Để Trống!";
            //}
            nv.UserName = tenNV;
            nv.Birthday = Convert.ToDateTime(ngaySinhNV);
            nv.Gender = Convert.ToBoolean(gioiTinhNV);
            nv.Address = diachiNV;
            nv.PhoneNumber = sdtNV;
            nv.Status = Convert.ToBoolean(tinhTrang);
            ddd.tbl_Users.InsertOnSubmit(nv);
            ddd.SubmitChanges();
            return RedirectToAction("NhanVien", "NhanVien");

        }

        [HttpGet]
        public ActionResult SuaNV(int id_nv)
        {
            var sua_nv = ddd.tbl_Users.First(nv => nv.Id == id_nv);
            return View(sua_nv);
        }

        [HttpPost]
        public ActionResult SuaNV(int id_nv, FormCollection collection)
        {
            var s_manv = ddd.tbl_Users.First(nv => nv.Id == id_nv);
            var s_tennv = collection["tennv"];
            var s_ngaysinh = collection["ngaysinh"];
            var s_gioitinh = Request.Form["gioitinh"];
            var s_sdt = collection["sdt"];
            var s_diachi = collection["diachi"];


            s_manv.Id = id_nv;
            //if (string.IsNullOrEmpty(s_tennv))
            //{
            //    ViewData["SuaNV1"] = "Họ Tên Không Được Để Trống!";
            //}
            //else if (string.IsNullOrEmpty(s_ngaysinh))
            //{
            //    ViewData["SuaNV2"] = "Ngày Sinh Không Được Để Trống!";
            //}
            //else if (string.IsNullOrEmpty(s_sdt))
            //{
            //    ViewData["SuaNV3"] = "Số Điện Thoại Không Được Để Trống!";
            //}
            //else if (string.IsNullOrEmpty(s_diachi))
            //{
            //    ViewData["SuaNV4"] = "Địa Chỉ Không Được Để Trống!";
            //}
            if (s_manv != null)
            {
                s_manv.UserName = s_tennv;
                s_manv.Birthday = Convert.ToDateTime(s_ngaysinh);
                s_manv.Gender = Convert.ToBoolean(s_gioitinh);
                s_manv.PhoneNumber = s_sdt;
                s_manv.Address = s_diachi;

                UpdateModel(Convert.ToString(id_nv));
                ddd.SubmitChanges();
                return RedirectToAction("NhanVien", "NhanVien");

            }
            return this.SuaNV(id_nv);
        }

        [HttpGet]
        public ActionResult XoaNV(int id_nv)
        {
            var xoa_nv = ddd.tbl_Users.First(nv => nv.Id == id_nv);
            return View(xoa_nv);
        }

        [HttpPost]
        public ActionResult XoaNV(int id_nv, FormCollection collection)
        {
            var xoa_nv = ddd.tbl_Users.Where(nv => nv.Id == id_nv).First();
            List<tbl_Account> xoa_tk = ddd.tbl_Accounts.Where(ac => ac.UserId == id_nv).ToList();
            ddd.tbl_Accounts.DeleteAllOnSubmit(xoa_tk);
            ddd.tbl_Users.DeleteOnSubmit(xoa_nv);
            ddd.SubmitChanges();
            return RedirectToAction("NhanVien", "NhanVien");
        }

        public ActionResult LayDSNV()
        {
            List<tbl_User> lay_nv = ddd.tbl_Users.ToList();
            return PartialView(lay_nv);
        }

        public ActionResult LayDSNVBH()
        {
            List<tbl_Account> lay_nv = ddd.tbl_Accounts.Where(nv =>nv.tbl_TypeOfAccount.Id == 2).ToList();
            return PartialView(lay_nv);
        }
    }
}