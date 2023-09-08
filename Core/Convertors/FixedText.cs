namespace Core.Convertors;

public class FixedText
{
    public static string FiXEmail(string email)
    {
        return email.Trim().ToLower();
    }

}