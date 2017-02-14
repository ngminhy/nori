using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Biz4CMS.ViewModels;

namespace Biz4CMS.Areas.Admin.Controllers
{
   [Authorize]
    public class OrderDetailController : Controller
    {
        //
        // GET: /bo/OrderDetail/
        Biz4Db db = new Biz4Db(); 
        public ActionResult Index(int? OrderId )
        {
           
            ViewBag.OrderId = OrderId;
            var order = db.Orders.Where(p => p.OrderId == OrderId).FirstOrDefault();
           order.State = Biz4CMS.Util.Common.GetOrderStatus(order.OrderStatusId);
            ViewBag.order = order;
            return View();
            
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request, int OrderId) {
            var OrderDetails = db.OrderDetails.Where(p => p.OrderId == OrderId).OrderByDescending(p => p.OrderDetailId).Select(p => new OrderDetailDto() {
                OrderId = p.OrderId,
                OrderDetailId  = p.OrderDetailId,
                ProductId = p.ProductId,
                ProductName = p.Product.Name,
                ListCakeName = p.ListCakeName,
                UnitPrice = p.UnitPrice,
                Quantity = p.Quantity   ,
                Total = p.Quantity * p.UnitPrice
            
            }).ToList();
            return this.Json(OrderDetails.ToDataSourceResult(request),JsonRequestBehavior.AllowGet );
        
        }
        
        
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, OrderDetail  model)
        {
            var OrderDetailToDelete = db.OrderDetails.First(p => p.OrderDetailId  == model.OrderDetailId );

            if (OrderDetailToDelete != null)
            {
                db.OrderDetails.Remove(OrderDetailToDelete);
                db.SaveChanges();
            }

            return Json(new[] { OrderDetailToDelete }.ToDataSourceResult(request));
        }
    }
}
