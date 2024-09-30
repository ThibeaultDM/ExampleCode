﻿using System.ComponentModel.DataAnnotations;

namespace Orchestration.Models.Response
{
    public class InvoiceLineDetailResponse : BaseResponse
    {
        private decimal _Amount;
        private decimal _VatAmount;
        private decimal _LineAmount;

        /// <summary>
        /// Price of all the items without taxes
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]// Max value of a decimal
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        /// <summary>
        /// Amount of taxes on total amount of products
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]
        public decimal VATAmount
        {
            get { return _VatAmount; }
            set { _VatAmount = value; }
        }

        [Range(0, 79228162514264337593543950335d)]
        public decimal VATRate { get; set; }

        /// <summary>
        /// Total price
        /// </summary>
        [Range(0, 79228162514264337593543950335d)]
        public decimal LineAmount
        {
            get { return _LineAmount; }
            set { _LineAmount = value; }
        }

        [Range(0, 79228162514264337593543950335d)]
        public decimal PricePerUnit { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 0;

        [MaxLength(100)]
        public string Description { get; set; }
    }
}