namespace ModuleCustomer.Models.Response
{
    public class CustomerDetailResponse : BaseResponse
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }
        public CompanyResponse Company { get; set; }
        public bool IsActive { get; set; }
        public List<AddressResponse> Addresses { get; set; }
        public CreditResponse CreditInfo { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {FamilyName} {Gender} {Company} {IsActive} {Addresses} {CreditInfo}";
        }
    }
}