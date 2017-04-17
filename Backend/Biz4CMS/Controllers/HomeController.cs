using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Biz4CMS.ViewModels;

namespace Biz4CMS.Controllers
{
    public class HomeController : Controller
    {
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult ProductDetail(int id = 0)
        {
            //var product = new BriefArticleDto();
          var product = db.Products.Where(a => a.ProductId == id).Select(a => new BriefProductDto
            {
                Name = a.Name, // or pc.ProdId
                ProductId = a.ProductId,
                MainImage = a.MainImage == null ? "" : a.MainImage,
                Description = a.Description == null ? "" : a.Description,
                Code = a.Code == null ? "" : a.Code,
                BasePrice = a.BasePrice,
                Price = a.Price,
                PageURL = a.PageURL
            }).FirstOrDefault();
            return PartialView("_ProductDetail", product);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }


		public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Message model)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(model);
                model.CreatedDate = DateTime.Now; 
                db.SaveChanges();
                SendEmailToManager("FullName:" + model.FullName + "<br /> Email:" + model.Email + "<br /> " + model.Content); 
                //return RedirectToAction("index");
            }

            return Json("true", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Banner()
        {
            var banners = db.Banners.Where(p => p.Tag == "Top").OrderBy(p => p.BannerId).ToList();
            return PartialView("_Banner", banners);
        }
        public ActionResult MiddleBanner()
        {
            var banners = db.Banners.Where(p => p.Tag == "Middle").OrderBy(p => p.BannerId).ToList();
            return PartialView("MiddleBanner", banners);
        }
        public ActionResult RightBanner()
        {
            var banners = db.Banners.Where(p => p.Tag == "Right").OrderBy(p => p.BannerId).ToList();
            return PartialView("RightBanner", banners);
        }

        public ActionResult Slider()
        {
            var banners = db.Banners.Where(p => p.Tag == "Top").OrderBy(p => p.BannerId).ToList();
            return PartialView("_Slider", banners);
        }

        //public ActionResult RightMenu()
        //{
        //    var rightMenu = new List<BoxLinkDto>();
        //    var galerry = new BoxLinkDto();
        //    galerry.BoxName = "Gallery";
        //    galerry.BoxLink = "/Gallery";
        //    galerry.BoxType = "Gallery";
        //    galerry.Links = db.Categorys.Where(p => p.Tag == "product").AsEnumerable().Select(p => new LinkDto() { Title = p.Name, Link = "/category/?categoryid=" + p.CategoryId   }).ToList(); ;
        //    rightMenu.Add(galerry);  
        //    var categorys = db.Categorys.Where(p => p.Tag == "Article");
        //    foreach (var item in categorys)
        //    {
        //        var links = db.Articles.Where(p => p.CategoryId == item.CategoryId && p.Active ).OrderByDescending(p => p.ArticleId).Take(10).AsEnumerable().Select(p => new LinkDto() { Title = p.Title, Link = string.Format("/article/details/{0}", p.ArticleId) }).ToList();
        //        var boxLink = new BoxLinkDto();
        //        boxLink.BoxName = item.Name;
        //        boxLink.BoxLink = Url.Action("Index", "Article", new { categoryId = item.CategoryId });
        //        boxLink.Links = links;
        //        rightMenu.Add(boxLink);
 
        //    }
        //    return PartialView("RightMenu",rightMenu);
        //}
        public ActionResult TopMenu()
        {
            var menus = db.Menus.Where(p => p.ParentId == 0 && p.Tag == "Top").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => new LinkDto() { Title = p.Text, Link = p.Link,MenuId = p.MenuId }).ToList();
            foreach (var item in menus)
            {
                item.SubMenus = db.Menus.Where(p => p.ParentId == item.MenuId && p.Tag == "Top").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => new LinkDto() { Title = p.Text, Link = p.Link, MenuId = p.MenuId }).ToList();
            }
            return PartialView("TopMenu", menus);
        }
        public ActionResult BoxMenu(int id,int type = 0 )
        {
            var menu = db.Menus.Where(p => p.MenuId == id && p.Tag == "Right").FirstOrDefault();
            
            ViewBag.MenuTitle = menu.Text;
            ViewBag.MenuLink =  string.IsNullOrEmpty(menu.Link)?"":menu.Link;
            ViewBag.type = type;
            var menus = db.Menus.Where(p => p.ParentId == id && p.Tag == "Right").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => new LinkDto() { Title = p.Text, Link = p.Link }).ToList();
            return PartialView("BoxMenu", menus);
        }
        public ActionResult RightMenu()
        {
            
            var menus = db.Menus.Where(p => p.ParentId == 0 && p.Tag == "Right").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => p.MenuId).ToList();
            return PartialView("RightMenu", menus);
        }

        public ActionResult RightBox()
        {
            return PartialView("RightBox");
        }
        public ActionResult RightMenuB()
        {

            var menus = db.Menus.Where(p => p.ParentId == 0 && p.Tag == "Right").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => p.MenuId).ToList();
            return PartialView("RightMenuB", menus);
        }
        public ActionResult BottomMenu()
        {
            ViewBag.MenuTitle = "Nori Express";
            var menus = db.Menus.Where(p => p.ParentId == 0 && p.Tag == "Top").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => new LinkDto() { Title = p.Text, Link = p.Link }).ToList();
            return PartialView("BottomMenu", menus);
        }
        public ActionResult Textlink()
        {

            var menus = db.Menus.Where(p => p.ParentId == 0 && p.Tag == "Textlink").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => new LinkDto() { Title = p.Text, Link = p.Link }).ToList();
            return PartialView("Textlink", menus);
        }
        public ActionResult UserComment()
        {
            var comments = db.Comments.OrderByDescending(p => p.Order).ThenByDescending(p => p.CommentId).Take(3).ToList();
            return PartialView("UserComment", comments);
        }

        public ActionResult TopProduct()
        {
            var topproducts = db.Products.Where(p => p.IsSpecial ).OrderByDescending(p => p.ProductId).Take(12).Select(model => new BriefProductDto()
            {
                ProductId = model.ProductId,
                Description = model.Description,
                Code = model.Code,
                Folder = model.Folder,
                Name = model.Name,
                MainImage = model.MainImage,
                Price = model.Price,
                BasePrice = model.BasePrice

            }).ToList();
            return PartialView("TopProduct", topproducts);
        }

        public ActionResult TopArticle()
        {
            var topArticles = db.Articles.Where(p => p.Active).OrderByDescending(p => p.ArticleId).Take(3).Select(p => new BriefArticleDto()
            {
                ArticleId = p.ArticleId,
                MainImage = p.MainImage,
                Description = p.Description,
                Title = p.Title,
                PageURL = "/a/" + p.PageURL,
                ButtonLink = string.IsNullOrEmpty( p.ButtonLink) ? "" : p.ButtonLink,
                ButtonName = string.IsNullOrEmpty(p.ButtonName) ? "" : p.ButtonName

            }).ToList();
            return PartialView("TopArticle", topArticles);
        }
        public ActionResult NewProduct()
        {
            var products = db.Products.Where(p => !p.IsSpecial&&p.Active).OrderByDescending(p => p.ProductId).Take(18).Select(model => new BriefProductDto()
            {
                ProductId = model.ProductId,
                Description = model.Description,
                Folder = model.Folder,
                Name = model.Name,
                MainImage = model.MainImage,
                Price = model.Price,
                BasePrice = model.BasePrice

            }).ToList();
            return PartialView("TopProduct", products);
        }

        public ActionResult TopQuickLink()
        {
            var toplinks = db.QuickLinks.Where(p => p.Tag == "Top").OrderByDescending(p => p.Order).ThenByDescending(p => p.QuickLinkId).Take(3).ToList();
            return PartialView("TopQuickLink", toplinks);
        }
        public ActionResult MiddleQuickLink()
        {
            var quicklinks = db.QuickLinks.Where(p => p.Tag == "Middle").OrderByDescending(p => p.Order).ThenByDescending(p => p.QuickLinkId).ToList();
            return PartialView("MiddleQuickLink", quicklinks);
        }
        public ActionResult RightQuickLink()
        {
            var quicklinks = db.QuickLinks.Where(p => p.Tag == "Right").OrderByDescending(p => p.Order).ThenByDescending(p => p.QuickLinkId).ToList();
            return PartialView("RightQuickLink", quicklinks);
        }

        public ActionResult Video()
        {
            var videos = db.Videos.OrderByDescending(p => p.VideoId).Take(5).ToList();
            return PartialView("Video", videos);
        }
        public ActionResult Feedback()
        {
            var comments = db.Comments.OrderByDescending(p => p.Order).ThenByDescending(p => p.CommentId).ToList();
            return PartialView("Feedback", comments);
        }
        public ActionResult Videos()
        {
            var videos = db.Videos.OrderByDescending(p => p.VideoId).ToList();
            return View(videos);
        }
        private bool SendEmailToManager(string strBody, string subject = "Thông tin liên hệ - noriexpress.com")
        {


            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add("trucchi@noriexpress.com"); //recipient
            
            message.Subject = subject ;
            message.From = new System.Net.Mail.MailAddress("noriexpress.sg@gmail.com"); //from email
            message.Body = strBody;
            message.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");// you need an smtp server address to send emails
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential("noriexpress.sg@gmail.com", "nr123$65");

            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(message);
            return true;


        }
      
        public List<ProductCat> GetTopProduct()
        {
            var products = db.Database.SqlQuery<ProductCat>("getTopProduct").ToList();
            return products;
        }
    }
}
