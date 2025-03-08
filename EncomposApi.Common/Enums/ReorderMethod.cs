namespace EncomposApi.Enums;

/// <remarks>
///  As per database document.
/// </remarks>
public enum ReorderMethod
{
    ReplaceQtySold = 0,
    MinMax = 1,
    NoReorder = 5,
    ManuallyOrdered = 8,
    SpecialOrder = 10
}
