﻿using Dapper;
using Soft_furniture.DataAccess.Interfaces.Types;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Furniture_Types;
using Soft_furniture.Domain.Entities.Furniture_Type;

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

    public async Task<IList<Furniture_Type>> GetAllByCatalogIdAsync(long catalogId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM furniture_type WHERE furniture_catalog_id ={catalogId} ORDER BY id DESC";
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

    public async Task<Furniture_Type> GetByNameAsync(string name)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM furniture_type WHERE name = @Name";

            var result = await _connection.QuerySingleAsync<Furniture_Type>(query, new { Name = name });
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
