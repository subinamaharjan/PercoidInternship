using LoginPageSample.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LoginPageSample.Controllers
{
    public class LoginController : Controller
    {
        private readonly string connectionString = "Server=DESKTOP-SUBINA;Database=ProductDb;Integrated Security=True;TrustServerCertificate=True;";

        //Hashing
        public static class PasswordHelper
        {
            //password Hashing
            public static string HashPassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

            //password Verification against the hash
            public static bool VerifyPassword(string password,string hashedPassword)
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //POST login
        public ActionResult Authenticate(string username, string password)
        {
            
            
            //this code is used when we dont have any unhashed pw in the database 
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT *FROM Users WHERE Username=@Username";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    var hashedPassword = command.ExecuteScalar()?.ToString();
                    if (hashedPassword != null && PasswordHelper.VerifyPassword(password, hashedPassword))
                    {

                        TempData["Message"] = "Login Successfull";
                        return RedirectToAction("Welcome"); //Redirect to secure page
                    }


                    TempData["Error"] = "Invalid username or password.";
                    return RedirectToAction("Index");//redirecr back to the login page
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
        
        public ActionResult Signup(string username, string password, string email)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = PasswordHelper.HashPassword(password);

                //save the user to the database
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO Users(Username, Password, Email) VALUES(@Username, @Password, @Email)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("Email", email);
                        command.ExecuteNonQuery();
                    }
                }
                TempData["Message"] = "Signup Successful! Now you can login.";
                return RedirectToAction("Index");
            }
            return View();//if validation fails, return the form with errors.
        }




    }
}
