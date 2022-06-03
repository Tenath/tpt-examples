using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcBooks.Controllers
{
    public class HomeController : Controller
    {
        // public string Index() { return "HomeController works!"; }
        public IActionResult Index() { return View(); }

        public ViewResult Numbers(int min, int max)
        {
            ViewData["min"] = min;
            ViewData["max"] = max;
            return View();
        }
    }
}
