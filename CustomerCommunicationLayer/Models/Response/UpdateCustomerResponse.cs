namespace CustomerCommunicationLayer.Models.Response;

public class UpdateCustomerResponse
{
    public UpdateCustomerResponse()
    {
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string FamilyName { get; set; }
    public string Gender { get; set; }
    public bool IsActive { get; set; }
    public string StreetName { get; set; }
    public int Number { get; set; }
    public bool Succes { get; set; }
}