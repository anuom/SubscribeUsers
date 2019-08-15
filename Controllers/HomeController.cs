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

        public async Task<JsonResult> SubmitDetails(Subusers subusers)
        {
            if (subusers.Uemail == null)
            {
                return Json("NA");
            }

            var useremail = subusers.Uemail;

            var existingUser = _context.Subusers.Any(x => x.Uemail.Equals(useremail));

            if (existingUser)
            {
                return Json("Y");
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
                Host = "smtp.gmail.com",
                //Host = "smtp-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("sandbox.tst.acc@gmail.com", "Sandbox_test")
            };

            using (var message = new MailMessage("sandbox.tst.acc@gmail.com", useremail)
            {
                Subject = "Subscription successful",
                Body = "Thank you For Subscribing - AlgoDepth"
            })
            {
                await smtpClient.SendMailAsync(message);
            }

            return Json("N");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
