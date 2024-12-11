namespace WinformsApplication.Models.Response
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }

        public override string ToString() => $"{FirstName} {FamilyName}";
    }
}