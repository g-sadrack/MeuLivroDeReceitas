using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructure.Extensions;

public static class ConfigurationExtensio
{
    public static string ConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("SqlServer")!;
    }
}
