using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQL_DienDanDung.Models;

namespace WebQL_DienDanDung.Controllers
{
    public class KhachHangController : Controller
    {
        DienDanDungDBContextDataContext ddd = new DienDanDungDBContextDataContext();
        // GET: KhachHang
        public ActionResult KhachHang()
        {
            return View(ddd.tbl_Customers.ToList());
        }

        public ActionResult ThemKH()
        {
            return RedirectToAction("KhachHang", "KhachHang");
        }

        [HttpPost]
        public ActionResult ThemKH(FormCollection collection, tbl_Customer kh)
        {
            var tenKH = collection["TenKH"];
            var ngaySinhKH = collection["NgaySinhKH"];
            var gioiTinhKH = Request.Form["GioiTinhKH"];
            var sdtKH = collection["SdtKH"];
            var diachiKH = collection["DiaChiKH"];

            kh.UserName = tenKH;
            kh.Birthday = Convert.ToDateTime(ngaySinhKH);
            kh.Gender = Convert.ToBoolean(gioiTinhKH);
            kh.Address = diachiKH;
            kh.PhoneNumber = sdtKH;
            ddd.tbl_Customers.InsertOnSubmit(kh);
            ddd.SubmitChanges();
            return RedirectToAction("KhachHang", "KhachHang");

        }

        [HttpGet]
        public ActionResult SuaKH(int id_kh)
        {
            var sua_kh = ddd.tbl_Customers.First(kh => kh.Id == id_kh);
            return View(sua_kh);
        }

        [HttpPost]
        public ActionResult SuaKH(int id_kh, FormCollection collection)
        {
            var s_makh = ddd.tbl_Customers.First(kh => kh.Id == id_kh);
            var s_tenkh = collection["tenkh"];
            var s_ngaysinh = collection["ngaysinh"];
            var s_gioitinh = Request.Form["gioitinh"];
            var s_sdt = collection["sdt"];
            var s_diachi = collection["diachi"];


            s_makh.Id = id_kh;
            if (s_makh != null)
            {
                s_makh.UserName = s_tenkh;
                s_makh.Birthday = Convert.ToDateTime(s_ngaysinh);
                s_makh.Gender = Convert.ToBoolean(s_gioitinh);
                s_makh.PhoneNumber = s_sdt;
                s_makh.Address = s_diachi;

                UpdateModel(Convert.ToString(id_kh));
                ddd.SubmitChanges();
                return RedirectToAction("KhachHang", "KhachHang");

            }
            return this.SuaKH(id_kh);
        }

        [HttpGet]
        public ActionResult XoaKH(int id_kh)
        {
            var xoa_kh = ddd.tbl_Customers.First(kh => kh.Id == id_kh);
            return View(xoa_kh);
        }

        [HttpPost]
        public ActionResult XoaKH(int id_kh, FormCollection collection)
        {
            var xoa_kh = ddd.tbl_Customers.Where(kh => kh.Id == id_kh).First();
            ddd.tbl_Customers.DeleteOnSubmit(xoa_kh);
            ddd.SubmitChanges();
            return RedirectToAction("KhachHang", "KhachHang");
        }

        public ActionResult LayDSKH()
        {
            List<tbl_Customer> lay_kh = ddd.tbl_Customers.ToList();
            return PartialView(lay_kh);
        }
    }
}