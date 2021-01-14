using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebQL_DienDanDung.Models
{
    public class SanPham
    {
        public string masp { get; set; }
        public string tensp { get; set; }
        public int? soluong { get; set; }
        public string hinhanh { get; set; }
        public int? gia { get; set; }
        public int? thoihan { get; set; }
        public string loaisp { get; set; }
        public string nsx { get; set; }

        public SanPham(string masp, string tensp, int? soluong, string hinhanh, int? gia, int? thoihan, string loaisp, string nsx)
        {
            this.masp = masp;
            this.tensp = tensp;
            this.soluong = soluong;
            this.hinhanh = hinhanh;
            this.gia = gia;
            this.thoihan = thoihan;
            this.loaisp = loaisp;
            this.nsx = nsx;
        }
        public SanPham()
        {

        }
    }
}