using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQL_DienDanDung.Models;

namespace WebQL_DienDanDung.Controllers
{
    public class HomeController : Controller
    {
        DienDanDungDBContextDataContext ddd = new DienDanDungDBContextDataContext();
        public ActionResult Index()
        {
            var sp = LaySanPham();
            return View(sp);
        }

        public List<tbl_Product> LaySanPham()
        {
            return ddd.tbl_Products.Where(sp => sp.NumberOfProduct > 10).ToList();
        }

        public PartialViewResult DSSP()
        {
            var dssp = LaySanPham();
            return PartialView(dssp);
        }

    }
}
