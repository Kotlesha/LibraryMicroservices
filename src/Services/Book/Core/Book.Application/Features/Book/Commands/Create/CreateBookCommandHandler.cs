using AutoMapper;
using Book.Application.Abstractions.Service;
using Book.Application.Errors;
using Book.Domain.Repositories;
using MassTransit;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;
using Shared.Messaging.Messages.Book;

namespace Book.Application.Features.Book.Commands.Create;

using Book = Domain.Entities.Book;

internal class CreateBookCommandHandler(
    IBookRepository bookRepository,
    ICategoryRepository categoryRepository,
    IAuthorService authorService,
    IGenreService genreService,
    IMapper mapper,
    IPublishEndpoint publishEndpoint,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateBookCommand, Result<Guid>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IAuthorService _authorService = authorService;
    private readonly IGenreService _genreService = genreService;
    private readonly IMapper _mapper = mapper;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var bookNameAlreadyExists = await _bookRepository.GetBookByTitleAsync(
            request.BookDTO.Title, 
            cancellationToken);

        if (bookNameAlreadyExists is not null)
        { 
            return Result.Failure<Guid>(ApplicationErrors.Book.NameAlreadyExists); 
        }

        var category = await _categoryRepository.GetByIdAsync(
            request.BookDTO.CategoryId);

        if (category is null)
        {
            return Result.Failure<Guid>(ApplicationErrors.Book.NotFoundCategory);
        }

        var book = _mapper.Map<Book>(request.BookDTO);

        var authors = await _authorService.GetAuthorsAsync(request.BookDTO.AuthorsIds, cancellationToken);

        if (authors.IsFailure)
        {
            return Result.Failure<Guid>(authors.Error);
        }

        foreach (var author in authors.Value)
        {
            book.AddAuthorToBook(author);
        }

        var genres = await _genreService.GetGenresAsync(request.BookDTO.GenresIds, cancellationToken);

        if (genres.IsFailure)
        {
            return Result.Failure<Guid>(genres.Error);
        }

        foreach (var genre in genres.Value)
        {
            book.AddGenreToBook(genre);
        }

        _bookRepository.Add(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _publishEndpoint.Publish(
            new BookCreatedEvent(book.Id, book.Title, book.Price),
            cancellationToken);

        return book.Id;
    }
}