using Repository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wilber.Controllers
{
     [Authorize(Roles = "Dean, DSA")]
    public class UserRequestController : Controller
    {
        private static ItemsMasterControl dbItems = new ItemsMasterControl();
        private static RequestControl dbRequests = new RequestControl();
        //
        // GET: /UserRequest/
        public ActionResult Index()
        {
            IEnumerable<Request> request = dbRequests.GetRequest(User.Identity.Name);
            return View(request);
        }

        public ActionResult AvailableItems()
        {
            List<ItemQuantity> details = dbItems.getAllMasterList.Where(i => i.Status == "Available").ToList();
            return View(details);
        }

        public ActionResult RequestDetails(int id)
        {
            Request request = dbRequests.GetAllRequest.Single(r => r.RequestID == id);
            return View(request);
        }
    }
}
