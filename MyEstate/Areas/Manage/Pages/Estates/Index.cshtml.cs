using Core.Services.Interfaces;
using DataLayer.Entities.Estate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyEstate.Areas.Manage.Pages.Estates;

[Authorize]
public class IndexModel : PageModel
    {
        private IEstateService _estateService;

        public IndexModel(IEstateService estateService)
        {
            _estateService = estateService;
        }
        [BindProperty]
        public List<Estate> Estates { get; set; }
        public void OnGet()
        {
            var userName = User.Identity.Name;
            Estates = _estateService.GetEstatListById(userName);

        }
    }

