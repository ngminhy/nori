using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Biz4CMS.Areas.Admin.Controllers
{
   [Authorize]
    public class OrderController : Controller
    {
        //
        // GET: /bo/Order/
        Biz4Db db = new Biz4Db(); 
        public ActionResult Index()
        {
                                return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request) {
            var Orders = db.Orders.OrderByDescending(p=> p.OrderId).ToList();
            return this.Json(Orders.ToDataSourceResult(request),JsonRequestBehavior.AllowGet );
        
        }
        
        
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, Order  model)
        {
            var OrderToDelete = db.Orders.First(p => p.OrderId  == model.OrderId );

            if (OrderToDelete != null)
            {
                db.Orders.Remove(OrderToDelete);
                db.SaveChanges();
            }

            return Json(new[] { OrderToDelete }.ToDataSourceResult(request));
        }
    }
}
