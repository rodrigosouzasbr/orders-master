

using MediatR;
using Sales.Orders.Application.Common;
using Sales.Orders.Domain.Interfaces;

namespace Sales.Orders.Application.Commands
{
    public class CancelOrderCommandHandler
        : IRequestHandler<CancelOrderCommand, Result<bool>>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelOrderCommandHandler(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId);

            if (order is null)
                return Result<bool>.Failure("Order not found");

            try
            {
                order.Cancel();
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }

            _repository.Update(order);
            await _unitOfWork.CommitAsync();

            return Result<bool>.Success(true);
        }
    }
}
