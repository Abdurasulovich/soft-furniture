using Dapper;
using Soft_furniture.DataAccess.Interfaces.Orders;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Orders;
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
                "orders(user_id, deliver_id, product_price, delivery_price, total_price, latitude, longitude, " +
                "status, is_contracted, description, is_paid, payment_type, created_at, updated_at, product_id) " +
                "VALUES (@UserId, @DeliverId, @ProductPrice, @DeliveryPrice, @TotalPrice, @Latitude, @Longitude, @Status, " +
                "@IsContracted, @Description, @IsPaid, @PaymentType, @CreatedAt, @UpdatedAt, @ProductId);";
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
            string query = "SELECT * FROM orders " +
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

    public async Task<IList<OrderVM>> GetAllUserOrderAsync(long userId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM \"UserOrders\" WHERE user_id = {userId} ORDER BY total_price DESC";
            var result = (await _connection.QueryAsync<OrderVM>(query)).ToList();
            return result;

        }
        catch
        {
            return new List<OrderVM>();
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
                "description=@Description, is_paid=@IsPaid, payment_type=@Payment, created_at=@CreatedAt, updated_at=@UpdatedAt, product_id=@ProductId " +
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
