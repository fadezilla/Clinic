using System;
using System.Security.Cryptography;
using System.Text;

public static class Security
{
    public static string GenerateSSNHash(string ssn)
    {
        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(ssn));
            return Convert.ToBase64String(hash);
        }
    }
}
