using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eureka.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace eureka.Controllers
{
    public class HomeController : Controller
    {
        private eurekaContext _context;
        public HomeController(eurekaContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(TempData["user_id"] != null)
            {
                int uid = Convert.ToInt32(TempData["user_id"]);
                User user = _context.users.SingleOrDefault(User => User.userID == uid);
                @ViewBag.uname = user.first;
                // List<Wedding> AllWeddings = _context.weddings.ToList();
                // @ViewBag.weddings = AllWeddings;
                TempData["user_id"] = TempData["user_id"];
                return View("Dashboard");
            }
            else{
                @ViewBag.errors = "Unauthorized access attempt.";
                return View("Index");
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(LogReg incoming)
        {
            RegistrationViewModel registrant = new RegistrationViewModel();
            registrant = incoming.reg;
            List<User> check = _context.users.Where(User=>User.email == registrant.email).ToList();
            if(check.Count>0)
            {
                @ViewBag.errors = "Account already exists. Please login.";
                return View("Index", incoming);
            }
            TryValidateModel(registrant);
            if(ModelState.IsValid)
            {
                User newUser = new User();
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.password = Hasher.HashPassword(newUser, registrant.password);
                newUser.first = registrant.first;
                newUser.last = registrant.last;
                newUser.email = registrant.email;
                // List<RSVP> rsvps = new List<RSVP>();
                // newUser.rsvped = rsvps;
                newUser.created_at = DateTime.Now;
                newUser.updated_at = DateTime.Now;
                _context.users.Add(newUser);
                _context.SaveChanges();
                User confirmed = _context.users.Last();
                TempData["user_id"] = confirmed.userID;
                return View("Dashboard");
            }
            else
            {
                System.Console.WriteLine("We have an invalid registration");
            }
            return View("Index", incoming);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LogReg incoming)
        {
            LoginViewModel user = new LoginViewModel();
            user = incoming.login;
            TryValidateModel(user);
            if(ModelState.IsValid)
            {
                User identified = _context.users.SingleOrDefault(User=>User.email == user.lemail);
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(identified, identified.password, user.pwd))
                {
                    TempData["user_id"] = identified.userID;
                    return RedirectToAction("Dashboard");
                }
                else{
                    System.Console.WriteLine("Danger Will Robinson!");
                }
            }
            @ViewBag.errors = "Unable to authenticate account";
            System.Console.WriteLine("We have an invalid login");
            return View("Index", incoming);
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
