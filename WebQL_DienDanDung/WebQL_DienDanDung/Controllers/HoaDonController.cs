using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQL_DienDanDung.Models;

namespace WebQL_DienDanDung.Controllers
{
    public class HoaDonController : Controller
    {
        DienDanDungDBContextDataContext ddd = new DienDanDungDBContextDataContext();
        // GET: HoaDon
        public ActionResult HoaDon()
        {
            return View(ddd.tbl_Bills.ToList());
        }

        public ActionResult ThemHD()
        {
            return RedirectToAction("ThemCTHD1", "HoaDon");
        }
        [HttpPost]
        public ActionResult ThemHD(FormCollection collection, tbl_Bill hd)
        {
            var t_tenkh = Request.Form["TenKH"];
            var t_tennv = Request.Form["TenNV"];
            var t_tinhtrang = Request.Form["TinhTrang"];

            hd.CustomerID = Convert.ToInt32(t_tenkh);
            hd.PersionalID = Convert.ToInt32(t_tennv);
            hd.DateCreate = DateTime.Now;
            hd.Status = Convert.ToBoolean(t_tinhtrang);            

            ddd.tbl_Bills.InsertOnSubmit(hd);
            ddd.SubmitChanges();
            Session["cthd1"] = hd.Id;
            Session["TrangThai1"] = hd.Status;
            return this.ThemHD();

        }

        public ActionResult ChiTietHD1(int id_hd)
        {
            List<tbl_ProductOfBill> cthd = ddd.tbl_ProductOfBills.Where(ct => ct.BillId == id_hd).ToList();            
            return View(cthd);
        }

        public ActionResult ThemCTHD1()
        {
            return RedirectToAction("ChiTietHD1", "HoaDon", new { id_hd = Convert.ToInt32(Session["cthd1"]) });
        }
        [HttpPost]
        public ActionResult ThemCTHD1(FormCollection collection, tbl_ProductOfBill cthd)
        {
            var t_sohd = collection["sohoadon"];
            var t_tensp = Request.Form["tensp"];
            var t_soluong = collection["soluong"];

            cthd.BillId = Convert.ToInt32(t_sohd);
            cthd.ProductId = Convert.ToInt32(t_tensp);
            cthd.Number = Convert.ToInt32(t_soluong);

            ddd.tbl_ProductOfBills.InsertOnSubmit(cthd);
            ddd.SubmitChanges();
            return this.ThemCTHD1();

        }

        [HttpGet]
        public ActionResult ThanhToan(int id_hd)
        {
            var thanhtoan = ddd.tbl_Bills.First(tt => tt.Id == id_hd);
            return View(thanhtoan);
        }

        [HttpPost]
        public ActionResult ThanhToan(int id_hd, FormCollection collection)
        {
            var thanhtoan = ddd.tbl_Bills.First(tt => tt.Id == id_hd);
            var tt_tongtien = collection["tongtien"];
            var tt_tinhtrang = Request.Form["tinhtrang"];

            thanhtoan.Id = id_hd;

            if (thanhtoan != null)
            {
                thanhtoan.SumMoney = Convert.ToInt32(tt_tongtien);
                thanhtoan.Status = Convert.ToBoolean(tt_tinhtrang);

                UpdateModel(Convert.ToString(id_hd));
                ddd.SubmitChanges();
                return RedirectToAction("HoaDon", "HoaDon");

            }
            return this.ThanhToan(id_hd);
        }

        [HttpGet]
        public ActionResult XoaHD(int id_hd)
        {
            var xoa_hd = ddd.tbl_Bills.First(hd => hd.Id == id_hd);
            return View(xoa_hd);
        }

        [HttpPost]
        public ActionResult XoaHD(int id_hd, FormCollection collection)
        {
            var xoa_hd = ddd.tbl_Bills.Where(hd => hd.Id == id_hd).First();
            var xoa_cthd = ddd.tbl_ProductOfBills.Where(hd => hd.BillId == id_hd).ToList();
            ddd.tbl_ProductOfBills.DeleteAllOnSubmit(xoa_cthd);
            ddd.tbl_Bills.DeleteOnSubmit(xoa_hd);
            ddd.SubmitChanges();
            return RedirectToAction("HoaDon", "HoaDon");
        }


        //Chi Tiết Hóa Đơn
        public ActionResult ChiTietHD(int id_hd)
        {
            List<tbl_ProductOfBill> cthd = ddd.tbl_ProductOfBills.Where(ct => ct.BillId == id_hd).ToList();
            tbl_ProductOfBill lay_id = ddd.tbl_ProductOfBills.FirstOrDefault(ct => ct.BillId == id_hd);
            Session["cthd"] = lay_id.BillId;
            Session["TrangThai"] = lay_id.tbl_Bill.Status;
            return View(cthd);
        }

        public ActionResult ThemCTHD()
        {
            return RedirectToAction("ChiTietHD", "HoaDon", new { id_hd = Convert.ToInt32(Session["cthd"]) });
        }
        [HttpPost]
        public ActionResult ThemCTHD(FormCollection collection, tbl_ProductOfBill cthd)
        {
            var t_sohd = collection["sohoadon"];
            var t_tensp = Request.Form["tensp"];
            var t_soluong = collection["soluong"];

            cthd.BillId = Convert.ToInt32(t_sohd);
            cthd.ProductId = Convert.ToInt32(t_tensp);
            cthd.Number = Convert.ToInt32(t_soluong);

            ddd.tbl_ProductOfBills.InsertOnSubmit(cthd);
            ddd.SubmitChanges();
            return this.ThemCTHD();

        }

        [HttpGet]
        public ActionResult SuaCTHD(int id_cthd, int id_msp)
        {
            var sua_cthd = ddd.tbl_ProductOfBills.FirstOrDefault(cthd => cthd.BillId == id_cthd && cthd.ProductId == id_msp);
            return View(sua_cthd);
        }
        [HttpPost]
        public ActionResult SuaCTHD(int id_cthd, int id_msp, FormCollection collection)
        {
            var s_cthd = ddd.tbl_ProductOfBills.First(cthd => cthd.BillId == id_cthd && cthd.ProductId == id_msp);
            var s_soluong = collection["soluong"];


            s_cthd.BillId = id_cthd;
            s_cthd.ProductId = id_msp;
            if (s_cthd != null)
            {
                s_cthd.Number = Convert.ToInt32(s_soluong);
                ddd.SubmitChanges();
                return RedirectToAction("ChiTietHD", "HoaDon", new { id_hd = id_cthd });

            }
            return this.SuaCTHD(id_cthd, id_msp);
        }

        [HttpGet]
        public ActionResult XoaCTHD(int id_cthd, int id_msp)
        {
            var xoa_cthd = ddd.tbl_ProductOfBills.First(cthd => cthd.BillId == id_cthd && cthd.ProductId == id_msp);
            return View(xoa_cthd);
        }

        [HttpPost]
        public ActionResult XoaCTHD(int id_cthd, int id_msp, FormCollection collection)
        {
            var xoa_cthd = ddd.tbl_ProductOfBills.Where(cthd => cthd.BillId == id_cthd && cthd.ProductId == id_msp).First();
            ddd.tbl_ProductOfBills.DeleteOnSubmit(xoa_cthd);
            ddd.SubmitChanges();
            return RedirectToAction("ChiTietHD", "HoaDon", new { id_hd = id_cthd });
        }

    }
}