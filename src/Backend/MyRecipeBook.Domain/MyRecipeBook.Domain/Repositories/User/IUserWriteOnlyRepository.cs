namespace MyRecipeBook.Domain.Repositories.User;

using MyRecipeBook.Domain.Entities;

public interface IUserWriteOnlyRepository
{
    // User é uma representação da tabela de usuários no banco de dados.
    public Task Add(User user);

}
