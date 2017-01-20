using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.ViewModels;
using Biz4CMS.Models;

namespace Biz4CMS.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /product/
        Biz4Db db = new Biz4Db();
        public ActionResult Details(string pageURL)
        {
            var Product = db.Products.Where(p => p.PageURL == pageURL && p.Active).FirstOrDefault();
            if (Product != null)
            {
                ViewBag.Brand = GetCategoryName(Product.BrandId);
                ViewBag.Country = GetCategoryName(Product.CountryId);
                ViewBag.RelatedProducts = db.Products.Where(p => (p.CategoryId == Product.CategoryId) && p.Active).OrderByDescending(p => p.Price).Take(6).Select(a => new BriefProductDto
                {
                    Name = a.Name, // or pc.ProdId
                    ProductId = a.ProductId,
                    MainImage = a.MainImage == null ? "" : a.MainImage,
                    Description = a.Description == null ? "" : a.Description,
                    Code = a.Code == null ? "" : a.Code,
                    BasePrice = a.BasePrice,
                    Price = a.Price,
                    PageURL = a.PageURL
                });
            }
            return View(Product);
        }

        public ActionResult Index(string pageURL)
        {
            if (HttpContext.Session["userinfo"] == null)
            {
                return RedirectToAction("Index", "Order");
            }
            var CakeFillers = db.CakeFillers.Select(p => new
            {
                CakeFillerId = p.CakeFillerId,
                FillerImage = p.FillerImage,
                Name = p.Name,
                Price = p.Price
            }).ToList()
                .Select(x => new CakeFiller()
                {
                    CakeFillerId = x.CakeFillerId,
                    FillerImage = x.FillerImage,
                    Name = x.Name,
                    Price = x.Price
                }).ToList();



            ViewData["CakeFillers"] = CakeFillers;
            if (!string.IsNullOrEmpty(pageURL))
            {

                var Category = db.Categorys.Where(p => (p.PageURL == pageURL)).FirstOrDefault();
                var Products = db.Products.Where(p => (p.CategoryId == Category.CategoryId) && p.Active).OrderByDescending(p => p.Price).Select(a => new BriefProductDto
                {
                    Name = a.Name, // or pc.ProdId
                    ProductId = a.ProductId,
                    MainImage = a.MainImage == null ? "" : a.MainImage,
                    Description = a.Description == null ? "" : a.Description,
                    Code = a.Code == null ? "" : a.Code,
                    BasePrice = a.BasePrice,
                    Price = a.Price,
                    PageURL = a.PageURL
                    // other assignments
                });
                if (CakeFillers != null) ViewBag.CakeFillers = CakeFillers;
                if (Category != null) ViewBag.GalleryName = Category.Name;
                return View(Products);
            }
            else
            {
                var ProductCats = GetTopProduct();
                return View("Home", ProductCats);
            }

        }

        private string GetCategoryName(int categoryID)
        {
            var category = db.Categorys.FirstOrDefault(p => p.CategoryId == categoryID);
            if (category != null) return category.Name;
            return "";
        }
        public List<ProductCat> GetTopProduct()
        {
            var products = db.Database.SqlQuery<ProductCat>("getTopProduct").ToList();
            return products;
        }


    }
}
