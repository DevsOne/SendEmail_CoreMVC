using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SendEmail_CoreMVC.Models;
using Microsoft.AspNetCore.Hosting;

namespace SendEmail_CoreMVC.Controllers
{
    public class HomeController : Controller
    {

        private IHostingEnvironment _env;

        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BulkEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(EmailViewModel model)
        {
            var emailTemplate = "WelcomeEmail";
            var emailSubject = "Welcome to our site.";
            var message = EMailTemplate(emailTemplate);
            message = message.Replace("@ViewBag.Name", model.Username);
            MessageServices msg = new MessageServices();
            await msg.SendEmailAsync(model.Username, model.Email, emailSubject, message, model.Attachments);
            ModelState.AddModelError("", "Email successfully sent.");
            return View("Index");
        }

        [HttpPost]
        public async Task<ActionResult> SendBulkEmail(BulkEmailViewModel model)
        {
            var emailTemplate = "WelcomeEmail";
            var emailSubject = "Welcome to our site.";
            var message = EMailTemplate(emailTemplate);
            message = message.Replace("@ViewBag.Name", model.Username);
            MessageServices msg = new MessageServices();
            await msg.SendBulkEmailAsync(model.Username, model.Email, emailSubject, message, model.Attachments);
            ModelState.AddModelError("", "Email successfully sent.");
            return View("BulkEmail");
        }

        public string EMailTemplate(string template)
        {
            string body = null;
            var templateFilePath = _env.WebRootPath + "\\templates\\" + template + ".cshtml";
            body = System.IO.File.ReadAllText(templateFilePath);
            return body;
        }
    }
}
