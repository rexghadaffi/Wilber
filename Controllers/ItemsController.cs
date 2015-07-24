using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Repository;

namespace Babawokie.Controllers
{
    [Authorize(Roles= "Admin")]
    public class ItemsController : Controller
    {
        //
        // GET: /Items/
        private ItemsControl db = new ItemsControl();

        public ActionResult Brand()
        {

            List<ItemBrand> brand = db.getAllBrand.ToList();
            return View(brand);
        }

        public ActionResult Category()
        {

            List<ItemCategory> category = db.getAllCategory.ToList();
            return View(category);
        }

        public ActionResult TypeList()
        {

            List<ItemType> type = db.getAllType.ToList();
            return View(type);
        }
        //
        // GET: /Items/Create
        public ActionResult Create(int id)
        {

            return View();
        }

        //
        // POST: /Items/Create
        [HttpPost]
        public ActionResult Create(int id, ItemName item)
        {

            TryUpdateModel(item);
            // TODO: Add insert logic here
            if (id == 1 && ModelState.IsValid)
            {
                db.InsertItem("tblbrand", "brandName", item.Name);
                return RedirectToAction("Brand");
            }
            else if (id == 2 && ModelState.IsValid)
            {
                db.InsertItem("tblcategory", "categoryName", item.Name);
                return RedirectToAction("Category");
            }
            else if (id == 3 && ModelState.IsValid)
            {
                db.InsertItem("tbltype", "typeName", item.Name);
                return RedirectToAction("TypeList");
            }
            return View();
        }

        //
        // GET: /Items/Edit/5
        public ActionResult EditBrand(int id)
        {

            ItemBrand brand = db.getAllBrand.Single(b => b.brandID == id);
            ViewBag.Header = "Brand";
            return View("Create", brand);
        }

        public ActionResult EditCategory(int id)
        {

            ItemCategory category = db.getAllCategory.Single(c => c.categoryID == id);
            ViewBag.Header = "Category";
            return View("Create", category);
        }

        public ActionResult EditType(int id)
        {

            ItemType type = db.getAllType.Single(t => t.typeID == id);
            ViewBag.Header = "Type";
            return View("Create", type);
        }
        //
        // POST: /Items/Edit/5
        [HttpPost]
        public ActionResult EditBrand(int id, ItemName item)
        {
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                db.EditItem("tblbrand", "brandName", item.Name, "brandID", id);
                return RedirectToAction("Brand");
            }

            return View("Create");
        }

        [HttpPost]
        public ActionResult EditCategory(int id, ItemName item)
        {
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                db.EditItem("tblcategory", "categoryName", item.Name, "categoryID", id);
                return RedirectToAction("Category");
            }
            return View("Create");
        }

        [HttpPost]
        public ActionResult EditType(int id, ItemName item)
        {
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                db.EditItem("tbltype", "typeName", item.Name, "typeID", id);
                return RedirectToAction("TypeList");
            }

            return View("Create");
        }

    }
}
