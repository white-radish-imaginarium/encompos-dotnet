namespace EncomposApi.Types.Optional;

public static class OptionalExtensions
{

    public static Optional<T> NotNull<T>(this T? value) where T : struct
    {
        if (value == null) return Optional<T>.Absent;
        return new Optional<T>(value.Value);
    }

    public static Optional<T> NotNull<T>(this T value) where T : class
    {
        if (value == null) return Optional<T>.Absent;
        return new Optional<T>(value);
    }

    public static Optional<T?> Nullable<T>(this Optional<T> option) where T : struct
    {
        if (!option.TryGetValue(out T value)) return Optional<T?>.Absent;
        return new Optional<T?>(value);
    }

    public static Optional<string> NotNullOrEmpty(this string value)
    {
        if (string.IsNullOrEmpty(value)) return Optional<string>.Absent;
        return new Optional<string>(value);
    }

    public static Optional<string> NotNullOrEmpty(this Optional<string> option)
    {
        return option.Filter(value => !string.IsNullOrEmpty(value));
    }

    public static Optional<bool> NotFalse(this bool value)
    {
        if (!value) return Optional<bool>.Absent;
        return new Optional<bool>(value);
    }

    public static Optional<bool> NotNullOrFalse(this bool? value)
    {
        if (value == null || value == false) return Optional<bool>.Absent;
        return new Optional<bool>(value.Value);
    }

    public static bool IsTrue(this Optional<bool> option)
    {
        if (!option.TryGetValue(out bool value)) return false;
        return value;
    }

    public static bool IsNullOrEmpty(this Optional<string> option)
    {
        if (!option.TryGetValue(out string value)) return false;
        return string.IsNullOrEmpty(value);
    }
}
