using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Repository;

namespace Babawokie.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ItemsMasterController : Controller
    {
        ItemsMasterControl db = new ItemsMasterControl();
        ItemsControl itemDB = new ItemsControl();
        //
        // GET: /ItemsMaster/
        public ActionResult Index()
        {
            List<ItemDetails> details = db.getAllDetails.ToList();
            return View(details);
        }
    

        //
        // GET: /ItemsMaster/Create
        public ActionResult Create()
        {
            SelectItemListCreate();
            return View();
        }

        //
        // POST: /ItemsMaster/Create
        [HttpPost]
        public ActionResult Create(ItemDetails item)
        {
            SelectItemListCreate();
            try
            {
                UpdateModel(item);
                db.InsertItem(db.paramAddItem(item), item.Quantity);
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        //
        // GET: /ItemsMaster/Edit/5
        public ActionResult Edit(int id)
        {
            ItemDetails item = db.getAllDetails.Single(i => i.itemID == id);
            SelectItemListEdit(item.categoryID, item.brandID, item.typeID);
            return View(item);
        }

        //
        // POST: /ItemsMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ItemDetails data)
        {
            ItemDetails item = db.getAllDetails.Single(i => i.itemID == id);
            SelectItemListEdit(item.categoryID, item.brandID, item.typeID);

            try
            {
                UpdateModel(data);
                db.paramQuery("edit_item_details", db.paramEditItem(data, id));
                return RedirectToAction("Index");
            }
            catch  { return View(item);  }
        }

        #region Select List Item Constructor

        private void SelectItemListCreate()
        {
            ViewBag.categoryID = new SelectList(itemDB.getAllCategory, "categoryID", "Name");
            ViewBag.brandID = new SelectList(itemDB.getAllBrand, "brandID", "Name");
            ViewBag.typeID = new SelectList(itemDB.getAllType, "typeID", "Name");
        }

        private void SelectItemListEdit(int categoryID, int brandID, int typeID)
        {
            ViewBag.categoryID = new SelectList(itemDB.getAllCategory, "categoryID", "Name", categoryID);
            ViewBag.brandID = new SelectList(itemDB.getAllBrand, "brandID", "Name", brandID);
            ViewBag.typeID = new SelectList(itemDB.getAllType, "typeID", "Name", typeID);
        }

        #endregion
    }
}
