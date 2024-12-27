namespace Shared.Messaging.Messages.Book;

public sealed record BookUpdatedEvent(Guid BookId, string Title, decimal Price);
