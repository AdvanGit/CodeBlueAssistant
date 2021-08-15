using Hospital.ViewModel;
using Hospital.ViewModel.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Hospital.ASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRootViewModelFactory _rootViewModelFactory;

        public HomeController(ILogger<HomeController> logger, IRootViewModelFactory rootViewModelFactory)
        {
            _logger = logger;
            _rootViewModelFactory = rootViewModelFactory;
        }

        public IActionResult Index()
        {
            return View(_rootViewModelFactory.CreateRegistratorViewModel());
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
