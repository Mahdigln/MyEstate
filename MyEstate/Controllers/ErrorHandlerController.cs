using Microsoft.AspNetCore.Mvc;

namespace MyEstate.Controllers;

    [Route("/ErrorHandler/{code}")]
    public class ErrorHandlerController : Controller
    {
        public IActionResult Index(int code)
        {
            switch (code)
            {
                case 404:
                    return View("NotFound");

                //case 500:
                //    return View("ServerError");
            }
            return View("NotFound");
        }
    }

