using ContactUs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace ContactUs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Post(ContactData contactData)
        {
            if (!ModelState.IsValid)
            {
                return await Task.FromResult(View("Index", new ContactData(contactData.FirstName, contactData.LastName, contactData.Email, contactData.Message)));
            }
            else
            {
                // I chose to just output to a txt file, could have serialized to JSON or anything else.
                // This also could have been abstracted out into a service but since it is simply doing one small file write
                // I saw no need to do that
                string filePath = Path.Combine(_env.WebRootPath, "contact_msgs.txt");

                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath).Dispose();
                }

                string contactLogMessage =
                    $"First Name: {contactData.FirstName}{Environment.NewLine}" +
                    $"Last Name: {contactData.LastName}{Environment.NewLine}" +
                    $"Email Address: {contactData.Email}{Environment.NewLine}" +
                    $"Message: {Environment.NewLine}{contactData.Message}{Environment.NewLine}{Environment.NewLine}";

                // Output as unicode text, limit to 4096 buffer
                byte[] encodedMessage = Encoding.Unicode.GetBytes(contactLogMessage);

                using (FileStream sourceStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    await sourceStream.WriteAsync(encodedMessage);
                };
            }

            return await Task.FromResult(RedirectToAction("Index"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}