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
    public class LocationController : Controller
    {
        //
        // GET: /Admin/Location/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var Locations = db.Location.OrderByDescending(p => p.LocationId).ToList();
            return this.Json(Locations.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.Location> Locations)
        {
            var results = new List<Biz4CMS.Models.Location>();

            if (Locations != null && ModelState.IsValid)
            {
                foreach (var Location in Locations)
                {

                    db.Location.Add(Location);
                    results.Add(Location);
                }
                db.SaveChanges();
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.Location> Locations)
        {
            if (Locations != null && ModelState.IsValid)
            {
                foreach (var Location in Locations)
                {
                    var target = db.Location.FirstOrDefault(p => p.LocationId == Location.LocationId);
                    if (target != null)
                    {
                        target.Address = Location.Address;
                        target.Lat = Location.Lat;
                        target.Lon = Location.Lon;
                        target.Active = Location.Active;
                    }

                }
                db.SaveChanges();
            }

            return Json(Locations.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.Location> Locations)
        {
            if (Locations.Any())
            {
                foreach (var Location in Locations)
                {
                    var locationToDelete = db.Location.First(p => p.LocationId == Location.LocationId);
                    if (locationToDelete != null)
                    {
                        db.Location.Remove(locationToDelete);
                    }
                }
                db.SaveChanges();
            }

            return Json(Locations.ToDataSourceResult(request, ModelState));
        }

    }
}
