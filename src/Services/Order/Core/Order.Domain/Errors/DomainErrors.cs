using Shared.CleanArchitecture.Common.Components;

namespace Order.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Order
        {
            public static readonly Error BookNotAvailable = new(
                code: "Order.AddBookToOrder",
                message: "This book is not available");

            public static readonly Error BookAlreadyExists = new(
                code: "Order.AddBookToOrder",
                message: "This book is already in the order");

            public static readonly Error BookNotFound = new(
                code: "Order.RemoveBookFromOrder",
                message: "This book is not found in the order");
        }
    }
}
