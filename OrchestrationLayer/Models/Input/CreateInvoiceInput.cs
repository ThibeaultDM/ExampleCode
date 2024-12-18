﻿namespace Orchestration.Models.Input;

public class CreateInvoiceInput
{
    public Guid ProxyId { get; set; }
    public string VatNumber { get; set; }

    public List<CreateInvoiceLine> InvoiceLines { get; set; }
    public bool IsPaid { get; set; }
}