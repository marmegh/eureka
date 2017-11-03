using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eureka.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
                @ViewBag.uid = uid;
                List<Idea> AllIdeas = _context.ideas.Include(idea=>idea.origin).Include(Idea=>Idea.liked).ToList();
                @ViewBag.ideas = AllIdeas;
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
                newUser.alias = registrant.last;
                newUser.email = registrant.email;
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

        [HttpPost]
        [Route("newidea")]
        public IActionResult NewIdea(IdeaViewModel incoming)
        {
            TempData["user_id"] = TempData["user_id"];
            Idea newIdea = new Idea();
            newIdea.idea = incoming.idea;
            int uid = Convert.ToInt32(TempData["user_id"]);
            newIdea.userID = uid;
            User user = _context.users.SingleOrDefault(User => User.userID == uid);
            newIdea.origin = user;
            newIdea.created_at = DateTime.Now;
            newIdea.updated_at = DateTime.Now;
            List<Like> likes = new List<Like>();
            newIdea.liked = likes;
            _context.ideas.Add(newIdea);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("like/{id}")]
        public IActionResult Like(int id)
        {
            int uid = Convert.ToInt32(TempData["user_id"]);
            TempData["user_id"] = TempData["user_id"];
            Idea idea = _context.ideas.Where(thought => thought.ideaid == id).Include(thought => thought.liked).SingleOrDefault();
            List<Like> idealikes = idea.liked.Where(like => like.userID == uid).ToList();
            List<Like> AllLikes = idea.liked; 
            if(idealikes.Count < 1)
            {                
                User user = _context.users.SingleOrDefault(User => User.userID == uid);                
                Like newLike = new Like();
                newLike.userID = uid;
                newLike.ideaid = id;
                newLike.liked = idea;
                newLike.liker = user;
                newLike.created_at = DateTime.Now;
                newLike.updated_at = DateTime.Now;
                _context.likes.Add(newLike);
                _context.SaveChanges();
            }
            // int start = idea.userID;
            // if(start == uid)
            // {
            //     System.Console.WriteLine("We can delete this");
            // }
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("profile/{id}")]
        public IActionResult Profile(int id)
        {
            int uid = Convert.ToInt32(TempData["user_id"]);
            User user = _context.users.Where(u=>u.userID == id).Include(u=>u.likes).Include(u=>u.ideas).ThenInclude(i=>i.liked).SingleOrDefault();
            List<Like> AllLikes = _context.likes.Include(like=>like.liker).Include(like=>like.liked).ToList();
            List<User> AllUsers = _context.users.ToList();
            @ViewBag.user = user;
            @ViewBag.AllLikes = AllLikes;
            @ViewBag.users = AllUsers;
            TempData["user_id"] = TempData["user_id"];
            return View("Profile");
        }
        [HttpGet]
        [Route("display/{id}")]
        public IActionResult Display(int id)
        {
            int uid = Convert.ToInt32(TempData["user_id"]);
            Idea idea = _context.ideas.Where(i=>i.ideaid == id).Include(i=>i.liked).SingleOrDefault();
            List<Like> AllLikes = _context.likes.Include(like=>like.liker).Include(like=>like.liked).ToList();
            List<User> AllUsers = _context.users.ToList();
            @ViewBag.AllLikes = AllLikes;
            @ViewBag.users = AllUsers;
            @ViewBag.idea = idea;
            TempData["user_id"] = TempData["user_id"];
            return View("Display");
        }
    }
}
