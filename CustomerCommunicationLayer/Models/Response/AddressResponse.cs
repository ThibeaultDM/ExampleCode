namespace CustomerCommunicationLayer.Models.Response
{
    public class AddressResponse
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }
        public string StreetName { get; set; }
        public int Number { get; set; }
    }
}