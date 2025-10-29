using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipes;
using MyRecipeBook.Domain.Services.LoggedUser;

namespace MyRecipeBook.Application.UseCases.Recipe.Register;

public interface IRegisterRecipeUseCase : IUseCase<RequestRecipeJson, ResponseRegisteredRecipeJson> { }
public class RegisterRecipeUseCase : IRegisterRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterRecipeUseCase(IRecipeRepository recipeRepository, ILoggedUser loggedUser, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _recipeRepository = recipeRepository;
        _loggedUser = loggedUser;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisteredRecipeJson> Execute(RequestRecipeJson request, CancellationToken cancellationToken = default)
    {
        await Validate(request);
        var loggedUser = await _loggedUser.User();
        var recipe = _mapper.Map<Domain.Entities.Recipe>(request);
        recipe.UserId = loggedUser.Id;

        var instructions = request.Instructions.OrderBy(i => i.Step).ToList();
        for (var index = 0; index < instructions.Count; index++)
            instructions.ElementAt(index).Step = index + 1;

        recipe.Instructions = _mapper.Map<IList<Domain.Entities.Instruction>>(instructions);
        await _recipeRepository.Add(recipe);
        await _unitOfWork.Commit();
        return _mapper.Map<ResponseRegisteredRecipeJson>(recipe);
    }

    private async Task Validate(RequestRecipeJson request)
    {
        var result = new RecipeValidator().Validate(request);  

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).Distinct().ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}

