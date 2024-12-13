namespace CustomerCommunicationLayer.Models.Response;

public class GetCustomerByIdResponse : BaseResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string FamilyName { get; set; }
    public string Gender { get; set; }
    public CreditResponse Credit { get; set; }
    public bool IsActive { get; set; }
    public CompanyResponse Company { get; set; }
    public List<AddressResponse> Addresses { get; set; }
}