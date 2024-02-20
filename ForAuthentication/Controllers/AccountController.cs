using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForAuthentication.Models;
using System.Web.Security;

namespace ForAuthentication.Controllers
{
    public class AccountController : Controller
    {
        AuthenticateEntities db = new AuthenticateEntities();
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserTB user)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = db.UserTBs.Any(u => u.username == user.username && u.password == user.password);
                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Invalid Username";
                    return View("Login");
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserTB user)
        {
            if (ModelState.IsValid)
            {
                db.UserTBs.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}