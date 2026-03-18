

namespace Sales.Orders.Application.Commands
{
    using MediatR;
    using Sales.Orders.Application.Common;
    using Sales.Orders.Application.DTOs;
    using Sales.Orders.Application.Queries;
    using Sales.Orders.Domain.Interfaces;

    public class GetOrderByIdHandler
        : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
    {
        private readonly IOrderRepository _repository;

        public GetOrderByIdHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.Id);

            if (order is null)
                return Result<OrderDto>.Failure("Order not found");

            var dto = new OrderDto(
                order.Id,
                order.Customer.Name,
                order.Total,
                order.OrderStatus.ToString()
            );

            return Result<OrderDto>.Success(dto);
        }
    }
}
