using NewInvoiceBusinessLayer.Enums;
using NewInvoiceServiceLayer.Objects;
using QueasoFramework.BusinessModels.Rules;

namespace NewInvoiceServiceLayer.Rules;

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
        PropertyName = propertyName;
        calcultedtotal = 0;
        try
        {
            calcultedtotal = quantity * amount;
        }
        catch (Exception ex)
        {
            Passed = false;
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
        PropertyName = propertyName;
        calculatedVatAmount = 0;
        try
        {
            // TODO how will the VATRate be given, as a percentage or ...
            calculatedVatAmount = amountWhitOutVat / 100 * vatRate;
        }
        catch (Exception ex)
        {
            Passed = false;
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
        PropertyName = propertyName;

        try
        {
            calculatedTotal = invoiceLines.Select(l => l.LineAmount).Sum();
        }
        catch (Exception ex)
        {
            Passed = false;
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
        PropertyName = propertyName;
        if (!vatNumber.ToUpper().StartsWith("BE0"))
        {
            Passed = false;
            SetFailedMessage($"{EnumDescription.GetDescription(InvoiceExceptionTypes.InvalidVATNumberBE0)}");
        }
        else if (!CheckValidityVatNumberModulo97(vatNumber))
        {
            Passed = false;
            SetFailedMessage($"{EnumDescription.GetDescription(InvoiceExceptionTypes.InvalidVATNumber97)}");
        }

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
            int lastTwoNumbers = Convert.ToInt32(vatNumber[^2..]);
            int otherNumbers = Convert.ToInt32(vatNumber[2..^2]); // Start after "BE0"

            isValid = 97 - (otherNumbers % 97) == lastTwoNumbers;
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
        PropertyName = propertyName;

        try
        {
            calculatedTotalVAT = invoiceLines.Select(l => l.VATAmount).Sum();
        }
        catch (Exception ex)
        {
            Passed = false;
            SetFailedMessage($"An Error occurred while calculating the VATAmount.: {ex.Message}");
        }

        return this;
    }
}