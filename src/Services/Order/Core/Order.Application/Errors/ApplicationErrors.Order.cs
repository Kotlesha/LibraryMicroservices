using Shared.CleanArchitecture.Common;

namespace Order.Application.Errors;

public static partial class ApplicationErrors
{
    public static class Order
    {
        public static readonly Error NonExistentIds = new(
             code: "Book.NonExistentIds",
             message: "There are some ids who doesn't exist");

        public static readonly Error NotAvailable = new(
             code: "Book.NotAvailable",
             message: "Some books are not available");

    }
}
