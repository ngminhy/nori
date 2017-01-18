using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Biz4CMS.ViewModels;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Biz4CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        //
        // GET: /Admin/product/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var Products = (from p in db.Products
                            join c in db.Categorys
                            on p.CategoryId equals c.CategoryId
                            orderby p.ProductId descending
                            select new ProductDto
                            {
                                ProductId = p.ProductId,
                                CategoryId = p.CategoryId,
                                BrandId = p.BrandId,
                                CountryId = p.CountryId,
                                Code = p.Code,
                                MaterialId = p.MaterialId,

                                Description = p.Description,
                                Tags = p.Tags,
                                Folder = p.Folder,
                                ImagesJson = p.ImagesJson,
                                Name = p.Name,
                                CategoryName = c.Name,
                                PageURL = p.PageURL
                            }).ToList();

            return this.Json(Products.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

        // GET: /Admin/product/Create

        public ActionResult Create()
        {
            var Product = new ProductDto();
            //product.CreatedDate = DateTime.Now;
            ViewBag.Categorys = db.Categorys.Where(p => p.Tag == "product").ToList();
            ViewBag.Brands = db.Categorys.Where(p => p.Tag == "brand").ToList();
            ViewBag.Countries = db.Categorys.Where(p => p.Tag == "country").ToList();
            ViewBag.Materials = db.Categorys.Where(p => p.Tag == "material").ToList();

            return View(Product);
        }

        //
        // POST: /Admin/product/Create

        [HttpPost]
        public ActionResult Create(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.PageURL)) model.PageURL = ConvertToUnSign(model.Name.ToLower());
                Product Product = new Product(model);                
                if (model.ImagesJson != null)
                {
                    List<FileInfo> images = JsonConvert.DeserializeObject<List<FileInfo>>(model.ImagesJson);
                    if (images.Any(p => p.EntryType == 0))
                    {
                        Product.Pictures = new List<Picture>();
                        foreach (var image in images.Where(p => p.EntryType == 0))
                        {
                            Picture pic = new Picture();
                            pic.Src = image.Name;
                            pic.Size = image.Size;
                            Product.Pictures.Add(pic);
                        }
                    }
                }
                db.Products.Add(Product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Edit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var updateProduct = db.Products.FirstOrDefault(p => p.ProductId == model.ProductId);
                if (updateProduct != null)
                {
                    TryUpdateModel(updateProduct);
                    if (string.IsNullOrEmpty(updateProduct.PageURL)) updateProduct.PageURL = ConvertToUnSign(updateProduct.Name.ToLower());
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
        //
        // GET: /Admin/product/Edit/5

        public ActionResult Edit(int id)
        {
            var ProductDto = new ProductDto(  db.Products.FirstOrDefault(p => p.ProductId == id));
            ViewBag.Categorys = db.Categorys.Where(p => p.Tag == "product").ToList();
            ViewBag.Brands = db.Categorys.Where(p => p.Tag == "brand").ToList();
            ViewBag.Countries = db.Categorys.Where(p => p.Tag == "country").ToList();
            ViewBag.Materials = db.Categorys.Where(p => p.Tag == "material").ToList();

            return View("Create", ProductDto);
        }

        //


        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ProductDto model)
        {
            var ProductToDelete = db.Products.First(p => p.ProductId == model.ProductId);

            if (ProductToDelete != null)
            {
                db.Products.Remove(ProductToDelete);
                db.SaveChanges();
            }

            return Json(new[] { ProductToDelete }.ToDataSourceResult(request));
        }
        public static string ConvertToUnSign(string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
