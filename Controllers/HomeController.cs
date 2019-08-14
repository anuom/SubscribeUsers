using Microsoft.AspNetCore.Mvc;
using SubscribeUsers.Models;
using System;
using System.Threading.Tasks;

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

        public async Task<IActionResult> SubmitDetails([FromBody]Subusers subusers)
        {
            if (subusers == null)
            {
                return new JsonResult("object is null");
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
            return new JsonResult(new
            {
                idOfTheNewField = UD.Uid
            });
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
