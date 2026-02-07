using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.Services.Mappers;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Response;

namespace MyRecieBook.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserMapper userMapper;

    public UserController()
    {
        userMapper = new UserMapper();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisterUserJson request)
    {
        var useCase = new RegisterUserUseCase(userMapper);
        var result = useCase.Execute(request);
        return Created(string.Empty, result);
    }
}