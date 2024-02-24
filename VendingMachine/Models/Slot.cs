namespace VendingMachine.Models
{
    internal class Slot
    {
        internal required string Identifier { get; set; }
        internal Stack<Snack> Snacks { get; set; } = new Stack<Snack>();
        internal bool IsSoldOut => Snacks.Count == 0;
    }
}
