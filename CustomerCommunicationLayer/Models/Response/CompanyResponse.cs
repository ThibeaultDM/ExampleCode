﻿namespace CustomerCommunicationLayer.Models.Response;

public class CompanyResponse
{
    public Guid Id { get; set; }
    public string PublicName { get; set; }
    public bool IsActive { get; set; }
    public List<AddressResponse> Addresses { get; set; }
}