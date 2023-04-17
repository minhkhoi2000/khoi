using BANHANG.contex;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BANHANG.ListtoDataTableConverter;

namespace BANHANG.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        webbanhangEntities5 objwebbanhangEntities5 = new webbanhangEntities5();

        // GET: Admin/Brand
        public ActionResult Index(string currenFiter, string SearchString, int? page)
        {
            var lstBrand = new List<Brand>();
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
                lstBrand = objwebbanhangEntities5.Brands.Where(n => n.name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objwebbanhangEntities5.Brands.ToList();
            }
            ViewBag.currenFiter = SearchString;
            int pageSize = 4;
            int pageName = (page ?? 1);
            lstBrand = lstBrand.OrderByDescending(n => n.id).ToList();
            return View(lstBrand.ToPagedList(pageName, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            //this.LoadData();

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBra)
        {
            //this.LoadData();

            if (ModelState.IsValid)
            {
                try
                {

                    if (objBra.ImageUpLoat != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBra.ImageUpLoat.FileName);
                        string extension = Path.GetExtension(objBra.ImageUpLoat.FileName);
                        fileName = fileName + extension;
                        objBra.avatar = fileName;
                        objBra.ImageUpLoat.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                    }
                    objwebbanhangEntities5.Brands.Add(objBra);
                    objwebbanhangEntities5.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(objBra);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBrand = objwebbanhangEntities5.Brands.Where(n => n.id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objwebbanhangEntities5.Brands.Where(n => n.id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Edit(int id, Brand objBrand)
        {
            if (objBrand.ImageUpLoat != null)
            {
                string filename = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoat.FileName);
                string extention = Path.GetExtension(objBrand.ImageUpLoat.FileName);
                filename = filename + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extention;
                objBrand.avatar = filename;
                objBrand.ImageUpLoat.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), filename));
            }
            objwebbanhangEntities5.Entry(objBrand).State = System.Data.Entity.EntityState.Modified;
            objwebbanhangEntities5.SaveChanges();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objwebbanhangEntities5.Brands.Where(n => n.id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objBr)
        {
            var objBrand = objwebbanhangEntities5.Brands.Where(n => n.id == objBr.id).FirstOrDefault();

            objwebbanhangEntities5.Brands.Remove(objBr);
            objwebbanhangEntities5.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}