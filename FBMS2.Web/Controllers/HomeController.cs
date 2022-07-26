using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FBMS2.Web.ViewModels;

namespace FBMS2.Web.Controllers;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.LongTime = DateTime.Now.ToLongDateString();
            ViewBag.Message = "The Date Now Is" ;
            return View();
        }

        public IActionResult About()
        {
            var about = new AboutViewModel 
            {
                Title = "Food Bank & Recipe",
                Message = "Helping Food Banks With Inventory & Recipe Management",
                Formed = new DateTime(2022, 07,22)

            };

            return View(about);
        }

        [Authorize]
        public IActionResult Secure()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

