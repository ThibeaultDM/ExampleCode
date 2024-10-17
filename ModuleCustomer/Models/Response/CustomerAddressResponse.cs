﻿namespace ModuleCustomer.Models.Response
{
    public class CustomerAddressResponse
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public bool IsDefault { get; set; }

        public override string ToString()
        {
            return $"{City}, {Postcode}, {StreetName}, {HouseNumber}";
        }
    }
}