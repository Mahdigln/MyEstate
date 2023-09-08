using Microsoft.AspNetCore.Mvc;
using MyEstate.Models;
using System.Diagnostics;
using Core.DTOs.Mail;
using Core.Services.Interfaces;

namespace MyEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailService _mailService;
        private IEstateService _estateService;
        public HomeController(ILogger<HomeController> logger, IMailService mailService, IEstateService estateService)
        {
            _logger = logger;
            _mailService = mailService;
            _estateService = estateService;
        }

        public IActionResult Index()
        {
            return View(_estateService.GetEstate().Item1);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("/ContactUs")]
        public IActionResult SendMail()
        {
            return View();
        }

      
        [HttpPost("/ContactUs")]
        public async Task<IActionResult> SendMail(MailRequest request)
        {
            try
            {
                await _mailService.SendEmailAsync(request);
                ViewBag.Success = true;
                return View();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }


        //[HttpPost("/AboutUs")]
        [HttpGet("/AboutUs")]
        public IActionResult AboutUs()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}