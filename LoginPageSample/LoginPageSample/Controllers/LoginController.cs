using LoginPageSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginPageSample.Controllers
{
    public class LoginController : Controller
    {
        private static readonly List<User> Users = new List<User>
        {
            new User{Id=1, UserName="admin",Password="1234"},
            new User{Id=2,UserName="user",Password="user123"}
        };

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //POST login
        public ActionResult Authenticate(string username, string password)
        {
            var user = Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                //save user info in  a session or authenticate user
                Session["UserId"] = user.Id;
                Session["UserName"] = user.UserName;
                return RedirectToAction("Welcome");
            }
            ViewBag.Error = "Invalid username or password.";
            return View("Index");
        }

        public ActionResult Welcome()
        {
            if (Session["UserId"] != null)
            {
                ViewBag.UserName = Session["UserName"];
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}