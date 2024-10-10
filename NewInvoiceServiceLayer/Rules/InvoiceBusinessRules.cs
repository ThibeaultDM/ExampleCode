using NewInvoiceServiceLayer.Objects;
using QueasoFramework.BusinessModels.Rules;

namespace NewInvoiceServiceLayer.Rules
{
    internal class InvoiceBusinessRules : BusinessRule
    {
        /// <summary>
        /// Calculates total _amount to be paid for an invoiceLine
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="quantity"></param>
        /// <param name="amount"></param>
        /// <param name="calcultedtotal"></param>
        /// <returns></returns>
        public InvoiceBusinessRules CalculatedAmount_Line(string propertyName, decimal quantity, decimal amount, out decimal calcultedtotal)
        {
            this.PropertyName = propertyName;
            calcultedtotal = 0;
            try
            {
                calcultedtotal = quantity * amount;
            }
            catch (Exception ex)
            {
                this.Passed = false;
                SetFailedMessage($"An Error occurred while calculating total amount to be paid for an invoiceLine.: {ex.Message}");
            }

            return this;
        }

        /// <summary>
        /// Calculates total _amount of tax to be paid for an invoiceLine
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="vatRate"></param>
        /// <param name="amountWhitOutVat"></param>
        /// <param name="calculatedVatAmount"></param>
        /// <returns></returns>
        public InvoiceBusinessRules CalculateVATAmount_Line(string propertyName, decimal vatRate, decimal amountWhitOutVat, out decimal calculatedVatAmount)
        {
            this.PropertyName = propertyName;
            calculatedVatAmount = 0;
            try
            {
                // TODO how will the VATRate be given, as a percentage or ... 
                calculatedVatAmount = amountWhitOutVat / 100 * vatRate;
            }
            catch (Exception ex)
            {
                this.Passed = false;
                SetFailedMessage($"An Error occurred while calculating total amount of tax to be paid for an invoiceLine.: {ex.Message}");
            }

            return this;
        }

        /// <summary>
        /// Calculates total value of the goods for an invoice
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="invoiceLines"></param>
        /// <param name="calculatedTotal"></param>
        /// <returns></returns>
        public InvoiceBusinessRules CalculateTotal_Invoice(string propertyName, List<BO_InvoiceLine> invoiceLines, out decimal calculatedTotal)
        {
            calculatedTotal = 0;
            this.PropertyName = propertyName;

            try
            {
                calculatedTotal = invoiceLines.Select(l => l.LineAmount).Sum();
            }
            catch (Exception ex)
            {
                this.Passed = false;
                SetFailedMessage($"An Error occurred while calculating total value of the goods for an invoiceLine.: {ex.Message}");
            }

            return this;
        }

        /// <summary>
        /// Checks if a valid VATNumber was given
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="vatNumber"></param>
        /// <returns></returns>
        public InvoiceBusinessRules CheckValidityVatNumber(string propertyName, string vatNumber)
        {
            this.PropertyName = propertyName;
            if (!vatNumber.ToUpper().StartsWith("BE0"))
            {
                this.Passed = false;
                SetFailedMessage($"A valid VATNumber needs to start with BE0.");
            }
            //else if (!CheckValidityVatNumberModulo97(vatNumber))
            //{
            //    this.Passed = false;
            //    SetFailedMessage($"Not a valid VATNumber.");
            //}

            return this;
        }

        /// <summary>
        /// Does the modulo 97 for a VATNumber check
        /// </summary>
        /// <param name="vatNumber"></param>
        /// <returns></returns>
        private bool CheckValidityVatNumberModulo97(string vatNumber) //https://www.fiducial.be/nl/news/Hoe-kunt-u-weten-of-uw-klant-u-een-correct-BTW-nummer-gaf
        {
            bool isValid;

            try
            {
                int lastTwoNumbers = Convert.ToInt32(vatNumber.Substring((vatNumber.Length - 2)));
                int otherNumbers = Convert.ToInt32(vatNumber.Substring(1));

                if (97 - (otherNumbers - (otherNumbers / 97 * 97)) == lastTwoNumbers)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Calculates total _amount of taxes to be paid for an invoice
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="invoiceLines"></param>
        /// <param name="calculatedTotalVAT"></param>
        /// <returns></returns>
        public BusinessRule CalculateVatAmount_Invoice(string propertyName, List<BO_InvoiceLine> invoiceLines, out decimal calculatedTotalVAT)
        {
            calculatedTotalVAT = 0;
            this.PropertyName = propertyName;

            try
            {
                calculatedTotalVAT = invoiceLines.Select(l => l.VATAmount).Sum();
            }
            catch (Exception ex)
            {
                this.Passed = false;
                SetFailedMessage($"An Error occurred while calculating the VATAmount.: {ex.Message}");
            }

            return this;
        }
    }
}