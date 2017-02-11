using Biz4CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biz4CMS.Controllers
{
    public class LocationController : Controller
    {
        //
        // GET: /Location/

        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            var Locations = db.Location.OrderByDescending(p => p.LocationId).ToList();
            ViewBag.data = Locations;

            return View();
        }

    }
}
