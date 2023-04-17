using BANHANG.contex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHANG.Models
{
    public class HomeModel
    {
        public List<product> ListProduct { get; set; }
        public List<catetory> ListCateroty { get; set; }
    }
}