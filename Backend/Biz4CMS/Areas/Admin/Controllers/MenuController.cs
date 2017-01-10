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
    public class MenuController : Controller
    {


        //
        // GET: /Admin/Menu/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            //var Menus = db.Menus.Where(p => p.ProductId == ProductId).OrderByDescending(p => p.MenuId).ToList();
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var Menus = db.Menus.OrderByDescending(p => p.MenuId).ToList();
            return this.Json(Menus.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.Menu> Menus)
        {
            var results = new List<Biz4CMS.Models.Menu>();

            if (Menus != null && ModelState.IsValid)
            {
                foreach (var Menu in Menus)
                {

                    db.Menus.Add(Menu);
                    results.Add(Menu);
                }
                db.SaveChanges();
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.Menu> Menus)
        {
            if (Menus != null && ModelState.IsValid)
            {
                foreach (var Menu in Menus)
                {
                    var target = db.Menus.FirstOrDefault(p => p.MenuId == Menu.MenuId);
                    if (target != null)
                    {
                        target.Text = Menu.Text;
                        target.Link = Menu.Link;
                        target.Tag = Menu.Tag;
                        target.ParentId = Menu.ParentId;
                        target.Order = Menu.Order;
                    }

                }
                db.SaveChanges();
            }

            return Json(Menus.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Biz4CMS.Models.Menu> Menus)
        {
            if (Menus.Any())
            {
                foreach (var menu in Menus)
                {
                    var menuToDelete = db.Menus.First(p => p.MenuId == menu.MenuId);
                    if (menuToDelete != null)
                    {
                        db.Menus.Remove(menuToDelete);
                    }
                }
                db.SaveChanges();                
            }

            return Json(Menus.ToDataSourceResult(request, ModelState));
        }


    }
}
