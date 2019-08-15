using Microsoft.AspNetCore.Mvc;
using SubscribeUsers.Models;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SubscribeUsers.Controllers
{
    public class HomeController : Controller
    {
        private readonly context _context;
        public HomeController(context context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public  async Task<IActionResult> SubmitDetails(Subusers subusers)
        {
            var model = "existing user";
            if (subusers == null)
            {
                return new JsonResult("object is null");
            }

            var useremail = subusers.Uemail;

            var existingUser = _context.Subusers.Any(x => x.Uemail.Equals(useremail));

            if (existingUser)
            {
                model = "this email address is already subscribed";
                return new JsonResult(model);
            }

                Subusers UD = new Subusers();
                UD.Uname = subusers.Uname;
                UD.Uemail = subusers.Uemail;

                _context.Subusers.Add(UD);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }

                var smtpClient = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("p.anuom@hotmail.com", "*5*Believer")
                };

                using (var message = new MailMessage("p.anuom@hotmail.com", useremail)
                {
                    Subject = "Subscription successful",
                    Body = "Thank you For Subscribing - AlgoDepth"
                })
                {
                    await smtpClient.SendMailAsync(message);
                }
                HttpStatusCode response = new HttpResponseMessage().StatusCode;
                model = "Thank you for subscribing";
                return new JsonResult(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
