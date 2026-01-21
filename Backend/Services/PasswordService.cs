using System.Security.Cryptography;

namespace Backend.Services;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string storedHash);
}

public class PasswordService : IPasswordService
{
    private const int SaltSize = 16; 
    private const int KeySize = 32; 
    private const int Iterations = 100_000;
    private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;

    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, KeySize);
        return $"{Convert.ToHexString(salt)}:{Convert.ToHexString(hash)}";
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromHexString(parts[0]);
        byte[] hash = Convert.FromHexString(parts[1]);

        byte[] newHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, KeySize);
        return CryptographicOperations.FixedTimeEquals(hash, newHash);
    }
}