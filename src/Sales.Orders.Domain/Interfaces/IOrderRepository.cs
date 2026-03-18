using Sales.Orders.Domain.Entities;

namespace Sales.Orders.Domain.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> FindByIdAsync(Guid id);
    void Update(Order entity);
    Task<List<Order>> GetPendingAsync();
    Task<Order?> GetByIdAsync(Guid id);
    Task<(List<Order> Orders, int TotalCount)> GetPagedAsync(int page, int pageSize);
}