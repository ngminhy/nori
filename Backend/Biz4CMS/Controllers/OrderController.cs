using Biz4CMS.Models;
using Biz4CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biz4CMS.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }

        //POST: /Order/

        [HttpPost]
        public ActionResult Index(FormCollection values)
        {
            var userinfo = new UserInfo();
            TryUpdateModel(userinfo);
            HttpContext.Session["userinfo"] = userinfo;
            return RedirectToAction("Index", "product");
        }
    }
}
