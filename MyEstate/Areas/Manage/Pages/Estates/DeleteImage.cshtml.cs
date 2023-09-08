using Core.Services.Interfaces;
using DataLayer.Entities.Estate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyEstate.Areas.Manage.Pages.Estates
{
    [Authorize]
    public class EditImageModel : PageModel
    {
        private IEstateService _estateService;
        private IUserService _userService;
        public EditImageModel(IEstateService estateService, IUserService userService)
        {
            _estateService = estateService;
            _userService = userService;
        }

        [BindProperty]
        public EstateImage EstateImages { get; set; }
        [BindProperty]
        public List<EstateImage> EstateImageslist { get; set; }
        public IActionResult OnGet(int id)
        {
            if (_userService.IsUsersEstate(User.Identity.Name, id))
            {
                EstateImages = _estateService.GetEstatImageeById(id);
                EstateImageslist = _estateService.GetEstatImagees(id);
                return Page();
            }
            else
            {
                return NotFound();
            }

        }

        //public void OnPost(string imgname)
        //{
        //    _estateService.DeleteEstateImage(imgname);

        //}
        public IActionResult OnGetDelete(string img)
        {
            _estateService.DeleteEstateImage(img);
            return RedirectToPage("DeleteImage");
        }
    }
}
