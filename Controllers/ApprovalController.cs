using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Repository;

namespace Wilber.Controllers
{
    [Authorize(Roles = "Dean, DSA, Admin")]
    public class ApprovalController : Controller
    {       
        protected ApprovalControl dbApproval = new ApprovalControl();
        
        //
        // GET: /Approval/

        public ActionResult Index()
        {
            ViewBag.Sample = "Pending Requests";
            IEnumerable<Request> requests = dbApproval.GetPendingRequest(User.Identity.Name).ToList();
            return View(requests);
        }
        public ActionResult Approve(int id)
        {          
            dbApproval.paramQuery("approve_request", id);
            return RedirectToAction("Index");
        }
        public ActionResult Reject(int id)
        {
            dbApproval.paramQuery("reject_request", id);
            return RedirectToAction("Index");
        }
    }
}
