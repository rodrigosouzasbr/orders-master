

using MediatR;
using Sales.Orders.Application.Common;
using Sales.Orders.Domain.Entities;
using Sales.Orders.Domain.Interfaces;
using Sales.Orders.Domain.ValueObjects;

namespace Sales.Orders.Application.Commands
{
    public class AddOrderItemHandler
        : IRequestHandler<AddOrderItemCommand, Result<Guid>>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddOrderItemHandler(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.FindByIdAsync(request.OrderId);

            if (order is null)
                return Result<Guid>.Failure("Order not found");

            var item = new OrderItem(
                new Product(request.Item.ProductId, request.Item.ProductName),
                request.Item.Quantity,
                request.Item.UnitPrice,
                request.Item.Discount
            );

            order.AddItem(item);  

            _repository.Update(order);

            await _unitOfWork.CommitAsync();

            return Result<Guid>.Success(item.Id);
        }
    }
}
