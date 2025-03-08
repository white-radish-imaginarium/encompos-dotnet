using System.Linq;
using FluentValidation;

namespace EncomposApi;

public record NextAvailableProductCodeResult
{
    /// <summary>
    /// Nests subdepartments.
    /// </summary>
    public string Prefix { get; init; }

    /// <summary>
    /// Include inventory defaults.
    /// </summary>
    public int Length { get; init; }

    public string ProductCode { get; init; }
}
