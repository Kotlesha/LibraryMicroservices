using AutoMapper;
using Book.Application.Abstractions.Service;
using Book.Application.Errors;
using Book.Domain.Repositories;
using MassTransit;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;
using Shared.Messaging.Messages.Book;

namespace Book.Application.Features.Book.Commands.Update;

using Book = Domain.Entities.Book;

internal class UpdateBookCommandHandler(
    IBookRepository bookRepository,
    ICategoryRepository categoryRepository,
    IAuthorService authorService,
    IGenreService genreService,
    IPublishEndpoint publishEndpoint,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateBookCommand, Result>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IAuthorService _authorService = authorService;
    private readonly IGenreService _genreService = genreService;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(request.BookId, cancellationToken);

        if (book is null)
        {
            return Result.Failure(ApplicationErrors.Book.NotFound);
        }

        var category = await _categoryRepository.GetByIdAsync(
            request.BookDTO.CategoryId);

        if (category is null)
        {
            return Result.Failure(ApplicationErrors.Book.NotFoundCategory);
        }

        var authorsValidationResult = await _authorService.GetAuthorsAsync(
            request.BookDTO.AuthorsIds, 
            cancellationToken);

        if (authorsValidationResult.IsFailure)
        {
            return Result.Failure(authorsValidationResult.Error);
        }

        var genresValidationResult = await _genreService.GetGenresAsync(
            request.BookDTO.GenresIds, 
            cancellationToken);

        if (genresValidationResult.IsFailure)
        {
            return Result.Failure(genresValidationResult.Error);
        }

        var newBook = _mapper.Map<Book>(request.BookDTO);
        book.Update(newBook);

        book.UpdateAuthors(authorsValidationResult.Value);
        book.UpdateGenres(genresValidationResult.Value);

        _bookRepository.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _publishEndpoint.Publish(
            new BookUpdatedEvent(
                book.Id,
                book.Title,
                book.Price),
            cancellationToken);

        return Result.Success();
    }
}