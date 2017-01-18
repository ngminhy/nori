using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biz4CMS.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Biz4CMS.ViewModels
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Folder { get; set; }
        public string MainImage { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public bool Active { get; set; }
        public string ImagesJson { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BasePrice { get; set; }
        public int Price { get; set; }
        public bool IsSpecial { get; set; }
        public int BrandId { get; set; }
        public int CountryId { get; set; }
        public string Code { get; set; }
        public string PageURL { get; set; }
        public int MaxFiller { get; set; }
        public ProductDto(Product model)
        {
            ProductId = model.ProductId;
            CategoryId = model.CategoryId;
            BrandId = model.BrandId;
            Code = model.Code;
            CountryId = model.CountryId;
            MaterialId = model.MaterialId;
            Description = model.Description;
            Content = model.Content;
            MainImage = model.MainImage;
            Tags = model.Tags;
            Folder = model.Folder;
            ImagesJson = model.ImagesJson;
            Name = model.Name;
            Active = model.Active;
            IsSpecial = model.IsSpecial;
            BasePrice = model.BasePrice;
            Price = model.Price;
            PageURL = model.PageURL;
            MaxFiller = model.MaxFiller;

        }
        public ProductDto()
        { }



        public int MaterialId { get; set; }
    }
}