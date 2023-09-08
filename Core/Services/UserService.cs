using Core.Convertors;
using Core.DTOs;
using Core.Generator;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.User;

namespace Core.Services;

public class UserService:IUserService
{
    private MyEstateContext _context;

    public UserService(MyEstateContext context)
    {
        _context = context;
    }
    public bool IsExistUserName(string userName)
    {
        return _context.Users.Any(u => u.UserName == userName);
    }

    public bool IsExistEmail(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }
    public int AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user.UserId;
    }
    public User LoginUser(LoginViewModel login)
    {
        string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
        string email = FixedText.FiXEmail(login.Email);
        return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
    }
    public User GetUserByUserName(string username)
    {
        return _context.Users.SingleOrDefault(u => u.UserName == username);
    }

    public int GetUserIdByUserName(string username)
    {
        return _context.Users.Single(u => u.UserName == username).UserId;
    }

    public User GetUserById(int userId)
    {
        return _context.Users.Find(userId);
    }

    public void UpdateUser(User user)
    {
        _context.Update(user);
        _context.SaveChanges();
    }

    public bool IsUsersEstate(string userName, int estateId)
    {
        int userId = GetUserIdByUserName(userName);
        return _context.Estates.Any(c => c.UserId == userId && c.EstateId == estateId);
    }

    public InformationUserViewModel GetUserInformation(string username)
    {
        var user = GetUserByUserName(username);
        InformationUserViewModel information = new InformationUserViewModel();
        information.UserName = user.UserName;
        information.Email = user.Email;
        information.Phone = user.Phone;
        information.Description = user.Description;
        information.Career=user.Career;
        information.InstagramId = user.InstagramId;

        return information;
    }

    public InformationUserViewModel GetUserInformation(int userId)
    {
        var user = GetUserById(userId);
        InformationUserViewModel information = new InformationUserViewModel();
        information.UserName = user.UserName;
        information.Email = user.Email;
        information.Phone = user.Phone;
        information.Description = user.Description;
        information.Career = user.Career;
        information.InstagramId = user.InstagramId;

        return information;
    }

    public EditProfileViewModel GetDataForEditProfileUser(string? username)
    {
        return _context.Users.Where(u => u.UserName == username)
            .Select(u => new EditProfileViewModel()
            {
                ProfileImageName = u.ProfileImageName,
                Email = u.Email,
                UserName = u.UserName,
                Career = u.Career,
                InstagramId = u.InstagramId,
                Description = u.Description,
            }).Single();
    }

    public void EditProfile(string username, EditProfileViewModel profile)
    {
        if (profile.ProfileImage != null)
        {
            string imagePath = "";
            if (profile.ProfileImageName != "Defult.jpg")
            {
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProfileImage", profile.ProfileImageName);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                profile.ProfileImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(profile.ProfileImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProfileImage", profile.ProfileImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.ProfileImage.CopyTo(stream);
                }

            }
           

        }
        var user = GetUserByUserName(username);
        user.UserName = profile.UserName;
        user.Email = profile.Email;
        user.Career = profile.Career;
        user.InstagramId = profile.InstagramId;
        user.Description = profile.Description;
        user.ProfileImageName = profile.ProfileImageName;
        UpdateUser(user);

    }
    public bool CompareOldPassword(string oldPassword, string username)
    {
        string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
        return _context.Users.Any(u => u.UserName == username && u.Password == hashOldPassword);
    }

    public void ChangeUserPassword(string userName, string newPassword)
    {
        var user = GetUserByUserName(userName);
        user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
        UpdateUser(user);
    }

    public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
    {
        return _context.Users.Where(u => u.UserName == username)
            .Select(u => new SideBarUserPanelViewModel()
            {
                UserName = u.UserName,
                ProfileImageName = u.ProfileImageName,
                
            }).Single();
    }
}