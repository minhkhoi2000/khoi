using BANHANG.Conttext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHANG.Models
{
    public class HomeModel
    {
        public List<product> Listproducts { get; set; }
        public List<catetory> ListCategorys { get; set; }
    }
}