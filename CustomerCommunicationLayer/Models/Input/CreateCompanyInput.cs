namespace CustomerCommunicationLayer.Models.Input
{
    public class CreateCompanyInput
    {
        public List<CreateAddressInput> Addresses { get; set; }
        public bool IsActive { get; set; }
        public string PublicName { get; set; }
    }
}