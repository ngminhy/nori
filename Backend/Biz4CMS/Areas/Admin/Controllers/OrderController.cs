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
        static List<OrderStatus> listorderstatus = new List<OrderStatus>();

        static OrderController()
        {
            listorderstatus.Add(new OrderStatus
            {
                OrderStatusId = 1,
                Title = "Đặt hàng thành công"
            });

            listorderstatus.Add(new OrderStatus
            {
                OrderStatusId = 2,
                Title = "Đã tiếp nhận đơn hàng"
            });
            listorderstatus.Add(new OrderStatus
            {
                OrderStatusId = 3,
                Title = "Đang đóng gói"
            });
            listorderstatus.Add(new OrderStatus
            {
                OrderStatusId = 4,
                Title = "Đang vận chuyển"
            });
            listorderstatus.Add(new OrderStatus
            {
                OrderStatusId = 5,
                Title = "Giao hàng thành công"
            });

        }
        public JsonResult GetListOrderStatus()
        {
            return Json(listorderstatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            ViewData["OrderStatus"] = listorderstatus;
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

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.Order> Orders)
        {
            if (Orders != null && ModelState.IsValid)
            {
                foreach (var Order in Orders)
                {
                    var target = db.Orders.FirstOrDefault(p => p.OrderId == Order.OrderId);
                    if (target != null)
                    {
                        target.OrderStatusId = Order.OrderStatusId;
                    }

                }
                db.SaveChanges();
            }

            return Json(Orders.ToDataSourceResult(request, ModelState));
        }
    }
}
