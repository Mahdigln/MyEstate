using System.Drawing;
using System.Drawing.Drawing2D;
using Core.Convertors;
using Core.DTOs;
using Core.Generator;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Estate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class EstateService : IEstateService
{
    private MyEstateContext _context;
    IUserService _userService;
    public EstateService(MyEstateContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public int AddEstate(Estate estate, List<IFormFile> imgEstate, IFormFile imgDemo, string userName)
    {
        estate.CreateDate = DateTime.Now;
        var ImageName = new EstateImage()
        {
            EstateImageName = "no-photo.jpg"
        };
        // Upload estate images
        if (imgEstate != null)
        {

            var listimg = estate.EstateImages = new List<EstateImage>();
            foreach (var imgEstates in imgEstate)
            {
                if (imgEstates.IsImage())
                {
                    ImageName.EstateImageName =
                        NameGenerator.GenerateUniqCode() + Path.GetExtension(imgEstates.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/State/Images",
                        ImageName.EstateImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        imgEstates.CopyTo(stream);
                    }
                    listimg.Add(new EstateImage()
                    {
                        EstateImageName = ImageName.EstateImageName,
                        EstateId = estate.EstateId,

                    });

                }
            }



        }

        //Upload Demo
        if (imgDemo != null && imgDemo.IsImage())
        {
            estate.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgDemo.FileName);
            string demoePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/State/Demos", estate.DemoFileName);
            using (var stream = new FileStream(demoePath, FileMode.Create))
            {
                imgDemo.CopyTo(stream);
            }
           
        }



        estate.UserId = _userService.GetUserIdByUserName(userName);
        // _context.EstateImages.Add(EstateImages);
        _context.Estates.Add(estate);
        // _context.Add(estate);
        _context.SaveChanges();
        return estate.EstateId;

    }

    public void UpdateEstate(Estate estate, List<IFormFile> imgEstate, IFormFile imgDemo, string userName)
    {
        estate.UpdateDate = DateTime.Now;

        // Upload estate images
        if (imgEstate != null)
        {

            //List<EstateImage> lisEstateImages = new List<EstateImage>();
            //foreach (var imageName in lisEstateImages)
            //{
            //    // Delete estate images
            //    if (ImageName.EstateImageName != "no-photo.jpg")
            //    {
            //        string deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/State/Images", imageName.EstateImageName);
            //        if (File.Exists(deleteImagePath))
            //        {
            //            File.Delete(deleteImagePath);
            //        }
            //    }
            //}
            EstateImage ImageName = new EstateImage();
            var listimg = estate.EstateImages = new List<EstateImage>();
            foreach (var imgEstates in imgEstate)
            {
                if (imgEstates.IsImage())
                {

                    // Upload estate images

                    ImageName.EstateImageName =
                        NameGenerator.GenerateUniqCode() + Path.GetExtension(imgEstates.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/State/Images",
                        ImageName.EstateImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        imgEstates.CopyTo(stream);
                    }
                    listimg.Add(new EstateImage()
                    {
                        EstateImageName = ImageName.EstateImageName,
                        EstateId = estate.EstateId,

                    });

                }
            }

        }

        if (imgDemo != null && imgDemo.IsImage())
        {
            //Delete Demo
            if (estate.DemoFileName != null)
            {
                string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/State/Demos", estate.DemoFileName);
                if (File.Exists(deleteDemoPath))
                {
                    File.Delete(deleteDemoPath);
                }
            }
            //Upload Demo
            estate.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgDemo.FileName);
            string demoePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/State/Demos", estate.DemoFileName);
            using (var stream = new FileStream(demoePath, FileMode.Create))
            {
                imgDemo.CopyTo(stream);
            }
        }

        estate.UserId = _userService.GetUserIdByUserName(userName);
        _context.Estates.Update(estate);
        _context.SaveChanges();


    }

    public List<SelectListItem> GetEstateTypeForManageEstate()
    {
        return _context.EstateTypes
            .Select(g => new SelectListItem()
            {
                Text = g.EstateTypeTitle,
                Value = g.EstateTypeId.ToString()
            }).ToList();
    }

    public void AddFeature(int estateId, string featureTitle)
    {
        var feature = _context.EstateFeatures.Add(new EstateFeature()
        {
            EstateId = estateId,
            FeatureTitle = featureTitle
        });
        _context.Add(feature);
        _context.SaveChanges();
    }

    public Estate GetEstateById(int estateId)
    {
        return _context.Estates.Find(estateId);

    }
    public EstateImage GetEstatImageeById(int estateId)
    {
        return _context.EstateImages.Find(estateId);
    }

    public List<EstateImage> GetEstatImagees(int estateId)
    {
        return _context.EstateImages.Where(c => c.EstateId == estateId).ToList();
    }

    public void DeleteEstateImage(string imgName)
    {
        if (imgName != null)
        {
            string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/State/Images", imgName);
            if (File.Exists(deleteDemoPath))
            {
                File.Delete(deleteDemoPath);
            }
        }

        var delete = _context.EstateImages.Single(d => d.EstateImageName == imgName);
        _context.EstateImages.Remove(delete);
        _context.SaveChanges();

    }

    public void DeleteEstate(int estateId)
    {
      
        var img = GetEstatImagees(estateId);
        var estate = GetEstateById(estateId);
        //Delete Estate Images
        if (img != null)
        {
            var listimg = new List<EstateImage>();

            foreach (var imgEstates in img)
            {
                {
                    string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/State/Images", imgEstates.EstateImageName);
                    if (File.Exists(deleteDemoPath))
                    {
                        File.Delete(deleteDemoPath);
                    }
                    listimg.Remove(new EstateImage()
                    {
                        EstateImageName = imgEstates.EstateImageName,
                        EstateId = imgEstates.EstateId,

                    });

                }
                var delete = _context.EstateImages.Single(d => d.EstateImageName == imgEstates.EstateImageName);
                _context.EstateImages.Remove(delete);
            }
        }
       
        //Delete Demo
        if (estate.DemoFileName != null)
        {
            string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/State/Demos", estate.DemoFileName);
            if (File.Exists(deleteDemoPath))
            {
                File.Delete(deleteDemoPath);
            }
        }
        _context.Estates.Remove(estate);
        _context.SaveChanges();

    }
    public List<Estate> GetEstatListById(string username)
    {
        var userId = _userService.GetUserIdByUserName(username);
        return _context.Estates.Where(c=>c.UserId== userId).ToList();
    }

    public Tuple<List<ShowEstateListItemViewModel>, int> GetEstate(int pageId = 1, string filter = "", string estateType = "all", string orderByTime = "date",
        int startPrice = 0, int endPrice = 0, int take = 0)
    {
        if (take == 0)
            take = 8;

        IQueryable<Estate> result = _context.Estates;

        if (!string.IsNullOrEmpty(filter))
        {
            result = result.Where(c => c.EstateTitle.Contains(filter));
        }

        #region estateType

        switch (estateType)
        {
            case "all":
                break;
            case "garden":
            {
                result = result.Include(c=>c.EstateType).Where(c => c.EstateType.EstateTypeId==20);
                break;
            }
            case "residential":
            {
                result = result.Include(c => c.EstateType).Where(c => c.EstateType.EstateTypeId == 21);
                break;
            }
            case "commercial":
            {
                result = result.Include(c => c.EstateType).Where(c => c.EstateType.EstateTypeId == 22);
                break;
            }
            case "land":
            {
                result = result.Include(c => c.EstateType).Where(c => c.EstateType.EstateTypeId == 23);
                break;
            }
            case "Villa":
            {
                result = result.Include(c => c.EstateType).Where(c => c.EstateType.EstateTypeId == 24);
                break;
            }
            case "Office-complex":
            {
                result = result.Include(c => c.EstateType).Where(c => c.EstateType.EstateTypeId == 25);
                break;
            }
            case "residential-complexe":
            {
                result = result.Include(c => c.EstateType).Where(c => c.EstateType.EstateTypeId == 26);
                break;
            }
            case "Store":
            {
                result = result.Include(c => c.EstateType).Where(c => c.EstateType.EstateTypeId == 27);
                break;
            }


        }

        #endregion

        switch (orderByTime)
        {
            case "date":
                {
                    result = result.OrderByDescending(c => c.CreateDate);
                    break;
                }
            case "updatedate":
                {
                    result = result.OrderByDescending(c => c.UpdateDate);
                    break;
                }
        }

        if (startPrice > 0)
        {
            result = result.Where(c => c.Price > startPrice);
        }

        if (endPrice > 0)
        {
            result = result.Where(c => c.Price < startPrice);
        }



        int skip = (pageId - 1) * take;

        int pageCount = result.Include(c => c.EstateType).ToList().Select(c => new ShowEstateListItemViewModel()
        {
           EstateId = c.EstateId,
           DemoFileName = c.DemoFileName,
           EstateTitle = c.EstateTitle,
           Price = c.Price,
           TotalArea = c.TotalArea,
           EstateTypeTitle = c.EstateType.EstateTypeTitle
          

        }).Count() / take;

        var query = result.Include(c => c.EstateType).AsEnumerable().Select(c => new ShowEstateListItemViewModel()
        {
            EstateId = c.EstateId,
            DemoFileName = c.DemoFileName,
            EstateTitle = c.EstateTitle,
            Price = c.Price,
            TotalArea = c.TotalArea,
            EstateTypeTitle = c.EstateType.EstateTypeTitle

        }).Skip(skip).Take(take).ToList();
        return Tuple.Create(query, pageCount);
    }

    public Estate GetEstateForShow(int estateId)
    {
        return _context.Estates.Include(e => e.EstateImages)
            .Include(e => e.User)
            .Include(e => e.EstateType)
            .FirstOrDefault(e => e.EstateId == estateId);
    }

    public ShowEstateViewModel GetDataforShowEstate(int estateId)
    {
        return _context.Estates.Where(e => e.EstateId == estateId)
            .Select(estate => new ShowEstateViewModel()
            {
                UserId = estate.UserId,
                BuildingArea = estate.BuildingArea,
                DemoFileName = estate.DemoFileName,
                EstateId = estate.EstateId,
                EstateTitle = estate.EstateTitle,
                EstateTypeId = estate.EstateTypeId,
                NumberOfRoom = estate.NumberOfRoom,
                Price = estate.Price,
                TotalArea = estate.TotalArea,
                EstateTypeTitle = estate.EstateType.EstateTypeTitle,
                ProfileImageName = estate.User.ProfileImageName,
                Email = estate.User.Email,
                DescriptionforUser = estate.User.Description,
                InstagramId = estate.User.InstagramId,
                UserName = estate.User.UserName,
                Phone = estate.User.Phone,
                Career = estate.User.Career,
                EstateImagesViewModels = estate.EstateImages.Select(e=> new EstateImagesViewModel()
                {
                    EstateImageId = e.EstateImageId,
                    EstateImageName = e.EstateImageName,
                }).ToList()
            })
            .FirstOrDefault();
    }



    //public ShowEstateViewModel GetDataforShowEstate(int estateId)
    //{
    //    return _context.Estates.Where(e => e.EstateId == estateId)
    //        .Select(estate => new ShowEstateViewModel()
    //        {
    //            UserId = estate.UserId,
    //            BuildingArea = estate.BuildingArea,
    //            DemoFileName = estate.DemoFileName,
    //            EstateId = estate.EstateId,
    //            EstateTitle = estate.EstateTitle,
    //            EstateTypeId = estate.EstateTypeId,
    //            NumberOfRoom = estate.NumberOfRoom,
    //            Price = estate.Price,
    //            TotalArea = estate.TotalArea,
    //            EstateTypeTitle = estate.EstateType.EstateTypeTitle,
    //            EstateImagesViewModels = estate.EstateImages.Select(e => new EstateImagesViewModel()
    //            {
    //                EstateImageId = e.EstateImageId,
    //                EstateImageName = e.EstateImageName,
    //            }).ToList()
    //        })
    //        .FirstOrDefault();
    //}

    //public void DeleteEstate(int estateId)
    //{
    //    //var delete = _context.EstateImages.Where(c => c.EstateId == estateId).First();
    //    var estate = GetEstateById(estateId);

    //    _context.EstateImages.Where(c => c.EstateId == estateId).Select(c => c.EstateImageName == estateids);


    //    var estateImage = GetEstatImageeById(estateId);
    //    _context.EstateImages.Remove(estateImage);
    //    _context.Estates.Remove(estate);

    //}


}


