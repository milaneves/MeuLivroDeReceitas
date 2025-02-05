using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AutoMapper(services);
            AddUseCases(services);
            AddPasswordEncripter(services);
        }

        private static void AutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }

        private static void AddPasswordEncripter(IServiceCollection services)
        {
            services.AddScoped(option => new PasswordEncripter());
        }
    }
}
