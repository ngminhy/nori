using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz4CMS.ViewModels
{
    public class BriefProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int BasePrice { get; set; }
        public int MaxFiller { get; set; }
        public int Price { get; set; }
        //public string Tags { get; set; }
        //public bool IsDeleted { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public int NumofViews { get; set; }
        //public virtual List<Picture> Pictures { get; set; }
        //public string MainImage { get; set; }
        //public string Folder { get; set; }
        //public string ImagesJson { get; set; }
        public string Folder { get; set; }
        public string PageURL { get; set; }
        public BriefProductDto() { }

        public BriefProductDto(ProductCat item)
        {
            this.Name = item.Name;
            this.Description = item.Description;
            this.MainImage = item.MainImage;
            this.Price = item.Price;
            this.Code = item.Code;
            this.PageURL = item.PageURL;
            this.MaxFiller = item.MaxFiller;
        }

    }
}