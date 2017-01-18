using Biz4CMS.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biz4CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class CakeFillerController : Controller
    {
        //
        // GET: /Admin/CakeFiller/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var Menus = db.CakeFillers.OrderByDescending(p => p.CakeFillerId).ToList();
            return this.Json(Menus.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.CakeFiller> CakeFillers)
        {
            var results = new List<Biz4CMS.Models.CakeFiller>();

            if (CakeFillers != null && ModelState.IsValid)
            {
                foreach (var CakeFiller in CakeFillers)
                {

                    db.CakeFillers.Add(CakeFiller);
                    results.Add(CakeFiller);
                }
                db.SaveChanges();
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.CakeFiller> CakeFillers)
        {
            if (CakeFillers != null && ModelState.IsValid)
            {
                foreach (var CakeFiller in CakeFillers)
                {
                    var target = db.CakeFillers.FirstOrDefault(p => p.CakeFillerId == CakeFiller.CakeFillerId);
                    if (target != null)
                    {
                        target.Name = CakeFiller.Name;
                        target.FillerImage = CakeFiller.FillerImage;
                        target.Price = CakeFiller.Price;
                    }

                }
                db.SaveChanges();
            }

            return Json(CakeFillers.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.CakeFiller> CakeFillers)
        {
            if (CakeFillers.Any())
            {
                foreach (var CakeFiller in CakeFillers)
                {
                    var CakeFillerToDelete = db.CakeFillers.First(p => p.CakeFillerId == CakeFiller.CakeFillerId);
                    if (CakeFillerToDelete != null)
                    {
                        db.CakeFillers.Remove(CakeFillerToDelete);
                    }
                }
                db.SaveChanges();
            }

            return Json(CakeFillers.ToDataSourceResult(request, ModelState));
        }

    }
}
