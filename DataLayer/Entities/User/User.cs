using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace DataLayer.Entities.User;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0}نمیتواند بیشتر از {1} کاراکتر باشد.")]
    public string UserName { get; set; }

    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0}نمیتواند بیشتر از {1} کاراکتر باشد.")]
    public string Password { get; set; }

    [Display(Name = "شماره تلفن")]
    [MaxLength(200, ErrorMessage = "{0}نمیتواند بیشتر از {1} کاراکتر باشد.")]
    public string Phone { get; set; }

    public string ProfileImageName { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0}نمیتواند بیشتر از {1} کاراکتر باشد.")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
    public string Email { get; set; }

    [Display(Name = "توضیحات")]
    public string Description { get; set; }

    [Display(Name = "عنوان شغلی")]
    public string? Career { get; set; }

    [Display(Name = "آیدی اینستاگرام")]
    public string InstagramId { get; set; }

    #region Relations

    public List<Estate.Estate> Estates { get; set; }

    #endregion

}