using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CinemaApp.Web.ViewModels;

namespace CinemaApp.Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(ILogger<HomeController> logger)
        {

        }

        public IActionResult Index()
        {
            //two ways of transmitting data from Controller to view
            //1. Using ViewData/ViewBag
            //2. Pass ViewModel to the View

            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to the Cinema Web App!";

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
}
