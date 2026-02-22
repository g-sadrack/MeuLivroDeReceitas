using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.Services.Mappers;
using MyRecipeBook.Application.Services.Mappers.Interfaces;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection service, IConfiguration configuration)
    {
        AddPasswordEncripter(service, configuration);
        AddUseCases(service);
    }

    public static void AddUseCases(IServiceCollection service)
    {
        service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        service.AddScoped<IUserMapper, UserMapper>();
        service.AddScoped<RegisterUserUseCase>();

    }

    public static void AddPasswordEncripter(IServiceCollection service, IConfiguration configuration)
    {
        var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
        service.AddScoped(option => new PasswordEncripter(additionalKey!));
    }

}

