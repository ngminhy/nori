using Biz4CMS.Models;
using Biz4CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var cartitem = cart.GetCartItems();
            if (cartitem.Count == 0)
            {
                return RedirectToAction("Index", "product");
            }
            var Locations = db.Location.OrderByDescending(p => p.LocationId).ToList();
            ViewBag.data = Locations;
            var polyobj = db.ShippingLocation.Where(p => p.Active).OrderByDescending(p => p.ShippingLocationId).ToList();
            ViewBag.polyobj = polyobj;

           

            // Set up our ViewModel
            ViewBag.Cart = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            return View();
        }

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
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
            //return RedirectToAction("Index", "shoppingcart");

            var order = new Order();
            TryUpdateModel(order);

            try
            {

                //order.Username = User.Identity.Name;
                var cart = ShoppingCart.GetCart(this.HttpContext);

                order.Username = userinfo.Email;
                order.FullName = userinfo.Name;
                order.Phone = userinfo.Phone;
                order.Note = userinfo.Note;
                order.Address = userinfo.Address;
                order.Total = cart.GetTotal();
                order.OrderStatusId = 1;
                order.OrderDate = DateTime.Now;
                order.BookingTime = userinfo.BookingTime;
                order.BranchName = userinfo.BranchName;
                order.OrderCode = GetUniqueKey(8);
                //Save Order
                db.Orders.Add(order);
                db.SaveChanges();
                //Process the order
                //string strBody = cart.CreateOrderEmail();


                cart.CreateOrder(order);
                //strBody = strBody + "<h2>Thông tin người mua:</h2>Họ và Tên:  " + order.FullName + " <br />Số điện thoại: " + order.Phone + " <br />Email: " + order.Email + " <br />Địa chỉ: " + order.Address + " <br />Thanh toán: " + order.PaymentType + " (Chuyển khoản | Tiền mặt) <br />Ghi chú: " + order.Note + " <br /> <br />";
                //strBody = strBody + "<h3>Đơn hàng số: " + order.OrderId.ToString() + "</h3>";
                //SendEmail(order.Email, strBody);
                //return RedirectToAction("Complete",
                //    new { id = order.OrderId });
                return Json(new { id = order.OrderCode });

            }
            catch
            {

                //Invalid - redisplay with errors
                //return View(order);
                return Json(new { id = "" });
            }
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
