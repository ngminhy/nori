using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Biz4CMS.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace Biz4CMS.Controllers
{
    public class ShoppingCartController : Controller
    {
        Biz4Db storeDB = new Biz4Db();
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            if (HttpContext.Session["userinfo"] == null)
            {
                return RedirectToAction("Index", "Order");
            }
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            ViewBag.Cart = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            var userinfo = (UserInfo)HttpContext.Session["userinfo"];
            var order = new Order();
            order.Email = userinfo.Email;
            order.FullName = userinfo.Name;
            order.Phone = userinfo.Phone;
            order.Address = userinfo.Address;
            order.Note = userinfo.Note;
            order.BookingTime = userinfo.BookingTime;
            order.BranchName = userinfo.BranchName;
            return View(order);
        }
        private bool SendEmail(string toEmail,string strBody)
        {

            
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            //message.To.Add("sale@sqshops.com"); //recipient
            message.To.Add("sonquynhshop@gmail.com");
            message.To.Add(toEmail);
            message.Subject = "Thông tin đơn hàng - sqshops.com";
            message.From = new System.Net.Mail.MailAddress("sonquynhshop@gmail.com"); //from email
            message.Body = strBody;
            message.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");// you need an smtp server address to send emails
            smtp.UseDefaultCredentials =false;
            smtp.DeliveryMethod =  System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential("sonquynhshop@gmail.com", "sq!23456");

            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(message);

            return true;


        }

     

        // GET: /ShoppingCart/Complete
        public ActionResult Complete()
        {

            // Return the view
           
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
        // POST: /ShoppingCart/
        [HttpPost]
        public ActionResult Index(FormCollection values,int total)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {

                //order.Username = User.Identity.Name;
                var userinfo =(UserInfo) HttpContext.Session["userinfo"];
                order.Username = userinfo.Email;
                order.FullName = userinfo.Name;
                order.Phone = userinfo.Phone;
                order.Note = userinfo.Note;
                order.Address = userinfo.Address;
                order.Total = total;
                order.OrderStatusId = 1;
                order.OrderDate = DateTime.Now;
                order.BookingTime = userinfo.BookingTime;
                order.BranchName = userinfo.BranchName;
                order.OrderCode = GetUniqueKey(8);
                //Save Order
                storeDB.Orders.Add(order);
                storeDB.SaveChanges();
                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                //string strBody = cart.CreateOrderEmail();
                
                
                cart.CreateOrder(order);
                //strBody = strBody + "<h2>Thông tin người mua:</h2>Họ và Tên:  " + order.FullName + " <br />Số điện thoại: " + order.Phone + " <br />Email: " + order.Email + " <br />Địa chỉ: " + order.Address + " <br />Thanh toán: " + order.PaymentType + " (Chuyển khoản | Tiền mặt) <br />Ghi chú: " + order.Note + " <br /> <br />";
                //strBody = strBody + "<h3>Đơn hàng số: " + order.OrderId.ToString() + "</h3>";
                //SendEmail(order.Email, strBody);
                //return RedirectToAction("Complete",
                //    new { id = order.OrderId });
                return Json(new { id = order.OrderCode });

            }
            catch { 

                //Invalid - redisplay with errors
                //return View(order);
            return Json(new { id = "" });
            }
        }
        //
        // GET: /ShoppingCart/AddToCart/5
        public ActionResult AddToCart(int id, int count = 1, string listCakeFiller = "", string listCakeName = "" , long price =0)
        {
            // Retrieve the Product from the database
            var addedProduct = storeDB.Products
                .Single(Product => Product.ProductId == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct, count, listCakeFiller, listCakeName, price);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index", "product"); 
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the Product to display confirmation
            string ProductName = storeDB.Carts
                .Single(item => item.RecordId == id).Product.Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemove
            {
                Message = Server.HtmlEncode(ProductName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult UpdateCart(IList<int> rlist, IList<int> qlist)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the Product to display confirmation
            for(var index =0 ; index < rlist.Count; index++)
            {
                cart.UpdateQuantity(rlist[index], qlist[index]);                
            }
            // Display the confirmation message
            var results = new ShoppingCartRemove
            {
                Message ="Updated",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}
