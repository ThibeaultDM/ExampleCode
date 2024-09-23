using System.ComponentModel.DataAnnotations;

namespace Orchestration.Models.Response
{
    public class InvoiceLineResponse : BaseResponse
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Price of all the items without taxes
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]// Max value of a decimal
        public decimal Amount { get; set; }

        public string InvoiceNumber { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Amount of taxes on total amount of products
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]
        public decimal VATAmount { get; set; }

        public string VatNumber { get; set; }
    }
}