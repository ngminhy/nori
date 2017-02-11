using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biz4CMS.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public bool Active { get; set; }
    }
}