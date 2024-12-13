using QueasoFramework.BusinessModels.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueasoFramework.BusinessModels.Rules;

public class BusinessRule : IRuleBase
{
    #region Interface overrides

    //necessary interface overrides
    public string PropertyName { get; set; }

    public bool Passed { get; set; }
    public string FailedMessage { get; set; }

    #endregion Interface overrides

    #region Constructors

    /// <summary>
    /// Creates a business rule when no parameters are entered
    /// </summary>
    public BusinessRule()
    {
        PropertyName = string.Empty;
        Passed = true;
        FailedMessage = string.Empty;
    }

    #endregion Constructors

    #region Default rules for properties

    /// <summary>
    /// Rule: object cannot be null
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="valueToCheck"></param>
    /// <returns>Business rule</returns>
    public BusinessRule IsRequired(string propertyName, object valueToCheck)
    {
        PropertyName = propertyName;

        if (valueToCheck == null)
        {
            Passed = false;
            SetFailedMessage($"Property '{propertyName}' is required");
        }

        return this;
    }

    /// <summary>
    /// Rule: cannot be null or only whitespace characters
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="valueToCheck"></param>
    /// <returns>Business rule</returns>
    public BusinessRule IsRequired(string propertyName, string valueToCheck)
    {
        PropertyName = propertyName;

        if (string.IsNullOrWhiteSpace(valueToCheck) || valueToCheck == string.Empty)
        {
            Passed = false;
            SetFailedMessage($"Property '{propertyName}' is required and can not be empty or whitespace"); //$"Is required: {valueToCheck} for property {property}";
        }

        return this;
    }

    /// <summary>
    /// Rule: Min length of a string
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="valueToCheck"></param>
    /// <param name="minLength"></param>
    /// <returns>Business rule</returns>
    public BusinessRule MinLength(string propertyName, string valueToCheck, int minLength)
    {
        PropertyName = propertyName;

        if (valueToCheck != null && valueToCheck.Length < minLength) //note: extra check for null string + < ipv <=
        {
            Passed = false;
            SetFailedMessage($"Minimum length is {minLength} for property '{propertyName}'");
        }

        return this;
    }

    /// <summary>
    /// rule: max length of a string
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="valueToCheck"></param>
    /// <param name="maxLength"></param>
    /// <returns>business rule</returns>
    public BusinessRule MaxLength(string propertyName, string valueToCheck, int maxLength)
    {
        PropertyName = propertyName;

        if (valueToCheck != null && valueToCheck.Length > maxLength) //note: extra check for null string + > ipv >=
        {
            Passed = false;
            SetFailedMessage($"Maximum length is {maxLength} for property '{propertyName}'");
        }

        return this;
    }

    /// <summary>
    /// Rule: Allowed length range of a string
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="valueToCheck"></param>
    /// <param name="minLength"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public BusinessRule RangeLength(string propertyName, string valueToCheck, int minLength, int maxLength)
    {
        PropertyName = propertyName;

        if (valueToCheck != null && (valueToCheck.Length < minLength || valueToCheck.Length > maxLength)) //note: extra check for null string + < ipv <=
        {
            Passed = false;
            SetFailedMessage($"Length range is [{minLength}-{maxLength}] for property '{propertyName}'");
        }

        return this;
    }

    /// <summary>
    /// Rule: Minimum value of a decimal
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="toCheck"></param>
    /// <param name="minValue"></param>
    /// <returns>Business rule</returns>
    public BusinessRule MinValue(string propertyName, decimal? valueToCheck, decimal minValue)
    {
        PropertyName = propertyName;
        if (valueToCheck != null)
        {
            if (valueToCheck < minValue)
            {
                Passed = false;
                SetFailedMessage($"Minimum value is {minValue} for property '{propertyName}'");
            }
        }
        return this;
    }

    /// <summary>
    /// Rule: Maximum value of a decimal
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="valueToCheck"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public BusinessRule MaxValue(string propertyName, decimal valueToCheck, decimal maxValue)
    {
        PropertyName = propertyName;

        if (valueToCheck > maxValue)
        {
            Passed = false;
            SetFailedMessage($"Maximum value is {maxValue} for property '{propertyName}'");
        }
        return this;
    }

    /// <summary>
    /// Rule: Allowed value range of a decimal
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="valueToCheck"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public BusinessRule RangeValue(string propertyName, decimal valueToCheck, decimal minValue, decimal maxValue)
    {
        PropertyName = propertyName;

        if (valueToCheck < minValue || valueToCheck > maxValue)
        {
            Passed = false;
            SetFailedMessage($"Value range is [{minValue}-{maxValue}] for property '{propertyName}'");
        }

        return this;
    }

    #endregion Default rules for properties

    #region Default rules - Calculations and other

    /// <summary>
    /// Calculates the total sum
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="valueList"></param>
    /// <param name="calculatedSum"></param>
    /// <returns></returns>
    public BusinessRule GetSum(string propertyName, List<decimal> valueList, out decimal calculatedSum)
    {
        PropertyName = propertyName;

        calculatedSum = 0;
        try
        {
            calculatedSum = valueList.Sum();
        }
        catch (Exception ex)
        {
            Passed = false;
            SetFailedMessage($"An error occurred while setting property '{propertyName}': {ex.Message}");
        }

        return this;
    }

    #endregion Default rules - Calculations and other

    #region "Helper methods"

    protected void SetFailedMessage(string message)
    {
        FailedMessage = "BusinessRule broken: " + message;
    }

    #endregion "Helper methods"
}