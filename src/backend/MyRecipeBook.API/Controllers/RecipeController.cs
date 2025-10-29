using MyRecipeBook.API.Atributes;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.API.Controllers
{
    [AuthenticatedUser]
    public class RecipeController : MyRecipeBookBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredRecipeJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
         [FromServices] IRegisterRecipeUseCase useCase,
         [FromBody] RequestRecipeJson request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }
    }
}
