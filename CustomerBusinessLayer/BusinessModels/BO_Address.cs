using CustomerDataLayer.DataModels;
using System.ComponentModel.DataAnnotations;

namespace CustomerBusinessLayer.BusinessModels
{
    public class BO_Address
    {
        public BO_Address()
        {
            Id = new Guid();
            Company = [];
            Person = [];
        }

        public Guid Id { get; set; }
        public List<BO_Company> Company { get; set; }
        public List<DO_Person> Person { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }

        /// <summary>
        /// MaxLength(100)
        /// </summary>
        [MaxLength(100)]
        public string StreetName { get; set; }

        public int HouseNumber { get; set; }
    }
}