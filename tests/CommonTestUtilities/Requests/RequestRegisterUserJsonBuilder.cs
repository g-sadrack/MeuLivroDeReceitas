using Bogus;
using MyRecipeBook.Communication.Request;

namespace CommonTestUtilities.Requests;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build(int passwordLenght = 10)
    {
        // Exemplo de construção de um objeto RequestRegisterUserJson
        // return new RequestRegisterUserJson
        // {
        //     Name = "Test User",
        //     Email = "email@email.com"
        // };

        // Utilizando a biblioteca Bogus para gerar dados aleatórios
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.Name))
            .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLenght));
    }
}

