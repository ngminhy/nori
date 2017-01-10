using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.Models
{
    public class Banner
    {
        public int BannerId { get; set; }
        [StringLength(200)]
        public string Link { get; set; }
        [StringLength(200)]
        public string Filename { get; set; }
        [StringLength(200)]
        public string Heading1 { get; set; }
        [StringLength(200)]
        public string Heading2 { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }  
        [StringLength(200)]
        public string Tag { get; set; }
        [StringLength(200)]
        public string Align { get; set; }

        public bool IsDeleted { get; set; }
        public string FullPath { get {return "/Content/Images/Banners/" + Filename; }  }
    }
}
