using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biz4CMS.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Biz4CMS.ViewModels
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPageURL { get; set; }
        public List<BriefProductDto> Products { get; set; }
        public int NumofItem { get; set; }


    }
}