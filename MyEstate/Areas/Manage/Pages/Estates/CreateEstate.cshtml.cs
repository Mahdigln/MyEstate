using Core.Generator;
using Core.Services.Interfaces;
using DataLayer.Entities.Estate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyEstate.Areas.Manage.Pages.Estates;
[Authorize]
    public class CreateStateModel : PageModel
    {
        private IEstateService _stateService;

        public CreateStateModel(IEstateService stateService)
        {
            _stateService = stateService;
        }

        [BindProperty]
        public Estate Estate { get; set; }
        public void OnGet()
        {
            var estateType = _stateService.GetEstateTypeForManageEstate();
            ViewData["EstateType"] = new SelectList(estateType, "Value", "Text");

        }
        public IActionResult OnPost(List<IFormFile> imgEstate,IFormFile imgDemo)
        {
            if (!ModelState.IsValid)
                return Page();

            var username = User.Identity.Name;
            _stateService.AddEstate(Estate,imgEstate,imgDemo ,username);

            return RedirectToPage("Index");

        }

        //public void OnPostAddFeature(string featureTitle)
        //{
        //    _stateService.AddFeature(Estate.EstateId, featureTitle);
        //}


    }

