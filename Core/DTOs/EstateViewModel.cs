using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Estate;

namespace Core.DTOs;

public class ShowEstateListItemViewModel
{
    public int EstateId { get; set; }
    public string EstateTitle { get; set; }
    public int TotalArea { get; set; }
    public double Price { get; set; }
    public string DemoFileName { get; set; }
    public string EstateTypeTitle { get; set; }

}

public class ShowEstateViewModel
{
    public int EstateId { get; set; }
    public int EstateTypeId { get; set; }

   
    [Display(Name = "عنوان ملک")]
    public string EstateTitle { get; set; }

    [Display(Name = "متراژ کل")]
    public int TotalArea { get; set; }

    [Display(Name = "متراژ بنا")]
    public int BuildingArea { get; set; }
    [Display(Name = "تعداد اتاق")]
    public int NumberOfRoom { get; set; }

    [Display(Name = "قیمت")]
    public double Price { get; set; }
    public string DemoFileName { get; set; }
    public int UserId { get; set; }
    public string EstateTypeTitle { get; set; }
    public List<EstateImagesViewModel> EstateImagesViewModels { get; set; }

    //*******************************

    public string UserName { get; set; }


    [Display(Name = "شماره تلفن")]
    public string Phone { get; set; }

    public string ProfileImageName { get; set; }

    [Display(Name = "ایمیل")]
    public string Email { get; set; }

    [Display(Name = "توضیحات برای کاربر")]
    public string DescriptionforUser { get; set; }

    [Display(Name = "عنوان شغلی")]
    public string? Career { get; set; }

    [Display(Name = "آیدی اینستاگرام")]
    public string InstagramId { get; set; }
}

public class EstateImagesViewModel
{
    public int EstateImageId { get; set; }
    public string EstateImageName { get; set; }
    public ShowEstateViewModel ShowEstateViewModel { get; set; }
    
}

public class UsersEstate
{
    
}