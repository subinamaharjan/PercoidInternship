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

        //signup page Get
        public ActionResult Signup()
        {
            return View();
        }
        //signup Post
        [HttpPost]
        public ActionResult Signup(string username,string password)
        {
            //checks if any user exist
            if (Users.Any(u => u.UserName == username))
            {
                ViewBag.Error = "Username already exist.Please choose another.";
                return View();
            }

            //add new to the list
            var newUser = new User
            {
                Id = Users.Count + 1,
                UserName = username,
                Password = password
            };
            Users.Add(newUser);
            ViewBag.Success = "Signup Successful! you can login now.";
            return View();
            
        }

    }
}