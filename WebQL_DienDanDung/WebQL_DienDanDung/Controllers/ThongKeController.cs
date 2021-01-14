using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQL_DienDanDung.Models;
using CrystalDecisions.CrystalReports.Engine;
using WebQL_DienDanDung.CrystalReports;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WebQL_DienDanDung.Controllers
{
    public class ThongKeController : Controller
    {
        DienDanDungDBContextDataContext ddd = new DienDanDungDBContextDataContext();
        // GET: ThongKe
        public ActionResult ThongKe()
        {
            return View(ddd.tbl_Bills.Where(hd => hd.Status == Convert.ToBoolean("True")).ToList());
        }

        public ActionResult TimKiem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TimKiem(FormCollection collection)
        {
            if (collection["ngaybd"] != "" || collection["ngaykt"] != "")
            {
                var ngay1 = DateTime.Parse(collection["ngaybd"]).Date;
                var ngay2 = DateTime.Parse(collection["ngaykt"]).Date;

                if (ngay1 < ngay2)
                {
                    return View(ddd.tbl_Bills.Where(hd => hd.DateCreate >= ngay1 && hd.DateCreate <= ngay2 && hd.Status == Convert.ToBoolean("True")).ToList());
                }

                return View(new List<tbl_Bill>());
            }
            return View(ddd.tbl_Bills.Where(x => x.Status == true).ToList());
        }
        public ActionResult XuatFile()
        {
            List<tbl_Bill> bill = ddd.tbl_Bills.Where(hd => hd.Status == Convert.ToBoolean("True")).ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "ReportBill.rpt"));
            rd.SetDatabaseLogon("sa", "sa2012");
            report();

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "BillList.pdf");

            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            //stream.Seek(0, SeekOrigin.Begin);
            //return File(stream, "application/xlsx", "BillList.xlsx");
        }

        public void report()
        {
            ReportBill reportBill = new ReportBill();
            SqlConnection cnn;
            string connectionString = null;
            string sql = null;

            connectionString = ConfigurationManager.ConnectionStrings["QL_DIENDANDUNGConnectionString1"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            cnn.Open();

            sql = "SELECT * from tbl_Bill where status = 'true' ";
            SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
            DataSet ds = new DataSet();
            dscmd.Fill(ds, "Imagetest");
            cnn.Close();

            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "ReportBill.rpt"));
            cryRpt.DataSourceConnections.Clear();
            cryRpt.SetDataSource(ds.Tables[0]);
            //cryRpt.Subreports[0].DataSourceConnections.Clear();
            //cryRpt.Subreports[0].SetDataSource(ds.Tables[0]);
            reportBill.Refresh();
        }
    }
}