using BANHANG.contex;
using BANHANG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BANHANG.Controllers
{
    public class PaymentController : Controller
    {
        webbanhangEntities5 objwebbanhangEntities5 = new webbanhangEntities5();
        // GET: Payment
        public ActionResult Index()
        {

            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Oder objOrder = new Oder();
                objOrder.name = "DonHang" + DateTime.Now.ToString("yyyyMMddHHss");
                objOrder.uerId = int.Parse(Session["idUser"].ToString());
                objOrder.CreateOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objwebbanhangEntities5.Oders.Add(objOrder);
                objwebbanhangEntities5.SaveChanges();

                int intOrder = objOrder.id;
                List<OderDatail> lstOderDatail = new List<OderDatail>();
                foreach (var item in lstCart)
                {
                    OderDatail obj = new OderDatail();
                    obj.Quantity = item.Quatity;
                    obj.OderId = intOrder;
                    obj.ProductId = item.Product.id;
                    lstOderDatail.Add(obj);
                }
                objwebbanhangEntities5.OderDatails.AddRange(lstOderDatail);
                objwebbanhangEntities5.SaveChanges();

            }
            return View();
        }
    }
}