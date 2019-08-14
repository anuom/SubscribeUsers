using Microsoft.AspNetCore.Mvc;
using SubscribeUsers.Models;
using System;

namespace SubscribeUsers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ADProjContext _context;
        public HomeController(ADProjContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubmitDetails(UserDetails details)
        {
            if (details == null)
            {
                return new JsonResult("object is null");
            }

            UserDetails UD = new UserDetails();
            UD.UserName = details.UserName;
            UD.EmailAdress = details.EmailAdress;

            _context.UserDetails.Add(UD);

            try
            {
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return new JsonResult(UD);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
