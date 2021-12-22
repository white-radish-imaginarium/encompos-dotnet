namespace EncomposApi
{
    public record ImageQuery
    {
        public string[] PluCodes { get; init; }
        public long[] ImageIds { get; init; }
    }
}
