using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        [StringLength(200)]
        public string ListCakeFiller { get; set; }
        public string ListCakeName { get; set; }
        public long Price { get; set; }
        public string HashKey { get; set; }
        public virtual Product Product { get; set; }
    }
}