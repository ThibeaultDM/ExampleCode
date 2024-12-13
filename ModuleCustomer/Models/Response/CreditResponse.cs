namespace ModuleCustomer.Models.Response;

public class CreditResponse
{
    public Guid Id { get; set; }
    public decimal ToSpend { get; set; }

    public override string ToString()
    {
        return $"{Id} {ToSpend}";
    }
}