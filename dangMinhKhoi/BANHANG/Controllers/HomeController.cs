using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BANHANG.Conttext;
using BANHANG.Models;

namespace BANHANG.Controllers
{
    public class HomeController : Controller
    {
        webbanhangEntities objwebbanhangEntities = new webbanhangEntities();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategorys = objwebbanhangEntities.catetories.ToList();
            objHomeModel.Listproducts = objwebbanhangEntities.products.ToList();
            return View(objHomeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}