namespace EncomposApi.Enums
{
    /// <summary>
    /// Business process where the QOH movement originates.
    /// </summary>
    public enum MovementType
    {
        PointOfSale = 1,
        PointOfSaleRefund = 2,
        ManualAdjustment = 3,
        PurchaseOrder = 4,
        HandheldTask = 5,
        ReturnBatch = 6,
        PostVoid = 7,
        SystemStart = 8,
        HQ = 9,
        SpecialOrder = 10
    }
}
