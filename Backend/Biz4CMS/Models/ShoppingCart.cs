using Biz4CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biz4CMS.Models
{

    public partial class ShoppingCart
    {
        Biz4Db storeDB = new Biz4Db();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();

            //cart.ShoppingCartId = cart.GetCartId(context);
            var userinfo =(UserInfo) context.Session["userinfo"];
            cart.ShoppingCartId = userinfo.Email;
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(Product product, int count = 1,string listCakeFiller = "", string listCakeName = "", long price =0)
        {
            // Get the matching cart and product instances

            var hashkey = listCakeFiller.GetHashCode().ToString();
            var cartItem = storeDB.Carts.OrderByDescending(c=> c.ListCakeFiller).FirstOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == product.ProductId && c.HashKey == hashkey);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductId = product.ProductId,
                    CartId = ShoppingCartId,
                    Count = count,
                    ListCakeFiller = listCakeFiller,
                    HashKey = hashkey,
                    DateCreated = DateTime.Now,
                    ListCakeName = listCakeName,
                    Price = price

                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count += count;
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public void UpdateQuantity(int recordid, int count = 1)
        {
            // Get the matching cart and product instances
            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.RecordId == recordid);

            if (cartItem != null)
            {   cartItem.Count = count;
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = storeDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {

                storeDB.Carts.Remove(cartItem);
                // Save changes
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiply product price by count of that product to get 
            // the current price for each of those Products in the cart
            // sum all product price totals to get the cart total
            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Price).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Price,
                    Quantity = item.Count,
                    ListCakeFiller = item.ListCakeFiller,
                    ListCakeName = item.ListCakeName
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Price);

                storeDB.OrderDetails.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            storeDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }
        // create EmailBody for shopping cart
        public string CreateOrderEmail()
        {

            string strOut = "<h2>Thông tin đơn hàng </h2><table width='100%;border: 1px solid #cccccc;'><tr><th>Tên sản phẩm</th><th>Giá</th><th>Số lượng</th><th>Thành tiên</th></tr>";
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                string sItem = "<tr><td>" + item.Product.Name + "</td><td> " + item.Price + "</td><td>" + item.Count + "</td><td align='right' style='padding-right: 30px;'>" + (item.Price * item.Count) + "</td></tr>";
                orderTotal += (item.Count * item.Price);
                strOut = strOut + sItem;
            }
            strOut = strOut + "<tr><td><b>Tổng cộng</b></td><td></td><td></td><td align='right' style='padding-right: 30px;'>" + orderTotal.ToString() + "</td></tr></table>";
            // Set the order's total to the orderTotal count
            return strOut;
        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
        }
    }
}