using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(200)]
        public string Tag { get; set; }
        public bool IsDeleted { get; set; }
        public string PageURL { get; set; }
        public int SortNumber { get; set; }
    }
}
