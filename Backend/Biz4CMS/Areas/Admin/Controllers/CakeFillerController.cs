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
        public ActionResult Create()
        {
            var cakefiller = new CakeFiller();
            return View(cakefiller);
        }
        // POST: /bo/CakeFiller/Create

        [HttpPost]
        public ActionResult Create(CakeFiller model)
        {
            if (ModelState.IsValid)
            {
                db.CakeFillers.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Edit(CakeFiller model)
        {
            if (ModelState.IsValid)
            {
                var updateCakeFiller = db.CakeFillers.FirstOrDefault(p => p.CakeFillerId == model.CakeFillerId);
                if (updateCakeFiller != null)
                {

                    TryUpdateModel(updateCakeFiller);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
        //
        // GET: /bo/CakeFiller/Edit/5

        public ActionResult Edit(int id)
        {
            var cakefiller = db.CakeFillers.FirstOrDefault(p => p.CakeFillerId == id);
            return View("Create", cakefiller);
        }

        //


        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, CakeFiller model)
        {
            var cakefillerToDelete = db.CakeFillers.First(p => p.CakeFillerId == model.CakeFillerId);
            if (cakefillerToDelete != null)
            {
                db.CakeFillers.Remove(cakefillerToDelete);
                db.SaveChanges();
            }
            return Json(new[] { cakefillerToDelete }.ToDataSourceResult(request));
        }

    }
}
