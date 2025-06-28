using MyRecipeBook.API.Atributes;
using MyRecipeBook.Application.UseCases.User.Profile;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.API.Controllers
{
    [AuthenticatedUser]
    public class UserController : MyRecipeBookBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices]IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request)
        { 
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetUserProfile(
            [FromServices] IGetUserProfileUseCase useCase)
        {
            var result = await useCase.Execute();
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateUserUseCase useCase,
            [FromBody] RequestUpdateUserJson request)
        {
            var result = await useCase.Execute();
            return NoContent();
        }
    }
}
