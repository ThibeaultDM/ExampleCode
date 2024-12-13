namespace ModuleInvoice.Models.Response;

public class CustomerDetailResponse : BaseResponse
{
    public string FirstName { get; set; }
    public string FamilyName { get; set; }
    public string Gender { get; set; }
    public CompanyResponse Company { get; set; } = new();
    public bool IsActive { get; set; }
    public List<AddressResponse> Addresses { get; set; } = [];
    public CreditResponse CreditInfo { get; set; } = new();
}