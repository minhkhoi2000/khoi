using BANHANG.contex;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BANHANG.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        webbanhangEntities5 objwebbanhangEntities5 = new webbanhangEntities5();

        // GET: Admin/Brand
        public ActionResult Index(string currenFiter, string SearchString, int? page)
        {
            var lstca = new List<catetory>();
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
                lstca = objwebbanhangEntities5.catetories.Where(n => n.name.Contains(SearchString)).ToList();
            }
            else
            {
                lstca = objwebbanhangEntities5.catetories.ToList();
            }
            ViewBag.currenFiter = SearchString;
            int pageSize = 4;
            int pageName = (page ?? 1);
            lstca = lstca.OrderByDescending(n => n.id).ToList();
            return View(lstca.ToPagedList(pageName, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            //this.LoadData();

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(catetory objca)
        {
            //this.LoadData();

            if (ModelState.IsValid)
            {
                try
                {

                    if (objca.ImageUpLoat != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objca.ImageUpLoat.FileName);
                        string extension = Path.GetExtension(objca.ImageUpLoat.FileName);
                        fileName = fileName + extension;
                        objca.avatar = fileName;
                        objca.ImageUpLoat.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objwebbanhangEntities5.catetories.Add(objca);
                    objwebbanhangEntities5.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(objca);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objca = objwebbanhangEntities5.catetories.Where(n => n.id == id).FirstOrDefault();
            return View(objca);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objca = objwebbanhangEntities5.catetories.Where(n => n.id == id).FirstOrDefault();
            return View(objca);
        }
        [HttpPost]
        public ActionResult Edit(int id, catetory objca)
        {
            if (objca.ImageUpLoat != null)
            {
                string filename = Path.GetFileNameWithoutExtension(objca.ImageUpLoat.FileName);
                string extention = Path.GetExtension(objca.ImageUpLoat.FileName);
                filename = filename + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extention;
                objca.avatar = filename;
                objca.ImageUpLoat.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), filename));
            }
            objwebbanhangEntities5.Entry(objca).State = System.Data.Entity.EntityState.Modified;
            objwebbanhangEntities5.SaveChanges();
            return View(objca);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objca = objwebbanhangEntities5.catetories.Where(n => n.id == id).FirstOrDefault();
            return View(objca);
        }
        [HttpPost]
        public ActionResult Delete(catetory objca)
        {
            var objcate = objwebbanhangEntities5.Brands.Where(n => n.id == objca.id).FirstOrDefault();

            objwebbanhangEntities5.catetories.Remove(objca);
            objwebbanhangEntities5.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}