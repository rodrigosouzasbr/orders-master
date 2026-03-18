

using MediatR;
using Sales.Orders.Application.Common;
using Sales.Orders.Domain.Interfaces;

namespace Sales.Orders.Application.Commands
{
    public class UpdateOrderItemHandler
        : IRequestHandler<UpdateOrderItemCommand, Result<bool>>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderItemHandler(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId);

            if (order is null)
                return Result<bool>.Failure("Order not found");

            order.UpdateItem(
                request.ItemId,
                request.Quantity,
                request.UnitPrice,
                request.Discount
            );

            _repository.Update(order);
            await _unitOfWork.CommitAsync();

            return Result<bool>.Success(true);
        }
    }
}
