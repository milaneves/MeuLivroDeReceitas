﻿using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
    }
}
