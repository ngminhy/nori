using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public int ParentId { get; set; }
        [StringLength(200)]
        public string Text { get; set; }
        [StringLength(200)]
        public string Link { get; set; }
        [StringLength(200)]
        public string Tag { get; set; }

        public int Order { get; set; }
        public bool IsDeleted { get; set; }
    }
}
