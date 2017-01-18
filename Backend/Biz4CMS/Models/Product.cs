using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Biz4CMS.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Biz4CMS.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Code { get; set; }
        public int CountryId { get; set; }
        public Category Country { get; set; }
        public int BrandId { get; set; }
        public Category Brand { get; set; }
        public int MaterialId { get; set; }
        public Category Material { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        
        [AllowHtml]
        public String Content { get; set; }

        [StringLength(200)]
        public string Tags { get; set; }
        public bool Active { get; set; }
        public bool IsSpecial { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NumofViews { get; set; }
        public int BasePrice { get; set; }
        public int Discount { get; set; }
        public int Price { get; set; }
        public virtual List<Picture> Pictures { get; set; }
        [StringLength(200)]
        public string MainImage { get; set; }
        [StringLength(50)]
        public string Folder { get; set; }
        public string ImagesJson { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string PageURL { get; set; }
        public int MaxFiller { get; set; }

        public Product() { }
        public Product(ProductDto model)
        {
            ProductId = model.ProductId;
            CategoryId = model.CategoryId;
            Code = model.Code;
            BrandId = model.BrandId;
            CountryId = model.CountryId;
            MaterialId = model.MaterialId;
            Description = model.Description;
            Tags = model.Tags;
            Folder = model.Folder;
            ImagesJson = model.ImagesJson;
            Name = model.Name;
            Content = model.Content;
            Active = model.Active;
            MainImage = model.MainImage;
            IsSpecial = model.IsSpecial;
            Price = model.Price;
            BasePrice = model.BasePrice;
            CreatedDate = DateTime.Now;
            PageURL = model.PageURL;
            MaxFiller = model.MaxFiller;
        }
    }
}
