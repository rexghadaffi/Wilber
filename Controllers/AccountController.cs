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
   
    public class AccountController : Controller
    {
        private static LoginControl db = new LoginControl();
       
        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(Login login)
        {           
            TryUpdateModel(login);
            try
            {
                if (db.LoginSubmit(login.Username, login.Password,
                             "adminUsername",
                             "adminPassword",
                             "tblcompanyadmin") > 0)
                {
                    FormsAuthentication.SetAuthCookie(login.Username, login.Remember);                  
                    return RedirectToAction("Index", "Approval");
                }
                ViewBag.ErrorMessage = "Invalid Username/Password";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }                
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

	}
}