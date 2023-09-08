using System.Drawing;
using Microsoft.AspNetCore.Http;
namespace Core.Security;

public static class ImageValidator
{
    public static bool IsImage(this IFormFile file)
    {
        try
        {
            var img = Image.FromStream(file.OpenReadStream());
            return true;
        }
        catch
        {
            return false;
        }
    }
    //public static bool IsImage(this List<IFormFile> files)
    //{
    //    try
    //    {
    //        var img = Image.FromStream(files.o);
    //        return true;
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}
}