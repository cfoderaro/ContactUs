using ContactUs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactUs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(ContactData contactData)
        {
            if (!ModelState.IsValid)
            {
                return View(contactData);
            }
            else
            {
                // TODO: Write data to file
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