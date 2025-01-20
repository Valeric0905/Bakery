using BakeryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace BakeryWeb.Controllers
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

        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Contact Form
        public IActionResult Contact()
        {
            return View();
        }

        // POST: Submit Form
        [HttpPost]
        public ActionResult Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Send email
                SendEmail(model);

                ViewBag.Message = "Your message has been sent!";
                return View();
            }

            return View(model);
        }

        private void SendEmail(ContactFormModel model)
        {
            // Replace with your own email details
            var fromAddress = new MailAddress("", "Peky Bakery");
            var toAddress = new MailAddress("", "Recipient");
            const string subject = "New Contact Form Submission";

            // Body of the email with user input
            string body = $"Name: {model.Name}\nEmail: {model.Email}\nQuestion: {model.Question}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",  // Gmail's SMTP host
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("", "") // Your Gmail credentials
            };

            try
            {
                _logger.LogInformation("Attempting to send email...");
                smtp.Send(fromAddress.Address, toAddress.Address, subject, body);
                _logger.LogInformation("Email sent successfully.");
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError($"SMTP Exception: {smtpEx.Message}");
                ViewBag.Message = "There was an error sending your message via SMTP. Please try again.";
            }
            catch (Exception ex)
            {
                _logger.LogError($"General Exception: {ex.Message}");
                ViewBag.Message = "There was an error sending your message. Please try again.";
            }
        }
    }

    
}
