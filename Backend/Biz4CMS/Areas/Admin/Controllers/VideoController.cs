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
    public class VideoController : Controller
    {


        //
        // GET: /bo/Video/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            //var Videos = db.Videos.OrderByDescending(p => p.VideoId).ToList();
            return View();
        }


        // GET: /bo/Video/Create

        public ActionResult Create()
        {
            //var Video = new Video();
            //ViewBag.Videos = db.Videos.Where(p => p.Tag == "Video").ToList();
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var Videos = db.Videos.OrderByDescending(p => p.VideoId).ToList();
            return this.Json(Videos.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Video> Videos)
        {
            var results = new List<Video>();

            if (Videos != null && ModelState.IsValid)
            {
                foreach (var Video in Videos)
                {

                    db.Videos.Add(Video);
                    results.Add(Video);
                }
                db.SaveChanges();
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Video> Videos)
        {
            if (Videos != null && ModelState.IsValid)
            {
                foreach (var Video in Videos)
                {
                    var target = db.Videos.FirstOrDefault(p => p.VideoId == Video.VideoId);
                    if (target != null)
                    {
                        target.Filename = Video.Filename;
                        target.Link = Video.Link;
                        target.Title = Video.Title;
                    }

                }
                db.SaveChanges();
            }

            return Json(Videos.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Video> Videos)
        {
            if (Videos.Any())
            {
                foreach (var Video in Videos)
                {
                    var VideoToDelete = db.Videos.First(p => p.VideoId == Video.VideoId);
                    if (VideoToDelete != null)
                    {
                        db.Videos.Remove(VideoToDelete);
                    }
                }
                db.SaveChanges();
            }

            return Json(Videos.ToDataSourceResult(request, ModelState));
        }




    }
}
