namespace ModuleCustomer.Models.Response;

public class CompanyResponse
{
    public Guid Id { get; set; }
    public string PublicName { get; set; }
    public bool IsActive { get; set; }
    public List<AddressResponse> Addresses { get; set; }

    public override string ToString()
    {
        return $"{Id} {PublicName} {IsActive} {Addresses}";
    }
}