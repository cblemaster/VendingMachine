namespace VendingMachine.Models;

internal abstract class Snack
{
    internal required decimal Price { get; set; }
    internal required string Label { get; set; }
    
    public override string ToString() => string.Format("{0,-20} {1:C}", Label, Price);
    internal abstract string SendMessageWhenSold();
}
