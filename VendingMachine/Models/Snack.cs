namespace VendingMachine.Models
{
    internal class Snack
    {
        internal decimal Price { get; set; }
        internal required string Label { get; set; }
        internal virtual string SendMessageWhenSold() => "generic item message";
    }
}
