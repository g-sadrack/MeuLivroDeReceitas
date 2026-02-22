using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography
{
    public class PasswordEncripter(string additionKey)
    {
        private readonly string _additionKey = additionKey;

        public string Encrypt(string password)
        {
            var newPassword = $"{password}{_additionKey}";

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
