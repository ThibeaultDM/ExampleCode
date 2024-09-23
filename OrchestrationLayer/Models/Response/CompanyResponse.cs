namespace Orchestration.Models.Response
{
    public class CompanyResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public List<CustomerAddressResponse> Addresses { get; set; }
    }
}