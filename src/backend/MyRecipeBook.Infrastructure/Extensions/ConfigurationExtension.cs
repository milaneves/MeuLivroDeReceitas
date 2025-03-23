using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static string ConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString("Connection")!;
        }

    }
}
