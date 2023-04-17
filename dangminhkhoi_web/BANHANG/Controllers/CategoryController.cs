using BANHANG.contex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BANHANG.Controllers
{
    public class CategoryController : Controller
    {
        webbanhangEntities5 objwebbanhangEntities5 = new webbanhangEntities5();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objwebbanhangEntities5.catetories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int id)
        { 
            var listProduct= objwebbanhangEntities5.products.Where(n=>n.CategoryId == id).ToList();
            return View(listProduct);
        }
    }
}