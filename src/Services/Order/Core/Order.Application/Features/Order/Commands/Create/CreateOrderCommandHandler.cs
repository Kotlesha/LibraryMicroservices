﻿using AutoMapper;
using Order.Application.Errors;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Create;

using Book = Domain.Entities.Book;
using Order = Domain.Entities.Order;

internal class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IBookRepository bookRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand, Result<Guid>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request.OrderDTO);

        var books = new List<Book>();

        foreach (var bookid in request.OrderDTO.BooksIds) 
        {
            var book = await _bookRepository.GetByIdAsync(bookid, cancellationToken);

            if (book is not null)
            {
                books.Add(book);
            }
        }

        if (request.OrderDTO.BooksIds.Count() != books.Count)
        {
            return Result.Failure<Guid>(ApplicationErrors.Order.NonExistentIds);
        }

        var availableBooks = books.Where(book => book.IsAvailable).ToList();

        if (request.OrderDTO.BooksIds.Count() != availableBooks.Count)
        {
            return Result.Failure<Guid>(ApplicationErrors.Order.NotAvailable);
        }

        foreach (var book in books)
        {
            order.AddBookToOrder(book);
        }
        
        await _orderRepository.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
