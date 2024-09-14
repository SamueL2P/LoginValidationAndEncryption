using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginValidationAndEncryption.Data;
using LoginValidationAndEncryption.Models;

namespace LoginValidationAndEncryption.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user) {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {

                    var CurrentUser = session.Query<User>().Where(u => u.UserName == user.UserName).SingleOrDefault();

                    if (CurrentUser != null && user.Password !=null &&  BCrypt.Net.BCrypt.Verify(user.Password, CurrentUser.Password))
                    {
                       
                        return RedirectToAction("Home", new { username = CurrentUser.UserName });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid username or password.";
                        return View();
                    }
                }
            }
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(User user)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {

                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    session.Save(user);
                    transaction.Commit();

                    return RedirectToAction("Login");
                }
            }
        }

        public ActionResult Home(string username)
        {
            ViewBag.UserName = username;
            return View();
        }

    }
}