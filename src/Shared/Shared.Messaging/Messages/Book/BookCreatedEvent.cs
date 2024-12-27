namespace Shared.Messaging.Messages.Book;

public sealed record BookCreatedEvent(Guid BookId, string Title, decimal Price);