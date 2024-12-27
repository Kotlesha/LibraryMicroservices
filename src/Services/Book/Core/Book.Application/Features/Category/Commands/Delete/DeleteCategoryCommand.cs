using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Category.Commands.Delete;

public sealed record DeleteCategoryCommand(
    Guid CategoryId) : ICommand<Result>;