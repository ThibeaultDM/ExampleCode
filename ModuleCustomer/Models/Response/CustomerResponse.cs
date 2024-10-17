namespace ModuleCustomer.Models.Response
{
    public class CustomerResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }
    }
}