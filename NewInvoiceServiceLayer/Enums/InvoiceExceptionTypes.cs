namespace NewInvoiceBusinessLayer.Enums
{
    public enum InvoiceExceptionTypes
    {
        Error,
        HeaderNotFound,
        InvalidVATNumber,
        NonInvoiceHeaderWithThisName,
        BusinessRuleViolation,
        None
    }
}