namespace CustomerCommunicationLayer.Models.Input;

public class CreateCustomerInput
{
    public string FirstName { get; set; }
    public string FamilyName { get; set; }
    public string Gender { get; set; }
    public CreateCompanyInput Company { get; set; }
    public List<CustomerAddressInput> Addresses { get; set; }
}