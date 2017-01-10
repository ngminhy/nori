using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.Models
{
    public class QuickLink
    {
        public int QuickLinkId { get; set; }
        [StringLength(200)]

        public string Link { get; set; }
        [StringLength(200)]
        public string Filename { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        
        public int Order { get; set; }
        [StringLength(200)]
        public string Tag { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }        
        public bool IsDeleted { get; set; }
        public string FullPath { get {return "/Content/Images/QuickLinks/" + Filename; }  }
    }
}
