using Shared.CleanArchitecture.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Errors
{
    public static partial class DomainErrors
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
