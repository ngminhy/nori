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
    public class MessageController : Controller
    {
        //
        // GET: /bo/Message/
        Biz4Db db = new Biz4Db(); 
        public ActionResult Index()
        {
                                return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request) {
            var Messages = db.Messages.OrderByDescending(p=> p.MessageId).ToList();
            return this.Json(Messages.ToDataSourceResult(request),JsonRequestBehavior.AllowGet );
        
        }
        
        
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, Message  model)
        {
            var MessageToDelete = db.Messages.First(p => p.MessageId  == model.MessageId );

            if (MessageToDelete != null)
            {
                db.Messages.Remove(MessageToDelete);
                db.SaveChanges();
            }

            return Json(new[] { MessageToDelete }.ToDataSourceResult(request));
        }
    }
}
