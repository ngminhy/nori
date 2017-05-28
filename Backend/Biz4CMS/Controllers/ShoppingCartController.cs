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
            // if (HttpContext.Session["userinfo"] == null)
            // {
            //     return RedirectToAction("Index", "product");
            // }
            // var cart = ShoppingCart.GetCart(this.HttpContext);

            // // Set up our ViewModel
            // ViewBag.Cart = new ShoppingCartViewModel
            // {
            //     CartItems = cart.GetCartItems(),
            //     CartTotal = cart.GetTotal()
            // };
            // // Return the view
            // var userinfo = (UserInfo)HttpContext.Session["userinfo"];
            // var order = new Order();
            // order.Email = userinfo.Email;
            // order.FullName = userinfo.Name;
            // order.Phone = userinfo.Phone;
            // order.Address = userinfo.Address;
            // order.Note = userinfo.Note;
            // order.BookingTime = userinfo.BookingTime;
            // order.BranchName = userinfo.BranchName;
            // return View(order);
         
            return View();
        }
        
        public ActionResult Checkout()
        {
            var Locations = storeDB.Location.OrderByDescending(p => p.LocationId).ToList();
            ViewBag.data = Locations;
            var polyobj = storeDB.ShippingLocation.Where(p => p.Active).OrderByDescending(p => p.ShippingLocationId).ToList();
            ViewBag.polyobj = polyobj;
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
            //clear session
            HttpContext.Session["userinfo"] = null;
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
        public ActionResult Index(UserInfo info, List<CartItem> cart)
        {

            try
            {

                //check not exist account then save account


                //save to order and orderdetail
                var order = new Order();
                order.FullName = info.Name;
                order.Email = info.Email;
                order.Address = info.Address;
                order.Phone = info.Phone;
                order.Note = info.Note;
                order.Total = 0;
                order.PaymentType = 1;
                order.OrderStatusId = 1;
                order.OrderDate = DateTime.Now;
                order.OrderCode = GetUniqueKey(8);
                //Save Order
                storeDB.Orders.Add(order);
                storeDB.SaveChanges();

                decimal orderTotal = 0;

                // adding the order details for each
                foreach (var item in cart)
                {
                    var productitem = storeDB.Products.Where(p => p.ProductId == item.id).FirstOrDefault();
                    var productprice = item.price;
                    if (productitem != null)
                    {
                        productprice = productitem.Price;
                    }

                    var orderDetail = new OrderDetail
                    {
                        ProductId = item.id,
                        OrderId = order.OrderId,
                        UnitPrice = productprice,
                        Quantity = item.count
                    };
                    // Set the order total of the shopping cart
                    orderTotal += (item.count * productprice);
                    storeDB.OrderDetails.Add(orderDetail);
                }
                order.Total = orderTotal;
                storeDB.SaveChanges();

                //send mail to customer
                var strBody = "";
                var spayment = "Tiền mặt";
                if (order.PaymentType == 2)
                {
                    spayment = "Chuyển khoản";
                }
                strBody = strBody + "<h2>Cảm ơn quý khách đã đặt hàng. Chúng tôi sẽ sớm xử lý đơn hàng của quý khách.</h2> <br />";
                strBody = strBody + "<h3>Mã số đơn hàng của quý khách : " + order.OrderCode + "</h3>";
                var subject = "Thông tin đơn hàng từ epsi.vn";
                try
                {
                    //UtilHelper.SendEmail(order.Email, subject, strBody);
                }
                catch
                {

                }

                return Json(new { id = order.OrderCode });

            }
            catch
            {
                //Invalid - redisplay with errors
                return Json(new { id = "" });
            }
        }
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
                    " đã xóa khỏi giỏ hàng.",
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
                Message ="Đã thay đổi.",
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


        public JsonResult getPromotionByCode(string code)
        {
            var datepromotion = DateTime.Now;
            var dayofweek = ((int)datepromotion.DayOfWeek +1).ToString();

            var promotion = storeDB.Promotions.Where(p => p.Active && p.Code == code && p.FromDate <= datepromotion && p.ToDate >= datepromotion && p.ListDayofWeek.Contains(dayofweek)).SingleOrDefault();
            var objPromotion = new { isDiscount = false, value = 0 };
            if (promotion != null)
            {
                objPromotion = new { isDiscount = true, value = promotion.PercentDiscount };
            }
            return Json(objPromotion, JsonRequestBehavior.AllowGet);
        }
    }
}
