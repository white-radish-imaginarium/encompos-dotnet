using EncomposApi.Types.Optional;

namespace EncomposApi.Models;

public class AliasModel
{
    public AliasModel() { }
    public AliasModel(string code, string @operator, int quantity)
    {
        Code = code;
        Operator = @operator;
        Quantity = quantity;
    }
    public Optional<string> Code { get; set; }
    public Optional<string> Operator { get; set; }
    public Optional<int> Quantity { get; set; }
}
