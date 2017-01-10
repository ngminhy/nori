using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.Models
{
    public class Video
    {
        public int VideoId { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Link { get; set; }
        [StringLength(200)]
        public string Filename { get; set; }
          
        public bool IsDeleted { get; set; }
        public string FullPath { get { return "/Content/Images/Videos/" + Filename; } }
    }
}
