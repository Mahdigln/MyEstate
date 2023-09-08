using Core.DTOs;
using DataLayer.Entities.User;

namespace Core.Services.Interfaces;

public interface IUserService
{
    bool IsExistUserName(string userName);
    bool IsExistEmail(string email);
    int AddUser(User user);
    User LoginUser(LoginViewModel login);
    User GetUserByUserName(string username);
    int GetUserIdByUserName(string username);
    User GetUserById(int userId);
    public void UpdateUser(User user);
    bool IsUsersEstate(string userName, int estateId);

    #region UserPanel
    InformationUserViewModel GetUserInformation(string username);
    InformationUserViewModel GetUserInformation(int userId);
    EditProfileViewModel GetDataForEditProfileUser(string username);
    void EditProfile(string username, EditProfileViewModel profile);
    bool CompareOldPassword(string oldPassword, string username);
    void ChangeUserPassword(string userName, string newPassword);
    SideBarUserPanelViewModel GetSideBarUserPanelData(string username);


    #endregion

}