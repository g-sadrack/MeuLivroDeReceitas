using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class UnitOfWork(AppDbContext dbContext) : IUnityOfWork
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }

}

