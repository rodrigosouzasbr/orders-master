

namespace Sales.Orders.Application.Commands
{
    using MediatR;
    using Sales.Orders.Application.Common;
    using Sales.Orders.Application.DTOs;
    using Sales.Orders.Application.Queries;
    using Sales.Orders.Domain.Interfaces;

    public class GetPendingOrdersHandler
        : IRequestHandler<GetPendingOrdersQuery, Result<List<OrderDto>>>
    {
        private readonly IOrderRepository _repository;

        public GetPendingOrdersHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<OrderDto>>> Handle(GetPendingOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repository.GetPendingAsync();

            var result = orders.Select(o => new OrderDto(
                o.Id,
                o.Customer.Name,
                o.Total,
                o.OrderStatus.ToString()
            )).ToList();

            return Result<List<OrderDto>>.Success(result);
        }
    }
}
