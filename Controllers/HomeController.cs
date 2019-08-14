using Microsoft.AspNetCore.Mvc;
using SubscribeUsers.Models;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;


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

        public async Task<IActionResult> SubmitDetails(Subusers subusers)
        {
            if (subusers == null)
            {
                return new JsonResult("object is null");
            }

            var useremail = subusers.Uemail;

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

            return View("SubmitDetails");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
