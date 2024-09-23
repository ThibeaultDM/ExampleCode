namespace Training_UI.Models.Input
{
    public class CreateAddressInput
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }
        public string StreetName { get; set; }
        public int Number { get; set; }
    }
}