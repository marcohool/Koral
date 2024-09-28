﻿using Core.Application.APIs.KoralMatch.Models;
using Core.Application.Models.Matching;
using Core.Domain.Entities;

namespace Core.Application.Services.Interfaces;

public interface IMatchingService
{
    Task<IEnumerable<MatchResult>> GetMatches(
        ItemEmbedding itemEmbedding,
        CancellationToken cancellationToken
    );
}