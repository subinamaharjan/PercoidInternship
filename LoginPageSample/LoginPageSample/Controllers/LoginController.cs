using LoginPageSample.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginPageSample.Controllers
{
    public class LoginController : Controller
    {
        private readonly string connectionString = "Server=DESKTOP-SUBINA;Database=ProductDb;Integrated Security=True;TrustServerCertificate=True;";
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //POST login
        public ActionResult Authenticate(string username, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT *FROM Users WHERE Username=@Username AND Password=@Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    var result = command.ExecuteScalar();
                    int count = (result != null) ? Convert.ToInt32(result) : 0;


                    if (count > 0)
                    {
                        TempData["Message"] = "Login Successfull";
                        return RedirectToAction("Welcome"); //Redirect to secure page
                    }


                    TempData["Error"] = "Invalid username or password.";
                    return RedirectToAction("Index");
                }
            }
        }

        

        public ActionResult Welcome()
        {
            
                return View();
          
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
        public ActionResult Signup(User user)
        {
            if (ModelState.IsValid)
            {
                //save the user to the database
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO Users(Username, Password, Email) VALUES(@Username, @Password, @Email)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("Email", user.Email);
                        command.ExecuteNonQuery();
                    }
                }
                TempData["Message"] = "Signup Successful! Now you can login.";
                return RedirectToAction("Index");
            }
            return View(user);//if validation fails, return the form with errors.
        }




    }
}
