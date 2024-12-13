using CustomerDataLayer.DataModels;

namespace CustomerBusinessLayer.BusinessModels;

public class BO_CreditInfo
{
    public BO_CreditInfo()
    {
        Id = new Guid();
    }

    public Guid Id { get; set; }
    public decimal ToSpend { get; set; }
    public Guid IdCustomer { get; set; }
    public DO_Customer Customer { get; set; }
}