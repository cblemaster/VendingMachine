namespace VendingMachine.Models
{
    internal class VendingMachine
    {
        internal bool IsOn { get; set; }
        internal IEnumerable<Slot> Slots { get; set; } = Enumerable.Empty<Slot>();
        internal decimal AmountDeposited { get; set; }
        internal bool CustomerHasBalance => AmountDeposited > 0;
        internal bool CustomerHasSufficientBalanceForPurchase(decimal amount) =>
            amount <= AmountDeposited;
        internal string DisplayItems() { throw new NotImplementedException(); }
        internal string DisplayAmountDeposited() { throw new NotImplementedException(); }
        internal static void VendSnack() { }
        internal string FinishTransaction() { throw new NotImplementedException(); }
        internal string CreateAuditReport() { throw new NotImplementedException(); }
        internal string CreateSalesReport() { throw new NotImplementedException(); }

    }
}
