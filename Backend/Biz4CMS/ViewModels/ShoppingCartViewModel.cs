using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biz4CMS.Models;

namespace Biz4CMS.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}