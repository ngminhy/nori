using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Biz4CMS.Models;
using Kendo.Mvc.Extensions;

namespace Biz4CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class BannerController : Controller
    {


        //
        // GET: /Admin/Banner/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            //var Banners = db.Banners.Where(p => p.ProductId == ProductId).OrderByDescending(p => p.BannerId).ToList();
            return View();
        }


        // GET: /Admin/Banner/Create

        public ActionResult Create()
        {
            //var Banner = new Banner();
            //ViewBag.Banners = db.Banners.Where(p => p.Tag == "Banner").ToList();
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var Banners = db.Banners.OrderByDescending(p => p.BannerId).ToList();
            return this.Json(Banners.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Banner> Banners)
        {
            var results = new List<Banner>();

            if (Banners != null && ModelState.IsValid)
            {
                foreach (var Banner in Banners)
                {

                    db.Banners.Add(Banner);
                    results.Add(Banner);
                }
                db.SaveChanges();
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Banner> Banners)
        {
            if (Banners != null && ModelState.IsValid)
            {
                foreach (var Banner in Banners)
                {
                    var target = db.Banners.FirstOrDefault(p => p.BannerId == Banner.BannerId);
                    if (target != null)
                    {
                        target.Filename = Banner.Filename;
                        target.Tag = Banner.Tag;
                        target.Link = Banner.Link;
                        target.Align = Banner.Align;
                        target.Heading1 = Banner.Heading1;
                        target.Heading2 = Banner.Heading2;
                        target.Description = Banner.Description;
                    }

                }
                db.SaveChanges();
            }

            return Json(Banners.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Banner> banners)
        {
            if (banners.Any())
            {
                foreach (var banner in banners)
                {
                    var BannerToDelete = db.Banners.First(p => p.BannerId == banner.BannerId);
                    if (BannerToDelete != null)
                    {
                        db.Banners.Remove(BannerToDelete);
                    }
                }
                db.SaveChanges();
            }

            return Json(banners.ToDataSourceResult(request, ModelState));
        }




    }
}
