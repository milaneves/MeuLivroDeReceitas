using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Access
{
    public abstract class JwtTokenHandler
    {
        protected SymmetricSecurityKey SecurityKey(string singinKey)
        {
            var bytes = Encoding.UTF8.GetBytes(singinKey);
            return new SymmetricSecurityKey(bytes);
        }
    }
}
