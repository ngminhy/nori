using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.ViewModels
{
    public class PictureDto
    {
        public int PictureId { get; set; }
        public string Name { get; set; }
        public string Src { get; set; }
        public string Tags { get; set; }
        public bool IsCover { get; set; }
        public string ProductFolder { get; set; }
        public string Thumbnail { get { return "/Content/Images/Products/" + ProductFolder +"/"+ Src; } }
        public string Image { get { return "/Content/Images/Products/" + ProductFolder + "/" + Src; } }
        //public string MoreInfo { get; set; }
        //public int ProductId { get; set; }
        //public double Size { get; set; }
    }
}