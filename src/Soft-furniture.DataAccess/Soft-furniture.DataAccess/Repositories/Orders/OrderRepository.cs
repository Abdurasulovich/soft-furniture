using Dapper;
using Soft_furniture.DataAccess.Interfaces.Orders;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Orders;

namespace Soft_furniture.DataAccess.Repositories.Orders;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select Count(*) From orders";
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

    public async Task<int> CreateAsync(Order entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO " +
                "public.orders(user_id, deliver_id, product_price, delivery_price, total_price, latitude, longitude, " +
                "status, is_contracted, description, is_paid, payment_type, created_at, updated_at) " +
                "VALUES (@UserId, @DeliverId, @ProductPrice, @DeliveryPrice, @TotalPrice, @Latitude, @Longitude, @Status, " +
                "@IsContracted, @Description, @IsPaid, @Payment, @CreatedAt, @UpdatedAt);";
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
            string query = "Delete From orders Where id=@Id;";
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

    public async Task<IList<Order>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM public.orders Order By id Desc " +
                $"Offset {@params.GetSkipCount()} Limit {@params.PageSize}";
            var result = (await _connection.QueryAsync<Order>(query)).ToList();
            return result;

        }
        catch
        {
            return new List<Order>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Order?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select * From orders Where id=@Id";
            var result = await _connection.QuerySingleAsync<Order>(query, new { Id = id });
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

    public async Task<int> UpdateAsync(long id, Order entity)
    {
        try
        {
            await _connection.CloseAsync();
            string query = "UPDATE public.orders SET " +
                "SET user_id=@UserId, deliver_id=@DeliverId, product_price=@ProductPrice, delivery_price=@DeliveryPrice, " +
                "total_price=@TotalPrice, latitude=@Latitude, longitude=@Longitude, status=@Status, is_contracted=@IsContracted, " +
                "description=@Description, is_paid=@IsPaid, payment_type=@Payment, created_at=@CreatedAt, updated_at=@UpdatedAt" +
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
