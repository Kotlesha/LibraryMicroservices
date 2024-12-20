using AutoMapper;
using Order.Application.Errors;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Create;

using Book = Domain.Entities.Book;
using Order = Domain.Entities.Order;

internal class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IBookRepository bookRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork, 
    IUserIdProvider userIdProvider) : ICommandHandler<CreateOrderCommand, Result<Guid>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserIdProvider _userIdProvider = userIdProvider;

    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = _userIdProvider.GetAuthUserId();

        if (!Guid.TryParse(userId, out Guid id)) 
        {
            //return Result.Failure()
        }


        var order = /*_mapper.Map<Order>(request.OrderDTO);*/
            Order.Create(
                id,
                request.OrderDTO.TotalCost);

        var books = new List<Book>();


        foreach (var bookid in request.OrderDTO.BooksIds)
        {
            var book = await _bookRepository.GetByIdAsync(bookid, cancellationToken);

            if (book is null)
            {
                return Result.Failure<Guid>(ApplicationErrors.Order.NotFound);
            }

            if (!book.IsAvailable)
            {
                return Result.Failure<Guid>(ApplicationErrors.Order.NotAvailable);
            }

            books.Add(book);
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
