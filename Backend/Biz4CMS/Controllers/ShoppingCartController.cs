using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Biz4CMS.ViewModels;

namespace Biz4CMS.Controllers
{
    public class ShoppingCartController : Controller
    {
        Biz4Db storeDB = new Biz4Db();
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            ViewBag.Cart = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View();
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
        // POST: /ShoppingCart/
        [HttpPost]
        public ActionResult Index(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {

                //order.Username = User.Identity.Name;
                order.Username = new Guid().ToString();
                order.OrderDate = DateTime.Now;

                //Save Order
                storeDB.Orders.Add(order);
                storeDB.SaveChanges();
                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                string strBody = cart.CreateOrderEmail();
                cart.CreateOrder(order);
                strBody = strBody + "<h2>Thông tin người mua:</h2>Họ và Tên:  " + order.FullName + " <br />Số điện thoại: " + order.Phone + " <br />Email: " + order.Email +" <br />Địa chỉ: " + order.Address +" <br />Thanh toán: " + order.PaymentType  +" (Chuyển khoản | Tiền mặt) <br />Ghi chú: " + order.Note + " <br /> <br />";
                strBody = strBody + "<h3>Đơn hàng số: " + order.OrderId.ToString() + "</h3>";  
                SendEmail(order.Email,strBody );
                return RedirectToAction("Complete",
                    new { id = order.OrderId });

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id, int count = 1)
        {
            // Retrieve the Product from the database
            var addedProduct = storeDB.Products
                .Single(Product => Product.ProductId == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct, count);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
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
