using Microsoft.AspNetCore.Mvc;
using SubscribeUsers.Models;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace SubscribeUsers.Controllers
{
    public class HomeController : Controller
    {
        private readonly context _context;
        private readonly IConfiguration _config;
        public HomeController(context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// POST METHOD TO RECEIVE AN OBJECT WITH USER NAME AND EMAIL ADDRESS,
        /// AND TO SEND A CONFIRMATION EMAIL TO THE EMAIL ADDRESS
        /// </summary> 
        /// <param name="subusers"></param>
        /// <returns> STRING TO THE AJAX CALL </returns>
        public async Task<JsonResult> SubmitDetails(Subusers subusers)
        {
            //if the email address is null then return NA else continue
            if (subusers.Uemail == null)
            {
                return Json("NA");
            }

            //check for the email address in the context
            var useremail = subusers.Uemail;
            var existingUser = _context.Subusers.Any(x => x.Uemail.Equals(useremail));
            //if email address exists then return Y else continue
            if (existingUser)
            {
                return Json("Y");
            }

            //create an object to insert the values in the context
            Subusers UD = new Subusers();
                UD.Uname = subusers.Uname;
                UD.Uemail = subusers.Uemail;

            //add to the dataset
            _context.Subusers.Add(UD);

            //create an instance of smtpclient
            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                //Host = "smtp-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("sandbox.tst.acc@gmail.com", "Sandbox_test")
            };
            
            try
            {
                //prepare the email message
                using (var message = new MailMessage("sandbox.tst.acc@gmail.com", useremail)
                {
                    Subject = "Subscription successful",
                    Body = "Dear " + subusers.Uname + ", \n \n \t Thank you For Subscribing \n \n - AlgoDepth. \n \n \n PLEASE DO NOT RESPOND TO THIS EMAIL, this account is not monitored!"
                })

                //send the messsage
                await smtpClient.SendMailAsync(message);
                //save the changes in the database
                await _context.SaveChangesAsync();
            }
            
            //catch exception if any
            catch (Exception e)
            {
                return Json(e);
            }

            //return N - indicating new user is added
            return Json("N");
        }

    }
}
