using System.Security.Cryptography;
using System.Text;
using IAM.Application.common;

namespace IAM.Infrastructure.hasher;

public class SHA256Hasher : IHasher
{
    public string Hash(string code)
    {
        SHA256 sha256 = SHA256.Create();
        byte[] codeBytes = Encoding.UTF8.GetBytes(code);
        byte[] hasBytes = sha256.ComputeHash(codeBytes);
        StringBuilder hashed = new StringBuilder();
        for (int i = 0; i < hasBytes.Length; i++)
        {
            hashed.Append(hasBytes[i].ToString("X2"));
        }

        return hashed.ToString();
    }
}