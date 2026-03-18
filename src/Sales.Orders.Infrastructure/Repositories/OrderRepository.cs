using Microsoft.EntityFrameworkCore;
using Sales.Orders.Domain.Entities;
using Sales.Orders.Domain.Enums;
using Sales.Orders.Domain.Interfaces;
using Sales.Orders.Infrastructure.Data;

namespace Sales.Orders.Infrastructure.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<Order?> FindByIdAsync(Guid id)
    {
        return await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(Order entity)
    {
        _context.Orders.Update(entity);
    }

    public async Task<List<Order>> GetPendingAsync()
    {
        return await _context.Orders
            .Where(o => o.OrderStatus == OrderStatus.Pending)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<(List<Order>, int)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.Orders.AsNoTracking();

        var totalCount = await query.CountAsync();

        var orders = await query
            .OrderByDescending(o => o.CreatedAt) 
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (orders, totalCount);
    }
}