using Book.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Category.Commands.Create;

public sealed record CreateCategoryCommand(
    CategoryRequestDTO CategoryDTO) : ICommand<Result<Guid>>;