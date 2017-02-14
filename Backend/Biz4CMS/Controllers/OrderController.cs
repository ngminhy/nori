﻿using Biz4CMS.Models;
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
                checkorder.message = Biz4CMS.Util.Common.GetOrderStatus(status.OrderStatusId);
            }
            return View(checkorder);
        }



    }
}
