using Dapper;
using Soft_furniture.DataAccess.Interfaces.Delivers;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Delivers;
using Soft_furniture.Domain.Entities.Delivers;

namespace Soft_furniture.DataAccess.Repositories.Deliveries;

public class DeliverRepository : BaseRepository, IDeliveryRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM delivers";

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

    public async Task<int> CreateAsync(Deliver entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO " +
                "delivers(first_name, last_name, phone_number, phone_number_confirmed, is_male, password_hash, salt, birth_date, passport_seria_number, country, region, city, address, description, created_at, updated_at) " +
                $"VALUES (@FirstName, @LastName, @PhoneNumber, @PhoneNumberConfirmed, @IsMale, @PasswordHash, @Salt, '{entity.BirthDate.Year}-{entity.BirthDate.Month}-{entity.BirthDate.Day}', @PasspordSeriaNumber, " +
                $"@Country, @Region, @City, @Address, @Description, @CreatedAt, @UpdatedAt);";
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
            string query = "DELETE FROM delivers WHERE id = @id";
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

    public async Task<IList<DeliverViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM delivers ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<DeliverViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<DeliverViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<DeliverViewModel?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM delivers WHERE id = @Id";

            var result = await _connection.QuerySingleAsync<DeliverViewModel>(query, new { Id = id });
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

    public async Task<Deliver?> GetByIdCheckUser(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * From delivers where id=@Id";
            var result = await _connection.QuerySingleAsync<Deliver>(query, new { Id = id });
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

    public async Task<Deliver?> GetByPhoneAsync(string phone)
    {
        try
        {
            await _connection.OpenAsync();
            string query = " SELECT * FROM delivers WHERE phone_number = @PhoneNumber";
            var data = await _connection.QuerySingleAsync<Deliver>(query, new { PhoneNumber = phone });
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

    public async Task<int> UpdateAsync(long id, Deliver entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE delivers " +
                $"SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, phone_number_confirmed=@PhoneNumberConfirmed, is_male=@IsMaled, password_hash=@PasswordHash, salt=@Salt, birth_date='{entity.BirthDate.Year}-{entity.BirthDate.Month}-{entity.BirthDate.Day}', passport_seria_number=@PasspordSeriaNumber, country=@Country, region=@Region, city=@City, address=@Address, description=@Description, created_at=@CreatedAt, updated_at=@UpdatedAt " +
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
