using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz4CMS.ViewModels
{
   
    public class CartItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string img { get; set; }
        public int count { get; set; }
    }
}