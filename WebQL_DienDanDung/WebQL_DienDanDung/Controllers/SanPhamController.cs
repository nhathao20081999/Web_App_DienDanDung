using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQL_DienDanDung.Models;

namespace WebQL_DienDanDung.Controllers
{
    public class SanPhamController : Controller
    {
        DienDanDungDBContextDataContext ddd = new DienDanDungDBContextDataContext();
        // GET: Product
        public ActionResult AllSanPham()
        {
            var all_sp = ddd.tbl_Products.ToList();
            return View(all_sp);
        }
        public ActionResult Details(int id)
        {
            var details = ddd.tbl_Products.First(sp => sp.Id == id);
            return View(details);
        }
    }
}