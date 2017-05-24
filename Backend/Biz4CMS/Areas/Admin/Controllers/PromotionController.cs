using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Biz4CMS.ViewModels;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Biz4CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class PromotionController : Controller
    {
        //
        // GET: /Admin/product/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var Promotions = db.Promotions.ToList();
            return this.Json(Promotions.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // GET: /Admin/Promotion/Create

        public ActionResult Create()
        {
            var Promotion = new Promotion();
            Promotion.CreatedDate = DateTime.Now;
            Promotion.UpdatedDate = DateTime.Now;
            Promotion.FromDate = DateTime.Now;
            Promotion.ToDate = DateTime.Now;
            return View(Promotion);
        }

        //
        // POST: /Admin/Promotion/Create
        [HttpPost]
        public ActionResult Create(Promotion model)
        {
            if (ModelState.IsValid)
            {
                model.ListDayofWeek = GetListDayofWeek(model);
                db.Promotions.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Edit(Promotion model)
        {
            if (ModelState.IsValid)
            {
                //TryUpdateModel(model);
                model.UpdatedDate = DateTime.Now;
            
                model.ListDayofWeek = GetListDayofWeek(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        private string GetListDayofWeek(Promotion model)
        {
            var listdayofweek = "";
            if (model.Monday) listdayofweek += "2";
            if (model.Tuesday) listdayofweek += "3";
            if (model.Wednesday) listdayofweek += "4";
            if (model.Thursday) listdayofweek += "5";
            if (model.Friday) listdayofweek += "6";
            if (model.Saturday) listdayofweek += "7";
            if (model.Sunday) listdayofweek += "1";
            return listdayofweek;
        }
        //
        // GET: /Admin/product/Edit/Code

        public ActionResult Edit(string id)
        {
            var promotion = db.Promotions.FirstOrDefault(p => p.Code == id);
            return View("Create", promotion);
        }

        //
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, Promotion model)
        {
            if (model != null)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Json(new[] { model }.ToDataSourceResult(request));
        }
    }
}
