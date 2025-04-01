
namespace VendingMachine;

internal sealed class SnackJsonModel
{
    internal string Type { get; init; } = string.Empty;
    internal decimal Price { get; init; }
    internal string Label { get; init; } = string.Empty;
}
