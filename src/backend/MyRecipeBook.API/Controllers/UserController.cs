using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public IActionResult Register(RequestRegisterUserJson request)
        {
            var useCase = new RegisterUserUseCase();
            var result = useCase.Execute(request);
            return Created(string.Empty, result);
        }
    }
}
