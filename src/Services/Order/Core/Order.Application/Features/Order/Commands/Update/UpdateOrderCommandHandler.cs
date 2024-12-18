﻿using AutoMapper;
using global::Order.Domain.Repositories;
using Order.Application.Errors;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.CleanArchitecture.Presentation.Providers;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Update;

using Order = Domain.Entities.Order;

internal class UpdateOrderCommandHandler(
    IOrderRepository orderRepository,
    IBookRepository bookRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserIdProvider userIdProvider) : ICommandHandler<UpdateOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserIdProvider _userIdProvider = userIdProvider;

    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        var userId = _userIdProvider.GetAuthUserId();

        if (!Guid.TryParse(userId, out Guid id))
        {
            //return Result.Failure()
        }

        if (order is null) 
        {
            return Result.Failure(ApplicationErrors.Order.NotFound);
        }

        var newOrder = _mapper.Map<Order>(request.OrderDTO);

        var books = await _bookRepository.GetExistingEntitiesByIdsAsync(request.OrderDTO.BooksIds, cancellationToken);

        if(books.Count() > request.OrderDTO.BooksIds.Count())
        {
            return Result.Failure(ApplicationErrors.Order.NotAllBooksFound);
        }

        var unavailableBooks = books.Where(b => !b.IsAvailable).ToList();
        if (unavailableBooks.Any())
        {
            return Result.Failure(ApplicationErrors.Order.NotAllBooksIsAvailable);
        }

        order.UpdateBooks(books);

        await _orderRepository.UpdateAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
