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
    public class ProductController : Controller
    {
        webbanhangEntities5 objwebbanhangEntities5 = new webbanhangEntities5();

        // GET: Admin/Product
        public ActionResult Index(string currenFiter,string SearchString, int? page)
        {
            var lstproduct=new List<product>();
            if (SearchString != null)
            {
                page=1;
            }
            else
            {
                SearchString=currenFiter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstproduct= objwebbanhangEntities5.products.Where(n=>n.name.Contains(SearchString)).ToList();
            }
            else
            {
                lstproduct = objwebbanhangEntities5.products.ToList();
            }
            ViewBag.currenFiter = SearchString;
            int pageSize = 4;
            int pageName = (page ?? 1);
            lstproduct = lstproduct.OrderByDescending(n=>n.id).ToList();
            return View(lstproduct.ToPagedList(pageName, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            Commom objCommom = new Commom();
            var lstCat= objwebbanhangEntities5.catetories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommom.ToSlectList(dtCategory,"id", "name");
            var lstBrand= objwebbanhangEntities5.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable( lstBrand);
            ViewBag.ListBrand = objCommom.ToSlectList(dtBrand, "id", "name");


            List<ProductType> lstproductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.id = 1;
            objProductType.name = "Giảm Giá Sốc";
            lstproductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.id = 2;
            objProductType.name = "Đề Xuất";
            lstproductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstproductType);
            ViewBag.ProductType  = objCommom.ToSlectList(dtProductType, "id", "name");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(product objProduct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpLoat != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoat.FileName);
                        string extention = Path.GetExtension(objProduct.ImageUpLoat.FileName);
                        filename = filename + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extention;
                        objProduct.avatar = filename;
                        objProduct.ImageUpLoat.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), filename));

                    }
                    objwebbanhangEntities5.products.Add(objProduct);
                    objwebbanhangEntities5.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Details( int id)
        {
            var objProduct= objwebbanhangEntities5.products.Where(n=>n.id==id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objwebbanhangEntities5.products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(product objPro)
        {
            var objProduct = objwebbanhangEntities5.products.Where(n => n.id == objPro.id).FirstOrDefault();

            objwebbanhangEntities5.products.Remove(objProduct);
            objwebbanhangEntities5.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objwebbanhangEntities5.products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(int id,product objProduct)
        {
            if (objProduct.ImageUpLoat != null)
            {
                string filename = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoat.FileName);
                string extention = Path.GetExtension(objProduct.ImageUpLoat.FileName);
                filename = filename + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extention;
                objProduct.avatar = filename;
                objProduct.ImageUpLoat.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), filename));
            }
            objwebbanhangEntities5.Entry(objProduct).State= System.Data.Entity.EntityState.Modified;
            objwebbanhangEntities5.SaveChanges();
            return View(objProduct);
        }
    }
}