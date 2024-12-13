using System.ComponentModel;

namespace NewInvoiceBusinessLayer.Enums;

public enum InvoiceExceptionTypes
{
    [Description("Header not Found")]
    HeaderNotFound,

    [Description("No headers available")]
    NoHeadersAvailable,

    [Description("Invalid VatNumber, a VATNumber needs to start with BE0")]
    InvalidVATNumberBE0,

    [Description("Invalid VatNumber, the VATNumber Failed the modulo 97 check")]
    InvalidVATNumber97,

    [Description("Company not found")]
    CompanyNotFound,

    [Description("Not A Guid Format")]
    NotGuid,

    // For exception Saving to database
    InvalidVATNumber,

    BusinessRuleViolation,
    Error
}