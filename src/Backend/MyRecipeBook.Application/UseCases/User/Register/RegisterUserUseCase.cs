using FluentValidation.Results;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.Services.Mappers.Interfaces;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.MyRecipebookException;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase(
    IUserWriteOnlyRepository writeOnlyRepository,
    IUserReadOnlyRepository readOnlyRepository,
    IUnityOfWork unityOfWork,
    PasswordEncripter passwordEncripter,
    IUserMapper userMapper) : IRegisterUserUseCase
{

    private readonly IUserWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IUserMapper _userMapper = userMapper;
    private readonly PasswordEncripter _passwordEncripter = passwordEncripter;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request) {

        // validar a request 
        Validate(request);

        // mapear a request em uma entidade
        var user = _userMapper.ToEntity(request);

        // criptografia da senha
        user.Password = _passwordEncripter.Encrypt(request.Password);

        // salvar no banco de dados
        await _writeOnlyRepository.Add(user);
        await _unityOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = request.Name,
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var emailExists = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure( string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED ) );
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);

        }
    }
}