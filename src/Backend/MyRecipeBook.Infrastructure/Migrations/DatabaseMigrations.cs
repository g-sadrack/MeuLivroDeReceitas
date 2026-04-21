using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace MyRecipeBook.Infrastructure.Migrations;

public class DatabaseMigrations
{
    public static void Migrate(string connectionString)
    {
        EnsureDatabaseCreated_SqlServer(connectionString);
    }

    private static void EnsureDatabaseCreated_SqlServer(string connectionString)
    {
        // Cria o construtor de connection string para manipular a string de conexão
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.InitialCatalog;

        connectionStringBuilder.Remove("Database");

        using var dbconnection = new SqlConnection(connectionStringBuilder.ConnectionString);

        // Parâmetros para a consulta de verificação do banco de dados
        var parameters =  new DynamicParameters();
        parameters.Add("name", databaseName);

        // Verifica se o banco de dados já existe
        var records = dbconnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

        if(!records.Any())
        {
            dbconnection.Execute($"CREATE DATABASE {databaseName}");
        }
    }
}
