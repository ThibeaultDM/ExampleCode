namespace CustomerCommunicationLayer.Models.Response
{
    public class CompanyResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public List<AddressResponse> Addresses { get; set; }
    }
}