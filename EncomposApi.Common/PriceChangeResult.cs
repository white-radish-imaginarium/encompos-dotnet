using EncomposApi.Models;
using EncomposApi.Types.Optional;
using FluentValidation;
using System;

namespace EncomposApi
{
    public record PriceChangeResult
    {
        public int Remaining { get; init; }
        public PriceChangeQuery NextQuery { get; init; }
        public PriceChangeModel[] PriceChanges { get; init; }
    }
}
