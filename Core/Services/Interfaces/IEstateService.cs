using Core.DTOs;
using DataLayer.Entities.Estate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Core.Services.Interfaces;

public interface IEstateService
{
    int AddEstate(Estate estate, List<IFormFile> imgEstate,IFormFile imgDemo, string userName);
    void UpdateEstate(Estate estate, List<IFormFile> imgEstate, IFormFile imgDemo, string userName);

    List<SelectListItem> GetEstateTypeForManageEstate();

    //int  AddFeature(Estate estate,string featureTitle); 
    void AddFeature(int estateId, string featureTitle);
     Estate GetEstateById(int estateId);
     EstateImage GetEstatImageeById(int estateId);
     List<EstateImage> GetEstatImagees(int estateId);
     void DeleteEstateImage(string imgName);
     void DeleteEstate(int estateId);
     List<Estate> GetEstatListById(string username);

     Tuple<List<ShowEstateListItemViewModel>, int> GetEstate(int pageId = 1, string filter = "",
         string estateType = "all", string orderByTime = "date",
         int startPrice = 0, int endPrice = 0, int take = 0);

     Estate GetEstateForShow(int estateId);
     public ShowEstateViewModel GetDataforShowEstate(int estateId);


}