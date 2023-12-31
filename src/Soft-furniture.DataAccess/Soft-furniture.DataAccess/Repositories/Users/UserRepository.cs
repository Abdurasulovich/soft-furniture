﻿using Dapper;
using Soft_furniture.DataAccess.Interfaces.Users;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Users;
using Soft_furniture.Domain.Entities.Users;

namespace Soft_furniture.DataAccess.Repositories.Users;

public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM users";

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

    public async Task<int> CreateAsync(User entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO " +
                "users(first_name, last_name, phone_number, phone_number_confirmed, password_hash, salt, country, " +
                "region, city, address, created_at, updated_at, identityrole) " +
                "VALUES (@FirstName, @LastName, @PhoneNumber, @PhoneNumberConfirmed, @PasswordHash, @Salt, @Country, @Region, @City, @Address, @CreatedAt, @UpdatedAt, @IdentityRole);";
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
            string query = "DELETE FROM users WHERE id = @id";
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

    public async Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM users ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<UserViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<UserViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<UserViewModel?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM users WHERE id = @Id";

            var result = await _connection.QuerySingleAsync<UserViewModel>(query, new { Id = id });
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

    public async Task<User?> GetByIdCheckUser(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * From users where id=@Id";
            var result = await _connection.QuerySingleAsync<User>(query, new { Id = id });
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

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        try
        {
            await _connection.OpenAsync();
            string query = " SELECT * FROM users WHERE phone_number = @PhoneNumber";
            var data = await _connection.QuerySingleAsync<User>(query, new { PhoneNumber = phone });
            return data;
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

    public async Task<(int ItemsCount, IList<UserViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM users WHERE first_name ILIKE '%{search}%' " +
                           $"OR last_name ILIKE '%{search}%' " +
                           $"OR phone_number ILIKE '%{search}%' " +
                           $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<UserViewModel>(query)).ToList();
            return (result.Count, result);
        }
        catch
        {
            return (0, new List<UserViewModel>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, User entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE users " +
                "SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, phone_number_confirmed=@PhoneNumberConfirmed, password_hash=@PasswordHash, salt=@Salt, country=@Country, region=@Region, city=@City, address=@Address, created_at=@CreatedAt, updated_at=@UpdatedAt, identityrole=@IdentityRole" +
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
