using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biz4CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /bo/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
