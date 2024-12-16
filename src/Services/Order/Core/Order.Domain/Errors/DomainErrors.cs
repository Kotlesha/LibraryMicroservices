using Shared.Components.Errors;

namespace Order.Domain.Errors;

public static class DomainErrors
{
    public static class Order
    {
        public static readonly Error BookNotAvailable = Error.(
            code: "Order.AddBookToOrder",
            message: "This book is not available");

        public static readonly Error BookAlreadyExists = Error.(
            code: "Order.AddBookToOrder",
            message: "This book is already in the order");

        public static readonly Error BookNotFound = Error.(
            code: "Order.RemoveBookFromOrder",
            message: "This book is not found in the order");
    }
}
