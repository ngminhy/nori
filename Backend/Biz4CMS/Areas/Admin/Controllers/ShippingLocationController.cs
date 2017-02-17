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
    public class ShippingLocationController : Controller
    {
        //
        // GET: /Admin/ShippingLocation/

        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var list = db.ShippingLocation.OrderByDescending(p => p.ShippingLocationId).ToList();
            return this.Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        public ActionResult Create()
        {
            var ShippingLocation = new ShippingLocation();
            return View(ShippingLocation);
        }
        // POST: /bo/ShippingLocation/Create

        [HttpPost]
        public ActionResult Create(ShippingLocation model)
        {
            if (ModelState.IsValid)
            {
                db.ShippingLocation.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Edit(ShippingLocation model)
        {
            if (ModelState.IsValid)
            {
                var update = db.ShippingLocation.FirstOrDefault(p => p.ShippingLocationId == model.ShippingLocationId);
                if (update != null)
                {

                    TryUpdateModel(update);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
        //
        // GET: /bo/ShippingLocation/Edit/5

        public ActionResult Edit(int id)
        {
            var ShippingLocation = db.ShippingLocation.FirstOrDefault(p => p.ShippingLocationId == id);
            return View("Create", ShippingLocation);
        }

        //


        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ShippingLocation model)
        {
            var ShippingLocationDelete = db.ShippingLocation.First(p => p.ShippingLocationId == model.ShippingLocationId);
            if (ShippingLocationDelete != null)
            {
                db.ShippingLocation.Remove(ShippingLocationDelete);
                db.SaveChanges();
            }
            return Json(new[] { ShippingLocationDelete }.ToDataSourceResult(request));
        }

    }
}
