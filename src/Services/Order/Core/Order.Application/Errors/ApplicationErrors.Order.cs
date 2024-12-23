using Shared.Components.Errors;

namespace Order.Application.Errors;

public static partial class ApplicationErrors
{
    public static class Order
    {
        public static readonly Error NonExistentIds = Error.Conflict(
             code: "Book.NonExistentIds",
             message: "There are some ids who doesn't exist");

        public static readonly Error NotAvailable = Error.BadRequest(
             code: "Book.NotAvailable",
             message: "Some books are not available");

        public static readonly Error NotFound = Error.NotFound(
             code: "Order.NotFound",
             message: "Order doesn't exist");
      
        public static readonly Error NotBelongToUser = Error.Conflict(
             code: "Order.NotBelong",
             message: "This order doesn't belong to the user");

        public static readonly Error NotAllBooksFound = Error.NotFound(
             code: "Order.NotAllBooksFound",
             message: "Not all books in the order were found");

        public static readonly Error NotAllBooksIsAvailable = Error.BadRequest(
             code: "Order.NotAllBooksIsAvailable",
             message: "Not all books in the order were available");

        public static readonly Error AlreadyCancelled = Error.BadRequest(
             code: "Order.AlreadyCancelled",
             message: "This order has already been cancelled");
    }
}