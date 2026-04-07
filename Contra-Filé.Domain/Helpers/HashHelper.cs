namespace Contra_Filé.Domain.Helpers;

public static class HashHelper
{
    public static string Hash(string newPassword, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(newPassword, salt);
    }
    public static bool Verify(string rawPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(rawPassword, hashedPassword);
    }
}