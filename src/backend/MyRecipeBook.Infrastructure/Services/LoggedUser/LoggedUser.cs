using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Services.LoggedUser;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyRecipeBook.Infrastructure.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly MyRecipeBookDbContext _dbContext;
        private readonly ITokenProvider _tokenProvides;

        public LoggedUser(MyRecipeBookDbContext dbContext, ITokenProvider tokenProvides)
        {
            _dbContext = dbContext;
            _tokenProvides = tokenProvides;

        }

        public async Task<User> User()
        {
            var token = _tokenProvides.Value();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var userIdentifier = Guid.Parse(identifier);

            return await _dbContext
                .Users
                .AsNoTracking()
                .FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);


        }
    }
}
