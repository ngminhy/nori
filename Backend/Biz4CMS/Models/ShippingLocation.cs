using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biz4CMS.Models
{
    public class ShippingLocation
    {
        public int ShippingLocationId { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        public string Polyobj { get; set; }
        public bool Active { get; set; }
    }
}