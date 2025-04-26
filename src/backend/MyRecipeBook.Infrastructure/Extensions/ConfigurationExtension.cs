using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static bool IsUnitTestEnviroment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("InMemoryTest");
        }
        public static string ConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString("Connection")!;
        }

    }
}
