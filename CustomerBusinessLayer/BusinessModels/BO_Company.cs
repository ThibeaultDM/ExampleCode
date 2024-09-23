using System.ComponentModel.DataAnnotations;

namespace CustomerBusinessLayer.BusinessModels
{
    public class BO_Company
    {
        public BO_Company()
        {
            Id = new Guid();
            Addresses = new List<BO_Address>();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// MaxLength(100)
        /// </summary>
        [MaxLength(100)]
        public string PublicName { get; set; }

        public bool IsActive { get; set; }

        public List<BO_Address> Addresses { get; set; }

        public BO_Customer Customer { get; set; }
    }
}