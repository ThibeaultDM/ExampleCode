namespace WinformsApplication.Models.Response
{
    public class CustomerDetailResponse : BaseResponse
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public CompanyResponse Company { get; set; }
        public List<CustomerAddressResponse> Addresses { get; set; }
        public CreditResponse CreditInfo { get; set; }
    }
}