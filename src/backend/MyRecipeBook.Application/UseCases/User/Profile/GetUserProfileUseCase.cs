using MyRecipeBook.Domain.Services.LoggedUser;

namespace MyRecipeBook.Application.UseCases.User.Profile
{
    public interface IGetUserProfileUseCase : IUseCaseWithResponse<ResponseUserProfileJson> { }

    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;   

        public GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper) 
        { 
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseUserProfileJson> Execute(CancellationToken cancellation = default)
        {
            var user = await _loggedUser.User();
            return _mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}
