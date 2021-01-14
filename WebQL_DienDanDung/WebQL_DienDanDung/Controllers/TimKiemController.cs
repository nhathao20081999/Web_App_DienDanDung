using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQL_DienDanDung.Models;

namespace WebQL_DienDanDung.Controllers
{
    public class TimKiemController : Controller
    {
        DienDanDungDBContextDataContext ddd = new DienDanDungDBContextDataContext();
        // GET: TimKiem
        public ActionResult TimKiem()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TimKiem(string id)
        {

            var sp = laySanPhamTimKiem(id);
            return View(sp);

        }

        public ActionResult GetDataSearch(string value)
        {
            var sp = GetTopDataProduct(value);
            var list = new List<tbl_Product>();
            foreach (var item in sp)
            {
                list.Add(new tbl_Product
                {
                    ProductName = item.ProductName,
                    Id = item.Id
                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private List<tbl_Product> laySanPhamTimKiem(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return ddd.tbl_Products.Where(a => a.ProductName.ToLower().Contains("@(*&@*($&@*&$*(@$&(*@$&(@&$(*!&$*(&$")).ToList();
            }
            return ddd.tbl_Products.Where(a => a.ProductName.Contains(id)).ToList();
        }

        private List<tbl_Product> GetTopDataProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return ddd.tbl_Products.Where(a => a.ProductName.ToLower().Contains("@(*&@*($&@*&$*(@$&(*@$&(@&$(*!&$*(&$")).ToList<tbl_Product>();
            }
            return ddd.tbl_Products.Where(a => a.ProductName.Contains(id)).Take(10).ToList<tbl_Product>();
        }

    }
}