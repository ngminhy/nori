using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Text.RegularExpressions;

namespace Biz4CMS.Areas.Admin.Controllers
{
   [Authorize]
    public class CategoryController : Controller
    {
        //
        // GET: /bo/Category/
        Biz4Db db = new Biz4Db(); 
        public ActionResult Index()
        {
                                return View();
        }
  
        public JsonResult Get([DataSourceRequest]DataSourceRequest request) {
            var Categorys = db.Categorys.OrderBy(p=> p.Tag).OrderByDescending(p=> p.CategoryId).ToList();
            return this.Json(Categorys.ToDataSourceResult(request),JsonRequestBehavior.AllowGet );
        
        }
        
        // GET: /bo/Category/Create

        public ActionResult Create()
        {
            var Category = new Category();
            return View(Category);
        }

        //
        // POST: /bo/Category/Create

        [HttpPost]
        public ActionResult Create(Category  model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.PageURL)) model.PageURL = ConvertToUnSign(model.Name.ToLower());
                db.Categorys.Add(model);
                db.SaveChanges(); 
            }
            return RedirectToAction("Index");
           
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var updateCategory = db.Categorys.FirstOrDefault(p => p.CategoryId == model.CategoryId);
                if (updateCategory != null)
                {
                    TryUpdateModel(updateCategory);
                    if (string.IsNullOrEmpty(updateCategory.PageURL)) updateCategory.PageURL = ConvertToUnSign(updateCategory.Name.ToLower());
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
        //
        // GET: /bo/Category/Edit/5

        public ActionResult Edit(int id)
        {
            var Category = db.Categorys.FirstOrDefault(p => p.CategoryId == id); 
            return View("Create",Category);
        }

        //
        

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, Category  model)
        {
            var CategoryToDelete = db.Categorys.First(p => p.CategoryId  == model.CategoryId );

            if (CategoryToDelete != null)
            {
                db.Categorys.Remove(CategoryToDelete);
                db.SaveChanges();
            }

            return Json(new[] { CategoryToDelete }.ToDataSourceResult(request));
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
