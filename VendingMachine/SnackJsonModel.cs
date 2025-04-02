
namespace VendingMachine;

internal sealed class SnackJsonModel
{
    public string Type { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string Label { get; init; } = string.Empty;
}
