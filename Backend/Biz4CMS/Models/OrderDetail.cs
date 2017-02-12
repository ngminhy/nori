using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biz4CMS.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        [StringLength(200)]
        public string ListCakeFiller { get; set; }
        public string ListCakeName { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}