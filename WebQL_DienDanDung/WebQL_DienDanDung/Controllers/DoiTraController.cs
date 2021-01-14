using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQL_DienDanDung.Models;

namespace WebQL_DienDanDung.Controllers
{
    public class DoiTraController : Controller
    {
        DienDanDungDBContextDataContext ddd = new DienDanDungDBContextDataContext();
        // GET: DoiTra
        public ActionResult DoiTra()
        {
            return View(ddd.tbl_Bills.Where(hd => hd.Status == Convert.ToBoolean("True")).ToList());
        }

        public ActionResult ChiTietDT(int id_hd)
        {
            List<tbl_ProductOfBill> cthd = ddd.tbl_ProductOfBills.Where(ct => ct.BillId == id_hd).ToList();
            tbl_ProductOfBill lay_id = ddd.tbl_ProductOfBills.FirstOrDefault(ct => ct.BillId == id_hd);
            Session["cthd"] = lay_id.BillId;
            Session["TrangThai"] = lay_id.tbl_Bill.Status;
            Session["HanBH"] = DateTime.Now.Date -lay_id.tbl_Bill.DateCreate;
            return View(cthd);
        }
    }
}