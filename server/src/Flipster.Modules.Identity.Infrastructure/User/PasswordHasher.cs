using System.Security.Cryptography;
using Flipster.Modules.Identity.Domain.User.Services;

namespace Flipster.Modules.Identity.Infrastructure.User;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));
        using var algorithm = new Rfc2898DeriveBytes(password, saltSize: 16, iterations: 10000);
        var key = Convert.ToBase64String(algorithm.GetBytes(32));
        var salt = Convert.ToBase64String(algorithm.Salt);
        return $"{salt}.{key}";
    }

    public bool VerifyHashedPassword(string passwordHash, string password)
    {
        if (string.IsNullOrEmpty(passwordHash))
            throw new ArgumentNullException(nameof(passwordHash));
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));
        var parts = passwordHash.Split('.', 2);
        if (parts.Length != 2)
            throw new FormatException("Unexpected hash format. Should be formatted as `{salt}.{hash}`");
        var salt = Convert.FromBase64String(parts[0]);
        var key = Convert.FromBase64String(parts[1]);
        using var algorithm = new Rfc2898DeriveBytes(password, salt, iterations: 10000);
        var keyToCheck = algorithm.GetBytes(32);
        return keyToCheck.Length == key.Length && Compare(keyToCheck, key);
    }

    private static bool Compare(byte[] array1, byte[] array2)
    {
        var diff = (uint)array1.Length ^ (uint)array2.Length;
        for (var i = 0; i < array1.Length && i < array2.Length; i++)
            diff |= (uint)(array1[i] ^ array2[i]);
        return diff == 0;
    }
}