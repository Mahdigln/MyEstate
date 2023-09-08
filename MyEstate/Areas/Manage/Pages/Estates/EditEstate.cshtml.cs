using Core.Services.Interfaces;
using DataLayer.Entities.Estate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyEstate.Areas.Manage.Pages.Estates;
[Authorize]
public class EditEstateModel : PageModel
    {
        private IEstateService _estateService;
        private IUserService _userService;
        public EditEstateModel(IEstateService estateService, IUserService userService)
        {
            _estateService = estateService;
            _userService = userService;
        }


        [BindProperty]
        public Estate Estate { get; set; }
        
        public IActionResult OnGet(int id)
        {
            
            if (_userService.IsUsersEstate(User.Identity.Name, id))
            {
            Estate = _estateService.GetEstateById(id);
            var estateType = _estateService.GetEstateTypeForManageEstate();
            ViewData["EstateType"] = new SelectList(estateType, "Value", "Text", Estate.EstateTypeId);
            return Page();
            }
            else
            {
                return NotFound();
            }

    }

        public IActionResult OnPost(List<IFormFile> imgEstate, IFormFile imgDemo)
        {
            if (!ModelState.IsValid)
                return Page();

            var username = User.Identity.Name;
           _estateService.UpdateEstate(Estate, imgEstate, imgDemo, username);

           
            return RedirectToPage("Index");

        }

    }

