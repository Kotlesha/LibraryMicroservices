using Book.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Category.Commands.Update;

public sealed record UpdateCategoryCommand(Guid CategoryId,
    CategoryRequestDTO CategoryDTO) : ICommand<Result>;