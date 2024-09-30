namespace EncomposApi.Sync
{
    public class InventorySyncRequest
    {
        public string[] Codes { get; init; }

        public InventoryPageModel Paging { get; init; } 

        public SyncTarget[] Targets { get; init; }
    }
}
