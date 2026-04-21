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
    IUserWriteOnlyRepository _writeOnlyRepository,
    IUserReadOnlyRepository _readOnlyRepository,
    IUnityOfWork _unityOfWork,
    PasswordEncripter _passwordEncripter,
    IUserMapper _userMapper) : IRegisterUserUseCase
{

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request) {

        // validar a request 
        await Validate(request);

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