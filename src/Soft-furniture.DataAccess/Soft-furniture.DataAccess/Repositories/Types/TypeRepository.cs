using Dapper;
using Soft_furniture.DataAccess.Interfaces.Types;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Furniture_Catalog;
using Soft_furniture.Domain.Entities.Furniture_Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Soft_furniture.DataAccess.Repositories.Types;

public class TypeRepository : BaseRepository, ITypeRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM furniture_type";

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

    public async Task<int> CreateAsync(Furniture_Type entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO furniture_type(name, furniture_catalog_id, image_path, description, created_at, updated_at) " +
	                        "VALUES(@Name, @FurnitureCatalogId, @ImagePath, @Description, @CreatedAt, @UpdatedAt); ";
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
            string query = "DELETE FROM furniture_type WHERE id = @id";
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

    public async Task<IList<Furniture_Type>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM furniture_type ORDER BY id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";
            var result = (await _connection.QueryAsync<Furniture_Type>(query)).ToList();
            return result;

        }
        catch
        {
            return new List<Furniture_Type>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Furniture_Type?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM furniture_type WHERE id = @Id";

            var result = await _connection.QuerySingleAsync<Furniture_Type>(query, new { Id = id });
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

    public async Task<int> UpdateAsync(long id, Furniture_Type entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE furniture_type " +
                "SET name=@Name, furniture_catalog_id=@FurnitureCatalogId, image_path=@ImagePath, description=@Description, created_at=@CreatedAt, updated_at=@UpdatedAt " +
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
