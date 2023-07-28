using Dapper;
using Soft_furniture.DataAccess.Interfaces.Catalogs;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Furniture_Catalog;
using static Dapper.SqlMapper;

namespace Soft_furniture.DataAccess.Repositories.Catalogs;

public class CatalogRepository : BaseRepository, ICatalogRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM furniture_catalog";

            var result = await _connection.QuerySingleAsync<long>(query);
            return result;

        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(Catalog entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.furniture_catalog(name, image_path, created_at, updated_at) " +
                "VALUES (@Name, @ImagePath, @CreatedAt, @UpdatedAt);";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;

        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM furniture_catalog WHERE id = @id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
            return result;

        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Catalog>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM furniture_catalog ORDER BY id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";
            var result = (await _connection.QueryAsync<Catalog>(query)).ToList();
            return result;

        }
        catch
        {
            return new List<Catalog>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Catalog?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM furniture_catalog WHERE id = @Id";

            var result = await _connection.QuerySingleAsync<Catalog>(query, new { Id = id });
            return result;

        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Catalog entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE furniture_catalog " +
                "SET name=@Name, image_path=@ImagePath, created_at = @CreatedAt, updated_at=@UpdatedAt " +
                $"WHERE id={id};";

            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
