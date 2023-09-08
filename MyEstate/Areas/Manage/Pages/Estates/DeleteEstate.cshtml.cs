using Core.Services.Interfaces;
using DataLayer.Entities.Estate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyEstate.Areas.Manage.Pages.Estates
{
    [Authorize]
    public class DeleteEstateModel : PageModel
    {
        private IEstateService _estateService;
        private IUserService _userService;
        public DeleteEstateModel(IEstateService estateService, IUserService userService)
        {
            _estateService = estateService;
            _userService = userService;
        }

        [BindProperty]
        public List<EstateImage> EstateImageslist { get; set; }
        [BindProperty]
        public Estate Estate { get; set; }
        public IActionResult OnGet(int id)
        {
           
            if (_userService.IsUsersEstate(User.Identity.Name, id))
            {
                Estate = _estateService.GetEstateById(id);
                EstateImageslist = _estateService.GetEstatImagees(id);
                var estateType = _estateService.GetEstateTypeForManageEstate();
                ViewData["EstateType"] = new SelectList(estateType, "Value", "Text", Estate.EstateTypeId);
                return Page();
            }
            else
            {
                return NotFound();
            }
           
            
        }
        public IActionResult OnPost(int id)
        {
            _estateService.DeleteEstate(id);
            return RedirectToPage("Index");
            //_estateService.DeleteEstate(id, EstateImageslist);
        }
    }
}
