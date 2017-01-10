using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }
        [StringLength(200)]
        public string JobTitle { get; set; }
        
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(200)]
        public string Tag { get; set; }
        public int Order { get; set; }

    }
}
