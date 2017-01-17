using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Biz4CMS.ViewModels;
using System.Configuration;

namespace Biz4CMS.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/
        Biz4Db db = new Biz4Db();
        public ActionResult Index(string pageURL, int? page)
        {
            int pageSize = 0;
            int.TryParse(ConfigurationManager.AppSettings["PageSize"].ToString(), out pageSize);
            if (pageSize == 0) { pageSize = 8; }
            int index = page.HasValue ? page.Value : 1;
            int CategoryID = 0;
            ViewBag.PageIndex = index;
            ViewBag.Category = db.Categorys.Where(p => p.PageURL == pageURL).FirstOrDefault();
            if (!string.IsNullOrEmpty(pageURL))
            {
                ViewBag.Title = ViewBag.Category.Name + (ViewBag.PageIndex > 1 ? " - Page: " + ViewBag.PageIndex : "");
                ViewBag.Description = ViewBag.Category.Description;
                CategoryID = ViewBag.CategoryID = ViewBag.Category.CategoryId;
            }
            else
            {
                ViewBag.Title = "Tin tức" + (ViewBag.PageIndex > 1 ? " - Page: " + ViewBag.PageIndex : "");
                ViewBag.CategoryID = 0;
            }
            var articles = db.Articles.Where(p => (CategoryID == 0 || p.CategoryId == CategoryID) && p.Active).OrderByDescending(p => p.ArticleId).Skip((index - 1) * pageSize).Take(pageSize).Select(p => new BriefArticleDto()
            {
                ArticleId = p.ArticleId,
                MainImage = p.MainImage,
                Description = p.Description,
                Title = p.Title,
                PageURL = p.PageURL


            }).ToList();
            if (articles == null || articles.Count == 0) return RedirectToAction("index", "home");


            ViewBag.TotalPage = (int)Math.Ceiling(((double)db.Articles.Where(p => (CategoryID == 0 || p.CategoryId == CategoryID) && p.Active).Count()) / pageSize);
            return View(articles);
        }



        public ActionResult Details(string pageURL)
        {
            var article = db.Articles.Where(p => p.PageURL == pageURL && p.Active).FirstOrDefault();
            if (article == null) article = db.Articles.Where(p => p.Active).OrderByDescending(p => p.ArticleId).FirstOrDefault();
            if (article == null) return RedirectToAction("index", "home");
            ViewBag.RelatedArticles = db.Articles.Where(p => p.CategoryId == article.CategoryId && p.Active).OrderByDescending(p => p.ArticleId).Take(30).AsEnumerable().Select(p => new LinkDto() { Title = p.Title, Link = string.Format("{0}", p.PageURL) }).ToList();
            return View(article);
        }

    }
}
