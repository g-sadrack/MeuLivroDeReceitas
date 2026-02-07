using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.Services.Mappers;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Exception.MyRecipebookException;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase(UserMapper UserMapper)
    {

        private readonly UserMapper UserMapper = UserMapper;

        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            
            // validar a request 
            Validate(request);

            // mapear a request em uma entidade
            var user = UserMapper.ToEntity(request);

            // criptografia da senha
            user.Password = PasswordEncripter.Encrypt(request.Password);

            // salvar no banco de dados

            return new ResponseRegisteredUserJson {
                Name = request.Name,
            };
        }

        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);

            }
        }
    }
}
