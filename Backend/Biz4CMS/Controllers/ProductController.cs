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
            //if (HttpContext.Session["userinfo"] == null)
            //{
            //    return RedirectToAction("Index", "Order");
            //}
            var CakeFillers = db.CakeFillers.Select(p => new
            {
                CakeFillerId = p.CakeFillerId,
                FillerImage = p.FillerImage,
                Name = p.Name,
                Price = p.Price
            }).ToList().OrderBy(p => p.Name)
                .Select(x => new CakeFiller()
                {
                    CakeFillerId = x.CakeFillerId,
                    FillerImage = x.FillerImage,
                    Name = x.Name,
                    Price = x.Price
                }).ToList();

            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            ViewBag.Cart = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

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
        public List<ProductCategory> GetTopProduct()
        {
            var products = db.Database.SqlQuery<ProductCat>("getTopProduct").ToList();
            var productcats = new List<ProductCategory>();
            var preCat1 = -1;
            var productCategory = new ProductCategory();
            foreach (var item in products)
            {
                if (preCat1 != item.CategoryId)
                {
                    if ( preCat1 > -1) {
                        productcats.Add(productCategory);
                    }
                    productCategory = new ProductCategory();
                    productCategory.Products = new List<BriefProductDto>();
                    productCategory.CategoryName = item.CategoryName;
                    productCategory.CategoryId = item.CategoryId;
                    productCategory.CategoryPageURL = item.CategoryPageURL;
                    preCat1 = item.CategoryId;
                }
                var product = new BriefProductDto(item);

                productCategory.Products.Add(product);
                productCategory.NumofItem++;
            }
            productcats.Add(productCategory);
            return productcats;
        }


    }
}
