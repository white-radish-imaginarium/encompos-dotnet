namespace EncomposApi.Enums
{
    public enum MovementReason
    {
        POS_Purchase = 0,               //Down
        Damaged = 1,                    //Down
        SupplierReturns = 2,            //Down
        Recount = 3,                    //Both
        ReceiveWithoutPO = 4,           //Up
        InitialPhysicalInventory = 5,   //Both
        PO_ReceiveAdjust = 6,           //Up
        POS_Return = 7,                 //Up
        InitialProductEntry = 8,        //Both
        PostVoid = 9,                   //Up
        PhysicalInvAdjustment = 10,     //Both
        StoreUse = 11,                  //Both
        ZeroOut = 12,                   //Both
        QuickUpDown = 13,               //Both
        QuickRecount = 14,              //Both
        PO_ReverseAdjust = 15,          //Down
        StoreTransfer = 16,             //Both
        Expired = 17,                   //Down
        Missing = 18,                   //Down
        TheftOrShrinkage = 19,          //Down
        InternetSale = 20,              //Down
        ConvertToRental = 21,           //Down
        Donation = 22,                  //Down
        MergedProducts = 23,            //Up
        ImportQohFile = 24              //Both
    }
}
