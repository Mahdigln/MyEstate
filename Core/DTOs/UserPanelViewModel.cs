using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs;

    public class InformationUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string? Career { get; set; }
        public string InstagramId { get; set; }

    }

public class SideBarUserPanelViewModel
{
    public string UserName { get; set; }
    
    public string ProfileImageName { get; set; }

}

public class EditProfileViewModel
{
    [Display(Name = "نام کابری")]
    [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0}نمیتواند بیشتر از {1} کاراکتر باشد.")]
    public string UserName { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0}نمیتواند بیشتر از {1} کاراکتر باشد.")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
    public string Email { get; set; }

    public IFormFile ProfileImage { get; set; }
    public string ProfileImageName { get; set; }
    [Display(Name = "توضیحات")]
    public string Description { get; set; }

    [Display(Name = "عنوان شغلی")]
    public string? Career { get; set; }

    [Display(Name = "آیدی اینستاگرام")]
    public string InstagramId { get; set; }

}

public class ChangePasswordViewModel
{
    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string OldPassword { get; set; }

    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string Password { get; set; }

    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    [Compare("Password", ErrorMessage = "پسورد های وارد شده مغایرت دارند")]
    public string RePassword { get; set; }

}
