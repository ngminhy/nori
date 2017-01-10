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
    public class QuickLinkController : Controller
    {


        //
        // GET: /Admin/QuickLink/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            var QuickLinks = db.QuickLinks.OrderByDescending(p => p.QuickLinkId).ToList();
            return View(QuickLinks);
        }


        // GET: /Admin/QuickLink/Create

        public ActionResult Create()
        {
            //var QuickLink = new QuickLink();
            //ViewBag.QuickLinks = db.QuickLinks.Where(p => p.Tag == "QuickLink").ToList();
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var QuickLinks = db.QuickLinks.OrderByDescending(p => p.QuickLinkId).ToList();
            return this.Json(QuickLinks.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<QuickLink> QuickLinks)
        {
            var results = new List<QuickLink>();

            if (QuickLinks != null && ModelState.IsValid)
            {
                foreach (var QuickLink in QuickLinks)
                {

                    db.QuickLinks.Add(QuickLink);
                    results.Add(QuickLink);
                }
                db.SaveChanges();
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<QuickLink> QuickLinks)
        {
            if (QuickLinks != null && ModelState.IsValid)
            {
                foreach (var QuickLink in QuickLinks)
                {
                    var target = db.QuickLinks.FirstOrDefault(p => p.QuickLinkId == QuickLink.QuickLinkId);
                    if (target != null)
                    {
                        target.Filename = QuickLink.Filename;
                        target.Title = QuickLink.Title;
                        target.Description  = QuickLink.Description ;
                        target.Order = QuickLink.Order;
                        target.Tag = QuickLink.Tag;
                        target.Link = QuickLink.Link;
                    }

                }
                db.SaveChanges();
            }

            return Json(QuickLinks.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<QuickLink> QuickLinks)
        {
            if (QuickLinks.Any())
            {
                foreach (var QuickLink in QuickLinks)
                {
                    var QuickLinkToDelete = db.QuickLinks.First(p => p.QuickLinkId == QuickLink.QuickLinkId);
                    if (QuickLinkToDelete != null)
                    {
                        db.QuickLinks.Remove(QuickLinkToDelete);
                    }
                }
                db.SaveChanges();
            }

            return Json(QuickLinks.ToDataSourceResult(request, ModelState));
        }




    }
}
