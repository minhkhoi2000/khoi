using BANHANG.contex;
using BANHANG.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BANHANG.Controllers
{
    public class HomeController : Controller
    {
        webbanhangEntities5 objwebbanhangEntities5 = new webbanhangEntities5();
        public ActionResult Index()
        {
            HomeModel objHomeModel=new HomeModel();
            objHomeModel.ListCateroty = objwebbanhangEntities5.catetories.ToList();
            objHomeModel.ListProduct = objwebbanhangEntities5.products.ToList();
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
            //GET: Register
        }
        [HttpGet]
            public ActionResult Register()
            {
                return View();
            }

            //POST: Register
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Register(user _user)
            {
                if (ModelState.IsValid)
                {
                    var check = objwebbanhangEntities5.users.FirstOrDefault(s => s.email == _user.email);
                    if (check == null)
                    {
                        _user.password = GetMD5(_user.password);
                    objwebbanhangEntities5.Configuration.ValidateOnSaveEnabled = false;
                    objwebbanhangEntities5.users.Add(_user);
                    objwebbanhangEntities5.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Email already exists";
                        return View();
                    }


                }
                return View();


            }

            [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objwebbanhangEntities5.users.Where(s => s.email.Equals(email) && s.password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().firstname + " " + data.FirstOrDefault().lastname;
                    Session["Email"] = data.FirstOrDefault().email;
                    Session["idUser"] = data.FirstOrDefault().id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        public static String GetMD5(string str)
        {
            MD5 md5=new MD5CryptoServiceProvider();
            byte[] fromData=Encoding.UTF8.GetBytes(str);
            byte[] targetData=md5.ComputeHash(fromData);
            string byte25tring = null;
            for (int i=0;i< targetData.Length;i++)
            {
                byte25tring += targetData[1].ToString("x2");
            }
            return byte25tring;
        }
        public ActionResult Search(string currenFiter, string SearchString, int? page)
        {
            var lstproduct = new List<product>();
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
                lstproduct = objwebbanhangEntities5.products.Where(n => n.name.Contains(SearchString)).ToList();
            }
            else
            {
                lstproduct = objwebbanhangEntities5.products.ToList();
            }
            ViewBag.currenFiter = SearchString;
            int pageSize = 4;
            int pageName = (page ?? 1);
            lstproduct = lstproduct.OrderByDescending(n => n.id).ToList();
            return View(lstproduct.ToPagedList(pageName, pageSize));
        }
        public ActionResult Seach(string seach, int id=0)
        {
            return View();
        }
    }

}