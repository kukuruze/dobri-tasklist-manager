using Dobri_Tasklist_Manager.Data;
using Dobri_Tasklist_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Dobri_Tasklist_Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            if(Active.IsLogged)
            {
                return RedirectToAction("Privacy");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(User obj)
        {

            foreach (var item in _db.Users)
            {
                if (item.Username == obj.Username && item.Password == obj.Password)
                {
                    Active.IsLogged = true;
                    if (obj.Username == "admin")
                    {
                        Active.IsAdmin = true;
                    }
                    else
                    {
                        Active.IsAdmin = false;
                    }
                    Active.CurrentUserId = item.Id;
                    return RedirectToAction("Privacy");
                }
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
