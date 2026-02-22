using MyRecipeBook.Communication.Request;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Services.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User ToEntity(RequestRegisterUserJson request);
    }
}
