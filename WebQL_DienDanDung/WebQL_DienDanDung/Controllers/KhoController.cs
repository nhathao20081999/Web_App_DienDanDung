using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQL_DienDanDung.Models;

namespace WebQL_DienDanDung.Controllers
{
    public class KhoController : Controller
    {
        DienDanDungDBContextDataContext ddd = new DienDanDungDBContextDataContext();
        // GET: Kho
        public ActionResult Kho()
        {
            return View(ddd.tbl_Stores.ToList());
        }

        public ActionResult ThemKho()
        {
            return RedirectToAction("Kho", "Kho");
        }
        [HttpPost]
        public ActionResult ThemKho(FormCollection collection, tbl_Store kho)
        {
            var maSP = Request.Form["masp"];
            var slTon = collection["soluongton"];


            kho.ProductId = Convert.ToInt32(maSP);
            kho.Number = Convert.ToInt32(slTon);

            ddd.tbl_Stores.InsertOnSubmit(kho);
            ddd.SubmitChanges();
            return RedirectToAction("Kho", "Kho");

        }

        [HttpGet]
        public ActionResult SuaKho(int id_kho)
        {
            var sua_kho = ddd.tbl_Stores.First(kho => kho.Id == id_kho);
            return View(sua_kho);
        }

        [HttpPost]
        public ActionResult SuaKho(int id_kho, FormCollection collection)
        {
            var s_makho = ddd.tbl_Stores.First(kho => kho.Id == id_kho);
            var s_tenkho = collection["masp"];
            var s_soluongton = collection["soluongton"];


            s_makho.Id = id_kho;
            //if (string.IsNullOrEmpty(s_tenkho))
            //{
            //    ViewData["SuaKho1"] = "Tên Không Được Để Trống!";
            //}
            //else if (string.IsNullOrEmpty(s_soluongton))
            //{
            //    ViewData["SuaKho1"] = "Sô Lượng Không Được Để Trống!";
            //}
            if (s_makho != null)
            {
                s_makho.Number = Convert.ToInt32(s_soluongton);

                UpdateModel(Convert.ToString(id_kho));
                ddd.SubmitChanges();
                return RedirectToAction("Kho","Kho");

            }
            return this.SuaKho(id_kho);
        }

        [HttpGet]
        public ActionResult XoaKho(int id_kho)
        {
            var xoa_kho = ddd.tbl_Stores.First(kho => kho.Id == id_kho);
            return View(xoa_kho);
        }

        [HttpPost]
        public ActionResult XoaKho(int id_kho, FormCollection collection)
        {
            var xoa_kho = ddd.tbl_Stores.Where(kho => kho.Id == id_kho).First();
            ddd.tbl_Stores.DeleteOnSubmit(xoa_kho);
            ddd.SubmitChanges();
            return RedirectToAction("Kho", "Kho");
        }
    }
}