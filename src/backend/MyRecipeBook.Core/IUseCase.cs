namespace MyRecipeBook.Core
{
    public interface IUseCase
    {
        Task Execute();
    }

    public interface IUseCaseWithRequest<TRequest>
    {
        Task Execute(TRequest request, CancellationToken cancellation = default);
    }

    public interface IUseCaseWithResponse<TResponse>
    {
        Task<TResponse> Execute(CancellationToken cancellation = default);
    }

    public interface IUseCase<TRequest, TResponse>
    {
        Task<TResponse> Execute(TRequest request, CancellationToken cancellation = default);
    }
}
