using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Models;
using Repository;

namespace Babawokie.Controllers
{
    [Authorize(Roles="DSA, Dean")]
    public class ScheduleController : Controller
    {
        private static RequestControl db = new RequestControl();
        private static ScheduleControl dbSchedule = new ScheduleControl();
        //
        // GET: /Account/
        public ActionResult Index(string serial)
        {
            ViewBag.CalendarEvents = dbSchedule.GetScheduleJS(serial);
            Request request = new Request() { SerialNumber = serial, End = DateTime.Now };
            return View(request);
        }

        [HttpPost]
        public ActionResult Index(Request request, string serial)
        {
            request = db.SetRequest(User.Identity.Name, request, serial);

            TryUpdateModel(request);
            if (ModelState.IsValid)
            {
                db.paramQuery("add_request", db.paramInsertRequest(request));
                int requestID = db.noParamQuery("get_requestID");
               
                return RedirectToAction("RequestDetails", "UserRequest", new { id = requestID });
            }
            return View("Index");
        }
    }
}