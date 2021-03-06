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
            var polyobj = db.ShippingLocation.Where(p => p.Active).OrderByDescending(p => p.ShippingLocationId).ToList();
            ViewBag.polyobj = polyobj;
            return View();
        }

        //POST: /Order/

        [HttpPost]
        public ActionResult Index(string name, string email, string phone,string address,string bookingtime,string branchname,string note,string ward,string dist,string city)
        {
            var userinfo = new UserInfo();
            var  longaddress = "";
            if (!string.IsNullOrEmpty(address))
            {
                longaddress = address + " Phường " + ward + " " + dist + " Thành Phố " + city;
                branchname = "";


            }
            
            userinfo.Address = longaddress.ToUpper();
            userinfo.BookingTime = bookingtime;
            userinfo.BranchName = branchname;
            userinfo.Email = email;
            userinfo.Phone = phone;
            userinfo.Name = name;
            userinfo.Note= note;
            //TryUpdateModel(userinfo);
            HttpContext.Session["userinfo"] = userinfo;
            return RedirectToAction("Index", "product");
        }


        public ActionResult CheckOrder(string id)
        {
            var status = new Order();
            if (!string.IsNullOrEmpty(id))
            {
                status = db.Orders.Where(p => p.OrderCode == id).FirstOrDefault();

            }
            var checkorder = new CheckOrder();
            checkorder.status = 0;
            checkorder.message = "Đơn hàng không tồn tại";
           if (status != null && status.OrderCode == id)
            {
                checkorder.status = status.OrderStatusId;
                checkorder.message = Biz4CMS.Util.Common.GetOrderStatus(status.OrderStatusId);
            }
            checkorder.ordercode = id;
            checkorder.liststatus = new string[5];
            checkorder.liststatus[0] = "Đặt thành công";
            checkorder.liststatus[1] = "Đã tiếp nhận";
            checkorder.liststatus[2] = "Đang đóng gói";
            checkorder.liststatus[3] = "Đang vận chuyển";
            checkorder.liststatus[4] = "Giao thành công";

            return View(checkorder);
        }



    }
}
