using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace DataLayer.Entities.Estate;

public class Estate
{
    [Key]
    public int EstateId { get; set; }
    [Required]
    public int EstateTypeId { get; set; }


    [Required]
    [Display(Name = "عنوان ملک")]
    [MaxLength(200, ErrorMessage = "{0}نمیتواند بیشتر از {1} کاراکتر باشد.")]
    public string EstateTitle { get; set; }

    [Display(Name = "متراژ کل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int TotalArea { get; set; }

    [Display(Name = "متراژ بنا")]
    public int BuildingArea { get; set; }
    [Display(Name = "تعداد اتاق")]
    public int NumberOfRoom { get; set; }

    [Display(Name = "قیمت")]
    public double Price { get; set; }
    public string DemoFileName { get; set; }
    [Required]
    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int UserId { get; set; }


    #region Relations

    public User.User User { get; set; }
    public List<EstateFeature> EstateFeatures { get; set; }
    public List<EstateImage> EstateImages { get; set; }

    [ForeignKey("EstateTypeId")]
    public EstateType EstateType { get; set; }

    #endregion


}