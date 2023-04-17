using BANHANG.contex;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BANHANG.Areas.Admin.Controllers
{
    public class orderController : Controller
    {
        webbanhangEntities5 objwebbanhangEntities5 = new webbanhangEntities5();
        // GET: Admin/order
        public ActionResult Index(string currenFiter, string SearchString, int? page)
        {
            var lstod = new List<Oder>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currenFiter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstod = objwebbanhangEntities5.Oders.Where(n => n.name.Contains(SearchString)).ToList();
            }
            else
            {
                lstod = objwebbanhangEntities5.Oders.ToList();
            }
            ViewBag.currenFiter = SearchString;
            int pageSize = 4;
            int pageName = (page ?? 1);
            lstod = lstod.OrderByDescending(n => n.id).ToList();
            return View(lstod.ToPagedList(pageName, pageSize));
        }
    }
}