namespace CustomerCommunicationLayer.Models.Input
{
    public class CreateAddressInput
    {
        public string City { get; set; }
        public int Postcode { get; set; }
        public string StreetName { get; set; }
        public int Number { get; set; }
    }
}