using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography
{
    public class PasswordEncripter
    {
        public static string Encrypt(string password)
        {
            var chaveAdicional = "MyRecipeBook2024!";

            var newPassword = $"{password}{chaveAdicional}";

            var bytes = Encoding.UTF8.GetBytes(newPassword);
            var hashBytes = SHA512.HashData(bytes);
            return StringBytes(hashBytes);
        }

        public static string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
