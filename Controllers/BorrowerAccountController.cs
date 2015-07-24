using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using Models;
namespace Babawokie.Controllers
{
     [Authorize(Roles = "Admin")]
    public class BorrowerAccountController : Controller
    {
        //
        // GET: /BorrowerAccount/
        public ActionResult Index()
        {
            BorrowerControl borrowerControl = new BorrowerControl();
            List<Borrower> borrower = borrowerControl.getAllClient.ToList();
            return View(borrower);       
        }       

        //
        // GET: /BorrowerAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BorrowerAccount/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude= "Status, ViolationCount" )]Borrower borrower)
        {
            TryUpdateModel(borrower);
            if (ModelState.IsValid)
            {
                BorrowerControl borrowerControl = new BorrowerControl();
                borrowerControl.paramQuery("add_client", borrowerControl.paramAddClient(borrower));
                return RedirectToAction("Index", "BorrowerAccount");
            }
            return View();
        }

        //
        // GET: /BorrowerAccount/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            BorrowerControl borrowerControl = new BorrowerControl();
            Borrower borrower = borrowerControl.getAllClient.Single(b => b.ID == id);
            return View(borrower);
        }

        //
        // POST: /BorrowerAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Borrower borrower)
        {
           
                BorrowerControl borrowerControl = new BorrowerControl();
                TryUpdateModel(borrower);
                if (ModelState.IsValid)
                {
                    borrowerControl.paramQuery("edit_client", borrowerControl.paramEditClient(borrower, id));
                    return RedirectToAction("Index");
                }          
                return View();         
        }    
      
    }
}
