using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
namespace Biz4CMS.Models
{
    public class FAQ
    {
        public int FAQId { get; set; }
        [StringLength(500)]
        public string Question { get; set; }
        [StringLength(200)]
        public string Author { get; set; }
        
        [StringLength(4000)]
        [AllowHtml]
        public string Description { get; set; }
        [StringLength(200)]
        public string Tag { get; set; }
        public int Order { get; set; }

    }
}
