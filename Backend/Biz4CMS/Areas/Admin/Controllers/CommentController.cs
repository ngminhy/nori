using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Biz4CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {


        //
        // GET: /bo/Comment/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            var Comments = db.Comments.OrderByDescending(p => p.CommentId).ToList();
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var Comments = db.Comments.OrderByDescending(p => p.CommentId).ToList();
            return this.Json(Comments.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.Comment> Comments)
        {
            var results = new List<Biz4CMS.Models.Comment>();

            if (Comments != null && ModelState.IsValid)
            {
                foreach (var Comment in Comments)
                {

                    db.Comments.Add(Comment);
                    results.Add(Comment);
                }
                db.SaveChanges();
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.Comment> Comments)
        {
            if (Comments != null && ModelState.IsValid)
            {
                foreach (var Comment in Comments)
                {
                    var target = db.Comments.FirstOrDefault(p => p.CommentId == Comment.CommentId);
                    if (target != null)
                    {
                        target.FullName = Comment.FullName;
                        target.JobTitle = Comment.JobTitle;
                        target.Description = Comment.Description;
                        target.Tag = Comment.Tag;
                    }

                }
                db.SaveChanges();
            }

            return Json(Comments.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.Comment> Comments)
        {
            if (Comments.Any())
            {
                foreach (var Comment in Comments)
                {
                    var CommentToDelete = db.Comments.First(p => p.CommentId == Comment.CommentId);
                    if (CommentToDelete != null)
                    {
                        db.Comments.Remove(CommentToDelete);
                    }
                }
                db.SaveChanges();                
            }

            return Json(Comments.ToDataSourceResult(request, ModelState));
        }


    }
}
