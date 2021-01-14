using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebQL_DienDanDung.Helper.ProductHelper
{
    public class ProductParams
    {
        public string search { get; set; }
        public string sort { get; set; }
        public string sortType { get; set; }
        public string fillTer { get; set; }
        public string fillType { get; set; }
    }
}