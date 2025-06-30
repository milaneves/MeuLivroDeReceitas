using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Infrastructure.Extensions;
using MyRecipeBook.Infrastructure.Security.Cryptography;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Generator;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Validator;
using MyRecipeBook.Infrastructure.Services.LoggedUser;
using System.Reflection;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddPasswordEncripter(services, configuration);
            AddRepositories(services);
            AddLoggedUser(services);

            if (configuration.IsUnitTestEnviroment())
                return;

            AddDbContext(services, configuration);
            AddTokens(services, configuration);
            AddFluentMigrator(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");
            services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
            });
        }

        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            var espirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SingningKey");
            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(espirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
        }

        private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

        private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
            services.AddScoped<IPasswordEncripter>(option => new Sha512Encripter(additionalKey!));
        }
    }
}
