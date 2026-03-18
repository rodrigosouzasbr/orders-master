

using MediatR;
using Sales.Orders.Application.Common;
using Sales.Orders.Domain.Interfaces;
using Sales.Orders.Domain.ValueObjects;

namespace Sales.Orders.Application.Commands
{

    public class UpdateOrderCommandHandler
    : IRequestHandler<UpdateOrderCommand, Result<bool>>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderCommandHandler(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.Id);

            if (order is null)
                return Result<bool>.Failure("Order not found");

            var customerResult = Customer.Create(request.CustomerId, request.CustomerName);

            var companyResult = Company.Create(request.CompanyId, request.CompanyName);
         
            order.UpdateHeader(customerResult.Value!, companyResult.Value!);

            _repository.Update(order);

            await _unitOfWork.CommitAsync();

            return Result<bool>.Success(true);
        }
    }



}
