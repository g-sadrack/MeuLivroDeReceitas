using MyRecipeBook.Application.Services.Mappers.Interfaces;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Services.Mappers
{
    public class UserMapper : IUserMapper
    {
        public User ToEntity(RequestRegisterUserJson request)
        {
            ArgumentNullException.ThrowIfNull(request);

            return new User
            {
                Name = request.Name,
                Email = request.Email
            };
        }
    }
}
