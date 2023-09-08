using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MyEstate.Controllers
{
    public class EstateController : Controller
    {
        private IEstateService _estateService;

        public EstateController(IEstateService estateService)
        {
            _estateService = estateService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("ShowEstate/{id}")]
        public IActionResult ShowEstate(int id)
        {
            var estateImages = _estateService.GetDataforShowEstate(id);
            if (estateImages == null)
            {
                return NotFound();
            }
            return View(estateImages);
        }
    }
}
