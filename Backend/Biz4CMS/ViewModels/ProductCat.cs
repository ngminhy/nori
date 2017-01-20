using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biz4CMS.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Biz4CMS.ViewModels
{
    public class ProductCat
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Price { get; set; }
        public string Code { get; set; }
        public string PageURL { get; set; }
        public string CategoryPageURL { get; set; }
        public int MaxFiller { get; set; }


    }
}