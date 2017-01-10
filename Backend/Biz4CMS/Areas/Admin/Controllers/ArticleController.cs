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
    public class ArticleController : Controller
    {
        //
        // GET: /bo/Article/
        Biz4Db db = new Biz4Db(); 
        public ActionResult Index()
        {
                                return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request) {
            var articles = db.Articles.OrderByDescending(p=> p.ArticleId).ToList();
            return this.Json(articles.ToDataSourceResult(request),JsonRequestBehavior.AllowGet );
        
        }
        
        // GET: /bo/Article/Create

        public ActionResult Create()
        {
            var article = new Article();
            article.CreatedDate = DateTime.Now;
            article.PublicDate = DateTime.Now;
            article.Order = 0;
            article.Active = true;
            ViewBag.Categorys = db.Categorys.Where(p => p.Tag == "Article").ToList();
            return View(article);
        }

        //
        // POST: /bo/Article/Create

        [HttpPost]
        public ActionResult Create(Article  model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.PageURL)) model.PageURL = ConvertToUnSign(model.Title.ToLower());
                db.Articles.Add(model);
                db.SaveChanges(); 
            }
            return RedirectToAction("Index");
           
        }
        [HttpPost]
        public ActionResult Edit(Article model)
        {
            if (ModelState.IsValid)
            {
                var updateArticle = db.Articles.FirstOrDefault(p => p.ArticleId == model.ArticleId);
                if (updateArticle != null)
                {
                    
                    TryUpdateModel(updateArticle);
                    if (string.IsNullOrEmpty(updateArticle.PageURL)) updateArticle.PageURL = ConvertToUnSign(updateArticle.Title.ToLower());
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
        //
        // GET: /bo/Article/Edit/5

        public ActionResult Edit(int id)
        {
            var article = db.Articles.FirstOrDefault(p => p.ArticleId == id);
            ViewBag.Categorys = db.Categorys.Where(p => p.Tag == "Article").ToList();
            return View("Create",article);
        }

        //
        

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, Article  model)
        {
            var articleToDelete = db.Articles.First(p => p.ArticleId  == model.ArticleId );
            if (articleToDelete != null)
            {
                db.Articles.Remove(articleToDelete);
                db.SaveChanges();
            }
            return Json(new[] { articleToDelete }.ToDataSourceResult(request));
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
