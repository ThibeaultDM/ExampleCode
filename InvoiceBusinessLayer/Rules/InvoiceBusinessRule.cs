using InvoiceBusinessLayer.BusinessObjects;
using QueasoFramework.BusinessModels.Rules;

namespace InvoiceBusinessLayer.Rules
{
    public class InvoiceBusinessRule : BusinessRule
    {
        /// <summary>
        /// Calculates total value 1 line
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="quantity"></param>
        /// <param name="amount"></param>
        /// <param name="calcultedtotal"></param>
        /// <returns></returns>
        public InvoiceBusinessRule CalculatedAmount_Line(string propertyName, decimal quantity, decimal amount, out decimal calcultedtotal)
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
                SetFailedMessage($"An Error occured while setting property {propertyName}: {ex.Message}");
            }

            return this;
        }

        public InvoiceBusinessRule CalculateVATAmount_Line(string propertyName, decimal vatRate, decimal amountWhitoutVat, out decimal calculatedVatAmount)
        {
            this.PropertyName = propertyName;
            calculatedVatAmount = 0;
            try
            {
                calculatedVatAmount = amountWhitoutVat / 100 * vatRate;
            }
            catch (Exception ex)
            {
                this.Passed = false;
                SetFailedMessage($"An Error occured while setting property {propertyName}: {ex.Message}");
            }

            return this;
        }

        // TODO have this checked
        /// <summary>
        /// Calculates total value of invoice before tax
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="invoiceLines"></param>
        /// <param name="calculatedTotal"></param>
        /// <returns></returns>
        public InvoiceBusinessRule CalculateTotal_Invoice(string propertyName, List<BO_InvoiceLine> invoiceLines, out decimal calculatedTotal)
        {
            calculatedTotal = 0;
            this.PropertyName = propertyName;
            try
            {
                foreach (BO_InvoiceLine invoice in invoiceLines)
                {
                    calculatedTotal += invoice.Amount;
                }
            }
            catch (Exception ex)
            {
                this.Passed = false;
                SetFailedMessage($"An Error occured while setting property {propertyName}: {ex.Message}");
            }

            return this;
        }

        public InvoiceBusinessRule CheckValidityVatNumber(string propertyName, string vatNumber)
        {
            this.PropertyName = propertyName;
            if (!string.IsNullOrWhiteSpace(vatNumber) && !vatNumber.ToUpper().StartsWith("BE0") && !CheckValidityVatNumberModulo97(vatNumber))
            {
                this.Passed = false;
                SetFailedMessage($"Property {propertyName}: does not contain a valid VATnumber");
            }

            return this;
        }

        private bool CheckValidityVatNumberModulo97(string vatNumber) // https://www.fiducial.be/nl/news/Hoe-kunt-u-weten-of-uw-klant-u-een-correct-BTW-nummer-gaf
        {
            int lastTwoNumbers = Convert.ToInt32(vatNumber.Substring((vatNumber.Length - 2)));
            int otherNumbers = Convert.ToInt32(vatNumber.Substring(2));
            bool isValid;

            if (97 - (otherNumbers - (otherNumbers / 97 * 97)) == lastTwoNumbers)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }

        //TODO break point this and look at it
        /// <summary>
        /// Calculates total amount of taxes to be paid
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
                foreach (BO_InvoiceLine invoice in invoiceLines)
                {
                    calculatedTotalVAT += invoice.VATAmount;
                }
            }
            catch (Exception ex)
            {
                this.Passed = false;
                SetFailedMessage($"An Error occured while setting property {propertyName}: {ex.Message}");
            }

            return this;
        }

        /// <summary>
        /// Calculates total amount to be paid
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="taxesAndValue"></param>
        /// <param name="totalAmount"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BusinessRule GetSum(string propertyName, List<decimal> taxesAndValue, out decimal totalAmount)
        {
            totalAmount = 0;

            try
            {
                totalAmount = taxesAndValue[0] + taxesAndValue[1];
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong calculating total amount");
            }

            return this;
        }
    }
}