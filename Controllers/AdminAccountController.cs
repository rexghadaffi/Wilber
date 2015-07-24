using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Repository;

namespace Babawokie.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminAccountController : Controller
    {
        // DB Repostiory reference
        private AdminControl db = new AdminControl();


        public ActionResult Index()
        {
            AdminControl adminControl = new AdminControl();
            List<Admin> admins = adminControl.getAllAdmin.ToList();
            return View(admins);
        }

        //
        // GET: /AdminAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Status")]Admin admin)
        {
            TryUpdateModel(admin);
            if (ModelState.IsValid)
            {
                AdminControl adminControl = new AdminControl();
                adminControl.paramQuery("add_admin", adminControl.paramAddAdmin(admin));
                return RedirectToAction("Index", "AdminAccount");
            }
            return View();
        }
        //
        // GET: /AdminAccount/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            AdminControl adminControl = new AdminControl();
            Admin admin = adminControl.getAllAdmin.Single(a => a.ID == id);
            return View(admin);
        }
        //
        // POST: /AdminAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Admin admin)
        {
            AdminControl adminControl = new AdminControl();
            TryUpdateModel(admin);
            if (ModelState.IsValid)
            {
                adminControl.paramQuery("edit_admin", adminControl.paramEditAdmin(admin, id));
                return RedirectToAction("Index", "AdminAccount");
            }
            return View();
        }


    }
}
