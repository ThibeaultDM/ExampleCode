namespace CustomerCommunicationLayer.Models.Input;

public class UpdateCustomerInput
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string FamilyName { get; set; }
    public string Gender { get; set; }
    public List<CustomerAddressInput> Addresses { get; set; }
}