using Npgsql;

namespace Soft_furniture.DataAccess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;

    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=soft-furniture-db; User Id=postgres; Password=java2001;");
    }
}
