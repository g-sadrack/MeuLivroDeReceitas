using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Response;

namespace MyRecipeBook.Application.UseCases.User.Register;

public interface IRegisterUserUseCase {
    public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);

}

