﻿using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Book.Queries.GetByTitle;

public sealed record GetBookByTitleQuery(string BookTitle) : IQuery<Result<BookResponseDTO>>;