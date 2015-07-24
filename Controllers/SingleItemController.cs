using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Models;
using Repository;

namespace Babawokie.Controllers
{
     [Authorize(Roles = "Admin")]
    public class SingleItemController : Controller
    {
        ItemsMasterControl db = new ItemsMasterControl();
        //
        // GET: /SingleItem/
        public ActionResult List(int id)
        {
            List<ItemQuantity> list = db.getAllMasterList.Where(i => i.itemID == id).ToList();

            return View(list);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ItemQuantity item = db.getAllMasterList.Single(i => i.ID == id);
            return View(item);
        }

        [HttpPost]      
        public ActionResult Edit(int id, ItemQuantity list)
        {
            list = db.getAllMasterList.Single(i => i.ID == id);
            try
            {
                UpdateModel(list);

                db.paramQuery("edit_single_item", db.paramEditSingleItem(list, list.ID));
                return RedirectToAction("List", new { id = list.itemID });

            }
            catch { return View(list); }
        }
    }
}