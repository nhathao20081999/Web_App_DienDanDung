using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQL_DienDanDung.Models;

namespace WebQL_DienDanDung.Controllers
{
    public class AdminController : Controller
    {
        DienDanDungDBContextDataContext ddd = new DienDanDungDBContextDataContext();
        // GET: Admin
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var taikhoan = collection["userName"];
            var matKhau = collection["password"];
            tbl_Account ad = ddd.tbl_Accounts.FirstOrDefault(us => us.UserName == taikhoan && us.Password == matKhau);
            if (string.IsNullOrEmpty(taikhoan))
            {
                ViewData["Loi1"] = "Tên đăng nhập không được để trống!";
            }
            else if (string.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi2"] = "Mật khẩu không được để trống!";
            }
            else if (ad != null)
            {
                Session["PQ"] = ad.tbl_TypeOfAccount.Id;
                Session["CV"] = ad.tbl_TypeOfAccount.TypeName;
                Session["ten"] = ad.tbl_User.UserName;
                Session["anh"] = ad.AccountImage;
                Session["TinhTrangHD"] = ad.Status;
                Session["AdminIsLogin"] = true;
                ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                return RedirectToAction("Admin", "Admin");
            }
            else
            {
                ViewData["Loi"] = "Tên đăng nhập hoặc mật khẩu không chính xác!";
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["AdminIsLogin"] = null;
            return RedirectToAction("DangNhap", "Admin");
        }

        public ActionResult Admin()
        {
            if (Session["AdminIsLogin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "Admin");
            }
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            //validate (tự validate)

            //xử lý upload
            file.SaveAs(Server.MapPath("~/Content/img/" + file.FileName));
            return "/Content/img/" + file.FileName;
        }

        public ActionResult ThemSP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemSP(FormCollection collection, tbl_Product sp)
        {
            var t_tensanpham = collection["tensanpham"];
            var t_soluong = collection["soluong"];
            var t_giaban = collection["giaban"];
            var t_thoihan = collection["hanbh"];
            var t_hinhanh = collection["hinhanh"];
            var t_nhomsp = Request.Form["nhomsp"];
            var t_nhasx = Request.Form["nhasx"];


            //if (string.IsNullOrEmpty(t_tensanpham))
            //{
            //    ViewData["Loi1"] = "Tên Sản Phẩm Không Được Để Trống";
            //}
            //else if (string.IsNullOrEmpty(t_soluong))
            //{
            //    ViewData["Loi2"] = "Số Lượng Sản Phẩm Không Được Để Trống";
            //}
            //else if (string.IsNullOrEmpty(t_giaban))
            //{
            //    ViewData["Loi3"] = "Giá bán Sản Phẩm Không Được Để Trống";
            //}
            //else if (string.IsNullOrEmpty(t_thoihan))
            //{
            //    ViewData["Loi4"] = "Thời Gian Bảo Hành Sản Phẩm Không Được Để Trống";
            //}
            //else
            //{
            sp.ProductName = t_tensanpham;
            sp.NumberOfProduct = Convert.ToInt32(t_soluong);
            sp.Cost = Convert.ToDecimal(t_giaban);
            sp.ExpiryDate = Convert.ToInt32(t_thoihan);
            sp.ProductImage = t_hinhanh;
            sp.TypeId = int.Parse(t_nhomsp);
            sp.ProducerId = int.Parse(t_nhasx);

            ddd.tbl_Products.InsertOnSubmit(sp);
            ddd.SubmitChanges();
            return RedirectToAction("QL_SanPham", "Admin", new { msp = sp.TypeId });
            //}
            //return this.ThemSP();
        }

        public ActionResult SuaSP(int id)
        {
            var sua_sp = ddd.tbl_Products.First(sp => sp.Id == id);
            tbl_TypeOfProduct nsp = ddd.tbl_TypeOfProducts.FirstOrDefault(mnsp => mnsp.Id == sua_sp.TypeId);
            tbl_Producer nsx = ddd.tbl_Producers.FirstOrDefault(mnsx => mnsx.Id == sua_sp.ProducerId);
            Session["TenNSP"] = nsp.TypeName;
            Session["TenNSX"] = nsx.ProducerName;
            return View(sua_sp);
        }

        [HttpPost]
        public ActionResult SuaSP(int id, FormCollection collection)
        {
            var s_masp = ddd.tbl_Products.First(sp => sp.Id == id);
            var s_tensanpham = collection["tensanpham"];
            var s_soluong = collection["soluong"];
            var s_giaban = collection["giaban"];
            var s_thoihan = collection["hanbh"];
            var s_hinhanh = Request.Form["Hinhanh"];


            s_masp.Id = id;
            //if (string.IsNullOrEmpty(s_tensanpham))
            //{
            //    ViewData["Loi1"] = "Tên Sản Phẩm Không Được Để Trống";
            //}
            //else if (string.IsNullOrEmpty(s_soluong))
            //{
            //    ViewData["Loi2"] = "Số Lượng Sản Phẩm Không Được Để Trống";
            //}
            //else if (string.IsNullOrEmpty(s_giaban))
            //{
            //    ViewData["Loi3"] = "Giá bán Sản Phẩm Không Được Để Trống";
            //}
            //else if (string.IsNullOrEmpty(s_thoihan))
            //{
            //    ViewData["Loi4"] = "Thời Gian Bảo Hành Sản Phẩm Không Được Để Trống";
            //}
            if (s_masp != null)
            {

                s_masp.ProductName = s_tensanpham;
                s_masp.NumberOfProduct = Convert.ToInt32(s_soluong);
                s_masp.ExpiryDate = Convert.ToInt32(s_thoihan);
                s_masp.Cost = Convert.ToDecimal(s_giaban);
                s_masp.ProductImage = s_hinhanh;
                UpdateModel(Convert.ToString(id));
                ddd.SubmitChanges();
                return RedirectToAction("QL_SanPham", "Admin", new { msp = s_masp.TypeId });

            }
            return this.SuaSP(id);
        }

        [HttpGet]
        public ActionResult XoaSP(int id)
        {
            var xoa_sp = ddd.tbl_Products.First(sp => sp.Id == id);
            return View(xoa_sp);
        }

        [HttpPost]
        public ActionResult XoaSP(int id, FormCollection collection)
        {
            var xoa_sp = ddd.tbl_Products.Where(sp => sp.Id == id).First();
            ddd.tbl_Products.DeleteOnSubmit(xoa_sp);
            ddd.SubmitChanges();
            return RedirectToAction("QL_SanPham", "Admin", new { msp = xoa_sp.TypeId });
        }

        public ActionResult QL_SanPham(int msp)
        {
            List<tbl_Product> list_sp = ddd.tbl_Products.ToList();
            List<tbl_Product> list_sp1 = list_sp.Where(sp => sp.TypeId == msp).ToList();
            return View(list_sp1);
        }

        public ActionResult NhomSP()
        {
            List<tbl_TypeOfProduct> nhom_sp = ddd.tbl_TypeOfProducts.ToList();
            return PartialView(nhom_sp);
        }

        public ActionResult Lay_NhomSP()
        {
            List<tbl_TypeOfProduct> nhom_sp = ddd.tbl_TypeOfProducts.ToList();
            return PartialView(nhom_sp);
        }

        public ActionResult NhaSX()
        {
            List<tbl_Producer> nhom_nsx = ddd.tbl_Producers.ToList();
            return PartialView(nhom_nsx);
        }

        public ActionResult Lay_SP()
        {
            List<tbl_Product> sp = ddd.tbl_Products.ToList();
            return PartialView(sp);
        }

        public ActionResult ThongKe()
        {
            return View();
        }
    }
}