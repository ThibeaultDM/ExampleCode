﻿namespace ModuleInvoice.Models.Input;

public class CreateInvoiceInput
{
    public Guid ProxyId { get; set; }
    public string VatNumber { get; set; }

    public List<CreateInvoiceLineInput> InvoiceLines { get; set; } = [];
    public bool IsPaid { get; set; } = false;
}