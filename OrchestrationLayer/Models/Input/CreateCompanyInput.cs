namespace Orchestration.Models.Input
{
    public class CreateCompanyInput
    {
        public List<CustomerAddressInput> Addresses { get; set; }
        public bool IsActive { get; set; }
        public string PublicName { get; set; }
    }
}