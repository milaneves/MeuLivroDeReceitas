using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography
{
    public class PasswordEncripter
    {
        public string Encrypt(string password)
        {
            var additionalKey = "ABC";
            var newPassword = $"{password}{additionalKey}";
            var bytes = Encoding.UTF8.GetBytes(newPassword);
            var hashbytes = SHA512.HashData(bytes);
            return StringBytes(hashbytes);
        }

        private static string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
