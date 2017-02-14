using Biz4CMS.Models;
using Biz4CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biz4CMS.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            var Locations = db.Location.OrderByDescending(p => p.LocationId).ToList();
            ViewBag.data = Locations;
            return View();
        }

        //POST: /Order/

        [HttpPost]
        public ActionResult Index(FormCollection values)
        {
            var userinfo = new UserInfo();
            TryUpdateModel(userinfo);
            HttpContext.Session["userinfo"] = userinfo;
            return RedirectToAction("Index", "product");
        }


        public ActionResult CheckOrder(string id)
        {
            var status = db.Orders.Where(p => p.OrderCode == id).FirstOrDefault();
            var checkorder = new CheckOrder();
            checkorder.status = 0;
            checkorder.message = "Đơn hàng không tồn tại";
           if (status != null && status.OrderCode == id)
            {
                checkorder.status = status.OrderStatusId;
                checkorder.message = getorderstatus(status.OrderStatusId);
            }
            return View(checkorder);
        }


        public string getorderstatus(int st)
        {
            var ms = "";
            switch (st)
            {
                case 1:
                    ms = "Đặt hàng thành công";
                            break;
                case 2:
                    ms = "Đã tiếp nhận đơn hàng";
                    break;
                case 3:
                    ms = "Đang đóng gói";
                    break;
                case 4:
                    ms = "Đang vận chuyển";
                    break;
                case 5:
                    ms = "Giao hàng thành công";
                    break;
                default:
                            break;
                    }
            return ms;
        }
    }
}
