using BANHANG.contex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BANHANG.Controllers
{
    public class ProductController : Controller
    {
        webbanhangEntities5 objwebbanhangEntities5 = new webbanhangEntities5();
        // GET: Product
        public ActionResult Detail(int id)
        {
            var objProduct = objwebbanhangEntities5.products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
    }

}